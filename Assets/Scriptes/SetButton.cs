using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BTN_TYPE
{
    MAIN,
    CHARACTERCHOICE,
    STAGE01,
    STAGESELECT,
    START,
    END,
    THEEND_TO_MAIN,
    CHARACTERSELECT,
    OVERPOPUPWINDOW,
    CLEARPOPUPWINDOW,
    OUTGAME,
    RETURNGAME
}

// Scripte Desc:
// MainCanvas를 제외한 장소의 버튼들을 관장하는 스크립트 입니다.
// 각 버튼이 자신의 기능을 가질 수 있도록 enum을 통해 Type을 분류한 후 onClick 이벤트를 주었습니다.

public class SetButton : MonoBehaviour
{
    Button btn;
    public BTN_TYPE type;
    CamPointManager cam;
    public AudioClip sfxs;    // 버튼 효과음
    public GameObject choiceParticle;

    void Start()
    {
        cam = MainCanvasManager.instance.camPointManager;
        btn = GetComponent<Button>();
        
        switch(type)
        {
            case BTN_TYPE.MAIN:
                btn.onClick.AddListener(SceneUIManager.instance.CallMain);
                break;
            case BTN_TYPE.CHARACTERCHOICE:
                btn.onClick.AddListener(SceneUIManager.instance.StageSelectMove);
                SoundManager.instance.Play(sfxs, SoundManager.instance.transform);
                break;
            case BTN_TYPE.STAGE01:
                btn.onClick.AddListener(YellowSkin);
                SoundManager.instance.Play(sfxs, SoundManager.instance.transform);  // 이 부분을 아래 스테이지 선택 enum에 넣고
                break;
            case BTN_TYPE.STAGESELECT:                                             // 함수 하나 만들어서 배열[스테이지 2개] 배열에 맞는 스테이지를 얻어서
                // 위의 STAGE01이 안 눌리면 onClick 못하게 하고 싶은데...음             if문 0, 1 번에 따라 해당 스테이지로 이동하는 함수 만들어서 실행하기
                btn.onClick.AddListener(SceneUIManager.instance.Stage01Move);
                SoundManager.instance.Play(sfxs, SoundManager.instance.transform);
                break;
            case BTN_TYPE.START:
                btn.onClick.AddListener(StartGame);
                SoundManager.instance.Play(sfxs, SoundManager.instance.transform);
                break;
            case BTN_TYPE.THEEND_TO_MAIN:
                this.transform.parent.gameObject.SetActive(false);
                btn.onClick.AddListener(SceneUIManager.instance.CallMain);
                SoundManager.instance.Play(sfxs, SoundManager.instance.transform);
                cam.UiCamPointMove(0);
                break;
            case BTN_TYPE.CHARACTERSELECT:                                              // 여기 선택 됐을 때 이펙트랑 소리 나오는 곳
                btn.onClick.AddListener(ShowCharacterSelectParticle);                   // 배열 만들어서 어떤 캐릭터인가 ? 묻고 GameManager의 배열에 몇번 째 캐릭인지 알려주기
                SoundManager.instance.Play(sfxs, SoundManager.instance.transform);      // 그리고 그 UI의 위치에 있는 파티클 열리게 하기
                break;
            case BTN_TYPE.OVERPOPUPWINDOW:
                btn.onClick.AddListener(OverPopUpWindow);
                SoundManager.instance.Play(sfxs, SoundManager.instance.transform);
                break;
            case BTN_TYPE.CLEARPOPUPWINDOW:
                btn.onClick.AddListener(ClearPopUpWindow);
                SoundManager.instance.Play(sfxs, SoundManager.instance.transform);
                break;
            case BTN_TYPE.OUTGAME:
                btn.onClick.AddListener(SceneUIManager.instance.CallMain);
                SoundManager.instance.Play(sfxs, SoundManager.instance.transform);
                btn.onClick.AddListener(OutGame);
                GameManager.instance.IsStart = false;
                break;
            case BTN_TYPE.RETURNGAME:
                btn.onClick.AddListener(ReturnGame);
                SoundManager.instance.Play(sfxs, SoundManager.instance.transform);
                break;
        }
    }

    public void OutGame()
    {
        cam.UiCamPointMove(0);
        GameManager.instance.IsLive = true;
    }

    public void ReturnGame()
    {
        RecordInfoManager.instance.outScene.gameObject.SetActive(false);
        GameManager.instance.IsLive = true;
    }

    public void YellowSkin()
    {
        if(type == BTN_TYPE.STAGE01)
        {
            GameObject nextBtn = GameObject.Find("Canvas").transform.Find("Start_B").gameObject;
            btn.GetComponent<Image>().color = Color.yellow;
            
            nextBtn.GetComponent<Image>().color = Color.yellow;
        }
    }

    public void StartGame()
    {
        GameManager.instance.IsStart = true;
        btn.gameObject.SetActive(false);
    }

    public void ShowCharacterSelectParticle()
    {
        this.choiceParticle.SetActive(true);
        StartCoroutine(GreenColorCo());
    }

    IEnumerator GreenColorCo()
    {
        btn.GetComponent<Image>().color = Color.green;
        yield return new WaitForSeconds(10);
        btn.GetComponent<Image>().color = Color.white;
    }

    public void OverPopUpWindow()
    {
        RecordInfoManager.instance.overPopUpWindow.SetActive(false);
        GameManager.instance.IsLive = true;
        GameManager.instance.IsEnd = true;
    }
    public void ClearPopUpWindow()
    {
        RecordInfoManager.instance.clearPopUpWindow.SetActive(false);
        GameManager.instance.IsLive = true;
        GameManager.instance.IsClear = true;
    }
}
