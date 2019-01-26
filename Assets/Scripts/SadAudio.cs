using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SadAudio : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string eventPath = "";

    private EventInstance instance;
    private ParameterInstance sad;
    private ParameterInstance observing;

    [Range(0,1)]
    public float sadLevel;
    [Range(0, 1)]
    public float observingLevel;

    private float lerpSad = 0f;
    private float lerpObs = 0f;

    public void Start() {
        instance = RuntimeManager.CreateInstance(eventPath);
        instance.getParameterByIndex(0, out sad);
        instance.getParameterByIndex(1, out observing);
        instance.start();

    }
    // Update is called once per frame
    void Update()
    {
        lerpSad = Mathf.Lerp(lerpSad, sadLevel, 0.05f);
        lerpObs = Mathf.Lerp(lerpObs, observingLevel, 0.05f);


        sad.setValue(lerpSad);
        observing.setValue(lerpObs);
    }
}
