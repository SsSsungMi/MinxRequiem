using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Progress;

// 플레이어의 기본 속성---------------------------------
[System.Serializable]
public class Status
{
    public float speed;         // 이동 속도
    public float defense;       // 방어력

    public float hp;              // 현재 Hp
    public int recoveryHp;      // Hp 회복량
    public int maxHp;           // 최대 Hp
    public int minHp;           // 최소 Hp

    public float mp;              // 현재 Mp
    public int recoveryMp;      // Mp 회복량
    public int maxMp;           // 최대 Mp
    public int minMp;           // 최소 Mp

    // 스킬 생기면 넣기
    //public List<Skill> towerSkills;
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
                GameManager.instance.IsEnd = true;
            }
        }
    }

    public virtual float Mp
    {
        get => status.mp;
        set
        {
            status.mp = value;
            if (status.mp > status.maxMp)
                status.mp = status.maxMp;
            if (status.mp <= status.minMp)
            {
                status.mp = status.minMp;
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
        //hitAniTimer = 0.1f;                             // 코루틴의 맞는 대기시간
        rigid = GetComponent<Rigidbody2D>();
        //hitAniDelay = new WaitForSeconds(hitAniTimer);  // 코루틴에 바로 적용 가능한 맞는 대기시간
        
    }


    protected void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();    // Get으로 ControlType을 가져와 형변환 시킴

        // 아래는 기존 방법
        // inputVec.x = Input.GetAxisRaw("Horizontal");
        // inputVec.y = Input.GetAxisRaw("Vertical");
    }

    protected void FixedMove()
    {
        // InputSystem Package 에서 Normalize 를 넣었기 때문에 빼주어도 된다.
        // Vector2 nextVec = inputVec.normalized * status.speed * Time.fixedDeltaTime;

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
    }
}
