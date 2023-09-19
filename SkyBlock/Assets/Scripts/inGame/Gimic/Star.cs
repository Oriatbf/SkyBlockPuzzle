using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Star : MonoBehaviour
{
    public GameObject player;
    public float playerStarCount;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (SoundEffectManager.SFX != null)
                SoundEffectManager.PlaySoundEffect(1);

            if (ParticleManager.Particles != null)
                Instantiate(ParticleManager.Particles[1], transform.position + Vector3.up * 0.5f, ParticleManager.Particles[1].transform.rotation);

            player.GetComponent<Player>().StarCount++;
            Destroy(gameObject);
        }
    }
}
