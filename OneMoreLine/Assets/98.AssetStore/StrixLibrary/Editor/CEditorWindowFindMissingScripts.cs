using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

//http://wiki.unity3d.com/index.php?title=FindMissingScripts

public class CEditorWindowFindMissingScripts : EditorWindow
{
    static int go_count = 0, components_count = 0, missing_count = 0;

    [MenuItem("Tools/Strix_Tools/FindMissingScripts")]
    public static void ShowWindow()
    {
        GetWindow<CEditorWindowFindMissingScripts>("MissingScripts Finder");
    }

    public void OnGUI()
    {
        if (GUILayout.Button("Find Missing Scripts in All Object"))
        {
            FindInSelected();
        }
    }
    private static void FindInSelected()
    {
        Transform[] arrTransform = FindObjectsOfType<Transform>();
        go_count = 0;
        components_count = 0;
        missing_count = 0;
        foreach (Transform pTransform in arrTransform)
        {
            FindInGO(pTransform.gameObject);
        }
        Debug.Log(string.Format("Searched {0} GameObjects, {1} components, found {2} missing", go_count, components_count, missing_count));
    }

    private static void FindInGO(GameObject g)
    {
        go_count++;
        Component[] components = g.GetComponents<Component>();
        for (int i = 0; i < components.Length; i++)
        {
            components_count++;
            if (components[i] == null)
            {
                missing_count++;
                string s = g.name;
                Transform t = g.transform;
                while (t.parent != null)
                {
                    s = t.parent.name + "/" + s;
                    t = t.parent;
                }
                Debug.Log(s + " has an empty script attached in position: " + i, g);
            }
        }
        // Now recurse through each child GO (if there are any):
        foreach (Transform childT in g.transform)
        {
            //Debug.Log("Searching " + childT.name  + " " );
            FindInGO(childT.gameObject);
        }
    }
}
#endif