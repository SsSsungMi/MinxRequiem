using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// ����ġ�� �ö� �������� �ϰ� �Ǿ��� �� ������ â�� �����ϴ� ��ũ��Ʈ �Դϴ�.
// ��� �������� �ڽİ� �迭�μ� ������ �ֽ��ϴ�.
// ��� �������� ��Ȱ��ȭ �ص� ���¿��� �������� ���� ���
// ������ �������� 3���� �����ϰ�, �ڽ��� Ȱ��ȭ��Ų �� TimeScale�� 0���� �ξ� ��� �ð��� ���߾����ϴ�.
// 1�� �̻��� �������� �ְ� ������ �������� ��쿡��
// �߰��� �غ��� �� ������ 2������ �����ϰ� ������ �� �ֵ��� �߽��ϴ�.

public class LevelUp : MonoBehaviour
{
    public Item[] items;
    //public SpecialItem[] specialitems;

    public void Start()
    {
        for(int i=0; i<items.Length; i++)
        { items[i].gameObject.SetActive(false); }
    }

    public void ShowLevelUpWindow(bool isShow)
    {
        if (isShow)
        {
            gameObject.SetActive(true);
            RandomItem();
            GameManager.instance.IsLive = false;
        }
        if (!isShow)
        {
            foreach (Item item in items)
            {
                item.gameObject.SetActive(false);
            }
            gameObject.SetActive(false);
            GameManager.instance.IsLive = true;
        }
    }

    public void RandomItem()
    {
        int[] randomItem = new int[3];
        while (true)
        {
            randomItem[0] = Random.Range(0, 10);    // int ���� �ִ� ������ -1 ���� ��ȯ
            randomItem[1] = Random.Range(0, 10);    // float ���� �ִ� �Ǽ����� ��ȯ�Ѵ�.
            randomItem[2] = Random.Range(0, 10);

            // 0���� 1���� 2���� ���� ���ļ� ���� �� ����.
            if (randomItem[0] != randomItem[1] && randomItem[1] != randomItem[2]
                && randomItem[0] != randomItem[2])
                break;
        }

        for (int i = 0; i < randomItem.Length; i++)
        {
            Item ranItem = items[randomItem[i]];

            if (ranItem.Level == ranItem.maxItemLevel)
            {   // ������ ���� ���� �Ǵ� ��Ⱑ ���´�.
                items[Random.Range(10, 12)].gameObject.SetActive(true);
            }
            else
                ranItem.gameObject.SetActive(true);
        }
    }
}