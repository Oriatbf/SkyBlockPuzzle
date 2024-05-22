using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject[] ParticlesDummy;
    public static GameObject[] Particles;
    public static ParticleManager Instance;

    private void Awake()
    {
        if (Instance != gameObject && Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Particles = ParticlesDummy;
    }
}
