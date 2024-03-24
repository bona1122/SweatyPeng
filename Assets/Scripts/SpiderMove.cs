using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMove : MonoBehaviour
{
    public GameManager gameManager;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //기본공격과 충돌
        if (collision.gameObject.tag == "normalAttack")
        {
            //Damaged 효과
            OnDamaged();
            gameManager.FilledIce();
        }
    }
    private void OnDamaged()
    {
            //레이어 변경
            gameObject.layer = 12;

            spriteRenderer.color = new Color(1, 1, 1, 0.4f); //마지막이 투명도

            //Invoke("OffDamagedEnemy", 3); //무적시간
            StartCoroutine("DamagedDelay");
    }
    IEnumerator DamagedDelay()
    {
        yield return new WaitForSeconds(2f);

        gameObject.layer = 11; //레이어 다시 돌려놓음
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
