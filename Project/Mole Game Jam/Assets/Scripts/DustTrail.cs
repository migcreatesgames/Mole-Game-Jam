using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTrail : MonoBehaviour
{//foot dust trail variables

    public bool EnableDustTrails = false; //field for user to toggle dust trails from inspector 
    public GameObject[] Meshes_DustTrails;
    protected bool canSpawnDustTrail;
    protected int dustTrailIndex = 0;

    protected int curSteps;

    public float AmmountToGround = .75f;
    [SerializeField]
    protected float stepRate = .08f;
    [SerializeField]
    protected float MaxStepRate = .08f;


    // Update is called once per frame
    void Update()
    {

        // spawn dust trails
    
        if (EnableDustTrails)
        {
            if (!canSpawnDustTrail)
            {
                if (stepRate > 0)
                {
                    stepRate -= Time.deltaTime;
                    return;
                }
                else
                {
                    //print("spawn dust trail");
                    canSpawnDustTrail = true;
                    Debug.Log("shoudl spawm dust trao;");
                    SpawnDustTrail();
                }
            }
        }
    }

    protected void SpawnDustTrail()
    {
        if (dustTrailIndex <= 2)
        {
            GameObject tmp;

            tmp = Instantiate(Meshes_DustTrails[dustTrailIndex], transform.position, Meshes_DustTrails[dustTrailIndex].transform.rotation);
            if (transform.rotation.y == 0)
                tmp.transform.Rotate(0, 90, 0);
            if (transform.eulerAngles.y == 180)
                tmp.transform.Rotate(0, -90, 0);

            tmp.GetComponent<DustTrailBehavior>().actor = gameObject;
            tmp.GetComponent<DustTrailBehavior>().EnableTrail();

            // basically a delay before the next dust can spawn
            EnableDustTrail();
            if (dustTrailIndex == 2)
            {
                dustTrailIndex = 0;
                return;
            }
        }
        dustTrailIndex++;
    }

    protected void EnableDustTrail()
    {
        stepRate = MaxStepRate;
        canSpawnDustTrail = false;
    }


}
