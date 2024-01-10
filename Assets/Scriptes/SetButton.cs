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
// MainCanvas�� ������ ����� ��ư���� �����ϴ� ��ũ��Ʈ �Դϴ�.
// �� ��ư�� �ڽ��� ����� ���� �� �ֵ��� enum�� ���� Type�� �з��� �� onClick �̺�Ʈ�� �־����ϴ�.

public class SetButton : MonoBehaviour
{
    Button btn;
    public BTN_TYPE type;
    CamPointManager cam;
    public AudioClip sfxs;    // ��ư ȿ����
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
                // ���� STAGE01�� �� ������ onClick ���ϰ� �ϰ� ������...��
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
