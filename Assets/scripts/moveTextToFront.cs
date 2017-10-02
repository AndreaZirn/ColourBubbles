using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Please don't mind me
/// </summary>
[ExecuteInEditMode]
public class moveTextToFront : MonoBehaviour {
	
	// Update is called once per frame
	void Update ()
	{
	    GetComponent<MeshRenderer>().sortingOrder = 100;
	}
}
