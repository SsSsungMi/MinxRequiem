using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// 인게임 씬에서 Cinemachine의 Vriteal Camera가 플레이어를 쫓으며 화면에 비춰질 수 있도록 한 스크립트 입니다.

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
