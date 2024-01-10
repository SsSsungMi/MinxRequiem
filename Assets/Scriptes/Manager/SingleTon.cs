using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripte Desc:
// DontDestroyOnLoad 부서지지 않아야 하는 오브젝트 들을 위한 싱글톤 패턴의
// 부모 스크립트입니다.

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
