using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Scripte Desc:
// ���ξ��� ĵ������ �̱������� ����� ������ ����ؼ� ���ŵǸ� �����ֵ��� �߽��ϴ�.
// ���� ���� "Main"�� ��쿡�� ĵ����������Ʈ�� ĵ������ Ȱ��ȭ / �ƴѰ�� ��Ȱ��ȭ ���׽��ϴ�.
// => ĵ���� ������Ʈ ��ü�� ��Ȱ��ȭ�� ���� �ƴϱ� ������ ��Ÿ���� ���� ���޹��� �� �ֽ��ϴ�.

public class MainCanvasManager : SingleTon<MainCanvasManager>
{
    Canvas mainCanvas;
    public RankScore rankManager;
    public UnlockItem unlockItem;
    public CamPointManager camPointManager;

    void Start()
    {
        mainCanvas = GetComponent<Canvas>();
        mainCanvas.sortingOrder = -3;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            mainCanvas.enabled = true;
        }
        else
        {
            mainCanvas.enabled = false;
        }
    }
}