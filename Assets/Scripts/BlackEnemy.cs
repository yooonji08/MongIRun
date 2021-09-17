using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackEnemy : MonoBehaviour
{
    Animator animator; //Animator 컴포넌트 변수
    AudioSource audioSrc; //AudioSource 컴포넌트 변수
    public AudioClip clip1; //dog오브젝트와 충돌했을 때 출력되는 소리
    private float speed = 0.02f; //enemy오브젝트가 움직이는 속도

    void Start()
    {
        this.animator = GetComponent<Animator>(); //enemy 애니메이션을 위해 Animator 컴포넌트 연결
        audioSrc = GetComponent<AudioSource>(); //충돌했을 때의 소리 출력을 위해 AudioSource 컴포넌트 연결
    }

    void Update()
    {
        if (!ButtonUI.isPause) //pause버튼을 누르지 않았다면
        {
            transform.Translate(speed, 0, 0); //speed만큼 enemy오브젝트 이동
            this.animator.speed = 2.0f; //애니메이션 전환 속도
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //충돌한 순간
    {
        //dog컴포넌트와 부딪힌 순간 충돌소리 출력
        if (collision.name.Contains("dog"))
        {
            audioSrc.PlayOneShot(clip1, 0.7f);
        }

        //만약 앞에 길이 막혀있다면(grass오브젝트와 부딪친 순간) 방향 바꾸고 다시 움직이기
        if (collision.name.Contains("grass2 (2)"))
        {
            //오른쪽으로 이미지 반전
            transform.localScale = new Vector3(-1 * 6.5f, 6.5f, 6.5f);
            speed *= -1; //-1곱해줘서 방향 바꾸기
        }
        else if (collision.name.Contains("grass2 (3)"))
        {
            //왼쪽으로 이미지 반전
            transform.localScale = new Vector3(1 * 6.5f, 6.5f, 6.5f);
            speed *= -1; //-1곱해줘서 방향 바꾸기
        }
    }
}
