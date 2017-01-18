using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class UseMic : MonoBehaviour
{
    AudioSource _audioSource;
    AudioSource audioSource
    {
        get
        {
            if (!_audioSource)
            {
                _audioSource = GetComponent<AudioSource>();
            }
            return _audioSource;
        }
    }

    string device = null;
    Rect playBtn = new Rect(100, 100, 200, 60);
    Rect stopBtn = new Rect(400, 100, 200, 60);
    Rect posLabel = new Rect(100, 400, 200, 60);

    void OnGUI()
    {
        if (GUI.Button(playBtn, "play"))
        {
            InitMic();
        }
        if (GUI.Button(stopBtn, "stop"))
        {
            StopMic();
        }

        GUI.Label(posLabel, GetData() + "");
    }

    public void InitMic()
    {
        if (Microphone.devices != null && Microphone.devices.Length > 0)
        {
            device = Microphone.devices[0];
            audioSource.clip = Microphone.Start(device, true, 10, 44100);
            audioSource.loop = true; // Set the AudioClip to loop
            audioSource.mute = true; // Mute the sound, we don't want the player to hear it
            while (!(Microphone.GetPosition(device) > 0)) { } // Wait until the recording has started
            audioSource.Play(); // Play the audio source!
        }
    }

    public float GetData()
    {
        float[] data = new float[256];
        float a = 0;
        audioSource.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }

    public void StopMic()
    {
        audioSource.Stop();
        Microphone.End(device);
    }
}
