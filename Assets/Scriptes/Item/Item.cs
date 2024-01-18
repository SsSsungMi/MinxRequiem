using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Item 정보 -----------------------------------------------------
// 
// Weapon --------------------------------------------------------
// Sword         = 쌍검         = 검격           = 양 옆으로 크게 검격을 발사한다.
// Whip          = 채찍         = 날카로운 상흔   = 주변으로 날까롭게 채찍질을 한다.
// LightningBook = 번개맞은 책   = 치리리볼 펼치기 = 주변 몬스터에게 번개가 날아가 꽂힌다.
// Poison        = 독극물 포션   = 독 웅덩이      = 독극물이 퍼져 몬스터에게 데미지를 입힌다.
// Tornado       = 회오리 지팡이 = 바람의 상처    = 날카로운 회오리바람이 주위를 회전하며, 몬스터에게 데미지를 입힌다.

// Passive --------------------------------------------------------
// Armor      = 갑옷           = 불굴의 의지      = 방어력이 높아져 데미지를 덜 받는다.
// Health     = 체력의 그릇     = 피의 갈망       = 체력이 서서히 회복된다.
// Increase   = 거대화 브로치   = 커져라 ~        = 공격 범위가 증가한다.
// Speed      = 헤르메스의 신발 = 쾌속질주         = 기분이 좋아져서 달리는 속도가 빨라진다.
// Magnetism  = 자기장 보석     = 욕망의 주문      = 더 넓은 범위의 아이템을 흡수한다.

//// 추가 아이템 ----------------------------------------------------------
//// GoldCoin = 골드 코인      = 운 테스트        = ! 당신의 운을 실험해보세요 ! 1000원 이내의 금액을 받을 수 있습니다.
//// HpMeat   = 묵직한 고기    = 美味한 맛!       = Hp 를 30만큼 회복한다.
//// ---------------------------------------------------------------
//// 모든 아이템이 찬 뒤 레벨업이 끝까지 올라간 아이템이 있다면 2가지 타입중 하나가 등장합니다.

// Scripte Desc:
// 게임 내의 공격과 패시브 아이템을 모두 관장하는 스크립트 입니다.
// 아이템은 자신의 속성들을 가진 상태이며,
// 인게임에서는 레벨업 창에 올라가 OnClick 되었을 때 아이템의 정보를 Status의 Slot로 옮겨줍니다.
// Slot에 공격 아이템이 들어간 경우에는 PlayerEffectDamage에 아이뎀의 이펙트를 전달해줍니다. 

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
    public Sprite image;             // icon이미지
    public string itemName;          // 이름
    public string tooltip;           // 아이템 설명
    public string ability;           // 능력 == 공격력, 방여력, 체력 증가, 스피드 증가 등..

    [Header("Item Type")]
    public ITEM_TYPE type;           // 프리펩으로 만들 때 지정 -> 상태창으로 넘어갈 때 무기/패시브 확인
    public ITEMNAME_TYPE name_type;  // 공격 이펙트 구분 용
    public int level = 1;            // 아이템 레벨
    public int maxItemLevel = 9;     // 아이템의 최대 레벨
    public TextMeshProUGUI levelText;
    public AudioClip buttonClip;

    [Header(" Level Damage")]
    [SerializeField]
    private float baseDamage;        // 각각 아이템마다 다름
    public float curDamage = 0;      // 레벨 별 데미지
    public float[] damages;          // 증가될 데미지의 값

    public float[] attackDelay;      // 레벨 별 어택 딜레이
    public float coAttackDelay;      // 어택 딜레이 코루틴 적용 용도

    public GameObject itemEffect;    // 각 무기 아이템의 이펙트

    private float curScale;          // 현재 무기의 피격 범위
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
    public float maxScale;           // 각 아이템마다 다르게 지정

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

    public void OnClik()    // 클릭 받음
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