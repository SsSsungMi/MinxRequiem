using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터 프리펩은 1개만 존재합니다.
// 다만 9종류의 몬스터는 Sprite Animation을 받아서 다른 존재로 등장할 수 있습니다.

// Scripte Desc:
// 몬스터가 죽어서 비활성화 처리 되었다면 ?  => 레벨에 맞는 몬스터로 활성화 시켜줍니다.
// 몬스터가 모두 활성화 된 상태하면 ?       => 레벨에 맞는 몬스터를 PoolMg의 자식으로 생성합니다.

public class PoolManager : MonoBehaviour
{
    public GameObject enemyPrefab;      // 적 프리펩
    [SerializeField] 
    List<GameObject> poolList;   // 생성된 적 프리펩을 담을 공간(풀)

    private void Awake()
    {
        poolList = new List<GameObject>();
        GameManager.instance.poolManager = this;
    }

    public GameObject Get(int index, SpawnData monData)
    {
        GameObject clone = null;

        foreach (GameObject enemy in poolList)
        {
            //  enemy = null 아니다. && 비활성화 된 오브젝트가 있는가?
            if(enemy != null && !enemy.activeSelf)
            {
                clone = enemy;
                clone.SetActive(true);
                break;
            }
        }

        // 몬스터가 전부 활성화 되어있다면?  clone = null이면?
        if (!clone)
        {
            // 생성 가능한 최대 개체수 = 딱히 없어도 될듯??
            // if (poolList.Count > 50)
            // { return clone; }

            if (monData.curCount < monData.maxCount)
            {
                monData.curCount++;
                // 클론 = 새로생성 ( enemy프리펩 , pool메니저의 자식으로 );
                clone = Instantiate(enemyPrefab, transform);
                poolList.Add(clone);    // 생성한걸 pool 배열에 넣어준다.
            }
        }
        return clone;
    }
}