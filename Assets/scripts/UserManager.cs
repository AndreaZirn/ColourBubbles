// -----------------------------------------------------------------------------------------------
//  Creation date :  18.08.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer
// -----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton class, storing arguments from the command line
/// </summary>
public class UserManager : MonoBehaviour
{

    private static readonly UserManager instance = new UserManager();
    public bool soundActive;
    public bool musicActive;
    public float volume;
    public string difficulty;
    public int playerAmount;
    public bool hasCmdLineSettings;


    private UserManager() { }

    public static UserManager Instance
    {
        get
        {
            return instance;
        }
    }
}
