using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   // ����ȭ�� �ʱ�ȭ
public class SpawnData
{
    [Header("Monster Data Info")]
    public float spwanTime;          // ��ȯ �ð� 
    public int spriteType;           // ��������Ʈ Ÿ�� -> anim = Enemy���� �ʱ�ȭ
    public int health;               // ü��
    public float atk;                // ���ݷ�
    public float speed;              // �ӵ�
    public int score;                // óġ ����

    [Header("monCount")]
    public int curCount;             // ��ü��
    public int maxCount;             // �ִ� ��ü��

    [Header("Drop Item")]
    public GameObject dropCoin;      // ���Ͱ� ���� ����Ʈ���� ����
    public int maxCoin;              // �ִ� ����
    public GameObject dropExp;      // ���Ͱ� ���� ����Ʈ���� ����ġ
}

// Scripte Desc:
// ������ ������ �����ϴ� ObjectPool ��ũ��Ʈ�� Enemy�� Data�� �迭�� ������ �ֽ��ϴ�.
// �ش� �����ʴ� �÷��̾ �ڽ����� ������ �ֽ��ϴ�.

public class MonstarSpawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnDatas;

    float spawnTimer = 0f;
    float stopTime = 0f;
    public int spawnLevel;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        spawnLevel = Mathf.Min(Mathf.FloorToInt(RecordInfoManager.instance.ReTime / 60f), spawnDatas.Length -1);

        // �ʱ�ȭ �ð� >  ������ ���� . �������� ��ȯ �ð��� ��ȯ������Ʈ�� ������ �ȴ�.
        if (spawnTimer > spawnDatas[spawnLevel].spwanTime)
        {
            spawnTimer = stopTime;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.poolManager.Get(0, spawnDatas[spawnLevel]);

        if(enemy == null)
            return;

        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnDatas[spawnLevel]);
    }
}
