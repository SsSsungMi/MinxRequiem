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
        
        //PlayerPrefs.DeleteAll();
        //              Ű ���� �ִ°� ? ���ٸ� = 0;
        bestScore = PlayerPrefs.GetInt(KeyName, 0);
        bestScoreLabel.text = $"�ְ� ���� : {bestScore.ToString()}";
    }

    // ������ = ���� óġ ����
    // ��ũ ���� = �÷��� �ð� ª�� + óġ�� ����

    // ��ũ ������?
    // �÷��̸� �Ѵ� -> Ŭ���� �Ǵ� ���ӿ��� -> �� ������ �Բ� �̸� �Է�ĭ�� �ش�
    //   -> �̸��� �Է� �� ��ư�� ������
    //   -> Main �� �ִ� Text �κп� (����, �̸�, ����, �ð�) �� �Ѱ��ش�.

    // ������?
    // ������ �ִ� ��� (������ ?) �� Ȯ���ϰ�
    // ������ ���� => ��ũ�������� ���� �����Ѵ� -> ������ ������ ���ο� ���������� �ű��
    //            -> ������ �����鿡 ���ο� ������ �Ѱ��ش�.

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

        // Set �κ��� �� ���ư����� ����� �غ�
        //Debug.Log("���� : " + PlayerPrefs.GetInt("����"));
        //Debug.Log("�̸� : "+ PlayerPrefs.GetString("�̸�"));

        //�� ����     = "���� ��" + Get Ÿ�� ("set���� ������ ������", ���̾��� ��); ���ڶ�� .ToString();
        PlayerNum.text = "" + PlayerPrefs.GetInt("", 0).ToString();
        playerName.text = "�̸� : " + PlayerPrefs.GetString("�̸� : ");
        bestScoreLabel.text = "�ְ� ���� : " + PlayerPrefs.GetInt("�ְ� ���� : ", 0).ToString("000, 000, 000");
        bestTiemLabel.text = "�÷��� �ð� : " + PlayerPrefs.GetFloat("�÷��� �ð� : ", 0).ToString("00:00");
        CoinLabel.text = "��ȭ : " + PlayerPrefs.GetInt("��ȭ : ", 0).ToString();
        //  GameManager.score = 0;
    }

    public void RankSwap(int score, float playtime)
    {
        // if ( score ���� > ���� ��ũ���� ���� )
        //  {
        //          if ( ���� == ���� ������ ��ũ ���� )
        //          { �÷��� �ð��� �� ª�� ���� ������ �����Ѵ�}
        //      ������ ���� ���� ������ �����Ѵ�.
        //  }
    }
}