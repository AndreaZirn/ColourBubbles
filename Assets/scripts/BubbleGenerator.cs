// -----------------------------------------------------------------------------------------------
//  Creation date :  13.06.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer, Patrick Del Conte
// -----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Spawns continously bubbles from the top, left and right side of the screen
/// </summary>
public class BubbleGenerator : MonoBehaviour {

    private List<BubbleColor.RYB> Colors; 
    public float BubblePerSecond = 0.2f;
    public float StartDelay = 1f;
    public GameObject BubblesPrefab;
    public float Speed;
    private GameObject BubblesContainer;
    public bool spawnRed = true;
    public bool spawnBlue = true;
    public bool spawnYellow = true;

    void Start()
    {
        Colors = new List<BubbleColor.RYB>();
        if(spawnRed) Colors.Add(new BubbleColor.RYB(1,0,0));
        if (spawnYellow) Colors.Add(new BubbleColor.RYB(0,1,0));
        if (spawnBlue) Colors.Add(new BubbleColor.RYB(0,0,1));
        BubblesContainer =new GameObject("bubbles");
        Invoke("StartSpawning", StartDelay);
        var gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void StartSpawning()
    {
        Invoke("CreateBubbles", 1f / BubblePerSecond);
    }

    void CreateBubbles()
    {
        var camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        var screenHeight = camera.orthographicSize * 2;
        var screenWidth = screenHeight * camera.aspect;
        var spawnState = Random.Range(0, 3);
        GameObject bubble = Instantiate(BubblesPrefab, Vector3.zero, Quaternion.identity);

        Vector2 position;
        //left screenside
        if (spawnState == 0)
        {
            position = new Vector2((-screenWidth / 2) - 1, Random.Range((-screenHeight / 2)+10, (screenHeight / 2)-2));
            bubble.transform.position = position;
            bubble.GetComponentInChildren<BubbleColor>().color = Colors[Random.Range(0, Colors.Count)];
            bubble.GetComponentInChildren<BubbleColor>().UpdateColor();
            bubble.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * Speed;
        }
        //Right side
        if(spawnState ==1)
        {
            position = new Vector2((screenWidth / 2) + 1, Random.Range((-screenHeight / 2)+10, (screenHeight / 2)-2));
            bubble.transform.position = position;
            bubble.GetComponentInChildren<BubbleColor>().color = Colors[Random.Range(0, Colors.Count)];
            bubble.GetComponentInChildren<BubbleColor>().UpdateColor();
            bubble.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * Speed;
        }
        //Top side
        if(spawnState == 2)
        {
            position = new Vector2(Random.Range((-screenWidth/2)+2, (screenWidth/2)-2), camera.orthographicSize + 1);
            bubble.transform.position = position;
            bubble.GetComponentInChildren<BubbleColor>().color = Colors[Random.Range(0, Colors.Count)];
            bubble.GetComponentInChildren<BubbleColor>().UpdateColor();
            bubble.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * Speed;
        }

        //Bottom Side
        if(spawnState == 3)
        {
            position = new Vector2(Random.Range((-screenWidth / 2)+2, (screenWidth / 2))-2, -camera.orthographicSize -1);
            bubble.transform.position = position;
            bubble.GetComponentInChildren<BubbleColor>().color = Colors[Random.Range(0, Colors.Count)];
            bubble.GetComponentInChildren<BubbleColor>().UpdateColor();
            bubble.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * Speed;
        }
        bubble.GetComponent<BubbleTouchScript>().Speed = Speed;
        bubble.transform.parent = BubblesContainer.transform;

        Invoke("CreateBubbles", 1f / BubblePerSecond);
    }
}
