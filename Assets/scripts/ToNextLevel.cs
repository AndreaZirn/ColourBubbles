// -----------------------------------------------------------------------------------------------
//  Creation date :  13.06.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer, Patrick Del Conte
// -----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToNextLevel : MonoBehaviour {

	public void LoadNextLevel()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().NextLevel();
    }
}
