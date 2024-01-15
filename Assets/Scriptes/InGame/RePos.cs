using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// 타일 맵과 몬스터, 드롭 코인, 드롭 경험치, 장애물(Obstacle)의 위치를 재정의해주는 스크립트 입니다.
// 오브젝트가 플레이어의 Area에서 일정 거리 이상 멀어지면 위치가 재정의 됩니다.

public class RePos : MonoBehaviour
{
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Area"))   // Area가 아니면 실행 x
            return;
        // 플레이어 위치
        Vector3 playerPos = GameManager.instance.player.transform.position;
        // 타일 자신의 위치
        Vector3 myPos = transform.position;
        // input.System package 쓰기 때문에 아래의 방향 값이 필요함
        Vector3 playerDir = GameManager.instance.player.inputVec;

        // 거리 계산  (-가 나오면 안 된다)
        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        //  절대값 계산
        float diffx = Mathf.Abs(dirX);  // x 간의 거리
        float diffy = Mathf.Abs(dirY);  // y 간의 거리

        // 삼항연산자
        // 방향 = 플레이어의 x > 0 맞는가?  => true 1 / false -1 
        dirX = dirX > 0 ? 1 : -1;
        dirY = dirY > 0 ? 1 : -1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffx > diffy)  // y축 보다 x축이 크다 => x축으로 수평 이동했다.
                {
                    // Debug.Log("타일 이동");
                    transform.Translate(Vector3.right * dirX * 60);
                }
                else if (diffx < diffy)  // x축 보다 y축이 크다 => y축으로 수평 이동했다.
                {
                    // Debug.Log("타일 이동");
                    transform.Translate(Vector3.up * dirY * 60);
                }
                break;
            case "Enemy":
                // enabled = 콜라이더가 존재하는가?
                if (col.enabled)
                {
                    transform.Translate(playerDir * 30 + new Vector3(Random.Range(-3f, 3f),
                                        Random.Range(-3f, 3f), 0f));
                }
                break;
            case "Coin":
                if (col.enabled)
                {
                    transform.Translate(playerDir * 50 + new Vector3(Random.Range(-3f, 3f),
                                        Random.Range(-3f, 3f), 0f));
                }
                break;
            case "Exp":
                if (col.enabled)
                {
                    transform.Translate(playerDir * 50 + new Vector3(Random.Range(-3f, 3f),
                                        Random.Range(-3f, 3f), 0f));
                }
                break;
            case "Tree":
                if (col.enabled)
                {
                    transform.Translate(playerDir * 50 + new Vector3(Random.Range(-3f, 3f),
                                        Random.Range(-3f, 3f), 0f));
                }
                break;
        }
    }
}
