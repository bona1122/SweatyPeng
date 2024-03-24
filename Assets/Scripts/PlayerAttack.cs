using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject rightAttack;
    public GameObject leftAttack;
    public GameObject rightIceAttack;
    public GameObject leftIceAttack;
    public SpriteRenderer spriteRenderer;
    public Transform rightPos;
    public Transform leftPos;
    public float cooltime;
    private float curtime;
    Animator anime;

    private AudioSource playerAudio;

    public AudioClip basicAttackSound;
    public AudioClip iceAttackSound;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (curtime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Z)) //기본공격
            {
                anime.SetTrigger("Attack");
                if (!spriteRenderer.flipX)
                    Instantiate(rightAttack, rightPos.position, transform.rotation);
                else
                    Instantiate(leftAttack, leftPos.position, transform.rotation);

                curtime = cooltime;

                playerAudio.PlayOneShot(basicAttackSound, 0.5f);//basic attack sound effect
            }
            else if(Input.GetKeyDown(KeyCode.X)) //얼음공격
            {
                if(gameManager.UIIce[2].fillAmount == 1) //꽉 찬 경우
                {
                    IceAttack();
                    curtime = cooltime;
                    gameManager.UIIce[2].fillAmount = 0;
                }
                else if(gameManager.UIIce[1].fillAmount ==1) //두번째 꺼까지 찬 경우
                {
                    IceAttack();
                    curtime = cooltime;
                    gameManager.UIIce[1].fillAmount = gameManager.UIIce[2].fillAmount;
                    gameManager.UIIce[2].fillAmount = 0;
                }
                else if(gameManager.UIIce[0].fillAmount ==1)
                {
                    IceAttack();
                    curtime = cooltime;
                    gameManager.UIIce[0].fillAmount = gameManager.UIIce[1].fillAmount;
                    gameManager.UIIce[1].fillAmount = 0;
                }

                playerAudio.PlayOneShot(iceAttackSound, 1.0f);//ice attack sound effect
            }
        }
        curtime -= Time.deltaTime;
    }
    void IceAttack()
    {
        anime.SetTrigger("Attack");
        if (!spriteRenderer.flipX)
            Instantiate(rightIceAttack, rightPos.position, transform.rotation);
        else
            Instantiate(leftIceAttack, leftPos.position, transform.rotation);
    }
}
