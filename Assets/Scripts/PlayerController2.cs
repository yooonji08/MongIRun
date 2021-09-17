using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour //stage2씬의 dog오브젝트와 연결되는 스크립트
{
    GameObject personObj; //화면에 있는 사람 오브젝트
    GameObject person2Obj;
    GameObject person3Obj; //백신을 받은 뒤의 사람 오브젝트
    GameObject person4Obj;
    public Text textComp; //백신 얻은 개수(countVaccine)를 UI에 나타낼 Text컴포넌트 변수
    AudioSource audioSrc;
    public AudioClip clip; //아이템 얻는 소리
    public AudioClip clip2; //사람이 백신을 받고 난 뒤의 소리
    private int vaccine; //현재 가진 백신 개수
    private int giveVaccine; //사람에게 백신 건내준 횟수, flag2 스크립트에서 변수를 사용하기 위해 정적으로 초기화
    private int countVaccine; //백신을 얻은 누적 개수
    private bool getItem = false; //강아지가 백신을 가지고 있는지 확인하기 위한 bool값
    Player player; //Player클래스의 객체 생성

    public int getGiveVaccine() //전용변수 giveVaccine의 get함수
    {
        return giveVaccine; //giveVaccine값 리턴
    }

    void Start()
    {
        vaccine = 0; //게임 실행할 때 항상 0으로 값 초기화
        giveVaccine = 0;
        countVaccine = 0;
        textComp.text = countVaccine.ToString(); //UI의 텍스트를 원하는 값으로 지정, int값을 string으로 바꾸기
        personObj = GameObject.Find("person1-1"); //화면에 있는 게임 오브젝트 찾아서 변수에 할당
        person2Obj = GameObject.Find("person2-1");
        person3Obj = GameObject.Find("person1-2");
        person4Obj = GameObject.Find("person2-2");
        person3Obj.SetActive(false); //백신 받은 뒤의 사람 이미지 비활성화
        person4Obj.SetActive(false);
        audioSrc = GetComponent<AudioSource>();
        //dog컴포넌트에 부착되어있는 Player스크립트에서 메소드를 가져오기 위해 객체에 할당
        player = GameObject.Find("dog").GetComponent<Player>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision) //충돌한 순간
    {
        //백신 아이템 오브젝트와 부딪친 순간
        if (collision.name.Contains("vaccine"))
        {
            getItem = true;
            audioSrc.PlayOneShot(clip, 4f); //소리 출력
            ++vaccine; //현재 얻은 백신 개수 추가
            textComp.text = (++countVaccine).ToString(); //UI에 누적 백신 개수 추가
            Debug.Log("획득한 백신: " + vaccine + "개");
        }
        //만약 enemy 이름을 가진 오브젝트를 만났다면
        else if (collision.name.Contains("enemy"))
        {
            player.puppyMinusHP(); //체력 감소
            Debug.Log("강아지 체력 감소 -10, 현재 체력: " + player.getHP());
        }
    }

    private void OnTriggerStay2D(Collider2D collision) //충돌하는 동안
    {
        //만약 사람과 충돌중이고
        if (collision.name.Contains("person1-1"))
        {
            if (getItem) //백신 아이템과 충돌했던 적이 있다면
            {
                Destroy(personObj); //기존 오브젝트 삭제
                person3Obj.SetActive(true); //새로운 오브젝트 활성화
                audioSrc.PlayOneShot(clip2, 1.0f); //소리 출력
                --vaccine; //현재 가진 백신 개수 감소
                ++giveVaccine; //백신 건네준 횟수 증가
                Debug.Log("건네준 백신: " + giveVaccine + "개");
            }
            
        }
        else if (collision.name.Contains("person2-1"))
        {
            if (getItem)
            {
                Destroy(person2Obj); //기존 오브젝트 삭제
                person4Obj.SetActive(true); //새로운 오브젝트 활성화
                audioSrc.PlayOneShot(clip2, 1.0f); //소리 출력
                --vaccine; //현재 가진 백신 개수 감소
                ++giveVaccine; //백신 건네준 횟수 증가
                Debug.Log("건네준 백신: " + giveVaccine + "개");
            }
        }
    }
}
