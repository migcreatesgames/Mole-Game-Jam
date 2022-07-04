using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigComponent : MonoBehaviour
{
    private DigDetection _detect;
    private enum DigStates
    {
        init_dig, digging, digging_complete
    }

    private DigStates _digStates = DigStates.digging_complete;
    
    private void Start () => Init();

    private void Init()
    {
        _detect = GetComponentInChildren<DigDetection>();
    }

    public void Dig(Entity digger)
    {
        // check if section in front of player can be dug
        // or far enough from obstables/wall to dig
        DetectionResults results = _detect.Results;
        
        
        // init dig action
        SetupDig(results);

        switch (_digStates)
        {
            case DigStates.init_dig:
                break;
            case DigStates.digging:
                break;
            case DigStates.digging_complete:
       
                break;
            default:
                break;
        }
      

    }

    private void SetupDig(DetectionResults results)
    {
          switch (results)        
        {

            case DetectionResults.dig_wall:
                // get dig target location
                // stop player from moving
                // start "setup dig" animation

                // start and increment counter that tracks time that
                // button is held down
                break;
            case DetectionResults.dig_floor:
                // get dig target location
                // stop player from moving
                // start "setup dig" animation

                // start and increment counter that tracks time that
                // button is held down
                break;
            case DetectionResults.not_enough_space:
                // else tell player area not diggable or too close to wall
                Debug.Log("warning: nothing to dig / too close to wall");
                break;
            case DetectionResults.wall:
                // else tell player area not diggable or too close to wall
                Debug.Log("warning: nothing to dig / too close to wall");
                break;
            case DetectionResults.hole:
                // else tell player area not diggable or too close to wall
                Debug.Log("warning: nothing to dig / too close to wall");
                break;
            default:
                break;
        }
    }
  
}



