using UnityEditor;
using UnityEngine;

public class ProcGenEditorTool : EditorWindow
{
    static EditorWindow _window;
    private GameObject[] _blocks; 
    
    [MenuItem("Tools/Swap Block", false, -1)]
    private static void Init()
    {
        _window = GetWindow(typeof(ProcGenEditorTool));
        _window.Show();
    }

    public static void SwapBlocks()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Wall");
        for (int i = 0; i < blocks.Length; i++)
            blocks[i] = GenerateNewBlock(blocks[i].transform);
    }

    private static GameObject GenerateNewBlock(Transform target)
    {
        // get new block
        target.gameObject.GetComponent<MeshRenderer>();
        // destroy old block mesh


        return null;
    }

    private Vector2 GenerateNewRotation()
    {
        return new Vector2(0, 0);
    }


    public void OnGUI()
    {
        //myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
        EditorGUILayout.LabelField("Level","hello");
        GUILayout.Button("Randomize Bricks");
        //EditorGUILayout.TextArea("value", );
    }

}
