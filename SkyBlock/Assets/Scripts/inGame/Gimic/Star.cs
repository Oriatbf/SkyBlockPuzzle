using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (SoundEffectManager.SFX != null)
                SoundEffectManager.PlaySoundEffect(1);

            if (ParticleManager.Particles != null)
                Instantiate(ParticleManager.Particles[1], transform.position + Vector3.up * 0.5f, ParticleManager.Particles[1].transform.rotation);

            InGameManager.Inst.getStar = true;
            Destroy(gameObject);
        }
    }
}
