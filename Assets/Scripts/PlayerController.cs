using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour //Stage1씬의 dog오브젝트와 연결되는 스크립트
{
    GameObject item; //아이템 오브젝트를 연결할 GameObject변수
    public Text textComp; //뼈다귀 얻은 개수(score)를 UI에 나타낼 Text컴포넌트 변수
    AudioSource audioSrc; //AudioSource컴포넌트 변수
    public AudioClip clip; //아이템 얻는 소리
    private int score; //뼈다귀 얻은 개수 변수
    Player player; //Player클래스의 객체 생성

    public int getScore() //전용변수 score의 get함수
    {
        return score; //score값 리턴
    }

    void Start()
    {
        score = 0; //점수를 게임 실행할 때 항상 0으로 값 초기화
        item = GameObject.Find("boneItem");
        gameObject.SetActive(true); //dog오브젝트 실행
        textComp.text = score.ToString(); //UI의 텍스트를 score변수 값으로 지정, int값을 문자열로 바꾸기
        audioSrc = GetComponent<AudioSource>();
        //dog컴포넌트에 부착되어있는 Player스크립트에서 메소드를 가져오기 위해 객체에 할당
        player = GameObject.Find("dog").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision) //충돌한 순간
    {
        //뼈다귀 아이템과 부딪친 순간
        if (collision.name.Contains("boneItem"))
        {
            audioSrc.PlayOneShot(clip, 4f); //소리 출력
            textComp.text = (++score).ToString(); //UI의 텍스트에 뼈다귀 얻은 개수(score) 추가
            player.puppyPlusHP(); //체력 증가
            Debug.Log("강아지 체력 증가 +1, 현재 체력: " + player.getHP());
        }
        //enemy오브젝트와 부딪친 순간
        else if (collision.name.Contains("enemy"))
        {
            player.puppyMinusHP(); //체력 감소
            Debug.Log("강아지 체력 감소 -10, 현재 체력: " + player.getHP());
        }
    }

}
