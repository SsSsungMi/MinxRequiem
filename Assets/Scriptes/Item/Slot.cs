using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UI;

// Scripte Desc:
// �ΰ��ӿ��� ���� �÷��̾��� ���¸� �����ִ� ������ ���� ���� �����մϴ�.
// �װ��� ������ �������� �� �� �ֵ��� �� ������Ʈ�� ���� ��
// ���� ��ũ��Ʈ�� ��� ������ ������ �� �ֵ��� �߽��ϴ�.

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
            itemImage.sprite = item.image;                   // �̹���
            itemLevel.text = "Lv. " + level.ToString();      // ����
            _TYPE = item.type;                               // ������ Ÿ��
            this.gameObject.SetActive(true);                 // ��ü ���� Ȱ��ȭ
        }
    }
}
