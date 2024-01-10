using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IHitable
{
    void Hit(float damage);
}

public interface IDeadable
{
    void Dead();
}

// Scripte Desc:
// �÷��̾� Ŭ���� �Դϴ�.
// ���� �������� ���� �����Ӱ�, �������� �Ծ��� ��, �׾��� �� ����մϴ�.
// �̵� �� ����Ǵ� �ִϸ��̼��� Spine �ִϸ��̼��� ����ؾ� �ؼ� SkeletonAnimation�� �̿��߽��ϴ�.
// �÷��̾��� Hp�� 0���� ����� ��쿡�� Over Clip�� �����ϸ� GameOver â�� Ȱ��ȭ �˴ϴ�.

public class Player : Character, IHitable, IDeadable
{
    SkeletonAnimation bone;
    GameObject hpBar;
    Image hpBarSprite;

    public float recoverHp = 0.1f;
    public AudioClip deadSound;    // ��ư ȿ����
    public GameObject hitEffect;

    private new void Start()
    {
        base.Start();
        bone = GetComponent<SkeletonAnimation>();
        bone.AnimationName = "Idle";
        hpBar = RecordInfoManager.instance.hpBarObj;
        hpBarSprite = hpBar.GetComponent<Image>();
    }

    public override float Hp
    {
        get => base.Hp;
    
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

            float curHp = status.hp;
            float maxHp = status.maxHp;
            // Mathf.Min = A �� B �߿��� �� ���� ���� ��ȯ�Ѵ�.
            hpBarSprite.fillAmount = curHp / maxHp;
        }
    }

    void Update()
    {
        if(GameManager.instance.IsEnd == false)
            Hp += recoverHp * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.IsLive)
            return;

        FixedMove();

        if (inputVec.x == 0f && inputVec.y == 0f)
        {
            bone.AnimationName = "Idle";
        }
        else
            bone.AnimationName = "Run";
    }

    public override void Hit(float damage)
    {
        damage = damage * ( 1 / (1+Defense));
        Hp -= damage;
        hitEffect.SetActive(true);
    }

    public override void Dead()
    {
        base.Dead();
        GameManager.instance.IsStart = false;   // �ð� ������ ����
        SoundManager.instance.Play(deadSound, SoundManager.instance.transform);
        GameManager.instance.IsEnd = true;
    }
}