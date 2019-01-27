using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class s_StartMenuScript : MonoBehaviour
{
    public Button quitButton;
    
    void Start()
    {
        Debug.Log("I love you all so much.");
        quitButton.onClick.AddListener(Quit);
    }

    // Update is called once per frame
    public void StartGame(){
        SadAudio.StartGame();
        SceneManager.LoadScene("StarterScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
