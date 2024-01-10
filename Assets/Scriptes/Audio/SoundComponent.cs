using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// ���� �޴����� ���� Clip�� �ް� ��� �� ��Ȱ��ȭ ���·� ���ư��� ������Ʈ ��ũ��Ʈ �Դϴ�.

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