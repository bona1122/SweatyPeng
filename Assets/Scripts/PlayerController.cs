using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    private Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    public float maxSpeed;
    //public float jumpPower;

    Animator anim;

    private AudioSource playerAudio;

    public AudioClip jumpSound;
    public AudioClip hurtedSound;
    public AudioClip dieSound;

    //점프 관련 변수들
    [SerializeField] float m_jumpForce = 0f;
    [SerializeField] int m_maxJumpCount = 0;
    public int m_jumpCount = 0;
    float m_distance = 0f;
    [SerializeField] LayerMask m_layerMask = 10;

    //미사일 맞는 소리
    private AudioSource missileAudio;//여기서 안할거면 의미 없는 코드;
    public AudioClip missileExplosion;//플레이어가 미사일 맞을 때 소리

    //폭탄맞는소리 실험
    //private AudioSource bombAudio;//여기서 안할거면 의미 없는 코드;
   // public AudioClip bombExplosion;//폭탄 터질 때 소리

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        //점프관련
        m_distance = GetComponent<CapsuleCollider2D>().bounds.extents.y + 0.05f;

        missileAudio = GetComponent<AudioSource>();  //미사일 맞는 소리

        //폭탄맞는소리실험
        //bombAudio = GetComponent<AudioSource>();//여기서 안할거면 의미 없는 코드;
    }
    void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.C) /*&& !anim.GetBool("isJumping")*/)
        {
            if (m_jumpCount < m_maxJumpCount)
            {
                m_jumpCount++;
                rigid.velocity = Vector2.up * m_jumpForce;
                anim.SetBool("isJumping", true); // animation jumping
                playerAudio.PlayOneShot(jumpSound, 0.5f);//jump sound effect
            }
        }
    }
    void CheckGround()
    {
        if (rigid.velocity.y < 0)
        {
            RaycastHit2D t_hit = Physics2D.Raycast(transform.position, Vector2.down, m_distance, m_layerMask);
            //Debug.Log(t_hit.collider.name);
            if (t_hit)
            {
                Debug.Log(t_hit.transform.gameObject.name);
                if (t_hit.transform.tag == "Platform")
                {
                    anim.SetBool("isJumping", false); //점프 고치려고 추가해봄
                    m_jumpCount = 0;
                }
            }
        }
    }

    void Update()
    {
        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //Direction Sprite 방향전환
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        //jump
        //if (Input.GetKeyDown(KeyCode.C) && !anim.GetBool("isJumping"))
        //{
        //    rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        //    anim.SetBool("isJumping", true); // animation jumping
        //    playerAudio.PlayOneShot(jumpSound, 0.5f);//jump sound effect
        //}
        TryJump();
        CheckGround();

        //animation walking
        if (rigid.velocity.normalized.x == 0)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);

    }

    void FixedUpdate()
    {
        //Move Speed
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //Max Speed
        if (rigid.velocity.x > maxSpeed) //Right Max Speed
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < -maxSpeed) //Left Max Speed
        {
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
        }
        //Landing Platform
        //if (rigid.velocity.y < 0)
        //{
        //    Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
        //    RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1);
        //    if (rayHit.collider != null)
        //    {
        //        if (rayHit.distance < 0.5f)
        //            anim.SetBool("isJumping", false);
        //    }
        //}
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //적과 충돌
        if (collision.gameObject.tag == "Enemy")
        {
            playerAudio.PlayOneShot(hurtedSound, 1.0f); //기본 맞는소리
            //Damaged 
            OnDamaged(collision.transform.position);
        }
        else if (collision.gameObject.tag =="Water")
        {
            playerAudio.PlayOneShot(dieSound, 1.0f);
            //다시 시작하기 버튼 활성화
            gameManager.restartButton.gameObject.SetActive(true);
            //유다이 메시지 띄우기
            gameManager.UIYouDied.SetActive(true);
        }
        else if(collision.gameObject.layer == 16) //기본공격과 충돌 시
        {
            playerAudio.PlayOneShot(hurtedSound, 1.0f);  //기본 맞는 소리
            OnDamaged(collision.transform.position);
        }
        else if(collision.gameObject.layer == 13) //폭탄과 충돌 시
        {
            //bombAudio.PlayOneShot(bombExplosion, 1.0f);//폭탄 터질 때 소리 함수 호출
            gameManager.heart -= 3; //즉사
        }
        else if(collision.gameObject.layer == 15) //공중플랫폼 닿는경우
        {
            anim.SetBool("isJumping", false); //점프 고치려고 추가해봄
            m_jumpCount = 0;
        }
        else if(collision.gameObject.layer == 14)
        {
            missileAudio.PlayOneShot(missileExplosion, 1.0f);//플레이어가 미사일 맞을 때 소리 함수
            OnDamaged(collision.transform.position);
        }

    }
    void OffDamaged()
    {
        gameObject.layer = 10; //레이어 다시 돌려놓음
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    void OnDamaged(Vector2 targetPos)
    {
        //playerAudio.PlayOneShot(hurtedSound, 1.0f);
        //health down
        gameManager.HealthDownEnemy();

        //레이어 변경
        gameObject.layer = 9;

        spriteRenderer.color = new Color(1, 1, 1, 0.4f); //마지막이 투명도

        //튕겨 나감
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 3, ForceMode2D.Impulse);

        Invoke("OffDamaged", 3); //무적시간
    }
}
