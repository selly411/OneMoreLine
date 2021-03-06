using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/* ============================================ 
   Editor      : Strix                               
   Date        : 2017-03-27 오전 8:00:42
   Description : 
   Edit Log    : 
   ============================================ */

public static class PrimitiveHelper
{
	static public bool IsSimilar( this float fValueTarget, float fValue, float fSimilarGap )
	{
		return Mathf.Abs(fValueTarget - fValue) < fSimilarGap;
	}

	static public Vector3 Inverse( this Vector3 vecTarget )
	{
		return vecTarget * -1;
	}

	static public Color ConvertToColor( this Vector3 sVector )
	{
		return new Color( sVector.x, sVector.y, sVector.z );
	}

	static public Vector3 RandomRange( Vector3 vecMinRange, Vector3 vecMaxRange )
	{
		float fRandX = Random.Range( vecMinRange.x, vecMaxRange.x );
		float fRandY = Random.Range( vecMinRange.y, vecMaxRange.y );
		float fRandZ = Random.Range( vecMinRange.z, vecMaxRange.z );

		return new Vector3( fRandX, fRandY, fRandZ );
	}

	static public Vector2 RandomRange( Vector2 vecMinRange, Vector2 vecMaxRange )
	{
		float fRandX = Random.Range( vecMinRange.x, vecMaxRange.x );
		float fRandY = Random.Range( vecMinRange.y, vecMaxRange.y );

		return new Vector2( fRandX, fRandY );
	}

	static public Vector3 AddFloat( this Vector3 vecOrigin, float fAddValue )
	{
		vecOrigin.x += fAddValue;
		vecOrigin.y += fAddValue;
		vecOrigin.z += fAddValue;

		return vecOrigin;
	}

	public static Vector3 GetPositionByResolution( Vector3 v3Pos )
	{
		return new Vector3( v3Pos.x / Screen.width, v3Pos.y / Screen.height, v3Pos.z );
	}

	public static float GetDistanceByResolution( Vector3 v3PosOne, Vector3 v3PosTwo )
	{
		Vector2 v2Offset = (v3PosOne - v3PosTwo);
		Vector2 v2CalcResolution = new Vector2( v2Offset.x / Screen.width, v2Offset.y / Screen.height );

		return v2CalcResolution.magnitude;
	}

	public static float GetDistanceByResolutionSqrt( Vector3 v3PosOne, Vector3 v3PosTwo )
	{
		float fOffsetOne = Mathf.Abs( v3PosOne.x - v3PosTwo.x ) / Screen.width;
		float fOffsetTwo = Mathf.Abs( v3PosOne.y - v3PosTwo.y ) / Screen.height;

		float fCalcSqrtDistance = Mathf.Sqrt( (fOffsetOne * fOffsetOne) + (fOffsetTwo * fOffsetTwo) );

		return fCalcSqrtDistance;
	}

	public static Vector3 GetPlaneRaycastPos( Plane sPlane, Ray sRay )
	{
		float fDistance = 0f;
		bool bSuccess = sPlane.Raycast( sRay, out fDistance );
		if (bSuccess == false)
		{
			Debug.Log( "Plane 과 Ray 교차에 실패했습니다. Plane 의 높이를 확인해주세요." );
			return Vector3.zero;
		}

		Vector3 v3CalcIntersectionPos = (sRay.origin + sRay.direction * fDistance);

		return v3CalcIntersectionPos;
	}

	public static Vector2 GetCenterByResolution()
	{
		return new Vector2( Screen.width * 0.5f, Screen.height * 0.5f );
	}

	public static float GetScreenRatio()
	{
		return Screen.width / (float)Screen.height;
	}

	public static Vector3 GetCenterPos( Vector3 v3PosOne, Vector3 v3PosTwo )
	{
		return (v3PosOne - v3PosTwo) * 0.5f;
	}

	static public string CutString( this string strText, char chCut, out string strCut )
	{
		strCut = null;
		int iLength = strText.Length;
		for (int i = 0; i < iLength; i++)
		{
			if (strText[i].Equals( chCut ))
			{
				strCut = strText.Substring( 0, i );
				strText = strText.Substring( i + 1 );
				break;
			}
		}

		return strText;
	}

