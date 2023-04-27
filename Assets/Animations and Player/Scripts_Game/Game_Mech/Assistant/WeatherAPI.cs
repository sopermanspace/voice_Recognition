using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
 using System.Xml;

public class WeatherAPI : MonoBehaviour
{
   public string appId = "2559718f20b7f9b7f2edd09debf659c3"; //openweathermap.org
   public string city ;
   
   public GPT3 gpt;

//    public SpeechDetect speechDetect;
public ComputeSpeech speechDetect;

    // Update is called once per frame
    // void Start()
    // {
    //     // Debug.Log("WeatherAPI");
    //     GetWeather();
    // }

public void GetWeather()
{
    // **********************************DEBUG**********************************************************
    if (string.IsNullOrEmpty(city))
    {
        Debug.LogError("City can not be null or empty.");
        return;
    }

    if (string.IsNullOrEmpty(appId))
    {
        Debug.LogError("App ID can not be null or empty");
        return;
    }
// ********************************************************************************************
 // Create a request to the OpenWeatherMap API
    var request = UnityWebRequest.Get($"https://api.openweathermap.org/data/2.5/weather?q={city}&mode=xml&lang=tr&units=metric&appid={appId}");
    
    request.SendWebRequest(); // Send the request

    while (!request.isDone)
    {
        // Wait for the request to complete
    }

    if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
    {
        Debug.LogError(request.error);
        return;
    }
  // Parse the response to  XML 
XmlDocument xmlResponse = new XmlDocument();
xmlResponse.LoadXml(request.downloadHandler.text);

// Access the temperature element
XmlNode tempNode = xmlResponse.SelectSingleNode("current/temperature");
XmlNode weatherNode = xmlResponse.SelectSingleNode("current/weather");

if (tempNode != null && weatherNode != null)
{
    float temp = float.Parse(tempNode.Attributes["value"].Value);
    Debug.Log("The temperature for " + city + " is " + temp + " degrees" );
    gpt._speaker.Speak("The temperature for " + city + " is " + (int)temp + " degrees");
 }
else
{
    Debug.LogError("Temperature and weather state not found in the response.");
 }




}




}//class

