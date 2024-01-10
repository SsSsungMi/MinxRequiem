using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// Scripte Desc:
// 스테이지 1의 최종 보스를 관리하는 스크립트 입니다.
// Enemy의 자식으로 속성을 가진 상태이나 Enemy스크립트의 Init()함수에서 스크립트를 바꿔주면서
// 활성화 시 가지고 와야하는 데이터들을 Ovrride로 다시 초기화 해주었습니다.
// 보스몬스터가 2마리 죽은 경우 게임이 끝나게 되며, 클리어창이 활성화 됩니다.

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