	static public string CutString( this string strText, char chCut, out int iValue )
	{
		iValue = -1;

		string strTemp;
		strText = strText.CutString( chCut, out strTemp );

		if (strTemp != null)
			int.TryParse( strTemp, out iValue );

		return strText;
	}

	public static string ToStringString_WithComma( this int iValue )
	{
		if (iValue.Equals( 0 )) return "0";

		if (iValue < 1000)
			return iValue.ToString();
		else
			return string.Format( "{0:#,###,###}", iValue );
	}

	public static string CommaString( this float fValue )
	{
		if (fValue.Equals( 0f )) return "0";
		return string.Format( "{0:#,###.#}", fValue );
	}
    
	static public ENUM[] DoGetEnumArray<ENUM>( int iIndexStart, int iIndexEnd )
	{
		int iLoopIndex = iIndexEnd - iIndexStart;
		if (iIndexStart == 0)
			iLoopIndex += 1;

		ENUM[] arrEnumArray = new ENUM[iLoopIndex];

		for (int i = 0; i < iLoopIndex; i++)
		{
			try
			{
				arrEnumArray[i] = (ENUM)System.Enum.Parse( typeof( ENUM ), string.Format( "{0}{1}", typeof( ENUM ).Name, i ) );
			}
			catch
			{
				Debug.LogWarning( typeof( ENUM ).ToString() + " 에 " + string.Format( "{0}{1}", typeof( ENUM ).Name, i ) + "이 존재하지 않습니다." );
				break;
			}
		}

		return arrEnumArray;
	}

	static public T[] GetEnumArray<T>()
		where T : System.IConvertible, System.IComparable
	{
		if (typeof( T ).IsEnum == false)
			throw new System.ArgumentException( "GetValues<T> can only be called for types derived from System.Enum", "T" );

		return (T[])System.Enum.GetValues( typeof( T ) );
	}

	static public void DoShuffleList<Compo>( this List<Compo> list, int iShuffleStartIndex = 0 )
	{
		if (list == null)
		{
			Debug.LogWarning( "Shuffle List에서 Shuffle할게 없다.." + typeof( Compo ).Name );
			return;
		}

		for (int i = iShuffleStartIndex; i < list.Count; i++)
		{
			int RandomIndex = Random.Range(iShuffleStartIndex, list.Count);
			Compo temp = list[RandomIndex];
			list[RandomIndex] = list[i];
			list[i] = temp;
		}
	}

	static public void DoShuffleList<Compo>( this List<Compo> list, int iShuffleStartIndex, int iShuffleFinishIndex )
	{
		if (list == null)
		{
			Debug.LogWarning( "Shuffle List에서 Shuffle할게 없다.." + typeof( Compo ).Name );
			return;
		}

		for (int i = iShuffleStartIndex; i < iShuffleFinishIndex; i++)
		{
			int RandomIndex = Random.Range( i, iShuffleFinishIndex );
			Compo temp = list[RandomIndex];
			list[RandomIndex] = list[i];
			list[i] = temp;
		}
	}

	static public void DoSwapList<Compo>( this List<Compo> list, int iIndexA, int iIndexB)
	{
		Compo pCompoTemp = list[iIndexA];
		list[iIndexA] = list[iIndexB];
		list[iIndexB] = pCompoTemp;
	}

	static public void DoResetTransform( this Transform pTrans )
	{
		pTrans.localPosition = Vector3.zero;
		pTrans.localRotation = Quaternion.identity;
		pTrans.localScale = Vector3.one;
	}

	public static int GetEnumLength<TENUM>()
		where TENUM : System.IConvertible, System.IComparable
	{
		System.Array pArray = GetEnumArray<TENUM>();

		return pArray.Length;
	}

	public static bool CheckIsValidString( string strTarget )
	{
		return strTarget != null && strTarget.Length != 0;
	}

	public static void PrintArray<ARRAY>(this ARRAY[] arrData)
	{
		StringBuilder pStringBuider = new StringBuilder();

		int iLen = arrData.Length;
		for (int i = 0; i < iLen; i++)
			pStringBuider.Append(i).Append(" : ").Append(arrData[i]).Append(System.Environment.NewLine);

		Debug.Log( pStringBuider.ToString());
	}

