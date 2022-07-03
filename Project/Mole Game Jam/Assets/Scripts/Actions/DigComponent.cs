using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigComponent : MonoBehaviour
{
    private DigDetection _detect;

    private void Awake()
    {
        _detect = GetComponentInChildren<DigDetection>();
    }

    public void Dig(Entity digger)
    {
        _detect.Detect(digger);
    }

}

