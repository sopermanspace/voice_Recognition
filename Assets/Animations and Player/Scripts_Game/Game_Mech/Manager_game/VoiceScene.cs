using System.Collections;
using UnityEngine;
using static SpeechRecognizerPlugin;
using System;

public class VoiceScene : MonoBehaviour, ISpeechRecognizerPlugin
{
   [SerializeField] private bool isListening = true;
   [SerializeField] private scene_Managers sceneManager;

    private void Start()
    {
        InitializeSpeechRecognizer();
    }

    private void InitializeSpeechRecognizer()
    {
        SpeechRecognizerPlugin plugin = SpeechRecognizerPlugin.GetPlatformPluginVersion(gameObject.name);
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
        string[] result = recognizedResult.ToLower().Split('~');

        foreach (string r in result)
        {
            if (IsPlayCommand(r))
            {
                // Load Scene
                sceneManager.OnLoad("Mob_assistant");
                return;
            }
            if (IsInventory(r))
            {
                sceneManager.LoadCharacter();
                return;
            }
            if (IsAbout(r))
            {
                sceneManager.LoadAbout();
                return;
            }
            if (r.Equals("exit"))
            {
                sceneManager.OnExit();
                return;
            }
        }

        Debug.Log("No match");
    }

    private bool IsPlayCommand(string command)
    {
        string[] playCommands = {
            "play",
            "start",
            "begin",
            "start game",
            "begin game",
            "play game",
            "play the game",
            "start the game",
            "begin the game",
            "start playing",
            "begin playing",
            "play playing",
            "play the playing"
        };

        return Array.Exists(playCommands, c => c.Equals(command));
    }
    private bool IsInventory(string command)
    {
        string[] playCommands = {
            "inventory",
            "open inventory",
            "load inventory",
            "show inventory",
            "show me inventory",
            "show me the inventory",
            "show me my inventory",
            "load the inventory",
            "load my inventory",
            "open my inventory",
            "open the inventory",
        };

        return Array.Exists(playCommands, c => c.Equals(command));
    }

   private bool IsAbout(string command)
    {
        string[] playCommands = {
            "about",
            "about me",
            "about the game",
            "about this game",
            "about this",
            "about this application",
            "about this app",
        };
        return Array.Exists(playCommands, c => c.Equals(command));
    }

    public void OnError(string recognizedError)
    {
        throw new NotImplementedException();
    }
}//class
