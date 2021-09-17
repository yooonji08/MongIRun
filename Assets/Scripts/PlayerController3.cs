using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController3 : MonoBehaviour //stage3씬의 dog오브젝트와 연결되는스크립트
{
    GameObject closeDoor; //닫친 문(closeDoor) 오브젝트
    GameObject openDoor; //열린 문(openDoor) 오브젝트
    public Text timeComp; //시간을 나타낼 Text컴포넌트 변수
    AudioSource audioSrc;
    public AudioClip clip; //아이템 얻는 소리
    public AudioClip clip2; //최종문에 도착했을 때 나오는 소리
    private float time; //타이머 변수(초 단위)
    private bool getKey = false; //강아지가 item오브젝트를 가졌는지 확인하기 위한 bool변수
    Player player; //Player클래스의 객체 생성
    
    void Start()
    {
        time = 60f; //타이머 값을 1분으로 초기화
        closeDoor = GameObject.Find("closeDoor");
        openDoor = GameObject.Find("openDoor");
        openDoor.SetActive(false); //열린 문 이미지 비활성화
        audioSrc = GetComponent<AudioSource>();
        //dog컴포넌트에 부착되어있는 Player스크립트에서 메소드를 가져오기 위해 객체에 할당
        player = GameObject.Find("dog").GetComponent<Player>();
    }

    void Update()
    {
        if (time > 0) //0초가 아니라면
            time -= Time.deltaTime; //계속 시간 감소
        else if (time <= 0) //만약 time이 0초 이하의 값이라면, 게임 오버
        {
            SceneManager.LoadScene("GameOver");
        }

        //정수부분만 출력하기 위해 올림함수 사용
        //UI의 텍스트에 시간 보여주기
        timeComp.text = Mathf.Ceil(time).ToString();  
    }

    private void OnTriggerEnter2D(Collider2D collision) //충돌한 순간
    {
        //enemy오브젝트(자동차)와 충돌한 순간
        if (collision.name.Contains("enemy"))
        {
            player.puppyMinusHP(); //체력 감소
            Debug.Log("강아지 체력 감소 -10, 현재 체력: " + player.getHP());
        }
        else if (collision.name.Contains("key")) //key(열쇠)오브젝트와 충돌한 순간
        {
            getKey = true; //bool변수에 true값을 주어 열쇠를 가진 상태로 바꾸기
            audioSrc.PlayOneShot(clip, 4f); //소리 1회 출력
            Debug.Log("열쇠 획득!");
        }
    }

    void GameClear() //씬 이동
    {
        SceneManager.LoadScene("FinalStage");
    }

    private void OnTriggerStay2D(Collider2D collision) //충돌하는 동안
    {
        //만약 door오브젝트와 충돌중이고
        if (collision.name.Contains("door"))
        {
            if (getKey) //key오브젝트와 충돌했던 적이 있다면
            {
                Destroy(closeDoor); //닫친 문 오브젝트 삭제
                openDoor.SetActive(true); //열린 문 오브젝트 활성화
                audioSrc.PlayOneShot(clip2, 0.5f); //소리 출력
                Debug.Log("Stage3 클리어!");
                Invoke("GameClear", 3f); //3초뒤에 GameClear함수로 이동
            }
        }
    }
}
