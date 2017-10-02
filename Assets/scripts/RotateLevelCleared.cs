// -----------------------------------------------------------------------------------------------
//  Creation date :  13.06.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer, Patrick Del Conte
// -----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Don't mind me. Possible future extension for letting the win award rotate after clearing a level
/// </summary>
public class RotateLevelCleared : MonoBehaviour
{
    private bool animationRunning = false;

	// Use this for initialization
	void Start () {
		
	}

    void OnEnable()
    {
        animationRunning = true;
    }
	
	// Update is called once per frame
	void Update () {
	    if (animationRunning)
	    {
	       
	    }
	}
}
