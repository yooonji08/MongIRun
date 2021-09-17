using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Flag2 : MonoBehaviour //Stage2씬의 깃발에 스크립트 연결
{
    AudioSource audioSrc; //AudioSource컴포넌트 변수
    public AudioClip clip1; //flag와 dog컴포넌트가 충돌한 순간 소리 출력을 위한 AudioClip컴포넌트 변수
    PlayerController2 playerController2; //PlayerController2클래스의 객체 선언

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        //dog컴포넌트에 부착되어있는 PlayerController2스크립트에서 메소드를 가져오기 위해 객체에 할당
        playerController2 = GameObject.Find("dog").GetComponent<PlayerController2>();
    }

    void nextStage()
    {
        SceneManager.LoadScene("Stage3");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //dog컴포넌트가 flag컴포넌트와 충돌한 순간(최종 깃발 도착한다면)
        if (collision.name.Contains("dog"))
        {
            if (playerController2.getGiveVaccine() == 2) //모든 백신을 사람에게 건네줬다면, 다음 스테이지로 이동
            {
                Debug.Log("Stage2 Clear!");
                audioSrc.PlayOneShot(clip1, 1.0f); //소리 출력
                Invoke("nextStage", 3f); //3초 후에 nextStage()함수 실행
            }
            else //백신을 모두 전해준게 아니라면, 콘솔창에 메시지 출력
            {
                Debug.Log("아직 모든 벡신을 건네주지 않았습니다.");
                Debug.Log("건네줘야하는 벡신: " + (2 - playerController2.getGiveVaccine()));
            }
        }
    }
}
