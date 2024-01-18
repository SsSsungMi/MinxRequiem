using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

// �÷��̾��� �⺻ �Ӽ�---------------------------------
[System.Serializable]
public class Status
{
    public float speed;         // �̵� �ӵ�
    public float defense;       // ����

    public float hp;            // ���� Hp
    public float recoveryHp;      // Hp ȸ����
    public int maxHp;           // �ִ� Hp
    public int minHp;           // �ּ� Hp

    public int maxProperty = 9;
}
//-----------------------------------------------------

// Scripte Desc:
// �߰� ĳ���͸� ������ �߻� Ŭ�����Դϴ�.
// InputSystem Package�� �̿��� �������� �޽��ϴ�.
// �÷��̾ ���� Status Data�� ������ ������, �÷��̾��� ��ũ��Ʈ���� ���� �Ӽ��� ������ �뵵�Դϴ�.

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
