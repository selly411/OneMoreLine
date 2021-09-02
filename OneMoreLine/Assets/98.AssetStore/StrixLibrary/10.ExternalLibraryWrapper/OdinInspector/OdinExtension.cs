#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-04-12 오후 3:11:35
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;
#endif

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

public interface IHasName
{
    string IHasName_GetName();
}

/// <summary>
/// 
/// </summary>
public static class OdinExtension
{
    static public ValueDropdownList<T> GetValueDropDownList_EnumSubString<T>()
    {
        ValueDropdownList<T> list = new ValueDropdownList<T>();

        string[] arrStateName = System.Enum.GetNames(typeof(T));
        for (int i = 0; i < arrStateName.Length; i++)
        {
            T eProjectileName = arrStateName[i].ConvertEnum<T>();
            System.Enum pEnum = eProjectileName as System.Enum;
            if(pEnum != null)
                list.Add(pEnum.ToStringSub(), eProjectileName);
        }

        return list;
    }

    static public ValueDropdownList<T> GetValueDropDownList_SubString<T>()
        where T : class
    {
        ValueDropdownList<T> list = new ValueDropdownList<T>();

        var pFilteredTypeList = GetTypeFilter(typeof(T));
        foreach(var pType in pFilteredTypeList)
        {
            T pCurrentT = System.Activator.CreateInstance(pType) as T;
            if(pCurrentT != null)
                list.Add(pCurrentT.ToStringSub(), pCurrentT);
        }

        return list;
    }

    static List<System.Type> list_For_GetValueDropDownList_HasName = new List<System.Type>();
    static public ValueDropdownList<T> GetValueDropDownList_HasName<T>()
    where T : class, IHasName
    {
        return ConvertTypeList_To_ValueDownList(new ValueDropdownList<T>(), GetTypeFilter(typeof(T)));
    }

    static public ValueDropdownList<T> ConvertTypeList_To_ValueDownList<T>(ValueDropdownList<T> list, IEnumerable<System.Type> pFilteredTypeList) where T : class, IHasName
    {
        foreach (var pType in pFilteredTypeList)
        {
            T pCurrentT = System.Activator.CreateInstance(pType) as T;
            if (pCurrentT != null)
                list.Add(pCurrentT.IHasName_GetName(), pCurrentT);
        }

        return list;
    }

    static public IEnumerable<System.Type> GetTypeFilter(System.Type pType)
    {
        return pType.Assembly.GetTypes()
            .Where(x => !x.IsAbstract)
            .Where(x => pType.IsAssignableFrom(x));
    }

    static public IEnumerable<System.Type> GetTypeFilter_AllAssembly(System.Type pType)
    {
        List<System.Type> listTypeReturn = new List<System.Type>();

#if UNITY_EDITOR
        Assembly[] arrAssemblies = System.AppDomain.CurrentDomain.GetAssemblies();
        foreach(var pAssembly in arrAssemblies)
        {
            listTypeReturn.AddRange(
            pAssembly.GetTypes()
            .Where(x => !x.IsAbstract)
            .Where(x => pType.IsAssignableFrom(x)));
        }
#endif

        return listTypeReturn;
    }


    static public ValueDropdownList<T> Get_ExistFileNameList_UnityObject<T>(string strDataPath)
        where T : UnityEngine.Object
    {
        if (string.IsNullOrEmpty(strDataPath) || Application.isEditor == false)
            return null;

        ValueDropdownList<T> list = new ValueDropdownList<T>();
        DirectoryInfo pDirectoryInfo = new DirectoryInfo(strDataPath);
        if (pDirectoryInfo == null || pDirectoryInfo.Exists == false)
        {
            Debug.LogError("Error - GetSpawnName DirectoryInfo Is Null - Path : " + strDataPath);
            return null;
        }

#if UNITY_EDITOR
        FileInfo[] fileInfo = pDirectoryInfo.GetFiles("*.*", SearchOption.AllDirectories);
        for (int i = 0; i < fileInfo.Length; i++)
        {
            FileInfo pFileInfo = fileInfo[i];
            if (pFileInfo.Extension.Contains("meta"))
                continue;

            string strName = pFileInfo.FullName.Replace("\\", "/");
            int iCutIndex = strName.IndexOf("Assets");
            string strPath = strName.Substring(iCutIndex);

            T pObject = AssetDatabase.LoadAssetAtPath<T>(strPath);
            list.Add(fileInfo[i].Name.Replace(pFileInfo.Extension, ""), pObject);
        }
#endif

        return list;
    }
}