using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankScore : MonoBehaviour
{ 
    public TextMeshProUGUI PlayerNum;   // 순위
    public TextMeshProUGUI playerName;  // 이름
    public TextMeshProUGUI bestScoreLabel;
    public TextMeshProUGUI bestTiemLabel;

    public TextMeshProUGUI CoinLabel;

    public GameObject rankPrefab;
    public GameObject rankObj;
    public bool isRanking;

    private string KeyName = "최고 점수";
    private int bestScore = 0;

    private int saveCoin = 0;

    private void Awake()
    {
        //              키 값이 있는가 ? 없다면 = 0;
        bestScore = PlayerPrefs.GetInt(KeyName, 0);
        bestScoreLabel.text = $"최고 점수 : {bestScore.ToString()}";
    }

    public bool IsRanking
    {
        get => isRanking;
        set
        {
            if (isRanking)
            {
                Record();
            }
        }
    }
   
    public void Record()
    {
        // 나중에 수정         > 최고점수보다 높을 때
        if (GameManager.instance.Score > 0)
        {
            // Set 타입 ( "변수명이됨" , 저장할 정보 셋팅);
            PlayerPrefs.SetInt("", GameManager.rankNum);
            PlayerPrefs.SetString("이름 : ", GameManager.playerName);
            PlayerPrefs.SetInt("최고 점수 : ", GameManager.instance.Score);
            PlayerPrefs.SetFloat("플레이 시간 : ", GameManager.instance.PlayTime);
            PlayerPrefs.SetInt("재화 : ", GameManager.instance.Coin);
            
            // PlayerPrefs.Save() -> static보다 더 영구적인 존재로 프로그램을 껏다 켜도 정보가 남는다.

            if (PlayerPrefs.HasKey("재화 : "))
            {
                saveCoin = PlayerPrefs.GetInt("재화 : ");
                CoinLabel.text = saveCoin.ToString();
            }
            PlayerPrefs.Save();
        }

        //빈 공간     = "변수 명" + Get 타입 ("set에서 지정한 변수명", 값이없을 때); 숫자라면 .ToString();
        PlayerNum.text = "" + PlayerPrefs.GetInt("", 0).ToString();
        playerName.text = "이름 : " + PlayerPrefs.GetString("이름 : ");
        bestScoreLabel.text = "최고 점수 : " + PlayerPrefs.GetInt("최고 점수 : ", 0).ToString();
        bestTiemLabel.text = "플레이 시간 : " + PlayerPrefs.GetFloat("플레이 시간 : ", 0).ToString("00:00");
        CoinLabel.text = "재화 : " + PlayerPrefs.GetInt("재화 : ", 0).ToString();
    }
}