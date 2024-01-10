using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Scripte Desc:
// ������ ����Ǿ��� �� ReCordInfoManager�� ���������� ������ �ִ� ������ �Ѱܹ��� ��
// �� ��Ȳ�� �´� â (Ŭ����, ���ӿ���)�� ���缭 ���� ������ִ� ��ũ��Ʈ �Դϴ�.
// �÷��̾�� �� â �ȿ��� �ڽ��� �̸��� �Է��� �� ������, �Էµ� ���� GameManager�� �̸����� ���޵˴ϴ�.

public class ResultWindow : MonoBehaviour
{
    public TextMeshProUGUI result;                      // �� ����
    public TextMeshProUGUI monsterKillCount = null;     // �� óġ ��
    public TextMeshProUGUI playTime;                    // �� �÷��� �ð�
    public TextMeshProUGUI playerName = null;           // ��Ÿ�� �� �Է��� �̸� ����
    public TextMeshProUGUI printPlayerName;             // â�� ��µ� �̸�
    public GameObject namePlace;                        // �̸� �Է� ĭ

    public void SetResultWindow(RecordInfoManager rm)
    {
        result.text = string.Format("�� ���� : " + "{0:N3}", rm.ReScore.ToString());
        monsterKillCount.text = "óġ�� ���� : " + rm.ReMonsterKillCount.ToString();
        playTime.text = "�÷��� �ð� : " + rm.ReTime.ToString("00:00");

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            printPlayerName.text = "" + playerName.text;
            string text = printPlayerName.text;
            GameManager.playerName = text;
            Destroy(namePlace);
            
            MainCanvasManager.instance.rankManager.Record();
        }
    }
}