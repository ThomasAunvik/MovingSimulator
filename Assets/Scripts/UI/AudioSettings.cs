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
        DontDestroyOnLoad(gameObject);
    }

    void Update(){
        if (VolumeSlider == null)
        {
            GameObject obj = GameObject.Find("AudioSlider");
            if (obj != null)
            {
                VolumeSlider = obj.GetComponent<Slider>();
            }
        }

        CheckForMainCamera();
        UpdateVolumeVariable();
        AdjustVolumeSetting();

    }

    void CheckForMainCamera(){
        if(MainCamera == null){
            MainCamera = Camera.main;
        }        
    }

    [SerializeField] private Slider VolumeSlider;

    void UpdateVolumeVariable(){
        volume = VolumeSlider.value;
    }

    void AdjustVolumeSetting(){
        VolumeSettings.setVolume(volume);
    }


}
