using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
    
public class AudioSettings : MonoBehaviour
{

    string masterBusString = "Bus:/";
    Bus masterBus;

    [Range(0, 2f)]
    public float volume;

    void Start()
    {
        masterBus = RuntimeManager.GetBus(masterBusString);
        masterBus.getVolume(out volume, out volume);
    }

    // Update is called once per frame
    void Update()
    {
        masterBus.setVolume(volume);
    }
}
