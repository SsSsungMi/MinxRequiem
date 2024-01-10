using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Scripte Desc:
// 씬의 이동을 관장하는 스크립트입니다.

public class SceneUIManager : SingleTon<SceneUIManager>
{
    public AudioClip clickSound;

    public void SceneLoader(string sceneName)       // 씬 불러오기
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
