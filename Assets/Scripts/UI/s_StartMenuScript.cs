using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_StartMenuScript : MonoBehaviour
{
    
    void Start()
    {
        Debug.Log("I love you all so much.");
    }

    // Update is called once per frame
    public void StartGame(){
        SadAudio.StartGame();
        SceneManager.LoadScene("StarterScene");
    }
}
