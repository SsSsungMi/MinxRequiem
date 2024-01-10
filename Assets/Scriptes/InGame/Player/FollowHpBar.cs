using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// UI 창에 있는 Hp 이미지가 플레이어의 위치에 따라 함께 이동할 수 있도록 카메라를 사용한 스크립트 입니다.

public class FollowHpBar : MonoBehaviour
{
    RectTransform hpPos;

    void Awake()
    {
        hpPos = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        // 따라오는 hp = 메인 카메라 . 월드 스크린 기준 (플레이어의 위치)
        hpPos.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}