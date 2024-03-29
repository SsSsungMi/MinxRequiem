using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

// 아래처럼 클래스를 만들고 []을 뽑아서 switch 안에 있는 정보를 담고 각 씬마다 사용하는건?
[Serializable]
public class TmepClass      
{
    UnityEvent unityEvent;
    //SoundManager.instance.Play(sfxs, SoundManager.instance.transform);
}

// Scripte Desc:
// MainCanvas를 제외한 장소의 버튼들을 관장하는 스크립트 입니다.
// 각 버튼이 자신의 기능을 가질 수 있도록 enum을 통해 Type을 분류한 후 onClick 이벤트를 주었습니다.

public class SetButton : MonoBehaviour
{
    TmepClass ee;
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
                SoundManager.instance.Play(sfxs, SoundManager.instance.transform);
                break;
            case BTN_TYPE.STAGESELECT:
                // 위의 STAGE01이 안 눌리면 onClick 못하게 하고 싶은데...음
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
            case BTN_TYPE.CHARACTERSELECT:
                btn.onClick.AddListener(ShowCharacterSelectParticle);
                SoundManager.instance.Play(sfxs, SoundManager.instance.transform);
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
        GameManager.instance.IsStart = false;
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
        GameManager.instance.IsEnd = false;

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
