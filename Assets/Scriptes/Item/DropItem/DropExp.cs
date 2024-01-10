using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// ���Ͱ� �׾��� �� ��ӵǴ� ����ġ�� �����ϱ� ���� ��ũ��Ʈ �Դϴ�.
// �÷��̾�� Ʈ���� �浹�� �߻��ϸ� ����ġ�� �����ϸ� ������Ʈ�� �ı��մϴ�.

public class DropExp : MonoBehaviour
{
    public Sprite image;
    public int exp;
    public AudioClip getSound;    // ��ư ȿ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            SoundManager.instance.Play(getSound, SoundManager.instance.transform);
            GameManager.instance.Exp += exp;
            Destroy(this.gameObject);
        }
    }
}
