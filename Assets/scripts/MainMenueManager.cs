// -----------------------------------------------------------------------------------------------
//  Creation date :  13.06.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer, Patrick Del Conte
// -----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles the level selection scene
/// </summary>
public class MainMenueManager : MonoBehaviour {

    public Scene StartScene;
    public Canvas Galaxy1;
    public Canvas Galaxy2;
    public Canvas Galaxy3;
    public Button BGalaxy1;
    public Button BGalaxy2;
    public Button BGalaxy3;
    public Sprite filledGalaxy1Ring;
    public Sprite filledGalaxy2Ring;
    public Sprite filledGalaxy3Ring;
    public Sprite blackGalaxyRing;

	// Use this for initialization
	void Start () {
        Galaxy1.enabled = false;
        Galaxy2.enabled = false;
        Galaxy3.enabled = false;
    }

    public void StartGameMainMenu()
    {
        SceneManager.LoadScene("Instruction");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ShowGalaxy1()
    {
        Galaxy1.enabled = true;
        Galaxy2.enabled = false;
        Galaxy3.enabled = false;
        BGalaxy1.transform.localScale = new Vector3(1.75f, 1.75f, 1);
        BGalaxy2.transform.localScale = new Vector3(1.5F, 1.5F, 1);
        BGalaxy3.transform.localScale = new Vector3(1.5F, 1.5F, 1);

        BGalaxy1.GetComponent<Image>().sprite = filledGalaxy1Ring;
        BGalaxy2.GetComponent<Image>().sprite = blackGalaxyRing;
        BGalaxy3.GetComponent<Image>().sprite = blackGalaxyRing;
    }

    public void ShowGalaxy2()
    {
        Galaxy1.enabled = false;
        Galaxy2.enabled = true;
        Galaxy3.enabled = false;
        BGalaxy1.transform.localScale = new Vector3(1.5F, 1.5F, 1);
        BGalaxy2.transform.localScale = new Vector3(1.75f, 1.75f, 1);
        BGalaxy3.transform.localScale = new Vector3(1.5F, 1.5F, 1);

        BGalaxy1.GetComponent<Image>().sprite = blackGalaxyRing;
        BGalaxy2.GetComponent<Image>().sprite = filledGalaxy2Ring;
        BGalaxy3.GetComponent<Image>().sprite = blackGalaxyRing;

    }

    public void ShowGalaxy3()
    {
        Galaxy1.enabled = false;
        Galaxy2.enabled = false;
        Galaxy3.enabled = true;
        BGalaxy1.transform.localScale = new Vector3(1.5F, 1.5F, 1);
        BGalaxy2.transform.localScale = new Vector3(1.5F, 1.5F, 1);
        BGalaxy3.transform.localScale = new Vector3(1.75f, 1.75f, 1);


        BGalaxy1.GetComponent<Image>().sprite = blackGalaxyRing;
        BGalaxy2.GetComponent<Image>().sprite = blackGalaxyRing;
        BGalaxy3.GetComponent<Image>().sprite = filledGalaxy3Ring;

    }

    public void LoadLevel(int i)
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().LoadLevel(i);
    }

}
