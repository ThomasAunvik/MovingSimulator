using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    
    public float volume{get;set;}

    public Camera MainCamera;
   
    //public AudioListener audioListener;

    void Awake(){
        DontDestroyOnLoad(this.GameObject);
    }

    void Update(){

        CheckForMainCamera();
        CheckForVolumeSlider();
        UpdateVolumeVariable();
        AdjustVolumeSetting();

    }

    void CheckForMainCamera(){
        if(MainCamera == null){
            MainCamera = Camera.main;
        }        
    }

    Slider VolumeSlider;

    void CheckForVolumeSlider(){
        if(VolumeSlider == null){
            VolumeSlider = GameObject.Find("AudioSlider").GetComponent<Slider>();
        }
    }

    void UpdateVolumeVariable(){
        volume = VolumeSlider.value;

    }

    void AdjustVolumeSetting(){
        VolumeSettings.setVolume(volume);
    }


}
