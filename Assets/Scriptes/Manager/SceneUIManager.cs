using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Scripte Desc:
// ���� �̵��� �����ϴ� ��ũ��Ʈ�Դϴ�.

public class SceneUIManager : SingleTon<SceneUIManager>
{
    public AudioClip clickSound;

    public void SceneLoader(string sceneName)       // �� �ҷ�����
    {
        SceneManager.LoadScene(sceneName);
    }

    public void CallMain()
    {
        SceneLoader("Main");
    }

    public void StageSelectMove()
    {
        SceneLoader("StageSelect");
    }

    public void Stage01Move()
    {
        SoundManager.instance.audioSource.clip = SoundManager.instance.bgms[1];
        SoundManager.instance.audioSource.Play();
        SceneLoader("Stage01");
    }
}
