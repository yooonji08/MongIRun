using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Flag1 : MonoBehaviour //Stage1씬의 깃발에 스크립트 연결
{
    AudioSource audioSrc; //AudioSource컴포넌트 변수
    public AudioClip clip1; //flag와 dog컴포넌트가 충돌한 순간 소리 출력을 위한 AudioClip컴포넌트 변수
    PlayerController playerController; //PlayerController클래스의 객체 선언

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        //dog컴포넌트에 부착되어있는 PlayerController스크립트에서 메소드를 가져오기 위해 객체에 할당
        playerController = GameObject.Find("dog").GetComponent<PlayerController>();
    }

    void nextStage()
    {
        SceneManager.LoadScene("Stage2");
    }

    private void OnTriggerEnter2D(Collider2D collision) //충돌한 순간
    {
        //dog컴포넌트가 flag컴포넌트와 충돌한 순간(최종 깃발 도착한다면)
        if (collision.name.Contains("dog"))
        {
            if (playerController.getScore() == 5) //모은 뼈다귀 개수가 5개라면 다음 스테이지로 이동
            {
                Debug.Log("Stage1 Clear!");
                audioSrc.PlayOneShot(clip1, 1.0f); //소리 1회 출력
                Invoke("nextStage", 3f); //3초 후에 nextStage()함수 실행
            }
            else //5개가 아니라면, 콘솔창에 메시지 출력
            {
                Debug.Log("찾은 뼈다귀의 개수가 부족합니다.");
                Debug.Log("부족한 뼈다귀: " + (5 - playerController.getScore()));
            }
        }
    }
}
