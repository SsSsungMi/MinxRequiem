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
// 플레이어 클래스 입니다.
// 방향 조작으로 인한 움직임과, 데미지를 입었을 때, 죽었을 때 기능합니다.
// 이동 시 변경되는 애니메이션은 Spine 애니메이션을 사용해야 해서 SkeletonAnimation을 이용했습니다.
// 플레이어의 Hp가 0으로 사망한 경우에는 Over Clip을 실행하며 GameOver 창이 활성화 됩니다.

public class Player : Character, IHitable, IDeadable
{
    SkeletonAnimation bone;
    GameObject hpBar;
    Image hpBarSprite;

    public float recoverHp = 0.1f;
    public AudioClip deadSound;    // 버튼 효과음
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
            // Mathf.Min = A 와 B 중에서 더 작은 값을 반환한다.
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
        GameManager.instance.IsStart = false;   // 시간 증가를 멈춤
        SoundManager.instance.Play(deadSound, SoundManager.instance.transform);
        GameManager.instance.IsEnd = true;
    }
}