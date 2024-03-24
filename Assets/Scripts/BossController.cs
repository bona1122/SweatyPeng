using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //Animator anime;
    //public float cooltime;
    //private float curtime;
    public GameObject BossNormalBullet;
    public Transform BossAttackPos;
    public GameObject missileAim;
    public GameObject BossMissileBullet;

    public GameObject BombAttack;
    public GameObject BombAim;

    private float randomXvalueCopy;

    private AudioSource bossAudio;

    public AudioClip aiming;
    public AudioClip bombFalling;
    public AudioClip basicShot;

    public GameManager gameManager;

    public int BossPattern; //패턴 선택
    public int AttackEnd; //패턴 한개가 끝났는지



    void Start()
    {
        //anime = GetComponent<Animator>();
        bossAudio = GetComponent<AudioSource>();
    }
    void Think()
    {
        BossPattern = Random.Range(0, 3); //0,1,2
    }
    void BossAttackStart()
    {
        Think(); //패턴 결정
        if (BossPattern == 0)
        {
            MissileAttack();
        }
        else if (BossPattern == 1)
        {
            NormalAttack();
        }
        else
        {
            StartCoroutine("BombAttackMany");
        }
        Invoke("BossAttackStart", 4);
    }
    void Update()
    {
        if(/*Input.GetKeyDown(KeyCode.R)*/ gameManager.BossStart==1)  //공격 시작!
        {
            BossAttackStart();
            gameManager.BossStart = 2;
            gameManager.BossHpBar.gameObject.SetActive(true);
        }
        
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //NormalAttack();
            NormalAttack();
        }
        if (Input.GetKeyDown(KeyCode.W)) //폭탄 공격
        {
            //float randomXvalue = Random.Range(100, 114);
            //randomXvalueCopy = randomXvalue;

            StartCoroutine("BombAttackMethod");
            //StartCoroutine("BombAttackMany");
        }

        if (Input.GetKeyDown(KeyCode.E)) //미사일 공격
        {
            MissileAttack();
        }
    }
    void NormalAttack()
    {
        StartCoroutine("NormalAttackMany");
        //Instantiate(BossNormalBullet, BossAttackPos.position, transform.rotation);
    }
    IEnumerator NormalAttackMany()
    {
        for (int k = 0; k < 5; k++)
        {
            Instantiate(BossNormalBullet, BossAttackPos.position, transform.rotation);
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator BombAttackMethod()
    {
        //+2
        float randomXvalue = Random.Range(100, 114);
        randomXvalueCopy = randomXvalue;
       
        InvokeBombAim();
        yield return new WaitForSeconds(2);
        InvokeBomb();
    }

    //++함수
    IEnumerator BombAttackMany()
    {
        for (int i = 0; i < 5; i++)
        {
            StartCoroutine("BombAttackMethod");
            yield return new WaitForSeconds(0.7f);
        }
    }

    void InvokeBombAim ()
    {
        bossAudio.PlayOneShot(aiming, 1.0f);
        Instantiate(BombAim, new Vector3(randomXvalueCopy, -2.2f, 0.0f), BombAim.transform.rotation);
    }
    void InvokeBomb ()
    {
        bossAudio.PlayOneShot(bombFalling, 1.0f);
        Instantiate(BombAttack, new Vector3(randomXvalueCopy, 4.0f, 0.0f), BombAttack.transform.rotation);
    }

    void MissileAttack()
    {
        StartCoroutine("MissileAimActive");
    }
    IEnumerator MissileAimActive()
    {
        missileAim.SetActive(true);
        yield return new WaitForSeconds(2);
        missileAim.SetActive(false);
        Instantiate(BossMissileBullet, BossAttackPos.position, transform.rotation);
        yield return new WaitForSeconds(0.5f);
        Instantiate(BossMissileBullet, BossAttackPos.position, transform.rotation);
        yield return new WaitForSeconds(0.5f);
        Instantiate(BossMissileBullet, BossAttackPos.position, transform.rotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //기본공격과 충돌
        if (collision.gameObject.tag == "normalAttack" && gameManager.BossHpBar.value > 0)
        {
            //Damaged 
            gameManager.BossHealthDown();
            gameManager.FilledIce();  //얼음 차기
        }
        //얼음공격과 충돌하면
        if (collision.gameObject.tag == "IceAttack"  && gameManager.BossHpBar.value > 0)
        {
            //피해입기
            gameManager.BossHealthDownIce();
        }
    }
}
