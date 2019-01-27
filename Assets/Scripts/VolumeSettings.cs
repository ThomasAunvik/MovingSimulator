using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;


public class VolumeSettings : MonoBehaviour
{
    public static VolumeSettings volumeSettings;

    string masterBusString = "Bus:/";
    static Bus masterBus;

    void Start()
    {
        if(volumeSettings != null)
        {
            Destroy(gameObject);
        }

        volumeSettings = this;

        DontDestroyOnLoad(gameObject);

        masterBus = RuntimeManager.GetBus(masterBusString);
    }



    // Update is called once per frame
    static public void setVolume(float volume)
    {
        masterBus.setVolume(volume);
    }
}
