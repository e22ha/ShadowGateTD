using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainSettings : MonoBehaviour
{
    public AudioMixerGroup Music;
    public AudioMixerGroup Effects;

    public Slider MusicSlider;
    public Slider EffectsSlider;
    
    private void Update()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("MusicLevel");
        EffectsSlider.value = PlayerPrefs.GetFloat("EffectLevel");
    }

    public void ChangeVolumeMusic()
    {
        Music.audioMixer.SetFloat("MusicVol", Mathf.Lerp(-80, 0, MusicSlider.value));
        PlayerPrefs.SetFloat("MusicLevel", MusicSlider.value);
    }

    public void ChangeVolumeEffects()
    {
        Effects.audioMixer.SetFloat("EffetsVol", Mathf.Lerp(-80, 0, EffectsSlider.value));
        PlayerPrefs.SetFloat("EffectLevel", EffectsSlider.value);
    }
}
