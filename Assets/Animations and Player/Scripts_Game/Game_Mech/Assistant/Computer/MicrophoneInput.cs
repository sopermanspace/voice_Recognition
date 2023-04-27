using UnityEngine;

public class MicrophoneInput : MonoBehaviour
{
     public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, 10, 44100);
        audioSource.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        audioSource.Play();
    }

    private void OnDestroy()
    {
        Microphone.End(null);
    }
}//class
