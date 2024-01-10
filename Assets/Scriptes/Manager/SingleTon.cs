using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// DontDestroyOnLoad �μ����� �ʾƾ� �ϴ� ������Ʈ ���� ���� �̱��� ������
// �θ� ��ũ��Ʈ�Դϴ�.

public class SingleTon<T> : MonoBehaviour where T : SingleTon<T>
{
    public static T instance = null;

    protected void Awake()
    {
        if(instance == null)
        {
            instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
