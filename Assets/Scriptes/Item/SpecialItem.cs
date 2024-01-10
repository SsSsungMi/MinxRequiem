using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;


// 추가 아이템 ----------------------------------------------------------
// GoldCoin = 골드 코인 = 운 테스트 = ! 당신의 운을 실험해보세요 ! 1000원 이내의 금액을 받을 수 있습니다.
// HpMeat = 묵직한 고기 = 美味한 맛! = Hp 를 30만큼 회복한다.
// ---------------------------------------------------------------

// Scripte Desc:
// 추가 아이템들을 구현하기 위한 씬입니다.
// 모든 아이템이 찬 뒤 레벨업이 끝까지 올라간 아이템이 있다면 2가지 타입중 하나가 등장합니다.

public class SpecialItem : MonoBehaviour
{
    public string itemName;          // 이름
    public Sprite image;             // icon이미지

    public string ability;           // 능력

    public AudioClip buttonClip;
    ITEMNAME_TYPE name_type;

    public void OnClik()    // 클릭 받음
    {
        switch (name_type)
        {
            case ITEMNAME_TYPE.GoldCoin:
                float randomCoin = UnityEngine.Random.Range(0, 1000);
                RecordInfoManager.instance.ReCoin += (int)randomCoin;
                ItemUiManager.instance.UseSpecialItem(this);
                SoundManager.instance.Play(buttonClip, SoundManager.instance.transform);
                break;
            case ITEMNAME_TYPE.HpMeat:
                GameManager.instance.player.Hp += 30;
                ItemUiManager.instance.UseSpecialItem(this);
                SoundManager.instance.Play(buttonClip, SoundManager.instance.transform);
                break;
        }
    }
}
