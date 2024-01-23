using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// �÷��̾ ���� �������� �Ծ��� �� �޾ƿ� ����Ʈ ������ �̿��Ͽ� �������� �ִ� ��ũ��Ʈ �Դϴ�.
// ��ƼŬ�� �ݶ��̴��� ����� �浹ó���� �浹���� �� IHitable �������̽��� ������ �ְ�, �÷��̾ �ƴ� ���
// �������� ������ �Լ��� ����˴ϴ�.

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
