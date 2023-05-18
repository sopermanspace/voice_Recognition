using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_Managers : MonoBehaviour
{
    public Animator anim;
  public GameObject[] Pannels;

    private void Start() {
        
            Pannels[0].SetActive(false); // Transition Pannel
            Pannels[1].SetActive(true);  // Menu Pannel
            Pannels[2].SetActive(false); // Character Pannel
            Pannels[3].SetActive(false); // About Pannel
    }

public void OnLoad(string scenename)
    {
      
        StartCoroutine(Loading(scenename));
    }
public  IEnumerator Loading(string scenenames)
    {
          Pannels[0].SetActive(true); 
          Pannels[1].SetActive(false); 
          anim.SetBool("Loading",true);
        yield return new WaitForSeconds(2); 
         
          SceneManager.LoadScene(scenenames);
          anim.SetBool("Loading",false);
        }

public void LoadAbout() =>  Pannels[3].SetActive(true);

public void LoadCharacter() =>  Pannels[2].SetActive(true);

public void LoadMenu() 
    {
         Pannels[1].SetActive(true);
         Pannels[2].SetActive(false);
         Pannels[3].SetActive(false);
    }

public void  OnExit () =>  Application.Quit();
  
  
}//class
