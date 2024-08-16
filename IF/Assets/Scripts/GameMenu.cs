using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public GameObject exit_Q;

    void Start()
    {
        exit_Q.gameObject.SetActive(false); // 스테이지 진행중 팝업X
    }

    public void ButtonExitPressed() // '게임 종료' 버튼 눌렀을 때
    {
        exit_Q.gameObject.SetActive(true);  // 종료 여부 팝업
    }

    public void ButtonYesPressed()   // 종료 여부 팝업에서 예 눌렀을 때
    {
        // Vue.js의 'unityEvent' 메소드를 호출
        // Application.ExternalCall("unityEvent", "Hello from Unity!");
        exit_Q.gameObject.SetActive(false); // 팝업 닫기
    }

    public void ButtonNoPressed()   // 종료 여부 팝업에서 아니오 눌렀을 때
    {
        exit_Q.gameObject.SetActive(false); // 팝업 닫기
    }
}
