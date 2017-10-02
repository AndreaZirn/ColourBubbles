// -----------------------------------------------------------------------------------------------
//  Creation date :  13.06.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer, Patrick Del Conte
// -----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script attached to the collectors. Handles incoming bubble collisions
/// </summary>
[RequireComponent(typeof(BubbleColor))]
public class BubbleCollector : MonoBehaviour
{
    private BubbleColor color;
    public int CollectedCount = 0;
    private TextMesh txtColor;
    public int ColorRequired = 3;

    public int ColorBubblesRemaining { get { return Mathf.Max(0,ColorRequired - CollectedCount); } }

    void Start()
    {
        txtColor = GetComponentInChildren<TextMesh>();
        color = GetComponent<BubbleColor>();
        color.UpdateColor();
        txtColor.text = ColorBubblesRemaining.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collect bubbles
        var bubble = collision.GetComponent<BubbleTouchScript>();
        if (bubble == null) return; //TODO could be handled with collision masks for efficiency


        var othercolor = bubble.GetComponent<BubbleColor>();
        if (color.Equals(othercolor))
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            CollectedCount += 1;
            Destroy(bubble.gameObject);

            txtColor.text = (ColorRequired - CollectedCount).ToString();

            if (ColorBubblesRemaining == 0)
            {
                //if the necessary amount of bubbles has been collected, make the placeholder disapear
                StartCoroutine(DeactivateLater(gameObject));
            }
        }

    }

    private IEnumerator DeactivateLater(GameObject collector)
    {
        yield return new WaitForSeconds(1);
        collector.SetActive(false);
    }

}