	static Dictionary<System.Type, CDictionary_ForEnumKey<System.Enum, string>> g_mapEnumToString = new Dictionary<System.Type, CDictionary_ForEnumKey<System.Enum, string>>();
	public static string ToString_GarbageSafe( this System.Enum eEnum )
	{
		System.Type pType = eEnum.GetType();
		if (g_mapEnumToString.ContainsKey( pType ) == false)
			g_mapEnumToString.Add( pType, new CDictionary_ForEnumKey<System.Enum, string>() );

		CDictionary_ForEnumKey<System.Enum, string> mapEnumToString = g_mapEnumToString[pType];
		if (mapEnumToString.ContainsKey( eEnum ) == false)
			mapEnumToString.Add( eEnum, System.Enum.GetName( pType, eEnum ) );

		return mapEnumToString[eEnum];
	}
    
	static public int Comparer_Object( GameObject pObjectX, GameObject pObjectY )
	{
		int iSiblingIndexX = pObjectX.transform.GetSiblingIndex();
		int iSiblingIndexY = pObjectY.transform.GetSiblingIndex();

		if (iSiblingIndexX < iSiblingIndexY)
			return -1;
		else if (iSiblingIndexX > iSiblingIndexY)
			return 1;
		else
			return 0;
	}

	static public int Comparer_Component( Component pObjectX, Component pObjectY )
	{
		int iSiblingIndexX = pObjectX.transform.GetSiblingIndex();
		int iSiblingIndexY = pObjectY.transform.GetSiblingIndex();

		if (iSiblingIndexX < iSiblingIndexY)
			return -1;
		else if (iSiblingIndexX > iSiblingIndexY)
			return 1;
		else
			return 0;
	}

	static public bool GetComponent<COMPONENT>( this UnityEngine.Component pTarget, COMPONENT pComponent )
	where COMPONENT : UnityEngine.Component
	{
		pComponent = pTarget.GetComponent<COMPONENT>();

		return pComponent != null;
	}

	static public int GetNumberDigit_1(this int iTarget)
	{
		return iTarget % 10;
	}

	static public int GetNumberDigit_10( this int iTarget )
	{
		iTarget = iTarget / 10;
		return iTarget % 10;
	}

	static public int GetNumberDigit_100( this int iTarget )
	{
		iTarget = iTarget / 100;
		return iTarget % 10;
	}

	public static float GetPercentage_1(float fCur, float fMax)
	{
		float fCalc = (fCur / fMax);
		if (float.IsNaN(fCalc))
			return 0f;

		return fCalc;
	}

	public static float GetCalcReverseFloat(float fLast, float fCurrent)
	{
		return (fLast / (fLast + (fCurrent - fLast)));
    }


	public enum ETransformSibling
	{
		First,
		Last,
	}

	static public void SetParent_SetSibling( this Transform pTransform, Transform pTransformTarget, ETransformSibling eTransformSibling )
	{
		pTransform.SetParent( pTransformTarget );
		if (eTransformSibling == ETransformSibling.First)
			pTransform.SetAsFirstSibling();
		else if (eTransformSibling == ETransformSibling.Last)
			pTransform.SetAsLastSibling();
	}

    static public Camera GetCameraMainOrNull_SameScene( MonoBehaviour pObjectTarget )
    {
        UnityEngine.SceneManagement.Scene pCurrentScene = pObjectTarget.gameObject.scene;
        Camera pMainCamera = null;
        Camera[] arrCamera = Camera.allCameras;
        for (int i = 0; i < arrCamera.Length; i++)
        {
            if (arrCamera[i].gameObject.scene == pCurrentScene)
            {
                pMainCamera = arrCamera[i];
                break;
            }
        }

        return pMainCamera;
    }

	static List<string> _listDigitNote = new List<string>();
	static List<int> _listDigit_NumberNote = new List<int>();

	static public List<int> CutDigitString_Number( this int iTarget )
	{
		_listDigit_NumberNote.Clear();
		string strTargetString = iTarget.ToString();
		for (int i = 0; i < strTargetString.Length; i++)
			_listDigit_NumberNote.Add( int.Parse( strTargetString[i].ToString()) );

		return _listDigit_NumberNote;
	}

