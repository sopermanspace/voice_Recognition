using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpeechRecognizerPlugin;
using System.Linq;
using System;
using UnityEngine.UI;
using Facebook.WitAi.TTS.Utilities;
using Facebook.WitAi.TTS.Samples;



public class SpeechRecog : MonoBehaviour, ISpeechRecognizerPlugin
{

[Header("Speech Recognition")]
[Tooltip("Command Text")]
public Text texts;
[Tooltip("Response Text")]
public Text texts1 = null;
[Tooltip("Response Text To String")]
protected string word ="";
[Tooltip("Player Prefab")]
public GameObject Player;
[Tooltip("Player Animator Instancer")]
public PlayerMov playerMov;

[SerializeField] public TTSSpeaker _speaker;


 void Start(){
      
      SpeechRecognizerPlugin plugin = SpeechRecognizerPlugin.GetPlatformPluginVersion(this.gameObject.name);
      plugin.SetLanguageForNextRecognition("en-US");
      plugin.StartListening();
      plugin.SetContinuousListening(true);
      plugin.SetMaxResultsForNextRecognition(1);
 }
 
    

 public void OnResult(string recognizedResult){
          char[] delimiterChars = { '~' };
         string[] result = recognizedResult.Split(delimiterChars);

             texts1.text = "";
       
             for (int i = 0; i < result.Length; i++)
                {
                texts1.text += result[i] + '\n';
           
           
                 if(result[i] == "hello")
                 {
                  playerMov.anim.SetTrigger("wave");
                  playerMov.anim.SetBool("Idle",false);
                 _speaker.Speak("Hi!"); 
   
                 if(_speaker.IsSpeaking == true)
                   {
                   texts.text = "Hi!"; 
                   }
                 }
                             
                 else if(result[i] == "how are you")
                  {
                    _speaker.Speak(texts.text);
                 
                  //   if(_speaker.IsSpeaking == true)
                  //     {
                  //  texts.text = "I am fine";
                  //     }
                  
                  }
                  
                   else if (result[i] == "what is your name")
                   {
                     _speaker.Speak(texts.text);
                   texts.text = "My name is Mario";
                    }

                    else if (result[i] == "forward" )
                      { _speaker.Speak(texts.text);
                         texts.text = "Moving forward";        
                           playerMov.Forward();
                         
                      }
                  else if (result[i] == "turn" )
                   {
                    playerMov.Back();
                    texts.text = "Moving Backward";
                    _speaker.Speak(texts.text);     
                    }  
                 else if (result[i] == "stop" )
                 {
                   texts.text = "stop";
                   playerMov.Stop();
                  _speaker.Speak(texts.text);
                  }
                  else if(result[i] == "jump")
                      {
                      texts.text = "Jumping";
                       playerMov.Jump();
                     _speaker.Speak(texts.text);
                     }    
                  else if(result[i] == "exit")
                        {
                        texts.text = "Quitting";
                       StartCoroutine(Wait());
                        }
        }
    }


IEnumerator Wait(){
    yield return new WaitForSeconds(0.5f);
     Application.Quit();
  
}



    public void OnError(string recognizedError)
    {
        throw new NotImplementedException();
    }






}//class
