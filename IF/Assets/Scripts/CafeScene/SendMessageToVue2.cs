using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using Unity.VisualScripting;

public class SendMessageToVue2 : MonoBehaviour
{
    public GameObject loadingUI;
    public InputField userSendText;
    public Text chatBotMessage;
    Text placeHolder;

    [DllImport("__Internal")]
    private static extern void UnityEvent(string message);

    void Start()
    {
        SendUserMessage("1[SceneNumber]원영씨 많이 기다리셨어요? 오는 길에 차가 조금 막혀서.. 실물로 뵙게 되니 더 미인이시네요!");

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
            placeHolder.text = " 상대방에게 말을 걸어보세요.";
        }
    }

    // Enter 키 입력 처리 메서드
    private void HandleInputFieldEndEdit(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SendMessage();
            loadingUI.SetActive(true);
            chatBotMessage.text = "";
        }
    }

    // InputField에 포커스를 주는 메서드
    private void FocusInputField()
    {
        userSendText.Select();
        userSendText.ActivateInputField();
    }

    // 메시지 전송 로직을 별도의 메서드로 분리
    private void SendMessage()
    {
        if (!string.IsNullOrEmpty(userSendText.text))
        {
            SendUserMessage("1[SceneNumber]"+userSendText.text);
            userSendText.text = ""; // 전송하면 text input field 내용 지우기
            FocusInputField(); // 메시지 전송 후 InputField에 다시 포커스
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
        Debug.Log("Sending to Vue message: " + message);
#endif
    }
}