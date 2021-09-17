using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//IPointerEnterHandler: 마우스 포인터가 특정 GameObject 위에 있을 때 감지하고 이벤트를 수행하기 위한 인터페이스
//IPointerExitHandler: 마우스 포인터가 GameObject 밖으로 나갈 때 이벤트를 수행하기 위한 인터페이스
public class ButtonType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform button; //버튼 크기를 바꿀 Transform 변수
    Vector3 defaultScale; //기존 버튼 크기

    private void Start()
    {
        defaultScale = transform.localScale; //기존 버튼 크기를 defaultScale에 할당하여 변수 초기화
    }

    public void OnPointerEnter(PointerEventData eventData) //마우스의 포인터가 버튼 위에 있다면
    {
        button.localScale = defaultScale * 1.1f; //버튼 크기 기존에 1.1배
    }
    public void OnPointerExit(PointerEventData eventData) //마우스의 포인터가 버튼 밖에 있다면
    {
        button.localScale = defaultScale; //버튼 기존 크기로 변경
    }
}
