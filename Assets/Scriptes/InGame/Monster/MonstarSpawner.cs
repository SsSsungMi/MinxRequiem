using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   // 직렬화로 초기화
public class SpawnData
{
    [Header("Monster Data Info")]
    public float spwanTime;          // 소환 시간 
    public int spriteType;           // 스프라이트 타입 -> anim = Enemy에서 초기화
    public int health;               // 체력
    public float atk;                // 공격력
    public float speed;              // 속도
    public int score;                // 처치 점수

    [Header("monCount")]
    public int curCount;             // 개체수
    public int maxCount;             // 최대 개체수

    [Header("Drop Item")]
    public GameObject dropCoin;      // 몬스터가 각각 떨어트리는 코인
    public int maxCoin;              // 최대 코인
    public GameObject dropExp;      // 몬스터가 각각 떨어트리는 경험치
}

// Scripte Desc:
// 몬스터의 생성을 관장하는 ObjectPool 스크립트로 Enemy의 Data를 배열로 가지고 있습니다.
// 해당 스포너는 플레이어가 자식으로 가지고 있습니다.

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

        // 초기화 시간 >  레벨에 따라서 . 데이터의 소환 시간과 소환오브젝트가 조절이 된다.
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
