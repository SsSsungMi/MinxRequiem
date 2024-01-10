using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// 몬스터가 죽었을 때 드롭되는 경험치를 관리하기 위한 스크립트 입니다.
// 플레이어와 트리거 충돌이 발생하면 경험치가 증가하며 오브젝트를 파괴합니다.

public class DropExp : MonoBehaviour
{
    public Sprite image;
    public int exp;
    public AudioClip getSound;    // 버튼 효과음

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
