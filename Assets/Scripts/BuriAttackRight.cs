using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuriAttackRight : MonoBehaviour
{
    public float speed;

    void Start()
    {
        Invoke("DestroyBullet", 1);
    }


    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //공격이 플랫폼에 맞는 경우
        if (collision.gameObject.layer == 8 || collision.gameObject.tag =="Enemy")
        {
            Destroy(gameObject);
        }

    }
    void DestroyBullet() // 소멸
    {
        Destroy(gameObject);
    }
}