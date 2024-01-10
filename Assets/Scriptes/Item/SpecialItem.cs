using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;


// �߰� ������ ----------------------------------------------------------
// GoldCoin = ��� ���� = �� �׽�Ʈ = ! ����� ���� �����غ����� ! 1000�� �̳��� �ݾ��� ���� �� �ֽ��ϴ�.
// HpMeat = ������ ��� = ڸګ�� ��! = Hp �� 30��ŭ ȸ���Ѵ�.
// ---------------------------------------------------------------

// Scripte Desc:
// �߰� �����۵��� �����ϱ� ���� ���Դϴ�.
// ��� �������� �� �� �������� ������ �ö� �������� �ִٸ� 2���� Ÿ���� �ϳ��� �����մϴ�.

public class SpecialItem : MonoBehaviour
{
    public string itemName;          // �̸�
    public Sprite image;             // icon�̹���

    public string ability;           // �ɷ�

    public AudioClip buttonClip;
    ITEMNAME_TYPE name_type;

    public void OnClik()    // Ŭ�� ����
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
