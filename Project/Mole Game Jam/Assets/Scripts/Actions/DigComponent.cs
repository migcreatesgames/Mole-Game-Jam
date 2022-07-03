using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigComponent : MonoBehaviour
{
    private DigDetection _detect;

    private void Start () => Init();

    private void Init()
    {
        _detect = GetComponentInChildren<DigDetection>();
    }


    public void Dig(Entity digger)
    {
        // check if section in front of player can be dug
        // or far enough from obstables/wall to dig
        if (_detect.Detect(digger))
        {
            // get dig target location
            // stop player from moving
            // start "setup dig" animation

            // start and increment counter that tracks time that
            // button is held down
        }
        else
        {
            // else tell player area not diggable or too close to wall
            Debug.Log("warning: nothing to dig / too close to wall");
        }
    }
}

