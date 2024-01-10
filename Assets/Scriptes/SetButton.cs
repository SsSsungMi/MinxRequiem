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
    CHARACTERSELECT
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
                
                break;
        }
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
        choiceParticle.SetActive(true);
    }
}
