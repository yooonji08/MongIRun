using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController4 : MonoBehaviour //Final Stage씬의 dog오브젝트와 연결되는 스크립트
{
    public GameObject playerBullet; //총알 프리팹 오브젝트
    public Transform playerPos; //플레이어의 위치
    public Text timeComp; //시간을 나타낼 Text컴포넌트 변수
    AudioSource audioSrc; //AudioSource컴포넌트 변수
    public AudioClip clip1; //총알 발사하는 소리를 위한 AudioClip컴포넌트 변수
    Animator animator; //강아지 애니메이션을 위한 Animator컴포넌트 변수
    private float time; //타이머 변수(초 단위)
    private float bulletSpeed = 10.0f; //총알 이동 속도
    Player player; //Player클래스의 객체 생성

    void Start()
    {
        time = 60f; //1분을 타이머 값으로 설정
        audioSrc = GetComponent<AudioSource>();
        this.animator = GetComponent<Animator>(); //강아지 에니메이션을 위해 Animator속성으로 초기화
        //dog컴포넌트에 부착되어있는 Player스크립트에서 메소드를 가져오기 위해 객체에 할당
        player = GameObject.Find("dog").GetComponent<Player>();
    }

    void Update()
    {
        palyerBullet(); //총알 생성 및 발사하는 함수

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

    void palyerBullet()
    {
        if (Input.GetKeyDown(KeyCode.X)) //X키를 눌렀다면
        {
            //총알을 playerPos의 위치에 playerPos의 회전값을 가지도록 복제
            GameObject newBullet = Instantiate(playerBullet, playerPos.transform.position, playerPos.transform.rotation);
            Rigidbody2D bulletComp = newBullet.GetComponent<Rigidbody2D>();

            //강아지가 바라보는 방향을 총알 발사 위치로 설정
            if (transform.localScale.x == 6.5) //강아지 오브젝트의 크기가 6.5라면(=오른쪽을 바라본다면)
            {
                animator.SetTrigger("isBark"); //강아지 짖기 애니메이션
                Vector2 pos = new Vector2(100.0f, 0);
                bulletComp.AddForce(pos * bulletSpeed); //오른쪽으로 총알 발사
            }
            else if (transform.localScale.x == -6.5) //강아지 오브젝트의 크기가 -6.5라면(=왼쪽을 바라본다면)
            {
                animator.SetTrigger("isBark"); //강아지 짖기 애니메이션
                Vector2 pos = new Vector2(-100.0f, 0);
                bulletComp.AddForce(pos * bulletSpeed); //왼쪽으로 총알 발사
            }
            audioSrc.PlayOneShot(clip1, 1.5f); //총알 발사 소리 출력
            Destroy(newBullet, 5f); //5초 후 공 제거
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //충돌한 순간
    {
        //virus 오브젝트 또는 virus의 총알과 충돌한 순간
        if (collision.name.Contains("virus") || collision.name.Contains("virusBullet"))
        {
            player.puppyMinusHP(); //체력 감소
            Debug.Log("강아지 체력 감소 -10, 현재 체력: " + player.getHP());
        }
        //열린문 오브젝트와 충돌한 순간
        else if (collision.name.Contains("openDoor"))
        {
            SceneManager.LoadScene("GameClear"); //게임클리어 씬으로 이동
        }
    }
}
