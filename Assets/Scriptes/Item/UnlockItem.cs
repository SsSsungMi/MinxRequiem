using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Scripte Desc:
// ������ ������ lockImage�� ���ְ� ToolTip�� �ʿ��� ������ �޾ƿ��� ��ũ��Ʈ �Դϴ�.
// �ش� ��ũ��Ʈ�� MainCanvas�� ������ ��� ������ Content ������Ʈ�� ������ ������,
// Contente�� ����� �������� �ڽ� �� ����Ʈ�� ������ �ֽ��ϴ�.

public class UnlockItem : MonoBehaviour
{
    public List<GameObject> baseItemList;   // Content -> ������ �ִ� ������ ������
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