	static public List<string> CutDigitString(this int iTarget)
	{
		_listDigitNote.Clear();
		string strTargetString = iTarget.ToString();
		for (int i = 0; i < strTargetString.Length; i++)
			_listDigitNote.Add( strTargetString[i].ToString() );

		return _listDigitNote;
	}

	static public List<string> CutDigitString_WithComma( this int iTarget )
	{
		_listDigitNote.Clear();
		string strTargetString = iTarget.ToString();
		for (int i = 0; i < strTargetString.Length; i++)
		{
			_listDigitNote.Add( strTargetString[i].ToString() );
			if (i != (strTargetString.Length - 1) && ( strTargetString.Length - (i + 1)) % 3 == 0)
				_listDigitNote.Add( "," );
		}

		return _listDigitNote;
	}

    /// <summary>
    /// Min과 Max값을 기준으로 Current값을 0 ~ 1사이값으로 변환합니다.
    /// </summary>
    /// <param name="fCurrent"></param>
    /// <param name="fMax"></param>
    /// <param name="fMin"></param>
    /// <returns></returns>
    static public float Convert_ThisValue_To_Delta_0_1(this float fCurrent, float fMax, float fMin = 0f)
    {
        if (fCurrent > fMax)
            fCurrent = fMax;

        return (fCurrent - fMin) / (fMax - fMin);
    }

    /// <summary>
    /// 0 ~ 1 값을 Min ~ Max값으로 변환합니다.
    /// </summary>
    /// <param name="fCurrent_0_1"></param>
    /// <param name="fMax"></param>
    /// <param name="fMin"></param>
    /// <returns></returns>
    static public float Convert_Delta_0_1_To_Min_Max(this float fCurrent_0_1, float fMax, float fMin = 0f)
    {
        return (fCurrent_0_1 * (fMax - fMin)) + fMin;
    }

    static public bool Check_ABSValue_IsGreater(this float fCurrent, float fTarget)
    {
        return Mathf.Abs(fCurrent) > Mathf.Abs(fTarget);
    }

    /// <summary>
    /// 테스트 코드 링크
    /// <see cref="PrimitiveHelper_Test.Test_Vector3Extension_InverseLerp"/>
    /// </summary>
    /// <param name="vecCurrentValue"></param>
    /// <param name="vecStart"></param>
    /// <param name="vecDest"></param>
    /// <returns></returns>
    public static float InverseLerp_0_1(this Vector3 vecCurrentValue, Vector3 vecStart, Vector3 vecDest)
    {
        Vector3 vecAB = vecDest - vecStart;
        Vector3 vecAV = vecCurrentValue - vecStart;
        return Vector3.Dot(vecAV, vecAB) / Vector3.Dot(vecAB, vecAB);
    }

    public static bool ContainEnumFlag<T>(this T eEnumFlag, T eEnum )
        where T : struct, System.IConvertible, System.IComparable, System.IFormattable
    {
        int iEnumFlag = eEnumFlag.GetHashCode();
        int iEnum = eEnum.GetHashCode();

        return (iEnumFlag & iEnum) != 0;
    }

    public static bool ContainEnumFlag<T>(this T eEnumFlag, params T[] arrEnum)
        where T : struct, System.IConvertible, System.IComparable, System.IFormattable
    {
        bool bIsContain = false;

        int iEnumFlag = eEnumFlag.GetHashCode();
        for (int i = 0; i < arrEnum.Length; i++)
        {
            int iEnum = arrEnum[i].GetHashCode();
            bIsContain = (iEnumFlag & iEnum) != 0;
            if (bIsContain)
                break;
        }

        return bIsContain;
    }

    public static Vector2 Get_Normal2D_To_RightDirection(this Vector2 vecNormal)
    {
        return Vector3.Cross(vecNormal, Vector3.forward);
    }

    public static Vector3 Get_Abs(this Vector3 vecTarget)
    {
        vecTarget.x = Mathf.Abs(vecTarget.x);
        vecTarget.y = Mathf.Abs(vecTarget.y);
        vecTarget.z = Mathf.Abs(vecTarget.z);

        return vecTarget;
    }

    public static Vector3 Forward(this Quaternion rot)
    {
        return rot * Vector3.forward;
    }
}