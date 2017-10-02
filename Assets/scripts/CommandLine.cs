// -----------------------------------------------------------------------------------------------
//  Creation date :  18.08.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer
// -----------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Saves additional given parameters from the command line in the global class UserManager
/// </summary>
public class CommandLine : MonoBehaviour
{

    /// <summary>
    /// Arguments[0] ExePath, [1] difficultyLvl, [2] playerAmount
    /// [3] bool Sound Active, [4] bool MusicActive, [5] float volume
    /// </summary>
    void Awake()
    {
        string[] arguments = System.Environment.GetCommandLineArgs();

        //only 1 argument means that no other arguments have been passed to
        //the exe except the path of the exe which always happens
        if (arguments.Length != 1)
        {
            UserManager.Instance.hasCmdLineSettings = true;
            UserManager.Instance.difficulty = arguments[1];
            UserManager.Instance.playerAmount = Int32.Parse(arguments[2]);
            UserManager.Instance.soundActive = Convert.ToBoolean(arguments[3]);
            UserManager.Instance.musicActive = Convert.ToBoolean(arguments[4]);
            UserManager.Instance.volume = float.Parse(arguments[5]);
            
            SceneManager.LoadScene("LevelSelection");
        }
        else
        {
            UserManager.Instance.hasCmdLineSettings = false;
        }
    }
}
