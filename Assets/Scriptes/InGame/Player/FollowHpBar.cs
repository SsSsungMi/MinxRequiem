using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// UI â�� �ִ� Hp �̹����� �÷��̾��� ��ġ�� ���� �Բ� �̵��� �� �ֵ��� ī�޶� ����� ��ũ��Ʈ �Դϴ�.

public class FollowHpBar : MonoBehaviour
{
    RectTransform hpPos;

    void Awake()
    {
        hpPos = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        // ������� hp = ���� ī�޶� . ���� ��ũ�� ���� (�÷��̾��� ��ġ)
        hpPos.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}