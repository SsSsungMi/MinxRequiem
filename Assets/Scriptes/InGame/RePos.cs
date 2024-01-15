using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// Ÿ�� �ʰ� ����, ��� ����, ��� ����ġ, ��ֹ�(Obstacle)�� ��ġ�� ���������ִ� ��ũ��Ʈ �Դϴ�.
// ������Ʈ�� �÷��̾��� Area���� ���� �Ÿ� �̻� �־����� ��ġ�� ������ �˴ϴ�.

public class RePos : MonoBehaviour
{
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Area"))   // Area�� �ƴϸ� ���� x
            return;
        // �÷��̾� ��ġ
        Vector3 playerPos = GameManager.instance.player.transform.position;
        // Ÿ�� �ڽ��� ��ġ
        Vector3 myPos = transform.position;
        // input.System package ���� ������ �Ʒ��� ���� ���� �ʿ���
        Vector3 playerDir = GameManager.instance.player.inputVec;

        // �Ÿ� ���  (-�� ������ �� �ȴ�)
        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        //  ���밪 ���
        float diffx = Mathf.Abs(dirX);  // x ���� �Ÿ�
        float diffy = Mathf.Abs(dirY);  // y ���� �Ÿ�

        // ���׿�����
        // ���� = �÷��̾��� x > 0 �´°�?  => true 1 / false -1 
        dirX = dirX > 0 ? 1 : -1;
        dirY = dirY > 0 ? 1 : -1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffx > diffy)  // y�� ���� x���� ũ�� => x������ ���� �̵��ߴ�.
                {
                    // Debug.Log("Ÿ�� �̵�");
                    transform.Translate(Vector3.right * dirX * 60);
                }
                else if (diffx < diffy)  // x�� ���� y���� ũ�� => y������ ���� �̵��ߴ�.
                {
                    // Debug.Log("Ÿ�� �̵�");
                    transform.Translate(Vector3.up * dirY * 60);
                }
                break;
            case "Enemy":
                // enabled = �ݶ��̴��� �����ϴ°�?
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
