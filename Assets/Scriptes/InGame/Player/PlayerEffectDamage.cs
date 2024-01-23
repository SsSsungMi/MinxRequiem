using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// 플레이어가 공격 아이템을 먹었을 때 받아온 이펙트 정보를 이용하여 데미지를 주는 스크립트 입니다.
// 파티클의 콜라이더를 사용한 충돌처리로 충돌했을 때 IHitable 인터페이스를 가지고 있고, 플레이어가 아닌 경우
// 데미지를 입히는 함수가 실행됩니다.

public class PlayerEffectDamage : MonoBehaviour
{
    public Item curItem;

    public void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<IHitable>(out var mon)
            && !other.GetComponent<Player>())
        {
            mon.Hit(curItem.curDamage);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<IHitable>(out var mon))
        {
            mon.Hit(curItem.curDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IHitable>(out var mon))
        {
            mon.Hit(curItem.curDamage);
        }
    }
}
