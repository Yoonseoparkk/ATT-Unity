using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class SendMessageToVue : MonoBehaviour
{
    ChatManager ChatManager;
    SendBtnCtrl SendBtnCtrl;

    public InputField userSendText;

    [DllImport("__Internal")]
    private static extern void UnityEvent(string message);

    void Start()
    {
        ChatManager = GetComponent<ChatManager>();
    }

    public void ButtonSendPressed() // '전송' 버튼 눌렀을 때
    {
        ChatManager.Chat(true, userSendText.text, "나", null);
        SendUserMessage(userSendText.text);

        userSendText.text = "";     // 전송하면 text input field 내용 지우기
    }

    // JavaScript 함수를 호출하는 메소드
    public void SendUserMessage(string message)
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        try
        {
            UnityEvent(message);    // JavaScript에서 정의된 함수 호출
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to call UnityEvent: " + e.Message);
        }

        #else
        Debug.Log("Sending to Vue: " + message);
        #endif
    }
}
