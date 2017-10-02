// -----------------------------------------------------------------------------------------------
//  Creation date :  18.08.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer
// -----------------------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Automatically loads the appropriate level given through command line arguments
/// </summary>
public class cmdLineLevelSelection : MonoBehaviour
{
    public Text playerAmount;
    void Start()
    {
        playerAmount.text = "Anzahl Spieler: " + UserManager.Instance.playerAmount.ToString();
        if (UserManager.Instance.hasCmdLineSettings)
        {
            var menueManager = GameObject.Find("UIManager").GetComponent<MainMenueManager>();

            switch (UserManager.Instance.difficulty)
            {
                case "easy":
                    menueManager.ShowGalaxy1();
                    break;
                case "medium":
                    menueManager.ShowGalaxy2();
                    break;
                case "hard":
                    menueManager.ShowGalaxy3();
                    break;
            }
        }
    }
}
