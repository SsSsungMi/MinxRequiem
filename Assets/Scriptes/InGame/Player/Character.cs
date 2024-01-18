using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

// 플레이어의 기본 속성---------------------------------
[System.Serializable]
public class Status
{
    public float speed;         // 이동 속도
    public float defense;       // 방어력

    public float hp;            // 현재 Hp
    public float recoveryHp;      // Hp 회복량
    public int maxHp;           // 최대 Hp
    public int minHp;           // 최소 Hp

    public int maxProperty = 9;
}
//-----------------------------------------------------

// Scripte Desc:
// 추가 캐릭터를 상정한 추상 클래스입니다.
// InputSystem Package를 이용한 움직임을 받습니다.
// 플레이어를 위한 Status Data를 가지고 있으며, 플레이어의 스크립트에서 따로 속성을 정해줄 용도입니다.

public abstract class Character : MonoBehaviour, IHitable, IDeadable
{
    public Status status;
    protected Animator animator;
    public Rigidbody2D rigid;
    public Vector2 inputVec;

    public virtual float Hp
    {
        get => status.hp;
        set
        {
            status.hp = value;
            if (status.hp > status.maxHp)
                status.hp = status.maxHp;
            if (status.hp <= status.minHp)
            {
                status.hp = status.minHp;
                Dead();
            }
        }
    }
    public virtual float Defense
    {
        get => status.defense;
        set
        {
            status.defense = value;
            if(status.defense >= (float)status.maxProperty)
            { status.defense = (float)status.maxProperty; }
        }
    }
    public virtual float Speed
    {
        get => status.speed;
        set
        {
            status.speed = value;
            if (status.speed >= (float)status.maxProperty)
            { status.speed = (float)status.maxProperty; }
        }
    }

    public void Start()
    {
        status.hp = status.maxHp;
        rigid = GetComponent<Rigidbody2D>();
    }

    protected void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    protected void FixedMove()
    {
        Vector2 nextVec = inputVec * status.speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    protected void LateUpdate()
    {
        if (inputVec.x < 0)
        {
            Quaternion turn = transform.rotation;
            turn.y = -180;
            this.transform.rotation = turn;
        }
        else if (inputVec.x > 0)
        {
            Quaternion turn = transform.rotation;
            turn.y = 0;
            this.transform.rotation = turn;
        }
    }

    public virtual void Hit(float damage)
    {
        Hp -= damage;
    }
    public virtual void Dead()
    {
        status.speed = 0;
        GameManager.instance.IsEnd = true;
    }
}
