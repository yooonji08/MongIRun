using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : MonoBehaviour //Stage2씬의 worm오브젝트 스크립트
{
    Animator animator;
    AudioSource audioSrc;
    public AudioClip clip1;
    float speed = 0.01f; //적이 움직이는 속도

    void Start()
    {
        this.animator = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!ButtonUI.isPause) //pause버튼을 누르지 않았다면
        {
            transform.Translate(-1 * speed, 0, 0); //speed만큼 적 이동
        }

        //지렁이가 화면을 벗어나면 오브젝트 삭제
        if (transform.position.x < -11)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //dog오브젝트와 충돌한 순간
        if (collision.name.Contains("dog"))
        {
            audioSrc.PlayOneShot(clip1, 1.0f); //충돌 소리 출력
        }
    }
}
