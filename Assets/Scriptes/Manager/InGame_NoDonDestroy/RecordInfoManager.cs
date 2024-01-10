using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

// Scripte Desc:
// �ΰ��Ӱ� ���õ� �ð�, ����, ���� óġ ��, ����ġ, ��ȭ�� �����ϴ� ��ũ��Ʈ �Դϴ�.
// �ִ� �÷��� �ð��� �ʰ��ϰų� �÷��̾��� ü���� 0�� �� ��쿡�� GameOverWindow�� Ȱ��ȭ �ϸ� ������ ��ũ�� �Ѱ��ݴϴ�.
// �ִ� �÷��� �ð� �̳��� ���� ���͸� óġ�� ��쿡�� ClearGameWindow�� Ȱ��ȭ �ϸ� ������ ��ũ�� �Ѱ��ݴϴ�.

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
    public Image expBar;                    // ����ġ ��
    public GameObject hpBarObj;             // Hp�� -> �÷��̾�� ����

    [Header("Ending Scene")]
    public GameObject overPopUpWindow;
    public GameObject clearPopUpWindow;
    public ResultWindow clearWindow;
    public ResultWindow overWindow;

    [Header("LevelUp Window")]
    public LevelUp levelUp;

    // �������� ���� ����
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
                int min = Mathf.FloorToInt(ReTime / 60);    // ��
                int sec = Mathf.FloorToInt(ReTime % 60);    // ���� ������ = ��
                //  �ð�  = �ð� ������ { 0�ڸ� : 2�ڸ� } : {�ݺ�} , ��, ��
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