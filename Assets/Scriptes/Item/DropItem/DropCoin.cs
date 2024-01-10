using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// ���Ͱ� �׾��� �� ��ӵǴ� ������ �����ϱ� ���� ��ũ��Ʈ �Դϴ�.
// �÷��̾�� Ʈ���� �浹�� �ϸ� ��ȭ�� �÷��� �� �����˴ϴ�.

public class DropCoin : MonoBehaviour
{
    public Sprite image;
    public int coin;
    public AudioClip getSound;    // ��ư ȿ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            SoundManager.instance.Play(getSound, SoundManager.instance.transform);
            RecordInfoManager.instance.ReCoin += coin;
            Destroy(this.gameObject);
        }
    }
}