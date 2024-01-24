using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundControll : MonoBehaviour
{
    public AudioMixerGroup Music;
    public AudioMixerGroup Effects;

    // Start is called before the first frame update
    private void Start()
    {
        Music.audioMixer.SetFloat("MusicVol", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("MusicLevel")));
        //Effects.audioMixer.SetFloat("EffetsVol", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("EffectLevel")));
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
