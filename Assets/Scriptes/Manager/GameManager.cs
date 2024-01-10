using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;


// Scripte Desc:
// 게임의 전반적인 부분을 관장하는 스크립트 입니다.
// 플레이어와 관련된 정보를 알고 있습니다.
// 4가지의 Bool 변수를 이용해서 게임의 흐름을 조절했습니다.
// 메인메뉴에서 확인 가능한 랭킹 정보를 전달할 수 있는 값들을 보관하기도 합니다.

// RecordInfoManager(시간, 점수, 재화)과 RecordInfoManager(이름)에서 랭킹순위에 필요한 각각의 정보를
// 전달 받아 MainCanvas의 Rank 칸에 정보를 넘겨줍니다.

public class GameManager : SingleTon<GameManager>
{
    [Header("Player Info")]
    public Player player;                       // 플레이어 정보
    private GameObject playerObj;               // 플레이어 오브젝트
    public GameObject playerPrefab;             // 복사할 프리펩

    private int monsterKillCount;               // 몬스터 처치 수
    [SerializeField] private int playCoin;      // 재화
    private float playExp;                      // 경험치
    public float[] nextExp = {  };
    public int playerLevel;                     // 레벨

    [Header("Pool")]
    public PoolManager poolManager;

    private bool isStart;   // 인게임 시작여부
    private bool isLive;    // 인게임 레벨업 시간 여부
    private bool isEnd;     // 패배
    private bool isClear;   // 승리

    [Header("Rank Info")]
    public static int rankNum =1;               // 랭킹 순위
    public static string playerName;            // 랭킹정보용 플레이어 이름
    private int playScore;                      // 총 점수
    private float playTime;                     // 플레이 타임

    //// 최대 플레이 타임        = 5 * 60초 = 5분;
    //public float maxPlayTime = 5 * 60f;         // 최대 플레이 타임
    public RankScore rankScore;


    // 인게임 시작 여부
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

    // 레벨업 중 시간 멈추기
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

    // 게임 패배 여부
    public bool IsEnd
    {
        get => isEnd;
        set
        {
            isEnd = value;
            if (isEnd)
            {
                // 랭크용 정보 전달
                Score = RecordInfoManager.instance.ReScore;
                Coin = RecordInfoManager.instance.ReCoin;
                PlayTime = RecordInfoManager.instance.ReTime;
                MonsterKillCount = RecordInfoManager.instance.ReMonsterKillCount;

                // 클리어 씬 활성화
                RecordInfoManager.instance.overWindow.gameObject.SetActive(true);
                RecordInfoManager.instance.overWindow.SetResultWindow(RecordInfoManager.instance);
                playerName = RecordInfoManager.instance.overWindow.playerName.text;

                // 랭크판 생성
                //MainCanvasManager.instance.rankManager.Record();
            }
            else if (!isEnd)
            {
                return;
            }
        }
    }
    // 게임 승리 여부
    public bool IsClear
    {
        get => isClear;
        set
        {
            isClear = value;
            if (IsClear)
            {
                // 랭크용 정보 전달
                Score = RecordInfoManager.instance.ReScore;
                Coin = RecordInfoManager.instance.ReCoin;
                PlayTime = RecordInfoManager.instance.ReTime;
                MonsterKillCount = RecordInfoManager.instance.ReMonsterKillCount;

                // 클리어 씬 활성화
                RecordInfoManager.instance.clearWindow.gameObject.SetActive(true);
                RecordInfoManager.instance.clearWindow.SetResultWindow(RecordInfoManager.instance);
                playerName = RecordInfoManager.instance.clearWindow.playerName.text;
                
                // 랭크판 생성
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

    public void FristInit()  // 게임 실행 후 단 1번 초기화
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

    public void Init()  // 게임 씬으로 들어갈 때마다 초기화
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
    
    public void Exit()  // 게임 종료
    {
    #if UNITY_EDITOR    // 유니티에서 종료했을 때
        UnityEditor.EditorApplication.isPlaying = false;
    #else               // 빌드 후 종료버튼을 눌렀을 때
        Application.Quit();
    #endif              // if 문 종료
    }
}
