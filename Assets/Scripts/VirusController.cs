using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VirusController : MonoBehaviour //Final Stage씬의 virus컴포넌트에 연결되는 스크립트
{
    Animator animator; //virus의 눈 깜빡임을 위한 애니메이션 변수
    AudioSource audioSrc;
    public AudioClip clip1; //총알 발사 소리
    public AudioClip clip2; //오브젝트가 파괴될 때 나오는 소리 변수
    float bulletSpeed = 15f; //총알 이동속도
    public GameObject enemyBullet; //총알 프리팹 연결
    public GameObject openDoor; //열린 문(openDoor)컴포넌트를 할당
    public Image imgComp; //virus의 체력바 불러오기
    Rigidbody2D rbComp;
    private int hp; //virus의 체력
    public float minX, maxX, minY, maxY; //virus의 최대,최소 이동범위
    private float time = 0f; //virus의 이동, 총알 생성 주기를 위한 변수

    void Start()
    {
        hp = 100; //hp를 100으로 초기화
        this.animator = GetComponent<Animator>();
        rbComp = GetComponent<Rigidbody2D>();
        audioSrc = GetComponent<AudioSource>();
        openDoor.SetActive(false); //openDoor컴포넌트 비활성화
    }

    void Update()
    {
        time += Time.deltaTime;

        if (!ButtonUI.isPause) //pause버튼을 누르지 않았다면
        {
            virusBullet(); //virus오브젝트 움직이기, 총알 발사
        }
    }

    void virusBullet() //virus오브젝트의 움직임과 총알 생성 및 발사를 관리하는 함수
    {
        if (time > 3f) //3초마다 오브젝브 랜덤 회전 및 총알 발사 
        {
            //메인 카메라가 비추는 화면 안에서만 랜덤 이동
            transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), transform.position.z); //랜덤 이동
            rbComp.AddTorque(Random.Range(-90, 90)); //랜덤 회전

            //virus오브젝트 위치에 총알 5개 생성 및 발사
            for (int i = 0; i < 5; i++)
            {
                GameObject newBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity); //총알 생성
                Rigidbody2D bulletComp = newBullet.GetComponent<Rigidbody2D>(); //새로운 총알에 Rigidbody컴포넌트 연결
                Vector2 pos = new Vector2(Random.Range(-50f, 50f), Random.Range(-50f, 50f)); //총알이 발사될 랜덤 위치
                bulletComp.AddForce(pos * bulletSpeed); //bulletSpeed만큼 힘을 가해서 총알 발사
                audioSrc.PlayOneShot(clip1, 0.5f); //총알 발사하는 소리 출력
                Destroy(newBullet, 5f); //5초 후 공 제거
            }
            time = 0; //총알 발사가 끝나면 time을 0으로 바꾸기
        }
    }

    void virusMinusHP() //virus가 파괴 조건을 관리하는 함수
    {
        this.hp -= 5; //체력 -5
        imgComp.fillAmount -= 0.05f; //바이러스의 체력 게이지바 5%만큼 감소

        if (hp <= 0) //hp가 0이하라면
        {
            audioSrc.PlayOneShot(clip2, 1f); //virus오브젝트가 파괴되는 사운드 출력
            openDoor.SetActive(true); //열린 문 오브젝트 실행
            gameObject.SetActive(false); //virus오브젝트 비활성화
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //충돌한 순간
    {
        //강아지가 발사한 총알과 충돌한 순간
        if (collision.name.Contains("puppyBullet"))
        {
            virusMinusHP(); //virus hp 감소
            Debug.Log("바이러스 체력 5% 감소, 현재 체력: " + hp);
        }
    }
}
