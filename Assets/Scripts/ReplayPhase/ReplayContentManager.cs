﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class manages choreography of the "replay" phase
/// </summary>
public class ReplayContentManager : ContentManager
{

	private void Start () {
		
	}

    public override void EnterPause()
    {

    }

    public override void ExitPause()
    {

    }

    private void Update () {
        Debug.Log("you're in the replay phase");
    }
}
