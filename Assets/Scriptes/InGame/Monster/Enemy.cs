using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// 9������ ���ʹ� Sprite Animation�� �޾Ƽ� �ٸ� ����� �����մϴ�.
// ������ ���� ���͸� ������ �� �ֵ��� �ִϸ��̼��� Spawner���� �������ݴϴ�.
// �׾��ٸ� ? => ��Ȱ��ȭ ��Ű�� ����� Clip�� �����մϴ�.
// PoolMg�� ���� Ȱ��ȭ�� �� �� �ڽ��� ����  OnEnable()�� Init()���� �ʱ�ȭ�մϴ�.

// ���� �Ǵ� Ȱ��ȭ �Ǿ��� ���� �÷��̾��� RigidBody2D�� �޾Ƽ� ��ġ�� ���󰡰� �մϴ�.

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
    public RuntimeAnimatorController[] monsterAnimCon;  // ���� �� ��ŭ -> �������� ��ü�Ѵ�.
    protected Rigidbody2D rb;
    protected SpriteRenderer sprite;

    [Header("Target Info")]
    public Rigidbody2D target;                          // Ÿ�� = �÷��̾�

    [Header("Drop Items")]
    protected int score;
    protected int maxCoin;
    protected GameObject coin;
    protected GameObject cloneCoin;     // ���� ���� �� ����
    protected int maxexp = 1;
    protected GameObject exp;
    protected GameObject cloneExp;      // ���� ���� �� ����ġ

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
        //      ����   = �÷��̾� ��ġ - ���� ��ġ
        Vector2 dirVec = target.position - rb.position;
        //  ���� ���� �� = ���� ���ȭ * �ӵ� * �����Ӱ� ������� ������ �ӵ� ����;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        // �̵��϶� ( ���� ��ġ + ���Ͱ� ������ ���� ���� )
        rb.MovePosition(rb.position + nextVec);
        // �������� �浹�� �̵��� ������ ���� �ʵ��� �����ӵ��� 0 ���� �����Ѵ�.
        rb.velocity = Vector2.zero;
    }

    protected void LateUpdate()
    {
        // SpriteRenderer�� Flip�� ��� False �̴�.
        // Ÿ���� x�� ������ x���� ū ��� FlipX�� true ���� ��ȯ�Ͽ� �̹����� �����ȴ�.
        sprite.flipX = target.position.x > rb.position.x;
    }

    protected void OnEnable()   // �ʱ�ȭ
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        hp = maxHp;
    }

    // Spawner�� ��ȯ�� ���͸� �ް� �ʱ�ȭ �����ش�.
    public virtual void Init(SpawnData monData)
    {
        // ���� = ���簡 ũ�� ����! -1 �ؼ� �����ش�.
        if (monData.spriteType >= monsterAnimCon.Length)
        {
            monData.spriteType = monsterAnimCon.Length - 1;
        }

        // ���� ��ü�� < �ִ� ��ü�� = ��ȯ �� ����
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