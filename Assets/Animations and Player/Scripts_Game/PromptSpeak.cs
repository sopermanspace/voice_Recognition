using UnityEngine;
using UnityEngine.UI;
using Facebook.WitAi.TTS.Utilities;
using Facebook.WitAi.TTS.Samples;

public class PromptSpeak : MonoBehaviour
{
       public TTSSpeaker _speaker;
       public bool playagain;

       public bool Dance = false;
    //    public InputField input;
[SerializeField] private IdlePlayer idlePlayer_anim;
 

private void Start() {
    _speaker.Speak("Hello, I am your assistant. I am here to help you with your tasks. What would you like to do?");
}

void Update()
{
    if(playagain == true)
    {
        _speaker.Speak("Hello, I am your assistant. What else would you like to do?");
        playagain = false;
    }
 if(_speaker.IsSpeaking)
    {
        Debug.Log("GPT3 is speaking");
        idlePlayer_anim.OnTelling();
        return;
    }
    if(Dance == true)
    {
        idlePlayer_anim.OnDance();
        Dance = false;
    }
    


}

// public void onButtonClicked()
// {
//     string text = input.text;
//     _speaker.Speak(text);
    
// }

}//class
