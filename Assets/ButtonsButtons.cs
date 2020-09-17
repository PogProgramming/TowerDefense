using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsButtons : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void btn_Exit()
    {
        Application.Quit(0);
    }

    public void btn_Play()
    {
        SceneManager.LoadScene(1);
        SceneManager.UnloadSceneAsync(0);
    }
}
