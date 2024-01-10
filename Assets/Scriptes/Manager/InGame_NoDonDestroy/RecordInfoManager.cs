using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

// Scripte Desc:
// 인게임과 관련된 시간, 점수, 몬스터 처치 수, 경험치, 재화를 관리하는 스크립트 입니다.
// 최대 플레이 시간을 초과하거나 플레이어의 체력이 0이 된 경우에는 GameOverWindow를 활성화 하며 정보를 랭크에 넘겨줍니다.
// 최대 플레이 시간 이내에 보스 몬스터를 처치한 경우에는 ClearGameWindow를 활성화 하며 정보를 랭크에 넘겨줍니다.

public class RecordInfoManager : MonoBehaviour
{
    public static RecordInfoManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            Init();
        }
    }

    public GameObject outScene;

    [Header("Player Text Info")]
    public TextMeshProUGUI timeLabel;
    public TextMeshProUGUI coinLabel;
    public TextMeshProUGUI monsterKillCountLabel;
    public TextMeshProUGUI playerLevel;
    public Image expBar;                    // 경험치 바
    public GameObject hpBarObj;             // Hp바 -> 플레이어로 연결

    [Header("Ending Scene")]
    public GameObject overPopUpWindow;
    public GameObject clearPopUpWindow;
    public ResultWindow clearWindow;
    public ResultWindow overWindow;

    [Header("LevelUp Window")]
    public LevelUp levelUp;

    // 보여주지 않을 정보
    private float reTime;
    private int reScore;
    private int reCoin;
    private int reMonsterKillCount;

    public float ReTime { get => reTime; set { reTime = value; } }
    public int ReScore { get => reScore; set { reScore = value; } }
    public int ReCoin { get => reCoin; set { reCoin = value; } }
    public int ReMonsterKillCount { get => reMonsterKillCount; set { reMonsterKillCount = value; } }
    
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        reTime = 0;
        reScore = 0;
        reCoin = 0;
        reMonsterKillCount = 0;
        expBar.fillAmount = 0;

        overWindow.gameObject.SetActive(false);
        clearWindow.gameObject.SetActive(false);
    }

    private void Update()
    {
    //    if (ReTime >= GameManager.instance.maxPlayTime)
    //        GameManager.instance.IsEnd = true;

        if (GameManager.instance.IsStart)
        {
            if (GameManager.instance.IsEnd)
                return;

            hpBarObj.SetActive(true);
            ReTime += Time.deltaTime;

            if (ReTime >= 0f)
            {
                int min = Mathf.FloorToInt(ReTime / 60);    // 분
                int sec = Mathf.FloorToInt(ReTime % 60);    // 나눈 나머지 = 초
                //  시간  = 시간 재정의 { 0자리 : 2자리 } : {반복} , 분, 초
                timeLabel.text = string.Format("{0:D2}:{1:D2}", min, sec);
            }
            coinLabel.text = ReCoin.ToString();
            monsterKillCountLabel.text = ReMonsterKillCount.ToString();
            playerLevel.text = GameManager.instance.playerLevel.ToString();
        }
    }
    public void LateUpdate()
    {
        float curExp = GameManager.instance.Exp;
        float maxExp = GameManager.instance.nextExp[
            Mathf.Min(GameManager.instance.playerLevel,
            GameManager.instance.nextExp.Length - 1)];
        expBar.fillAmount = curExp / maxExp;
    }
}