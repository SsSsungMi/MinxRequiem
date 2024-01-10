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
        
        //PlayerPrefs.DeleteAll();
        //              키 값이 있는가 ? 없다면 = 0;
        bestScore = PlayerPrefs.GetInt(KeyName, 0);
        bestScoreLabel.text = $"최고 점수 : {bestScore.ToString()}";
    }

    // 점수는 = 몬스터 처치 점수
    // 랭크 순위 = 플레이 시간 짧고 + 처치한 점수

    // 랭크 생성은?
    // 플레이를 한다 -> 클리어 또는 게임오버 -> 총 점수와 함께 이름 입력칸을 준다
    //   -> 이름을 입력 후 버튼을 누른다
    //   -> Main 에 있는 Text 부분에 (순위, 이름, 점수, 시간) 을 넘겨준다.

    // 순위는?
    // 이전에 있던 기록 (프리펩 ?) 을 확인하고
    // 점수가 높다 => 랭크프리펩을 새로 생성한다 -> 기존의 정보를 새로운 프리펩으로 옮긴다
    //            -> 기존의 프리펩에 새로운 정보를 넘겨준다.

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

        // Set 부분이 잘 돌아가는지 디버깅 해봄
        //Debug.Log("점수 : " + PlayerPrefs.GetInt("점수"));
        //Debug.Log("이름 : "+ PlayerPrefs.GetString("이름"));

        //빈 공간     = "변수 명" + Get 타입 ("set에서 지정한 변수명", 값이없을 때); 숫자라면 .ToString();
        PlayerNum.text = "" + PlayerPrefs.GetInt("", 0).ToString();
        playerName.text = "이름 : " + PlayerPrefs.GetString("이름 : ");
        bestScoreLabel.text = "최고 점수 : " + PlayerPrefs.GetInt("최고 점수 : ", 0).ToString("000, 000, 000");
        bestTiemLabel.text = "플레이 시간 : " + PlayerPrefs.GetFloat("플레이 시간 : ", 0).ToString("00:00");
        CoinLabel.text = "재화 : " + PlayerPrefs.GetInt("재화 : ", 0).ToString();
        //  GameManager.score = 0;
    }

    public void RankSwap(int score, float playtime)
    {
        // if ( score 점수 > 기존 랭크들의 점수 )
        //  {
        //          if ( 점수 == 같은 점수의 랭크 점수 )
        //          { 플레이 시간이 더 짧은 쪽의 순위를 스왑한다}
        //      점수가 높은 쪽의 순위를 스왑한다.
        //  }
    }
}