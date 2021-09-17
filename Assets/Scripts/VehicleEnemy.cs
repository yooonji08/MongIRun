using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleEnemy : MonoBehaviour //Stage3씬의 자동차 컴포넌트 스크립트
{
    AudioSource audioSrc;
    public AudioClip clip1; //dog컴포넌트와 자동차컴포넌트가 충돌할 때 출력되는 소리
    float speed = 0.03f; //자동차가 움직이는 속도

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!ButtonUI.isPause) //pause버튼을 누르지 않았다면
        {
            transform.Translate(speed, 0, 0); //speed만큼 자동차 이동
        }

        //각 자동차가 화면을 벗어나면 오브젝트 삭제
        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //충돌한 순간
    {
        if (collision.name.Contains("dog")) //dog오브젝트와 충돌한 순간
        {
            audioSrc.PlayOneShot(clip1, 0.5f); //소리 1회 출력
        }
    }
}
