using System.Collections;
using UnityEngine;
using static SpeechRecognizerPlugin;
using System;
using UnityEngine.UI;

public class SpeechDetect : MonoBehaviour, ISpeechRecognizerPlugin
{

[Header("Speech Recognition")]
[Tooltip("Command Text")]
public Text texts;

[Tooltip("Voice Prompt Text")]
public Text PromptTxt = null;

[Tooltip("Debug Text")]
public Text Debug_text;

public bool isListening = true;

[Space(10)]
[Header("GPT3")]
public GPT3 Gpt;
private bool askme;

[Space(10)]
[Header("Animation")]

[SerializeField] private IdlePlayer idlePlayer_anim;

[Space(10)]
[Header("Weather")]
public WeatherAPI weather;
public string fetechweather;

private void Start()
{
    InitializeSpeechRecognizer();
}

private void InitializeSpeechRecognizer()
{
    SpeechRecognizerPlugin plugin = SpeechRecognizerPlugin.GetPlatformPluginVersion(this.gameObject.name);
    plugin.SetLanguageForNextRecognition("en-US");
    
    if (isListening)
    {
        plugin.StartListening();
        plugin.SetContinuousListening(true);
        plugin.SetMaxResultsForNextRecognition(1);
    }
    else
    {
        Debug.LogWarning("Speech recognition is disabled");
    }
}

 public void OnResult(string recognizedResult)
{   
    char[] delimiterChars = { '~' };
    string[] result = recognizedResult.Split(delimiterChars);
    PromptTxt.text = "";
    texts.text = "";

    foreach (string r in result)
    {  
     if(!Gpt._speaker.IsSpeaking)
        {      
        switch(r.ToLower())
        {   
            case "weather":
                string city = r.Substring("weather in".Length).Trim();
                texts.text = "Fetching Weather";
                weather.city = city;
                weather.GetWeather();
                texts.text = $"Weather in {city}: {weather}";

                break;
           
            case "who are you":
                 texts.text = "I am your assistant SupermanSpace";
                 Gpt._speaker.Speak("I am your assistant SupermanSpace. I am a super assistant, made only for you.");

                break;

            case "hi":
            case "hello":
                idlePlayer_anim.OnGreetings();
                texts.text = "Greetings";
                Gpt._speaker.Speak("Hello, I am your assistant SupermanSpace. How are you ?");    

                break;

            case "dance":
                texts.text = "Dancing";  
                idlePlayer_anim.OnDance();
                
                break;

            case "stop":
            case "bye":
                texts.text = "Bye";
                Gpt._speaker.Speak("Bye, See you later");
     
                break;

            case "help":
                texts.text = "Help";
                Gpt._speaker.Speak("You can ask me about the weather, dance, or anything else you want to know.");
             
                break;

            case "exit":
            case "quit":
                texts.text = "Quitting";
                 StartCoroutine(Wait());
                break;

            default:
                PromptTxt.text += r;
                StartCoroutine(CreateRequestCoroutine());
                
                break;
         }     
                  
       }     
    }    

}

private IEnumerator CreateRequestCoroutine() 
{
    Gpt.prompt = PromptTxt.text;
    Debug_text.text = Gpt.prompt;
     StartCoroutine(Gpt.SendGpt3Request());
     yield return new WaitForSeconds(0.1f);
}

// Quit Application
private IEnumerator Wait()
{
     yield return new WaitForSeconds(0.3f);
     Application.Quit(); 
}

public void OnError(string recognizedError)
{
     throw new NotImplementedException();
}

    


}//class






