using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Item ���� -----------------------------------------------------
// 
// Weapon --------------------------------------------------------
// Sword         = �ְ�         = �˰�           = �� ������ ũ�� �˰��� �߻��Ѵ�.
// Whip          = ä��         = ��ī�ο� ����   = �ֺ����� ����Ӱ� ä������ �Ѵ�.
// LightningBook = �������� å   = ġ������ ��ġ�� = �ֺ� ���Ϳ��� ������ ���ư� ������.
// Poison        = ���ع� ����   = �� ������      = ���ع��� ���� ���Ϳ��� �������� ������.
// Tornado       = ȸ���� ������ = �ٶ��� ��ó    = ��ī�ο� ȸ�����ٶ��� ������ ȸ���ϸ�, ���Ϳ��� �������� ������.

// Passive --------------------------------------------------------
// Armor      = ����           = �ұ��� ����      = ������ ������ �������� �� �޴´�.
// Health     = ü���� �׸�     = ���� ����       = ü���� ������ ȸ���ȴ�.
// Increase   = �Ŵ�ȭ ���ġ   = Ŀ���� ~        = ���� ������ �����Ѵ�.
// Speed      = �츣�޽��� �Ź� = �������         = ����� �������� �޸��� �ӵ��� ��������.
// Magnetism  = �ڱ��� ����     = ����� �ֹ�      = �� ���� ������ �������� ����Ѵ�.

// Scripte Desc:
// ���� ���� ���ݰ� �нú� �������� ��� �����ϴ� ��ũ��Ʈ �Դϴ�.
// �������� �ڽ��� �Ӽ����� ���� �����̸�,
// �ΰ��ӿ����� ������ â�� �ö� OnClick �Ǿ��� �� �������� ������ Status�� Slot�� �Ű��ݴϴ�.
// Slot�� ���� �������� �� ��쿡�� PlayerEffectDamage�� ���̵��� ����Ʈ�� �������ݴϴ�. 

public enum ITEMNAME_TYPE
{
    SWORD, WHIP, LIGHTNINGBOOK, POISON, TORNADO,
    ARMOR, HEALTH, INCREASE, SPEED, MAGNETISM,
    GoldCoin, HpMeat
}

[System.Serializable]
public class LevelStatus
{
    public float damages;
    public float attackDelay;
}

public class Item : MonoBehaviour
{
    [Header("Main Item Info")]
    public Sprite image;             // icon�̹���
    public string itemName;          // �̸�
    public string tooltip;           // ������ ����
    public string ability;           // �ɷ� == ���ݷ�, �濩��, ü�� ����, ���ǵ� ���� ��..

    [Header("Item Type")]
    public ITEM_TYPE type;           // ���������� ���� �� ���� -> ����â���� �Ѿ �� ����/�нú� Ȯ��
    public ITEMNAME_TYPE name_type;  // ���� ����Ʈ ���� ��
    public int level = 1;            // ������ ����
    public int maxItemLevel = 9;     // �������� �ִ� ����
    public TextMeshProUGUI levelText;
    public AudioClip buttonClip;

    [Header(" Level Damage")]
    [SerializeField]
    private float baseDamage;        // ���� �����۸��� �ٸ�
    public float curDamage = 0;      // ���� �� ������
    public float[] damages;          // ������ �������� ��

    public float[] attackDelay;      // ���� �� ���� ������
    public float coAttackDelay;      // ���� ������ �ڷ�ƾ ���� �뵵��

    public GameObject itemEffect;    // �� ���� �������� ����Ʈ

    private float curScale;           // ������ �ǰ� ����
    public float CurScale
    {
        get => curScale;
        set
        {
            curScale = value;
            if(curScale >= maxScale)
                curScale = maxScale;
        }
    }
    public float maxScale;

    public void Awake()
    {
        curScale = 1f;
        maxScale = 3f;
        if (itemEffect != null)
            itemEffect.transform.localScale = Vector3.one * curScale;

    }

    public int Level
    {
        get => level;
        set
        {
            level = value;
            if (level >= maxItemLevel)
                level = maxItemLevel;

            levelText.text = "Lv." + level.ToString();

            if (type == ITEM_TYPE.WEAPON)
            {
                coAttackDelay = attackDelay[level - 1];
                curDamage     = damages[level - 1];
            }

            if (type == ITEM_TYPE.PASSIVE)
            {
                if (level > 0)
                {
                    switch (name_type)
                    {
                        case ITEMNAME_TYPE.ARMOR:
                            GameManager.instance.player.Defense += 0.2f;    // ��
                            break;
                        case ITEMNAME_TYPE.HEALTH:
                            GameManager.instance.player.recoverHp += 0.1f;  // �ð� �� ������ �ø���
                            break;
                        case ITEMNAME_TYPE.INCREASE:                        // ��
                            if ( curScale >= maxScale)
                            {
                                curScale = maxScale;
                            }
                            curScale += 1;
                            break;
                        case ITEMNAME_TYPE.SPEED:
                            GameManager.instance.player.Speed += 0.4f; // ��
                            break;
                        //case ITEMNAME_TYPE.MAGNETISM:
                        // ȹ�� ���� ����
                        //    break;.
                        
                    }
                }
            }
        }
    }

    public void OnClik()    // Ŭ�� ����
    {
        if(this.itemName == "HpMeat")
        {
            SoundManager.instance.Play(buttonClip, SoundManager.instance.transform);
        }
        SoundManager.instance.Play(buttonClip, SoundManager.instance.transform);
        Level++;
        
        ItemUiManager.instance.AddItem(this);
        if(itemEffect != null)
        {
            if (itemEffect.TryGetComponent<PlayerEffectDamage>(out var effect))
            {
                effect.curItem = this;
            }
        }
        //ItemUiManager.instance.StartCoroutine(UseCo());
        ItemUiManager.instance.UseItem(this);
        RecordInfoManager.instance.levelUp.ShowLevelUpWindow(false);
    }
}