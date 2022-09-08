using UnityEditor;
using UnityEngine;
using System;

[Serializable]
public class ProcGenEditorTool : EditorWindow
{
    private static EditorWindow _window;

    private static string[] _assetSearchFolders;
    private static int[] _possibleValues = new int[] {0, 1, 0, 2, 0, 3, 0, 4, 0, 5};
    private static GameObject[] _blocks;
    private static ProceduralGeneration _blockData;
    
    [MenuItem("Tools/Swap Block", false, -1)]  
    private static void Init()
    {
        _window = GetWindow(typeof(ProcGenEditorTool));
        _window.Show();
        
        _assetSearchFolders = new string[1];
        _assetSearchFolders[0] = "Assets/Scripts/ScriptableObjects";

        string[] SO_GUIDs = AssetDatabase.FindAssets("t:ProceduralGeneration", _assetSearchFolders);
        var SOpath = AssetDatabase.GUIDToAssetPath(SO_GUIDs[0]);
        Debug.Log($"path: {SOpath}");

        _blockData = (ProceduralGeneration)AssetDatabase.LoadAssetAtPath(SOpath, typeof(ProceduralGeneration));
        _blocks = _blockData.Blocks;
        
        for (int i = 0; i < _blocks.Length; i++)
            Debug.Log($"_blocks[{i}] = {_blocks[i].name}");
    }

    public static void SwapBlocks()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Wall");
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i] = GenerateNewBlock(blocks[i].transform);

        }
    }

    private static GameObject GenerateNewBlock(Transform target)
    {
        // get new block
        int index = UnityEngine.Random.Range(0, _possibleValues.Length); 
        GameObject tmpBlock = Instantiate(_blocks[_possibleValues[index]], target);
        tmpBlock.transform.position = Vector3.zero;

        //target.gameObject.GetComponentInChildren<MeshRenderer>();
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
