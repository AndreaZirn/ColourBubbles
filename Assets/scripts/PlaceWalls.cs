// -----------------------------------------------------------------------------------------------
//  Creation date :  13.06.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer, Patrick Del Conte
// -----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Adjusts walls in edit and playmode
/// </summary>
[ExecuteInEditMode]
public class PlaceWalls : MonoBehaviour {

    const float size = 1.5f;

    // Use this for initialization
    void Start () {
        var camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        var screenHeight = camera.orthographicSize * 2;
        var screenWidth = screenHeight * camera.aspect;


        var top = transform.Find("top");
        top.transform.position = new Vector3(0, screenHeight / 2 + size);
        top.transform.localScale = new Vector3(screenWidth + 2*size,1,1);

        var bottom = transform.Find("bottom");
        bottom.transform.position = new Vector3(0, -screenHeight / 2 -size);
        bottom.transform.localScale = new Vector3(screenWidth+2*size,1,1);

        var left = transform.Find("left");
        left.transform.position = new Vector3(-screenWidth / 2 -size, 0);
        left.transform.localScale = new Vector3(1, screenHeight+2*size, 1);


        var right = transform.Find("right");
        right.transform.position = new Vector3(screenWidth/2 + size,0);
        right.transform.localScale = new Vector3(1, screenHeight+2*size, 1);
    }
}
