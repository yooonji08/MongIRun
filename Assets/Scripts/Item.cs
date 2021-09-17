using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour //모든 스테이지에 있는 아이템 오브젝트에 스크립트 연결 
{
    private void OnTriggerEnter2D(Collider2D collision) //충돌한 순간
    {
        if (collision.name.Contains("dog")) //dog컴포넌트와 충돌한 순간
        {
            Destroy(gameObject); //충돌한 아이템 오브젝트만 삭제
        }
    }
}