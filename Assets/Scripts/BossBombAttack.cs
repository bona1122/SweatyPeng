using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBombAttack : MonoBehaviour
{
    public GameObject explosionParticle;
    //private AudioSource bombAudio;//여기서 안할거면 의미 없는 코드;
    //public AudioClip bombExplosion;
    void Start()
    {
        //bombAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Water" || collision.gameObject.tag == "Player")
        {

           // bombAudio.PlayOneShot(bombExplosion, 1.0f);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);

        }
    }
}
