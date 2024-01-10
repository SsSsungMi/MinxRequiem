using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;


// Scripte Desc:
// ������ �������� �κ��� �����ϴ� ��ũ��Ʈ �Դϴ�.
// �÷��̾�� ���õ� ������ �˰� �ֽ��ϴ�.
// 4������ Bool ������ �̿��ؼ� ������ �帧�� �����߽��ϴ�.
// ���θ޴����� Ȯ�� ������ ��ŷ ������ ������ �� �ִ� ������ �����ϱ⵵ �մϴ�.

// RecordInfoManager(�ð�, ����, ��ȭ)�� RecordInfoManager(�̸�)���� ��ŷ������ �ʿ��� ������ ������
// ���� �޾� MainCanvas�� Rank ĭ�� ������ �Ѱ��ݴϴ�.

public class GameManager : SingleTon<GameManager>
{
    [Header("Player Info")]
    public Player player;                       // �÷��̾� ����
    private GameObject playerObj;               // �÷��̾� ������Ʈ
    public GameObject playerPrefab;             // ������ ������

    private int monsterKillCount;               // ���� óġ ��
    [SerializeField] private int playCoin;      // ��ȭ
    private float playExp;                      // ����ġ
    public float[] nextExp = {  };
    public int playerLevel;                     // ����

    [Header("Pool")]
    public PoolManager poolManager;

    private bool isStart;   // �ΰ��� ���ۿ���
    private bool isLive;    // �ΰ��� ������ �ð� ����
    private bool isEnd;     // �й�
    private bool isClear;   // �¸�

    [Header("Rank Info")]
    public static int rankNum =1;               // ��ŷ ����
    public static string playerName;            // ��ŷ������ �÷��̾� �̸�
    private int playScore;                      // �� ����
    private float playTime;                     // �÷��� Ÿ��

    //// �ִ� �÷��� Ÿ��        = 5 * 60�� = 5��;
    //public float maxPlayTime = 5 * 60f;         // �ִ� �÷��� Ÿ��
    public RankScore rankScore;


    // �ΰ��� ���� ����
    public bool IsStart
    {
        get => isStart;
        set
        {
            isStart = value;
            if (isStart)
            {
                playerObj = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
                player = playerObj.GetComponent<Player>();
                RecordInfoManager.instance.levelUp.ShowLevelUpWindow(true);
                Init();
            }
            else if (!isStart)
            {
                return;
            }
        }
    }

    // ������ �� �ð� ���߱�
    public bool IsLive
    {
        get => isLive;
        set
        {
            isLive = value;
            if (isLive)
            {
                Time.timeScale = 1f;
            }
            else if (!isLive)
            {
                Time.timeScale = 0f;
            }
        }
    }

    // ���� �й� ����
    public bool IsEnd
    {
        get => isEnd;
        set
        {
            isEnd = value;
            if (isEnd)
            {
                // ��ũ�� ���� ����
                Score = RecordInfoManager.instance.ReScore;
                Coin = RecordInfoManager.instance.ReCoin;
                PlayTime = RecordInfoManager.instance.ReTime;
                MonsterKillCount = RecordInfoManager.instance.ReMonsterKillCount;

                // Ŭ���� �� Ȱ��ȭ
                RecordInfoManager.instance.overWindow.gameObject.SetActive(true);
                RecordInfoManager.instance.overWindow.SetResultWindow(RecordInfoManager.instance);
                playerName = RecordInfoManager.instance.overWindow.playerName.text;

                // ��ũ�� ����
                //MainCanvasManager.instance.rankManager.Record();
            }
            else if (!isEnd)
            {
                return;
            }
        }
    }
    // ���� �¸� ����
    public bool IsClear
    {
        get => isClear;
        set
        {
            isClear = value;
            if (IsClear)
            {
                // ��ũ�� ���� ����
                Score = RecordInfoManager.instance.ReScore;
                Coin = RecordInfoManager.instance.ReCoin;
                PlayTime = RecordInfoManager.instance.ReTime;
                MonsterKillCount = RecordInfoManager.instance.ReMonsterKillCount;

                // Ŭ���� �� Ȱ��ȭ
                RecordInfoManager.instance.clearWindow.gameObject.SetActive(true);
                RecordInfoManager.instance.clearWindow.SetResultWindow(RecordInfoManager.instance);
                playerName = RecordInfoManager.instance.clearWindow.playerName.text;
                
                // ��ũ�� ����
                //MainCanvasManager.instance.rankManager.Record();
            }
            else if (!IsClear)
            {
                return;
            }
        }
    }

    public int Score { get => playScore; set { playScore = value; } }
    public int Coin { get => playCoin; set { playCoin = value; } }
    public float PlayTime { get => playTime; set { playTime = value; } }
    public int MonsterKillCount { get => monsterKillCount; set { monsterKillCount = value; } }
    public float Exp 
    { 
        get => playExp; 
        set 
        { 
            playExp = value;
            if (Exp != 0)
            {
                UpExp();
                
            }
        } 
    }
   
    void Start()
    {
        FristInit();
    }

    public void FristInit()  // ���� ���� �� �� 1�� �ʱ�ȭ
    {
        playScore = 0;
        playCoin = Coin;
        playTime = 0;
        MonsterKillCount = 0;
        Exp = 0;
        playerLevel = 0;
        IsEnd = false;
        IsClear = false;
        IsStart = false;
        IsLive = true;
    }

    public void Init()  // ���� ������ �� ������ �ʱ�ȭ
    {
        playScore = 0;
        playCoin = Coin;
        playTime = 0;
        MonsterKillCount = 0;
        Exp = 0;
        playerLevel = 0;
        IsEnd = false;
        IsClear = false;
        IsLive = true;
    }

    public void UpExp()
    {
        if (Exp >= nextExp[Mathf.Min(playerLevel, nextExp.Length -1)])
        {
            playerLevel++;
            RecordInfoManager.instance.levelUp.ShowLevelUpWindow(true);
            Exp = 0;
        }
        return;
    }
    
    public void Exit()  // ���� ����
    {
    #if UNITY_EDITOR    // ����Ƽ���� �������� ��
        UnityEditor.EditorApplication.isPlaying = false;
    #else               // ���� �� �����ư�� ������ ��
        Application.Quit();
    #endif              // if �� ����
    }
}
