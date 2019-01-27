using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    public float volume{get;set;}

    public Camera MainCamera;

    //public AudioListener audioListener;

    void Update(){

        CheckForMainCamera();
        
        AdjustVolume();

    }

    void CheckForMainCamera(){
        if(MainCamera == null){
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            //audioListener = MainCamera.GetComponent<audioListener>();
        }        
    }

    void AdjustVolume(){
        
    }


}
