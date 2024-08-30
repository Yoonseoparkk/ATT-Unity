using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using Unity.VisualScripting;

public class SendMessageToVue : MonoBehaviour
{
    ChatManager ChatManager;
    public InputField userSendText;
    Text placeHolder;

    [DllImport("__Internal")]
    private static extern void UnityEvent(string message);

    void Start()
    {
        ChatManager = GetComponent<ChatManager>();

        // InputField의 onEndEdit 이벤트에 메서드 연결
        userSendText.onEndEdit.AddListener(HandleInputFieldEndEdit);

        placeHolder = GameObject.Find("Placeholder").GetComponent<Text>();

        // 시작할 때 InputField에 포커스 주기
        FocusInputField();
    }

    void Update()
    {
        // 매 프레임마다 InputField의 커서 깜박임 갱신
        if (userSendText.isFocused)
        {
            userSendText.ForceLabelUpdate();
            placeHolder.text = "";
        }
        else
        {
            placeHolder.text = "  메시지를 입력하세요.";
        }
    }

    // Enter 키 입력 처리 메서드
    private void HandleInputFieldEndEdit(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SendMessage();
        }
    }

    public void ButtonSendPressed() // '전송' 버튼 눌렀을 때
    {
        SendMessage();
    }

    // 메시지 전송 로직을 별도의 메서드로 분리
    private void SendMessage()
    {
        if (!string.IsNullOrEmpty(userSendText.text))
        {
            ChatManager.Chat(true, userSendText.text, "나", null);
            SendUserMessage(userSendText.text);
            userSendText.text = ""; // 전송하면 text input field 내용 지우기
            FocusInputField(); // 메시지 전송 후 InputField에 다시 포커스
        }
    }

    // InputField에 포커스를 주는 메서드
    private void FocusInputField()
    {
        userSendText.Select();
        userSendText.ActivateInputField();
    }

    private void ButtonAcitiveCtrl()
    {
    if (userSendText.text.Trim() == "")
        {
            GetComponent<Image>().color = new Color32(200, 200, 200, 255);
            GetComponent<Button>().enabled = false;
        }
        else
        {
            GetComponent<Image>().color = new Color32(255, 228, 1, 255);
            GetComponent<Button>().enabled = true;
        }

    }


// JavaScript 함수를 호출하는 메서드
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