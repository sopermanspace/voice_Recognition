using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ComputeSpeech : MonoBehaviour
{
   
[Tooltip("Text to display the response")]
public Text texts; // Text to display the response at the top of the left Anchor

[Tooltip("Voice Prompt Text")]
public Text PromptTxt = null;

[Tooltip("Debug Text")]
public Text Debug_text;

[Tooltip("Response Text To String")]
protected string word = "";
public bool isListening = true;


[Space(10)]
[Header("Animation")]
public Animator anim;
public GameObject danceGame;


[Space(10)]
[Header("Weather")]
public WeatherAPI weather;
public string fetechweather;

public GPT3 Gpt;


private void Awake()
{
    Gpt._speaker.Speak("");
}
private void Start()
{
    // Hide dance game
    danceGame.SetActive(false);

    // Get platform-specific SpeechRecognizerPlugin
    SpeechRecognizerPlugin plugin = SpeechRecognizerPlugin.GetPlatformPluginVersion(this.gameObject.name);

    // Set language to English US
    plugin.SetLanguageForNextRecognition("en-US");

    // Start listening
    plugin.StartListening();

    // Set continuous listening to true
    plugin.SetMaxResultsForNextRecognition(1);
    plugin.SetContinuousListening(true);

    // 
}

public void OnResult(string recognizedResult)
{
    if (Gpt._speaker.IsSpeaking)
    {
        Debug.Log("GPT3 is speaking");
        texts.text = "<<<<<<<<<<<GPT3 is speaking>>>>>>>>>>>";
        isListening = false;
        return;
    }
    char[] delimiterChars = { '~' };
    string[] result = recognizedResult.Split(delimiterChars);

    PromptTxt.text = "";
    texts.text = "";
    anim.SetBool("Idle", true);

    for (int i = 0; i < result.Length; i++)
    {
        if (result[i] == "hi" || result[i] == "hello")
        {
            anim.SetTrigger("Hi");
            Gpt._speaker.Speak("Hello, I am your assistant SupermanSpace. How are you ?");
            anim.SetBool("Idle", false);
        }
        else if (result[i] == "dance")
        {
            texts.text = "Dancing";
            anim.SetTrigger("Dance");
            danceGame.SetActive(true);
            anim.SetBool("Idle", false);
        }
        else if (result[i] == "jump")
        {
            texts.text = "Jumping";
            anim.SetTrigger("Jump");
            anim.SetBool("Idle", false);
        }
        else if (result[i].StartsWith("weather in"))
        {
            string city = recognizedResult.Substring("weather in".Length).Trim();
            texts.text = "Fetching Weather";
            weather.city = city;
            weather.GetWeather();
            texts.text = "Weather in " + city + ": " + weather;
        }
        else if (result[i] == "exit" || result[i] == "quit")
        {
            texts.text = "Quitting";
            StartCoroutine(Wait());
        }
        else
        {
            PromptTxt.text += result[i];
            Gpt.prompt = PromptTxt.text;
            Debug_text.text = Gpt.prompt;
        }
    }
     StartCoroutine(Gpt.SendGpt3Request());
     danceGame.SetActive(false);
     isListening = true;
}


private IEnumerator Wait()
{
     yield return new WaitForSeconds(0.5f);
     Application.Quit(); 
    }






}//class


















