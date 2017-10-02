// -----------------------------------------------------------------------------------------------
//  Creation date :  13.06.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer, Patrick Del Conte
// -----------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attached to every obstacle. Handles bubble collisions and floating behaviour
/// </summary>
public class Obstacles : MonoBehaviour
{
    public GameObject obstacle;
    public float speed;
    private float screenHeight;
    private float screenWidth;

    private GameManager gameManager;

    // Use this for initialization
    void Start()
    {
        obstacle.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(-2, 2)) * speed;
        var camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        screenHeight = camera.orthographicSize * 2;
        screenWidth = screenHeight * camera.aspect;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

   /// <summary>
   /// To lower the difficulty, the obstacles bounces on a straight line and never changes direction
   /// </summary>
    void Update()
    {
        if ((transform.position.y < -screenHeight / 2 || transform.position.y > screenHeight / 2) || (transform.position.x < -screenWidth / 2 || transform.position.x > screenWidth / 2))
        {
            if (Vector2.Dot(obstacle.GetComponent<Rigidbody2D>().velocity, -obstacle.transform.position) < 0)
            {
                Debug.Log("Bounced");
                obstacle.GetComponent<Rigidbody2D>().velocity = -obstacle.GetComponent<Rigidbody2D>().velocity;
            }
            else
            {
                Debug.Log("Not Bounced");
            }
        }
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bubble")
        {
            Destroy(other.gameObject);
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            gameManager.RemoveLife();
        }
    }
}
