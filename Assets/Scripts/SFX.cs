using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SFX : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string eventPath = "";

    private EventInstance instance;
    private ParameterInstance keep;
    private ParameterInstance trash;



    void Start()
    {
        instance = RuntimeManager.CreateInstance(eventPath);
        instance.getParameterByIndex(1, out keep);
        instance.getParameterByIndex(0, out trash);
    }


    public void SFXhit(float keepVal, float trashVal) {
        keep.setValue(keepVal);
        trash.setValue(trashVal);
        instance.start();
    }
}
