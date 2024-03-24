using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalBullet : MonoBehaviour
{
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        if (transform.position.x < 100)
            DestroyBullet();
    }
    void DestroyBullet() // 소멸
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            DestroyBullet();
    }
}
