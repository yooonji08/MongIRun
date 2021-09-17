using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour //모든 Stage씬의 dog컴포넌트에 부착되는 스크립트
{
    Rigidbody2D rigid2D;
    Animator animator; //Animator속성 변수
    AudioSource audioSrc;
    public AudioClip clip1; //강아지 점프하는 소리
    public Image imgComp; //UI에 있는 강아지의 체력바 불러오는 Image컴포넌트 변수
    private float jumpForce = 675.0f; //강아지의 점프 힘
    private float walkForce = 30.0f; //강아지의 걷는 힘
    private float maxWalkSpeed = 4.0f; //강아지의 최대 걷는 속도
    private int hp = 100; //강아지의 체력 변수
    public GameObject PopUp; //UI에 있는 PopUp오브젝트 변수

    public int getHP() //전용 변수 hp값을 반환하는 get함수
    {
        return hp;
    }

    void closePopUp()
    {
        PopUp.SetActive(false); //팝업창 비활성화
    }

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>(); //강아지 충돌 처리를 위해 컴포넌트 연결
        this.animator = GetComponent<Animator>(); //강아지 에니메이션을 위해 Animator속성으로 초기화
        audioSrc = GetComponent<AudioSource>();
        PopUp.SetActive(true); //게임 실행시 팝업창 띄우기
        Invoke("closePopUp", 2f); //2초 뒤에 closePopUp()함수 이동
    }

    void Update()
    {
        if (!ButtonUI.isPause) // pause버튼을 누르지 않았다면
        {
            puppyMove(); // 강아지 움직이기
        }
        // 강아지가 화면을 벗어나면 게임오버 UI화면으로 이동
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    // 시작 체력 100, 최대 체력 105(Stage1에만 해당)
    public void puppyPlusHP() // 체력 증가 관리하는 함수
    {
        this.hp += 1; // 체력 +1
        imgComp.fillAmount += 0.01f; // 강아지의 체력 게이지바 1%만큼 증가
    }

    // 시작 체력 100, 최대 체력 100
    public void puppyMinusHP() // 체력 감소 관리하는 함수
    {
        this.hp -= 7; // 체력 -7
        imgComp.fillAmount -= 0.07f; // 강아지의 체력 게이지바 7%만큼 감소

        if (hp <= 0) // hp가 0이하라면 게임오버
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    void puppyMove() // 강아지의 움직임을 제어하는 함수
    {
        animator.speed = 2.0f; // 강아지 눈 깜빡임 속도

        //점프
        if (Input.GetKeyDown(KeyCode.Z) && rigid2D.velocity.y == 0) // y축 속도가 0이고(이중점프 방지), 알파벳 Z키를 누른다면 강아지 점프
        {
            animator.SetTrigger("JumpTrigger"); // 점프 애니메이션 활성화
            rigid2D.AddForce(transform.up * jumpForce); // 다음 힘의 크기만큼 점프
            audioSrc.PlayOneShot(clip1, 1.5f); // 점프소리 출력
        }
        else if (Input.GetKey(KeyCode.DownArrow)) // 아래쪽 방향키를 눌렀다면
        {
            animator.SetTrigger("isStareDown"); // 강아지의 시선을 아래로 이동
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetTrigger("isStareUp"); // 강아지의 시선을 위로 이동
        }

        //좌우이동
        int key = 0; // 강아지 이동 방향을 구하기 위한 변수
        if (Input.GetKey(KeyCode.LeftArrow)) // 왼쪽 방향키를 누르는 동안 강아지가 왼쪽으로 이동
        {
            transform.localScale = new Vector3(-1 * 6.5f, 6.5f, 6.5f); // 이미지 반전
            this.animator.SetBool("isWalk", true); // 걷기 애니메이션 실행
            key = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow)) // 오른쪽 방향키를 누르는 동안 강아지가 오른쪽으로 이동
        {
            transform.localScale = new Vector3(6.5f, 6.5f, 6.5f); // 이미지 반전
            this.animator.SetBool("isWalk", true);  // 걷기 애니메이션 실행
            key = 1;
        }
        else // 아무것도 안눌렀다면 걷기 애니메이션 중지
        {
            this.animator.SetBool("isWalk", false);
        }

        // 강아지 속도 제한, x축의 속도를 절대값으로 만들어서 양수의 값을 speed변수에 할당
        float speed = Mathf.Abs(rigid2D.velocity.x);

        // 스피드 제한, 좌우로 이동시키기
        if (speed < maxWalkSpeed)
        {
            rigid2D.AddForce(key * walkForce * transform.right);
        }
        
        // 강아지 속도에 맞춰 애니메이션 속도 바꾸기
        if (rigid2D.velocity.y == 0) //y축의 속도가 0이고
        {
            animator.SetFloat("JumpSpeed", speed / 2.0f); //제자리 점프가 아니라면
        }
        else
        {
            animator.SetFloat("JumpSpeed", 1.0f); //제자리 점프
        }
    }
}
