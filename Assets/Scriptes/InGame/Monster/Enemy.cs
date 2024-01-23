using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// 9종류의 몬스터는 Sprite Animation을 받아서 다른 존재로 등장합니다.
// 레벨에 따른 몬스터를 생성할 수 있도록 애니메이션을 Spawner에게 전달해줍니다.
// 죽었다면 ? => 비활성화 시키며 오디오 Clip을 실행합니다.
// PoolMg에 의해 활성화가 될 때 자신의 값을  OnEnable()과 Init()으로 초기화합니다.

// 생성 또는 활성화 되었을 때는 플레이어의 RigidBody2D를 받아서 위치를 따라가게 합니다.

public class Enemy : MonoBehaviour, IHitable, IDeadable
{
    [Header("Monster Info")]
    protected float hp;
    protected int minHp = 0;
    protected int maxHp;
    protected float speed;
    protected float atk;

    [Header("Monster Anims")]
    protected Animator monAnim;
    public RuntimeAnimatorController[] monsterAnimCon;  // 몬스터 수 만큼 -> 프리펩을 대체한다.
    protected Rigidbody2D rb;
    protected SpriteRenderer sprite;

    [Header("Target Info")]
    public Rigidbody2D target;                          // 타겟 = 플레이어

    [Header("Drop Items")]
    protected int score;
    protected int maxCoin;
    protected GameObject coin;
    protected GameObject cloneCoin;     // 복사 생성 될 코인
    protected int maxexp = 1;
    protected GameObject exp;
    protected GameObject cloneExp;      // 복사 생성 될 경험치

    [Header("SoundClip")]
    public AudioClip monsterHitClip;
    public AudioClip ClearClip;

    public virtual float Hp
    {
        get => hp;
        set
        {
            hp = value;
            if(hp <= minHp)
            {
                Dead();
            }
        }
    }

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        monAnim = GetComponent<Animator>();
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate()
    {
        if (!GameManager.instance.IsStart)
            return;
        //      방향   = 플레이어 위치 - 몬스터 위치
        Vector2 dirVec = target.position - rb.position;
        //  다음 향할 곳 = 방향 평균화 * 속도 * 프레임과 상관없이 동일한 속도 증가;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        // 이동하라 ( 몬스터 위치 + 몬스터가 다음에 향항 방향 )
        rb.MovePosition(rb.position + nextVec);
        // 물리적인 충돌이 이동에 영향을 주지 않도록 물리속도를 0 으로 고정한다.
        rb.velocity = Vector2.zero;
    }

    protected void LateUpdate()
    {
        // SpriteRenderer의 Flip은 평소 False 이다.
        // 타겟의 x가 몬스터의 x보다 큰 경우 FlipX는 true 값을 반환하여 이미지가 반전된다.
        sprite.flipX = target.position.x > rb.position.x;
    }

    protected void OnEnable()   // 초기화
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        hp = maxHp;
    }

    // Spawner이 소환된 몬스터를 받고 초기화 시켜준다.
    public virtual void Init(SpawnData monData)
    {
        // 예외 = 현재가 크면 멈춤! -1 해서 맞춰준다.
        if (monData.spriteType >= monsterAnimCon.Length)
        {
            monData.spriteType = monsterAnimCon.Length - 1;
        }

        // 현재 개체수 < 최대 개체수 = 소환 수 조절
        if (monData.curCount < monData.maxCount)
        {
            monData.curCount++;
            monAnim.runtimeAnimatorController = monsterAnimCon[monData.spriteType];
        }

        if (monsterAnimCon[monData.spriteType] == monsterAnimCon[8])
        {
            gameObject.AddComponent<FirstBoss>();
            gameObject.GetComponent<FirstBoss>().Init(monData);
            Destroy(gameObject.GetComponent<Enemy>());
        }
        speed = monData.speed;
        maxHp = monData.health;
        Hp = maxHp;
        atk = monData.atk;
        score = monData.score;
        coin = monData.dropCoin;
        maxCoin = monData.maxCoin;
        exp = monData.dropExp;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Player>(out var player))
        {
            player.Hit(atk);
        }
    }
   
    public virtual void Hit(float damage)
    {
        Hp -= damage;
    }
    
    public virtual void Dead()
    {
        for (int i = 0; i < maxCoin; i++)
        {
            cloneCoin = Instantiate(coin, this.transform.position * 1f, Quaternion.identity);
        }
        for (int i = 0; i < maxexp; i++)
        {
            cloneExp = Instantiate(exp);
            cloneExp.gameObject.transform.position = this.transform.position;
        }
        RecordInfoManager.instance.ReScore += score;
        RecordInfoManager.instance.ReMonsterKillCount++;
        SoundManager.instance.Play(monsterHitClip, SoundManager.instance.transform);
        this.gameObject.SetActive(false);
    }
}