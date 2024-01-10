using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// 메인 씬에서 버튼 조작에 의해 카메라를 해당 포인트로 이동시키기 위한 스크립트 입니다.

public class CamPointManager : MonoBehaviour
{
    public GameObject[] camPoints;
    public AudioClip inClip;

    void Start()
    {
       transform.position = camPoints[0].transform.position;
    }

    public void UiCamPointMove(int index)
    {
        SoundManager.instance.Play(inClip, SoundManager.instance.transform);
        transform.position = camPoints[index].transform.position;
    }
}
