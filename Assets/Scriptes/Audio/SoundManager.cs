using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Scripte Desc:
// 게임의 소리를 ObjectPool 형식으로 사용하는 사운드메니져 스크립트 입니다.
// 배경 소리는 배열로 가지고 있으며, 각 씬을 이동할 때 재생해주고 있습니다.
// 효과음은 각 오브젝트가 가지고 있으며, Play함수로 실행할 때 soundPool오브젝트가
// Clip을 재생 후 비활성화 상태로 돌아갑니다.

public class SoundManager : SingleTon<SoundManager>
{
    public SoundComponent soundPrefab;
    public Queue<SoundComponent> soundPool = new Queue<SoundComponent>();
    public AudioClip[] bgms;    // 배경음악
    public AudioSource audioSource;

    [Header("Volume")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    public new void Awake()
    {
        base.Awake();
        Init();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgms[0];
        audioSource.Play();
    }

    public void Init()
    {
        for (int i = 0; i < 100; i++)
        {
            SoundComponent sound = Instantiate(soundPrefab, transform);
            sound.gameObject.SetActive(false);
            soundPool.Enqueue(sound);
        }
    }

    private void Update()
    {
        audioSource.volume = bgmSlider.value;
    }

    public SoundComponent Pop()
    {
        SoundComponent sm = soundPool.Dequeue();
        sm.gameObject.SetActive(true);
        return sm.GetComponent<SoundComponent>();
    }

    public void ReturnPool(SoundComponent returnObj)
    {
        returnObj.gameObject.SetActive(false);
        returnObj.transform.SetParent(transform);
        soundPool.Enqueue(returnObj);
    }

    public void Play(AudioClip clip, Transform target = null)
    {
        SoundComponent sound =  Pop();
        sound.transform.parent = target;
        sound.Play(clip);
    }
}
