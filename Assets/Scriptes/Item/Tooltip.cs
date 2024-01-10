using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

// Scripte Desc:
// 툴팁 설명 칸에 아이템의 정보를 받아 셋팅하기 위한 스크립트 입니다.

public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI nameText;            // 이름
    public TextMeshProUGUI descriptionText;     // 설명
    public TextMeshProUGUI abilityTextLabel;    // 고유능력 _ Text로
    
    public void SetUpTooltip(string name, string tooltip, string ability)
    {
        this.gameObject.SetActive(true);
        nameText.text = "이름 : " + name;
        descriptionText.text = tooltip;
        abilityTextLabel.text = "고유 능력 : " + ability;
    }
}