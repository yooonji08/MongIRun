using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingEnemy : MonoBehaviour //Stage2씬의 bee오브젝트 스크립트
{
    Animator animator;
    AudioSource audioSrc;
    public AudioClip clip1;
    float speed = 0.5f; //적이 움직이는 속도
    Vector3 startPos; //현재 적의 위치
    float maxY = 3.0f; //위아래 이동 최대값

    void Start()
    {
        this.animator = GetComponent<Animator>();
        startPos = transform.position;
        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!ButtonUI.isPause) //pause버튼을 누르지 않았다면
        {
            //3초마다 오브젝트의 방향(위,아래) 전환
            Vector3 pos = startPos; //오브젝트의 현재 위치
            //Mathf함수의 Sin함수로 -1또는 1을 리턴, 리턴한 값을 최대 이동범위 변수와 곱해서 좌표 바꾸기
            pos.y += maxY * Mathf.Sin(Time.time * speed);
            transform.position = pos;
        }                      
    }

    private void OnTriggerEnter2D(Collider2D collision) //충돌한 순간
    {
        //dog오브젝트와 충돌한 순간
        if (collision.name.Contains("dog"))
        {
            audioSrc.PlayOneShot(clip1, 1.0f); //충돌 소리 출력
        }
    }
}
