// -----------------------------------------------------------------------------------------------
//  Creation date :  13.06.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer, Patrick Del Conte
// -----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// unfinished class. Meant for implementing bouncing effects to the borders. 
/// Instead applied bouncy physics to the walls placed in the editor.
/// </summary>
public class WallBouncer : MonoBehaviour
{
    public GameObject bubble;
    private float screenHeight;
    private float screenWidth;
    public float DotProduct;

    // Use this for initialization
    void Start()
    {
        var camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        screenHeight = camera.orthographicSize * 2;
        screenWidth = screenHeight * camera.aspect;
       
    }
    
    void Update()
    {
        if ((transform.position.y < -screenHeight / 2 || transform.position.y > screenHeight / 2) || (transform.position.x < -screenWidth / 2 || transform.position.x > screenWidth / 2))
        {
            bubble.GetComponent<CircleCollider2D>().isTrigger = false;
            Debug.Log("triggerFalse");
        }
        else
        {
            bubble.GetComponent<CircleCollider2D>().isTrigger = true;
            Debug.Log("TriggerTrue");
        }
    }
}
