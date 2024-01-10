using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// 경험치가 올라 레벨업을 하게 되었을 때 열리는 창을 관장하는 스크립트 입니다.
// 모든 아이템을 자식과 배열로서 가지고 있습니다.
// 모든 아이템을 비활성화 해둔 상태에서 레벨업을 했을 경우
// 랜덥한 아이템을 3가지 선정하고, 자신을 활성화시킨 뒤 TimeScale을 0으로 두어 잠시 시간을 멈추었습니다.
// 1개 이상의 아이템이 최고 레벨에 도달했을 경우에는
// 추가로 준비해 둔 아이템 2종류가 랜덤하게 등장할 수 있도록 했습니다.

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
            randomItem[0] = Random.Range(0, 10);    // int 형은 최대 정수의 -1 값을 반환
            randomItem[1] = Random.Range(0, 10);    // float 형은 최대 실수까지 반환한다.
            randomItem[2] = Random.Range(0, 10);

            // 0번과 1번과 2번은 서로 겹쳐서 나올 수 없다.
            if (randomItem[0] != randomItem[1] && randomItem[1] != randomItem[2]
                && randomItem[0] != randomItem[2])
                break;
        }

        for (int i = 0; i < randomItem.Length; i++)
        {
            Item ranItem = items[randomItem[i]];

            if (ranItem.Level == ranItem.maxItemLevel)
            {   // 만랩일 때는 코인 또는 고기가 나온다.
                items[Random.Range(10, 12)].gameObject.SetActive(true);
            }
            else
                ranItem.gameObject.SetActive(true);
        }
    }
}