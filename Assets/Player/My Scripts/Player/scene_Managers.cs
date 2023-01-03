using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_Managers : MonoBehaviour
{
   
    public void OnLoad(string scenename)
    {
      
        SceneManager.LoadScene(scenename);
    }

    public void  OnExit () {
        Application.Quit();
    }
    
}
