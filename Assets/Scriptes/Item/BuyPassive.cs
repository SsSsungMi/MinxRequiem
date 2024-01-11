using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

// Scripte Desc:
// Shop 화면에서 사용할 예정인 스크립트입니다.
// 아이템을 클릭하고 구매를 누르면 재화가 줄어들고 플레이어의 기본 능력이 증가합니다.
// 삭막함을 피하기 위해 이전 팀플에 에니메이션으로 사용했던 이미지를 Spine기능으로 변경하여 넣어두었습니다.


public class BuyPassive : MonoBehaviour
{
    public List<Character> characters = new List<Character>();

    public GameObject[] passives = new GameObject[5];           // 이 부분 아이템들을 배열로 담는데 아이템이 눌렸을 때 금액 또는 이미지 색 변경해주고
    public AudioClip clickSound;                                // 구매를 눌렀을 때 아래 결과 호출해주고 실제 내가 가지고 있는 플레이어들의 방어력 등을 올려주기
    ITEMNAME_TYPE itemType;

    public TextMeshProUGUI[] priceTexts;

    public void BuyPassiveItem()
    {
        if (GameManager.instance.Coin != 0)
        {
            GameManager.instance.Coin -= 10;
            GameManager.instance.rankScore.CoinLabel.text = "재화 : " + GameManager.instance.Coin.ToString();
            PlayerPrefs.SetInt("재화 : ", GameManager.instance.Coin);
            SoundManager.instance.Play(clickSound, SoundManager.instance.transform);
        }

        //switch(itemType)
        //{
        //    case ITEMNAME_TYPE.ARMOR:
        //        break;
        //}
    }
}
