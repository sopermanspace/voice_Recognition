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

[Tooltip("Player Prefab")]
public GameObject Player;
[Tooltip("Player Animator Instancer")]
public PlayerMov playerMov;


[SerializeField] public TTSSpeaker _speaker;


 void Start(){
   pluginReF();
 }
 
  void pluginReF(){
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
           
                   if (result[i] == "forward" || result[i] == "go"  )
                      { 
                         texts.text = "Moving forward";    
                         _speaker.Speak(texts.text);    
                           playerMov.Forward();   
                      }
                  else if (result[i] == "turn" || result[i] == "back" )
                   {
                    playerMov.Back();
                    texts.text = "Moving Backward";
                    _speaker.Speak(texts.text);     
                    }  
                 else if (result[i] == "stop" || result[i] == "halt")
                 {
                   texts.text = "stop";
                   _speaker.Speak(texts.text);
                   playerMov.Stop();
                  
                  }
                  else if(result[i] == "jump" || result[i] == "up")
                      {
                      texts.text = "Jumping";
                      _speaker.Speak(texts.text);
                       playerMov.Jump();
                     }    
                  else if(result[i] == "exit" || result[i] == "quit")
                        {
                        texts.text = "Quitting";
                       StartCoroutine(Quit());
                        }
                    if(_speaker.IsSpeaking){
                      // if is speaking then ignore the other voice Commands
                        StartCoroutine(waitforseconds());
                      
                    }
        }
    }


IEnumerator Quit(){
    yield return new WaitForSeconds(0.5f);
     Application.Quit();
}
IEnumerator waitforseconds(){
    yield return new WaitForSeconds(0.5f);
}


    public void OnError(string recognizedError)
    {
        throw new NotImplementedException();
    }






}//class
