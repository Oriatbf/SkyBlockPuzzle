using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;
    public Slider Musicslider;
    public Slider Effectslider;

    void Start()
    {
        Musicslider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        Effectslider.value = PlayerPrefs.GetFloat("EffectVolume", 0.75f);
    }

    private void Update()
    {
       // Debug.Log(PlayerPrefs.SetFloat("MusicVolume", sliderValue));
    }
    public void MusicSetLevel(float sliderValue)
    {
        if(sliderValue <= 0)
            mixer.SetFloat("MusicVolume", -80);
        else
        {
            mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        }
    }
    public void EffectSetLevel(float sliderValue)
    {
        if (sliderValue <= 0)
            mixer.SetFloat("EffectVolume", -80);
        else
        {
            mixer.SetFloat("EffectVolume", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("EffectVolume", sliderValue);
        }
    }
}
