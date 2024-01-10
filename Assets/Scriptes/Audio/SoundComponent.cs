using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// 사운드 메니져를 통해 Clip을 받고 재생 후 비활성화 상태로 돌아가는 오브젝트 스크립트 입니다.

public class SoundComponent : MonoBehaviour
{
    public AudioSource audioSource;

    public void Play(AudioClip clip)
    {
        audioSource.volume = SoundManager.instance.sfxSlider.value;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
    }

    void Update()
    {
        if (audioSource.isPlaying == false)
        {
            SoundManager.instance.ReturnPool(this);
        }
    }
}