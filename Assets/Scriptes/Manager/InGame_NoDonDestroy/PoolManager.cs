using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �������� 1���� �����մϴ�.
// �ٸ� 9������ ���ʹ� Sprite Animation�� �޾Ƽ� �ٸ� ����� ������ �� �ֽ��ϴ�.

// Scripte Desc:
// ���Ͱ� �׾ ��Ȱ��ȭ ó�� �Ǿ��ٸ� ?  => ������ �´� ���ͷ� Ȱ��ȭ �����ݴϴ�.
// ���Ͱ� ��� Ȱ��ȭ �� �����ϸ� ?       => ������ �´� ���͸� PoolMg�� �ڽ����� �����մϴ�.

public class PoolManager : MonoBehaviour
{
    public GameObject enemyPrefab;      // �� ������
    [SerializeField] 
    List<GameObject> poolList;   // ������ �� �������� ���� ����(Ǯ)

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
            //  enemy = null �ƴϴ�. && ��Ȱ��ȭ �� ������Ʈ�� �ִ°�?
            if(enemy != null && !enemy.activeSelf)
            {
                clone = enemy;
                clone.SetActive(true);
                break;
            }
        }

        // ���Ͱ� ���� Ȱ��ȭ �Ǿ��ִٸ�?  clone = null�̸�?
        if (!clone)
        {
            // ���� ������ �ִ� ��ü�� = ���� ��� �ɵ�??
            // if (poolList.Count > 50)
            // { return clone; }

            if (monData.curCount < monData.maxCount)
            {
                monData.curCount++;
                // Ŭ�� = ���λ��� ( enemy������ , pool�޴����� �ڽ����� );
                clone = Instantiate(enemyPrefab, transform);
                poolList.Add(clone);    // �����Ѱ� pool �迭�� �־��ش�.
            }
        }
        return clone;
    }
}