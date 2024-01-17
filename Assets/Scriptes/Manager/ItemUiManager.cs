using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Spine.Unity.Examples.MixAndMatchSkinsExample;

public enum ITEM_TYPE
{
    WEAPON,
    PASSIVE
}

// Scripte Desc:
// 인게임 내의 아이템을 전달하고, 관장하는 스크립트 입니다.
// 인게임에서 레벨업UI창의 아이템이 OnClick 되었다면 ?
// 아이템의 TYPE에 따라 Status창에 아이템 정보(이미지, 레벨)를 넘겨주고,
// MainCanvas의 아이템 도감 속 아이템의 잠금이미지를 비활성화 시켜줍니다.
// 또한 그 아이템이 공격 아이템이라면 AttackDelay 시간에 따라 코루틴으로 공격을 실행하도록 합니다.
// (공격 아이템은 자신의 공격 이펙트를 가지고 있으며, 이펙트는 PlayerEffectDamage 스크립트를 가지고 있습니다.

public class ItemUiManager : MonoBehaviour
{
    public static ItemUiManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            Init();
        }
    }

    public Slot[] weaponSlots = new Slot[5];
    public Slot[] passiveSlots = new Slot[5];
    public LevelUp allItems;
    public GameObject curStatus;
    private bool statusPopUp = true;

    bool isFind = false;
    UnlockItem unlock;

    public bool IsFind
    {
        get => isFind;
        set
        {
            isFind = value;
            if (isFind)
            {
                unlock = MainCanvasManager.instance.unlockItem;
            }
        }
    }

    float attackDelay;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            weaponSlots[i].item = null;
        }

        for (int i = 0; i < passiveSlots.Length; i++)
        {
            passiveSlots[i].item = null;
        }
        curStatus.SetActive(true);
    }

    public void Update()
    {
        if(GameManager.instance.IsStart)
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                if(statusPopUp)
                {
                    curStatus.SetActive(false);
                    statusPopUp = false;
                }
                else
                {
                    curStatus.SetActive(true);
                    statusPopUp = true;
                }
            }
        }
    }

    // 아이템 도감의 잠금 이미지를 비활성화 시킨다.
    public void UseSpecialItem(Item item)
    {
        IsFind = true;
        unlock.UnLock(item);
    }

    public void UseItem(Item item)
    {
        if(item.curCo != null)
        {
            StopCoroutine(item.curCo);
        }
        attackDelay = item.coAttackDelay;
        item.curCo = StartCoroutine(ItemCo(item, attackDelay));
    }

    public IEnumerator ItemCo(Item item, float delay)
    {
        while (true)
        {
            CustomEffect(item);
            yield return new WaitForSeconds(delay);
        }
    }

    public void CustomEffect(Item item)
    {
        if (item.type == ITEM_TYPE.WEAPON)
        {
            item.itemEffect.SetActive(true);
            item.itemEffect.transform.position = GameManager.instance.player.transform.position;
        }
    }

    public void FirstCustomAddItem(Item adItem,int i)
    {
        //공격 아이템의 추가적인 부분//
        if(adItem.itemEffect == null)
        {
            return;
        }
        adItem.itemEffect = Instantiate(weaponSlots[i].item.itemEffect);
        adItem.itemEffect.transform.SetParent(GameManager.instance.player.transform);
        
        for (int j = 0; j < allItems.items.Length; j++)
        {
            allItems.items[j].CurScale = allItems.items[8].CurScale;
        }
    }
    public void UnFirstCustomAddItem(Item adItem)
    {
        // 크기 증가 아이템이 들어온 경우
        if (adItem.name == "Increase")
        {
            for (int j = 0; j < allItems.items.Length; j++)
            {
                allItems.items[j].CurScale = adItem.CurScale;
                // 이펙트 사이즈 증가 (레벨업마다 증가)
                if (allItems.items[j].itemEffect != null)
                {
                    allItems.items[j].itemEffect.transform.localScale = allItems.items[j].CurScale * Vector3.one;
                }
            }
        }
    }
    public void AddItem(Item adItem)
    {
        Slot[] inputItemSlots = (adItem.type == ITEM_TYPE.PASSIVE) ? passiveSlots : weaponSlots;
     
        for (int i = 0; i < inputItemSlots.Length; i++)
        {
            if(inputItemSlots[i].item == null)
            {
                inputItemSlots[i].SetItem(adItem, adItem.Level);
                FirstCustomAddItem(adItem, i);
            }
            else if (inputItemSlots[i].item.itemName == adItem.itemName)
            {
                inputItemSlots[i].SetItem(adItem, adItem.Level);
            }
            else
            {
                continue;
            }

            if (inputItemSlots[i].item != null)
            {
                IsFind = true;
                unlock.UnLock(adItem);
                UnFirstCustomAddItem(adItem);
                return;
            }
        }
    }
}
