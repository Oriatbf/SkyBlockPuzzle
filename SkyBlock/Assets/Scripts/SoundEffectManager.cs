using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public AudioClip[] SoundEffect;
    public static AudioClip[] SoundEffectTEMP;

    public static AudioSource SFX;

    private void Awake()
    {
        SFX = GameObject.FindGameObjectWithTag("SoundEffect").GetComponent<AudioSource>();
        SoundEffectTEMP = SoundEffect;
    }

    public static void PlaySoundEffect(int i)
    {
        if(SFX != null)
        {
            SFX.clip = SoundEffectTEMP[i];
            SFX.Play();
        }
    }
}
