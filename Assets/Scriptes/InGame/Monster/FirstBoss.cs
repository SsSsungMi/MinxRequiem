using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// Scripte Desc:
// �������� 1�� ���� ������ �����ϴ� ��ũ��Ʈ �Դϴ�.
// Enemy�� �ڽ����� �Ӽ��� ���� �����̳� Enemy��ũ��Ʈ�� Init()�Լ����� ��ũ��Ʈ�� �ٲ��ָ鼭
// Ȱ��ȭ �� ������ �;��ϴ� �����͵��� Ovrride�� �ٽ� �ʱ�ȭ ���־����ϴ�.
// �������Ͱ� 2���� ���� ��� ������ ������ �Ǹ�, Ŭ����â�� Ȱ��ȭ �˴ϴ�.

public class FirstBoss : Enemy, IHitable, IDeadable
{
    static FirstBoss instance;
    public override float Hp
    { 
        get => base.Hp; 
        set
        {
            hp = value;
            if(hp <= minHp)
            {
                Dead();
            }
        }
    }

    private void Start()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public override void Init(SpawnData monData)
    {
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
        if (collision.collider.TryGetComponent<IHitable>(out var player))
        {
            player.Hit(atk);
        }
    }

    public override void Hit(float damage)
    {
        Hp -= damage / 2;
    }

    public override void Dead()
    {
        RecordInfoManager.instance.ReScore += score;
        RecordInfoManager.instance.ReMonsterKillCount++;
        SoundManager.instance.Play(monsterHitClip, SoundManager.instance.transform);
        this.gameObject.SetActive(false);

        RecordInfoManager.instance.clearPopUpWindow.SetActive(true);
        GameManager.instance.IsLive = false;
        SoundManager.instance.Play(ClearClip, SoundManager.instance.transform);
    }
}
