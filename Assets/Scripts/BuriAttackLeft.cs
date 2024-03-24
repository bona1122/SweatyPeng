using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuriAttackLeft : MonoBehaviour
{
    public float speed;

    void Start()
    {
        Invoke("DestroyBullet", 1);
    }


    void Update()
    {
        transform.Translate(transform.right * -1 * speed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //기본공격이 플랫폼에 맞는 경우
        if (collision.gameObject.layer == 8 || collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
    void DestroyBullet() // 소멸
    {
        Destroy(gameObject);
    }
}
