using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Scripte Desc:
// Shop 화면에서 사용할 예정인 스크립트입니다.
// 
// 시간이 부족했던 관계로 특정 패시브 아이템을 업그레이드 시켜주는 기능은 완성하지 못했습니다.
// 
// 현재 스크립트상 가능한 기능은 플레이 이후 재화를 얻었을 다면 구매버튼을 눌렀을 때 재화가 줄어드는
// 기능 까지만 구현이 되어 있습니다.
// 삭막함을 피하기 위해 이전 팀플에 사용하지 못했던 Spine이미지를 추가로 넣어두었습니다.


public class BuyPassive : MonoBehaviour
{
    public GameObject[] passives = new GameObject[5];           // 이 부분 아이템들을 배열로 담는데 아이템이 눌렸을 때 금액 또는 이미지 색 변경해주고
    public AudioClip clickSound;                                // 구매를 눌렀을 때 아래 결과 호출해주고 실제 내가 가지고 있는 플레이어들의 방어력 등을 올려주기
    ITEMNAME_TYPE itemType;

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
