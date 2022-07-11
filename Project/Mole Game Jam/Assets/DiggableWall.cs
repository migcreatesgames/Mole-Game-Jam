using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggableWall : MonoBehaviour
{
    public GameObject[] linkedWalls;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyLinkedWalls()
    {
        foreach (var wall in linkedWalls)
        {
            Destroy(wall);
        }
    }
}
