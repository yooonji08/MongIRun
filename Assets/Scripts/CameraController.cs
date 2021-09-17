using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour //강아지의 x좌표에 따라 카메라 위치도 바뀌도록 하는 스크립트
{
    GameObject player;

    void Start()
    {
        player = GameObject.Find("dog"); //dog 오브젝트를 GameObject변수에 할당
    }

    void Update()
    {
        //player의 x좌표가 바뀌면 카메라의 x좌표도 따라서 바뀌도록 설정
        Vector3 playerPos = player.transform.position;
        transform.position = new Vector3(
            playerPos.x, transform.position.y, transform.position.z);
    }
}
