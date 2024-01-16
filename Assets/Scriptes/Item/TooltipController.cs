using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Scripte Desc:
// �ΰ����� �������̽� ����� �̿��Ͽ� ���콺�� �ö��� �� â�� Ȱ��ȭ�Ǹ� �ش� �������� ������ Ȯ���� �� �ְ�,
// ���콺�� ����� ����â�� ��Ȱ��ȭ �ǵ��� �� ���� ����ѷ� ��Ʈ��Ʈ �Դϴ�.

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
