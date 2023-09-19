using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject[] ParticlesDummy;
    public static GameObject[] Particles;

    private void Awake()
    {
        Particles = ParticlesDummy;
    }
}
