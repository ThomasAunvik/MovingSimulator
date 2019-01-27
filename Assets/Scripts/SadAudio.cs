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
    private ParameterInstance start;

    [Range(0,1)]
    private float sadLevel;
    [Range(0, 1)]
    private float observingLevel;
    private static bool gameStarted = false;

    private float lerpSad = 0f;
    private float lerpObs = 0f;

    public void Start() {
        instance = RuntimeManager.CreateInstance(eventPath);
        instance.getParameterByIndex(0, out sad);
        instance.getParameterByIndex(2, out observing);
        instance.getParameterByIndex(1, out start);
        instance.start();

    }
    // Update is called once per frame
    void Update()
    {
        lerpSad = Mathf.Lerp(lerpSad, sadLevel, 0.05f);
        lerpObs = Mathf.Lerp(lerpObs, observingLevel, 0.05f);


        sad.setValue(lerpSad);
        observing.setValue(lerpObs);
        if (gameStarted) {
            start.setValue(0.5f);
        }
    }

    public static void StartGame() {
        gameStarted = true;
    }

    public void setLevels(float s, float o) {
        sadLevel = s;
        observingLevel = o;
    }
   
}
