using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Scripte Desc:
// ������ �Ҹ��� ObjectPool �������� ����ϴ� ����޴��� ��ũ��Ʈ �Դϴ�.
// ��� �Ҹ��� �迭�� ������ ������, �� ���� �̵��� �� ������ְ� �ֽ��ϴ�.
// ȿ������ �� ������Ʈ�� ������ ������, Play�Լ��� ������ �� soundPool������Ʈ��
// Clip�� ��� �� ��Ȱ��ȭ ���·� ���ư��ϴ�.

public class SoundManager : SingleTon<SoundManager>
{
    public SoundComponent soundPrefab;
    public Queue<SoundComponent> soundPool = new Queue<SoundComponent>();
    public AudioClip[] bgms;    // �������
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
