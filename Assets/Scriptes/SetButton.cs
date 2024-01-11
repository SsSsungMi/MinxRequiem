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
                SoundManager.instance.Play(sfxs, SoundManager.instance.transform);  // �� �κ��� �Ʒ� �������� ���� enum�� �ְ�
                break;
            case BTN_TYPE.STAGESELECT:                                             // �Լ� �ϳ� ���� �迭[�������� 2��] �迭�� �´� ���������� ��
                // ���� STAGE01�� �� ������ onClick ���ϰ� �ϰ� ������...��             if�� 0, 1 ���� ���� �ش� ���������� �̵��ϴ� �Լ� ���� �����ϱ�
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
            case BTN_TYPE.CHARACTERSELECT:                                              // ���� ���� ���� �� ����Ʈ�� �Ҹ� ������ ��
                btn.onClick.AddListener(ShowCharacterSelectParticle);                   // �迭 ���� � ĳ�����ΰ� ? ���� GameManager�� �迭�� ��� ° ĳ������ �˷��ֱ�
                SoundManager.instance.Play(sfxs, SoundManager.instance.transform);      // �׸��� �� UI�� ��ġ�� �ִ� ��ƼŬ ������ �ϱ�
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
