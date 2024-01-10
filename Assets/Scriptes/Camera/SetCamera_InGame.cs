using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// �ΰ��� ������ Cinemachine�� Vriteal Camera�� �÷��̾ ������ ȭ�鿡 ������ �� �ֵ��� �� ��ũ��Ʈ �Դϴ�.

public class SetCamera_InGame : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    private void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }
    private void Update()
    {
        if ((GameManager.instance.IsStart))
        {
            cam.Follow = GameManager.instance.player.transform;
        }
    }
}
