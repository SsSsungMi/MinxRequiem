using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

// Scripte Desc:
// ���� ���� ĭ�� �������� ������ �޾� �����ϱ� ���� ��ũ��Ʈ �Դϴ�.

public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI nameText;            // �̸�
    public TextMeshProUGUI descriptionText;     // ����
    public TextMeshProUGUI abilityTextLabel;    // �����ɷ� _ Text��
    
    public void SetUpTooltip(string name, string tooltip, string ability)
    {
        this.gameObject.SetActive(true);
        nameText.text = "�̸� : " + name;
        descriptionText.text = tooltip;
        abilityTextLabel.text = "���� �ɷ� : " + ability;
    }
}