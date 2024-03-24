using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBullet : MonoBehaviour
{
    public float speed;
    public GameObject player;
    private Vector3 target;

    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(followTarget(player));
        StartCoroutine("DestroyAuto");
    }

    void Update()
    {
        transform.Translate(target * speed * Time.deltaTime);
    }
    IEnumerator DestroyAuto()
    {
        yield return new WaitForSeconds(8);
        DestroyBullet();
    }
    IEnumerator followTarget(GameObject _obj)
    {
        if (_obj != null)
        {
            while (gameObject.activeSelf)
            {
                target = (_obj.transform.position - transform.position).normalized;
                // 내적(dot)을 통해 각도를 구함. (Acos로 나온 각도는 방향을 알 수가 없음)
                float dot = Vector2.Dot(transform.up, target);
                if (dot < 1.0f)
                {
                    float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

                    // 외적을 통해 각도의 방향을 판별.
                    Vector3 cross = Vector3.Cross(transform.up, target);
                    // 외적 결과 값에 따라 각도 반영
                    if (cross.z < 0)
                    {
                        angle = transform.rotation.eulerAngles.z - Mathf.Min(10, angle);
                    }
                    else
                    {
                        angle = transform.rotation.eulerAngles.z + Mathf.Min(10, angle);
                    }

                    // angle이 윗 방향과 target의 각도.
                    // do someting.
                }
                yield return new WaitForSeconds(0.04f);
            }
        }
        else
        {
            Debug.Log("Check Target : target is null");
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") //플레이어와 충돌 시
        {  
            DestroyBullet(); // 사라지기
        }
    }
    void DestroyBullet() // 소멸
    {
        Destroy(gameObject);
    }
}
