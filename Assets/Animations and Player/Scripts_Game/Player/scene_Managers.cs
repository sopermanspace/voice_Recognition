using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_Managers : MonoBehaviour
{
    public Animator anim;
  public GameObject[] Pannels;

    private void Start() {
        
            Pannels[0].SetActive(false);
            Pannels[1].SetActive(true);
    }

    public void OnLoad(string scenename)
    {
      
        StartCoroutine(Loading(scenename));
    }
 public  IEnumerator Loading(string scenenames){
          Pannels[0].SetActive(true); // Loading Pannel
          Pannels[1].SetActive(false); // Menu Pannel
         anim.SetBool("Loading",true);
       yield return new WaitForSeconds(2); 
          SceneManager.LoadScene(scenenames);
           anim.SetBool("Loading",false);
        }


    public void  OnExit () {
        Application.Quit();
    }
    
}//class
