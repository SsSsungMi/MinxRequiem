using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankScore : MonoBehaviour
{ 
    public TextMeshProUGUI PlayerNum;   // ����
    public TextMeshProUGUI playerName;  // �̸�
    public TextMeshProUGUI bestScoreLabel;
    public TextMeshProUGUI bestTiemLabel;

    public TextMeshProUGUI CoinLabel;

    public GameObject rankPrefab;
    public GameObject rankObj;
    public bool isRanking;

    private string KeyName = "�ְ� ����";
    private int bestScore = 0;

    private int saveCoin = 0;

    private void Awake()
    {
        //              Ű ���� �ִ°� ? ���ٸ� = 0;
        bestScore = PlayerPrefs.GetInt(KeyName, 0);
        bestScoreLabel.text = $"�ְ� ���� : {bestScore.ToString()}";
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
        // ���߿� ����         > �ְ��������� ���� ��
        if (GameManager.instance.Score > 0)
        {
            // Set Ÿ�� ( "�������̵�" , ������ ���� ����);
            PlayerPrefs.SetInt("", GameManager.rankNum);
            PlayerPrefs.SetString("�̸� : ", GameManager.playerName);
            PlayerPrefs.SetInt("�ְ� ���� : ", GameManager.instance.Score);
            PlayerPrefs.SetFloat("�÷��� �ð� : ", GameManager.instance.PlayTime);
            PlayerPrefs.SetInt("��ȭ : ", GameManager.instance.Coin);
            
            // PlayerPrefs.Save() -> static���� �� �������� ����� ���α׷��� ���� �ѵ� ������ ���´�.

            if (PlayerPrefs.HasKey("��ȭ : "))
            {
                saveCoin = PlayerPrefs.GetInt("��ȭ : ");
                CoinLabel.text = saveCoin.ToString();
            }
            PlayerPrefs.Save();
        }

        //�� ����     = "���� ��" + Get Ÿ�� ("set���� ������ ������", ���̾��� ��); ���ڶ�� .ToString();
        PlayerNum.text = "" + PlayerPrefs.GetInt("", 0).ToString();
        playerName.text = "�̸� : " + PlayerPrefs.GetString("�̸� : ");
        bestScoreLabel.text = "�ְ� ���� : " + PlayerPrefs.GetInt("�ְ� ���� : ", 0).ToString();
        bestTiemLabel.text = "�÷��� �ð� : " + PlayerPrefs.GetFloat("�÷��� �ð� : ", 0).ToString("00:00");
        CoinLabel.text = "��ȭ : " + PlayerPrefs.GetInt("��ȭ : ", 0).ToString();
    }
}