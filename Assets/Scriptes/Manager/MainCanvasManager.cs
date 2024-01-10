using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Scripte Desc:
// 메인씬의 캔버스를 싱글톤으로 만들어 정보가 계속해서 갱신되며 남아있도록 했습니다.
// 현재 씬이 "Main"인 경우에는 캔버스오브젝트의 캔버스를 활성화 / 아닌경우 비활성화 시켰습니다.
// => 캔버스 오브젝트 자체가 비활성화된 것이 아니기 때문에 런타임중 값을 전달받을 수 있습니다.

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