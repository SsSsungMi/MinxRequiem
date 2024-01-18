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

//// �߰� ������ ----------------------------------------------------------
//// GoldCoin = ��� ����      = �� �׽�Ʈ        = ! ����� ���� �����غ����� ! 1000�� �̳��� �ݾ��� ���� �� �ֽ��ϴ�.
//// HpMeat   = ������ ���    = ڸګ�� ��!       = Hp �� 30��ŭ ȸ���Ѵ�.
//// ---------------------------------------------------------------
//// ��� �������� �� �� �������� ������ �ö� �������� �ִٸ� 2���� Ÿ���� �ϳ��� �����մϴ�.

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
    public float coAttackDelay;      // ���� ������ �ڷ�ƾ ���� �뵵

    public GameObject itemEffect;    // �� ���� �������� ����Ʈ

    private float curScale;          // ���� ������ �ǰ� ����
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
    public float maxScale;           // �� �����۸��� �ٸ��� ����

    public Coroutine curCo;

    public void Awake()
    {
        curScale = 1f;
        maxScale = 9f;
        if (itemEffect != null)
            itemEffect.transform.localScale = Vector3.one * CurScale;
    }

    public int Level
    {
        get => level;
        set
        {
            level = value;
            if (level >= maxItemLevel)
                level = maxItemLevel;


            if (type == ITEM_TYPE.WEAPON)
            {
                coAttackDelay = attackDelay[level - 1];
                curDamage     = damages[level - 1];
            }

            if (type == ITEM_TYPE.PASSIVE || level > 0)
            {
                switch (name_type)
                {
                    case ITEMNAME_TYPE.ARMOR:
                        GameManager.instance.player.Defense += 0.2f;
                        break;
                    case ITEMNAME_TYPE.HEALTH:
                        GameManager.instance.player.status.recoveryHp += 0.1f;
                        break;
                    case ITEMNAME_TYPE.INCREASE:                    
                        if (CurScale >= maxScale)
                            CurScale = maxScale;
                        CurScale += 0.1f;
                        break;
                    case ITEMNAME_TYPE.SPEED:
                        GameManager.instance.player.Speed += 0.4f;
                        break;
                    case ITEMNAME_TYPE.MAGNETISM:
                        GameManager.instance.player.magnetismArea.radius += 0.1f;
                        break;
                    case ITEMNAME_TYPE.GoldCoin:
                        break;
                    case ITEMNAME_TYPE.HpMeat:
                        break;
                }
            }
        }
    }

    public void OnClik()    // Ŭ�� ����
    {
        SoundManager.instance.Play(buttonClip, SoundManager.instance.transform);
        if (this.name_type != ITEMNAME_TYPE.GoldCoin && this.name_type != ITEMNAME_TYPE.HpMeat)
        { 
            Level++;
            levelText.text = "Lv." + level.ToString();
            ItemUiManager.instance.AddItem(this);
            if (itemEffect != null)
            {
                if (itemEffect.TryGetComponent<PlayerEffectDamage>(out var effect))
                {
                    effect.curItem = this;
                }
            }
            ItemUiManager.instance.UseItem(this);
        }
        
        if(this.name_type == ITEMNAME_TYPE.GoldCoin)
        {
            float randomCoin = UnityEngine.Random.Range(0, 1000.0f);
            RecordInfoManager.instance.ReCoin += (int)randomCoin;
            ItemUiManager.instance.UseSpecialItem(this);
            SoundManager.instance.Play(buttonClip, SoundManager.instance.transform);
        }

        if(this.name_type == ITEMNAME_TYPE.HpMeat)
        {
            GameManager.instance.player.Hp += 30;
            ItemUiManager.instance.UseSpecialItem(this);
            SoundManager.instance.Play(buttonClip, SoundManager.instance.transform);
        }

        RecordInfoManager.instance.levelUp.ShowLevelUpWindow(false);
    }
}