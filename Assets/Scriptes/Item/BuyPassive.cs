using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

// Scripte Desc:
// Shop ȭ�鿡�� ����� ������ ��ũ��Ʈ�Դϴ�.
// �������� Ŭ���ϰ� ���Ÿ� ������ ��ȭ�� �پ��� �÷��̾��� �⺻ �ɷ��� �����մϴ�.
// �踷���� ���ϱ� ���� ���� ���ÿ� ���ϸ��̼����� ����ߴ� �̹����� Spine������� �����Ͽ� �־�ξ����ϴ�.


public class BuyPassive : MonoBehaviour
{
    public List<Character> characters = new List<Character>();

    public GameObject[] passives = new GameObject[5];           // �� �κ� �����۵��� �迭�� ��µ� �������� ������ �� �ݾ� �Ǵ� �̹��� �� �������ְ�
    public AudioClip clickSound;                                // ���Ÿ� ������ �� �Ʒ� ��� ȣ�����ְ� ���� ���� ������ �ִ� �÷��̾���� ���� ���� �÷��ֱ�
    ITEMNAME_TYPE itemType;

    public TextMeshProUGUI[] priceTexts;

    public void BuyPassiveItem()
    {
        if (GameManager.instance.Coin != 0)
        {
            GameManager.instance.Coin -= 10;
            GameManager.instance.rankScore.CoinLabel.text = "��ȭ : " + GameManager.instance.Coin.ToString();
            PlayerPrefs.SetInt("��ȭ : ", GameManager.instance.Coin);
            SoundManager.instance.Play(clickSound, SoundManager.instance.transform);
        }

        //switch(itemType)
        //{
        //    case ITEMNAME_TYPE.ARMOR:
        //        break;
        //}
    }
}
