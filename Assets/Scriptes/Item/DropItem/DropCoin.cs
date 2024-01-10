using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// 몬스터가 죽었을 때 드롭되는 코인을 관리하기 위한 스크립트 입니다.
// 플레이어와 트리거 충돌을 하면 재화를 올려준 뒤 삭제됩니다.

public class DropCoin : MonoBehaviour
{
    public Sprite image;
    public int coin;
    public AudioClip getSound;    // 버튼 효과음

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