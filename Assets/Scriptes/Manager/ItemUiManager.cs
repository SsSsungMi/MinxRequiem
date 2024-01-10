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
// �ΰ��� ���� �������� �����ϰ�, �����ϴ� ��ũ��Ʈ �Դϴ�.
// �ΰ��ӿ��� ������UIâ�� �������� OnClick �Ǿ��ٸ� ?
// �������� TYPE�� ���� Statusâ�� ������ ����(�̹���, ����)�� �Ѱ��ְ�,
// MainCanvas�� ������ ���� �� �������� ����̹����� ��Ȱ��ȭ �����ݴϴ�.
// ���� �� �������� ���� �������̶�� AttackDelay �ð��� ���� �ڷ�ƾ���� ������ �����ϵ��� �մϴ�.
// (���� �������� �ڽ��� ���� ����Ʈ�� ������ ������, ����Ʈ�� PlayerEffectDamage ��ũ��Ʈ�� ������ �ֽ��ϴ�.

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
    }

    // ������ ������ ��� �̹����� ��Ȱ��ȭ ��Ų��.
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
        //���� �������� �߰����� �κ�//
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
        // ũ�� ���� �������� ���� ���
        if (adItem.name == "Increase")
        {
            for (int j = 0; j < allItems.items.Length; j++)
            {
                allItems.items[j].CurScale = adItem.CurScale;
                // ����Ʈ ������ ���� (���������� ����)
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
