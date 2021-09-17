using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour //모든 버튼에 연결되는 스크립트
{
    public static bool isPause = false; //pause버튼 활성화/비활성화를 위한 bool변수

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //키보드 Esc키를 눌렀다면
        {
            SceneManager.LoadScene("Menu"); //Menu 씬으로 이동
        }
    }

    //Main씬에서 사용되는 버튼
    public void ClickStartBtn() //게임시작 버튼을 눌렀다면
    {
        SceneManager.LoadScene("Stage1");
    }

    public void ClickTutorialBtn() //게임방법 버튼을 눌렀다면
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ClickStoryBrn() //게임이야기 버튼을 눌렀다면
    {
        SceneManager.LoadScene("GameStory");
    }

    //Stage씬에서 사용되는 버튼
    public void pauseButton() //pause버튼
    {
        if (isPause) //버튼 누르지 안았다면
        {
            Time.timeScale = 1f; //계속 시간 진행
            isPause = false;
        }
        else //눌렀다면
        {
            Time.timeScale = 0; //시간, 오브젝트 일시정지
            isPause = true;
        }
    }

    public void musicButton() //music버튼을 눌렀다면
    {
        AudioListener.pause = !AudioListener.pause; //음악 일시정지
    }

    //GameOver씬에서 사용되는 버튼
    public void ClickRestartBtn() //다시하기 버튼을 눌렀다면
    {
        SceneManager.LoadScene("Stage1");
    }

    public void ClickMenuBtn() //메뉴 버튼을 눌렀다면
    {
        SceneManager.LoadScene("Menu");
    }
}
