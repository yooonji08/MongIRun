using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour //cloudGroup컴포넌트에 연결되는 스크립트
{
    public GameObject cloud1; //cloudGroup1 오브젝트
    float cloudSpeed1 = 0.7f; //구름 움직이는 속도, 0.7
    float startPos1 = 50.0f; //구름 시작 위치
    float endPos1 = -20.0f; //구름 사라지는 위치

    public GameObject cloud2; //cloudGroup2 오브젝트
    float cloudSpeed2 = 1.0f; //구름 움직이는 속도, 1.0
    float startPos2 = 100.0f; 
    float endPos2 = -20.0f; 

    void Update()
    {
        CloudMove1();
        CloudMove2();
    }

    void CloudMove1() //cloudGroup1 오브젝트의 이동 함수
    {
        //왼쪽으로 이동
        cloud1.transform.Translate(-1 * cloudSpeed1 * Time.deltaTime, 0, 0);

        //일정 지점에 구름이 지난다면
        if (cloud1.transform.position.x <= endPos1)
        {
            //시작 위치로 이동
            cloud1.transform.Translate(-1 * (endPos1 - startPos1), 0, 0);
        }
    }

    void CloudMove2() //cloudGroup2 오브젝트의 이동 함수
    {
        //왼쪽으로 이동
        cloud2.transform.Translate(-1 * cloudSpeed2 * Time.deltaTime, 0, 0);

        //일정 지점에 구름이 지난다면
        if (cloud2.transform.position.x <= endPos2)
        {
            //시작 위치로 이동
            cloud2.transform.Translate(-1 * (endPos2 - startPos2), 0, 0);
        }
    }
}
