using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Scripte Desc:
// 아이템 도감의 lockImage를 없애고 ToolTip에 필요한 정보를 받아오는 스크립트 입니다.
// 해당 스크립트는 MainCanvas의 아이템 목록 하위의 Content 오브젝트가 가지고 있으며,
// Contente는 잠겨진 아이템을 자식 및 리스트로 가지고 있습니다.

public class UnlockItem : MonoBehaviour
{
    public List<GameObject> baseItemList;   // Content -> 하위에 있는 아이템 정보들
    int maxItemList = 10;

    private void Awake()
    {
        baseItemList = new List<GameObject>();
    }

    private void Start()
    {
        for(int i = 0; i < maxItemList; i++)
        {
            baseItemList.Add(this.transform.GetChild(i).gameObject);
        }
    }

    public void UnLock(Item findItem = null)
    {
        for (int i = 0; i < baseItemList.Count; i++)
        {
            if (findItem.itemName == baseItemList[i].GetComponent<Item>().itemName)
            {
                baseItemList[i].GetComponent<Image>().raycastTarget = true;
                baseItemList[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = findItem.image;
                baseItemList[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
