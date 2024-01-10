using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Scripte Desc:
// 게임이 동료되었을 때 ReCordInfoManager가 마지막으로 가지고 있던 정보를 넘겨받은 뒤
// 각 상황에 맞는 창 (클리어, 게임오버)에 맞춰서 값을 출력해주는 스크립트 입니다.
// 플레이어는 이 창 안에서 자신의 이름을 입력할 수 있으며, 입력된 값은 GameManager의 이름으로 전달됩니다.

public class ResultWindow : MonoBehaviour
{
    public TextMeshProUGUI result;                      // 총 점수
    public TextMeshProUGUI monsterKillCount = null;     // 총 처치 수
    public TextMeshProUGUI playTime;                    // 총 플레이 시간
    public TextMeshProUGUI playerName = null;           // 런타임 중 입력할 이름 공간
    public TextMeshProUGUI printPlayerName;             // 창에 출력될 이름
    public GameObject namePlace;                        // 이름 입력 칸

    public void SetResultWindow(RecordInfoManager rm)
    {
        result.text = "총 점수 : " + rm.ReScore.ToString("000");
        monsterKillCount.text = "처치한 몬스터 : " + rm.ReMonsterKillCount.ToString();
        playTime.text = "플레이 시간 : " + rm.ReTime.ToString("00:00");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            printPlayerName.text = "이름 : " + playerName.text;
            GameManager.playerName = printPlayerName.text;
            Destroy(namePlace);
        }
    }
}