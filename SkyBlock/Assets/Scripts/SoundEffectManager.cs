using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public AudioClip[] SoundEffect;
    public static AudioClip[] SoundEffectTEMP;
    public GameObject musicEffectScore;
    public static AudioSource SFX;
    public static SoundEffectManager Instance;

    private void Awake()
    {
        if (Instance != gameObject && Instance != null)
        {
            Destroy(gameObject);
            Destroy(musicEffectScore);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(musicEffectScore);
        }
     
        SFX = GameObject.FindGameObjectWithTag("SoundEffect").GetComponent<AudioSource>();
        SoundEffectTEMP = SoundEffect;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlaySoundEffect(1);
        }
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
