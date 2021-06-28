using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VoiceManager : MonoBehaviour
{
    public GameObject[] VoiceLines;
    public int index = 0;

    public void Play()
    {
        VoiceLines[index].SetActive(true);
        index++;
    }

    public void Stop()
    {
        VoiceLines[index-1].SetActive(false);
    }
}
