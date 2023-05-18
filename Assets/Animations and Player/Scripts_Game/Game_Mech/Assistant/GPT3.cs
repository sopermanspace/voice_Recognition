using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.UI;
using Facebook.WitAi.TTS.Utilities;
using Facebook.WitAi.TTS.Samples;
public class GPT3 : MonoBehaviour
{
 [Header("OpenAI API")]
 [Space(10)]
    private APILoader config;
    string Configkey;
    public const string API_URL = "https://api.openai.com/v1/completions";
    public int timeBetweenRequests = 2; // time in seconds
    private float lastRequestTime;
    public string prompt;
    [SerializeField]
    private string model = "text-davinci-002";// "";
    public int maxTokens = 500;
    public float temperature = 0.7f;
   
    [Space(10)]
    [Header("Voice Recognition")]
    public GameObject promptError;
    public TTSSpeaker _speaker;

    //  public Text apiText; 
  
    
void Start()
 {   
         LoadApi(); 
         lastRequestTime = Time.time;    
         promptError.SetActive(false);   
        
 }
 public void makeRequest(){
        StartCoroutine(SendGpt3Request());
 }

public IEnumerator SendGpt3Request()
{
    // Check if there is Any Error in the Input Fields
    if (string.IsNullOrEmpty(Configkey)) 
    {
         Debug.LogError("API key is not set. Please provide a valid API key.");
         promptError.gameObject.SetActive(true);
         yield break;
    }

     if(string.IsNullOrEmpty(prompt))
     {
            Debug.LogError("Prompt is not set. Please provide a valid prompt.");
            promptError.SetActive(true);
     
            yield break;
    }
     if(maxTokens < 1)
    {
        Debug.LogError("Max Tokens is not set. Please provide a valid Max Tokens.");
      
        yield break;
    }

  // Check the cache for the response
    string cacheKey = model + ":" + prompt + ":" + maxTokens + ":" + temperature;
    string cachedResponse = (string)Cache.GetCache(cacheKey);
    if (cachedResponse != null)
    {
        Debug.Log("Response found in cache.");
        _speaker.Speak(cachedResponse);
        yield break;
    }
 
        // check if the time between requests has passed
    if (Time.time - lastRequestTime < timeBetweenRequests)
    {
        yield return new WaitForSeconds(timeBetweenRequests);
    }
  

    // Create a new request
     UnityWebRequest request = new UnityWebRequest(API_URL, "POST");
    string requestBody = "{\"model\":\"" + model + "\", \"prompt\":\"" + prompt + "\", \"max_tokens\":" + maxTokens + ", \"temperature\":" + temperature + "}";
    byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);
     request.uploadHandler = new UploadHandlerRaw(bodyRaw);
     request.downloadHandler = new DownloadHandlerBuffer();
     request.SetRequestHeader("Content-Type", "application/json");
     request.SetRequestHeader("Authorization", "Bearer " + Configkey);
 
    // Send the request and wait for the response
    yield return request.SendWebRequest();

    // Check for errors else process the response
    if (request.result == UnityWebRequest.Result.ConnectionError 
    || request.result == UnityWebRequest.Result.ProtocolError 
    || request.result == UnityWebRequest.Result.DataProcessingError)
    {
        Debug.LogError(request.error);
        Debug.LogError(request.downloadHandler.text);
        Debug.LogError(request.responseCode);

    }
    else
    {
        // Filtering  Response Text To Get the response  
        string responseText = request.downloadHandler.text; // Fetch the response
        int startIndex = responseText.IndexOf("text\":\"") + "text\":\"".Length;  
        int endIndex = responseText.IndexOf("\",", startIndex);
        int length = endIndex - startIndex;
        string response = responseText.Substring(startIndex, length);
      
        Debug.Log(response);
        _speaker.Speak(response);

        // Cache the response
        Cache.SetCache(cacheKey, response);
  
    }
      lastRequestTime = Time.time; // update the last request time
}

//read the API key from the config file
public void LoadApi()
{
    APILoader config = Resources.Load<APILoader>("APIConfig");

    if (config == null)
    {
        Debug.LogError("APIConfig not found in Resources folder!");
        return;
    }

   Configkey = config.apiKey;
   
}


}//class
