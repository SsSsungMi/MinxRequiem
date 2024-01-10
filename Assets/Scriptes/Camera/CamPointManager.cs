using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// ���� ������ ��ư ���ۿ� ���� ī�޶� �ش� ����Ʈ�� �̵���Ű�� ���� ��ũ��Ʈ �Դϴ�.

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
