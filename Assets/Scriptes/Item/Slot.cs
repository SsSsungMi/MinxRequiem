using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UI;

// Scripte Desc:
// 인게임에는 현재 플레이어의 상태를 보여주는 공간이 왼쪽 위에 존재합니다.
// 그곳에 선택한 아이템이 들어갈 수 있도록 빈 오브젝트를 만들 뒤
// 현재 스크립트를 담아 정보를 셋팅할 수 있도록 했습니다.

public class Slot : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemLevel;

    public Item item;
    public ITEM_TYPE _TYPE;

    public void SetItem(Item adItem, int level)
    {
        item = adItem;
        if (item == null)
        {
            itemImage.sprite = null;
            itemName.text = "";
        }
        else
        {
            itemImage.sprite = item.image;                   // 이미지
            itemLevel.text = "Lv. " + level.ToString();      // 레벨
            _TYPE = item.type;                               // 아이템 타입
            this.gameObject.SetActive(true);                 // 전체 슬롯 활성화
        }
    }
}
