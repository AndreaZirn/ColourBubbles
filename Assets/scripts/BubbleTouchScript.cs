// -----------------------------------------------------------------------------------------------
//  Creation date :  13.06.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer, Patrick Del Conte
// -----------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts;
using TouchScript;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.Assertions.Must;

/// <summary>
/// Handles touch inputs and interaction with the bubbles
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class BubbleTouchScript : MonoBehaviour
{
    public float Speed = 0f;
    public float SmoothingOrder = 1; //is probably not necessary
    public float minDistToNext = 2;

    public Material lineMaterial;
    private Path path = new Path(200);
    private LineRenderer lineRenderer;
    private Vector3 targetPosition = new Vector3(0, 0, float.NegativeInfinity);
    private bool selected;
    private new Camera camera;
    private float lastDistFromTarget = float.PositiveInfinity;

    private Vector2 velocity1 = Vector2.zero;
    private Vector2 velocity2 = Vector2.zero;


    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void Update()
    {
        drawPath();
    }

    //draws a line. Bubble follows the drawn path
    private void drawPath()
    {
        if (path.Count <= 0) return;
        lineRenderer.positionCount = path.Count;
        int idx = 0;
        foreach (var vec in path)
        {
            lineRenderer.SetPosition(idx, vec);
            idx++;
        }
    }
    
    void FixedUpdate()
    {
        if (path.Count <= 0) return;

        if (float.IsNegativeInfinity(targetPosition.z)) targetPosition = path.Dequeue();

        float distFromTarget = (targetPosition - transform.position).sqrMagnitude;
        float predDistFromTarget = ((Vector2) targetPosition - predictPositionAfterUpdate()).sqrMagnitude;
        Vector2 toNext = Vector2.zero;
        Vector2 fromPrevious = Vector2.zero;
        int smoothcount = 0;
        var rigibody = GetComponent<Rigidbody2D>();

        if (lastDistFromTarget > predDistFromTarget && distFromTarget > minDistToNext)
        {
            //approaching target
            lastDistFromTarget = distFromTarget;
        }
        else
        {
            if (!path.hasNext()) return;
            do
            {
                targetPosition = path.Dequeue();
            } while ((targetPosition - transform.position).sqrMagnitude <= minDistToNext && path.hasNext());

            var currentNext = new Vector2(targetPosition.x, targetPosition.y);
            var currentPrevous = currentNext;

            for (int i = 0; i < SmoothingOrder && path.hasNext(i) && path.hasPrevious(i); i++)
            {
                fromPrevious += currentPrevous - path.Peek(-i);
                toNext += path.Peek(i) - currentNext;
                currentNext = path.Peek(i);
                currentPrevous = path.Peek(-i);
                smoothcount++;
            }
            lastDistFromTarget = (targetPosition - transform.position).sqrMagnitude;
        }

        //average velocities with the last two to achieve smooth paths
        Vector2 toTarget = targetPosition - transform.position;
        var vel = toTarget + fromPrevious + toNext;
        GetComponent<Rigidbody2D>().velocity = (vel / (smoothcount * 2 + 1)).normalized * Speed;
        velocity2 = velocity1;
        velocity1 = toTarget;
    }

    private Vector2 predictPositionAfterUpdate()
    {
        return new Vector2(transform.position.x, transform.position.y)
               + GetComponent<Rigidbody2D>().velocity * Time.fixedDeltaTime;
    }

    private void OnEnable()
    {
        if (TouchManager.Instance != null)
        {
            TouchManager.Instance.TouchesBegan += TouchesBeganHandler;
            TouchManager.Instance.TouchesEnded += TouchesEndHandler;
            TouchManager.Instance.TouchesMoved += TouchesDragHandler;
        }
    }

    private void OnDisable()
    {
      
        if (TouchManager.Instance != null)
        {
            TouchManager.Instance.TouchesBegan -= TouchesBeganHandler;
            TouchManager.Instance.TouchesEnded -= TouchesEndHandler;
            TouchManager.Instance.TouchesMoved -= TouchesDragHandler;
        }
    }

    private void TouchesDragHandler(object sender, TouchEventArgs e)
    {
        if (!selected) return;

        foreach (var point in e.Touches)
        {
            var touchpoint = point.Position;
            var clickpos = camera.ScreenToWorldPoint(touchpoint);
            path.Enqueue(clickpos);
        }
    }

    private void TouchesBeganHandler(object sender, TouchEventArgs e)
    {
        Debug.Log("touch began");
        var point = e.Touches[0];
        var ray = camera.ScreenPointToRay(point.Position);

        var raycastHit =
            Physics2D.Raycast(
                new Vector2(camera.ScreenToWorldPoint(point.Position).x,
                    camera.ScreenToWorldPoint(point.Position).y), Vector2.zero, 0f);
        if (raycastHit.transform == this.transform)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            selected = true;
            path.Clear();
            var pos = camera.ScreenToWorldPoint(point.Position);
            path.Enqueue(pos);
        }
    }

    private void TouchesEndHandler(object sender, TouchEventArgs e)
    {
        selected = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        BubbleTouchScript other = col.gameObject.GetComponent<BubbleTouchScript>();
        if (other != null && other.GetInstanceID() < this.GetInstanceID()) //make sure code is only executed in one of the two
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var otherRenderer = other.GetComponent<SpriteRenderer>();
            var color = GetComponent<BubbleColor>();
            var otherColor = other.GetComponent<BubbleColor>();
            var rb = GetComponent<Rigidbody2D>();
            var velocity = (rb.velocity + other.GetComponent<Rigidbody2D>().velocity);
            rb.velocity = velocity.normalized * velocity.magnitude / 2;
            color += otherColor;
            Debug.Log(color);
            color.UpdateColor();

            Destroy(other.gameObject);
        }
    }

    public void UpdateColor(Color colorRGB)
    {
        GetComponentInChildren<SpriteRenderer>().color = colorRGB;
        lineMaterial.color = colorRGB;

        GetComponent<LineRenderer>().startColor = colorRGB;
        GetComponent<LineRenderer>().endColor = colorRGB;
    }
}