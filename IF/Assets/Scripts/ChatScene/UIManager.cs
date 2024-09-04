using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class UIManager : MonoBehaviour
{
    public GameObject chatPanel;
    SendMessageToVue SendMessageToVue;

    // Unity 시작시 호출되는 메소드
    public void Start()
    {
        SendMessageToVue = GameObject.Find("ButtonSend").GetComponent<SendMessageToVue>();
        chatPanel.gameObject.SetActive(false);
    }

    public void ButtonStartPressed() // '게임 시작' 버튼 눌렀을 때
    {
        chatPanel.gameObject.SetActive(true);
        SendMessageToVue.SendUserMessage("0[SceneNumber]안녕하세요 원영씨! 처음 뵙겠습니다. 윤서한테 연락처 받고 연락드려요. 당신과 소개팅하게 된 이호준이라고 해요!");
    }
}
