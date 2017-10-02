// -----------------------------------------------------------------------------------------------
//  Creation date :  18.06.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer
// -----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles music settings. Included in every scene with audio
/// </summary>
public class CommandLineSettings : MonoBehaviour
{
    public GameObject collectorSoundEffectContainer;
    public GameObject obstaclesSoundEffectContainer;
    public AudioSource backGroundMusic;


    void Awake()
    {
        if (UserManager.Instance.hasCmdLineSettings)
        {
            var soundEffects = new List<AudioSource>();

            if (collectorSoundEffectContainer != null)
            {
                foreach (var audioSource in collectorSoundEffectContainer.GetComponentsInChildren<AudioSource>())
                {
                    soundEffects.Add(audioSource);
                }
            }

            if (obstaclesSoundEffectContainer != null)
            {
                foreach (var audioSource in obstaclesSoundEffectContainer.GetComponentsInChildren<AudioSource>())
                {
                    soundEffects.Add(audioSource);
                }
            }

            if (UserManager.Instance.soundActive)
            {
                if (UserManager.Instance.musicActive)
                {
                    backGroundMusic.mute = false;
                    backGroundMusic.volume = UserManager.Instance.volume;
                }
                else
                {
                    backGroundMusic.mute = true;
                }

                foreach (var audioSource in soundEffects)
                {
                    audioSource.volume = UserManager.Instance.volume / 2;
                    audioSource.mute = false;
                }
            }
            else
            {
                backGroundMusic.mute = true;

                foreach (var audioSource in soundEffects)
                {
                    audioSource.mute = true;
                }

            }
        }
    }
}
