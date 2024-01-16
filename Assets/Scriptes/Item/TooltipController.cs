using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Scripte Desc:
// 두가지의 인터페이스 기능을 이용하여 마우스가 올라갔을 때 창이 활성화되며 해당 아이템의 정보를 확인할 수 있고,
// 마우스를 떼어내면 정보창이 비활성화 되도록 한 툴팁 컨드롤러 스트립트 입니다.

public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Tooltip tooltip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Item item = GetComponent<Item>();

        if (item != null)
        {
            if (item.type == ITEM_TYPE.WEAPON)
            {
                tooltip.SetUpTooltip(item.itemName, item.tooltip, item.ability);
            }
            else if (item.type == ITEM_TYPE.PASSIVE)
            {
                tooltip.SetUpTooltip(item.itemName, item.tooltip, item.ability);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }
}
