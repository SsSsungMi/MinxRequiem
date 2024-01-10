using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// Shop ȭ�鿡�� ����� ������ ��ũ��Ʈ�Դϴ�.
// 
// �ð��� �����ߴ� ����� Ư�� �нú� �������� ���׷��̵� �����ִ� ����� �ϼ����� ���߽��ϴ�.
// 
// ���� ��ũ��Ʈ�� ������ ����� �÷��� ���� ��ȭ�� ����� �ٸ� ���Ź�ư�� ������ �� ��ȭ�� �پ���
// ��� ������ ������ �Ǿ� �ֽ��ϴ�.
// �踷���� ���ϱ� ���� ���� ���ÿ� ������� ���ߴ� Spine�̹����� �߰��� �־�ξ����ϴ�.

public class BuyPassive : MonoBehaviour
{
    public GameObject[] passives = new GameObject[5];
    public AudioClip clickSound;

    public void BuyPassiveItem()
    {
        if (GameManager.instance.Coin != 0)
        {
            GameManager.instance.Coin -= 10;
            GameManager.instance.rankScore.CoinLabel.text = "��ȭ : " + GameManager.instance.Coin.ToString();
            PlayerPrefs.SetInt("��ȭ : ", GameManager.instance.Coin);
            SoundManager.instance.Play(clickSound, SoundManager.instance.transform);
        }
    }
}
