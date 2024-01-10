using System.Collections;
using System.Collections.Generic;
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
    public GameObject[] passives = new GameObject[5];
    public AudioClip clickSound;

    public void BuyPassiveItem()
    {
        if (GameManager.instance.Coin != 0)
        {
            GameManager.instance.Coin -= 10;
            GameManager.instance.rankScore.CoinLabel.text = "재화 : " + GameManager.instance.Coin.ToString();
            PlayerPrefs.SetInt("재화 : ", GameManager.instance.Coin);
            SoundManager.instance.Play(clickSound, SoundManager.instance.transform);
        }
    }
}
