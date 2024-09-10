using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using DG.Tweening.Plugins;

public class SendMessageToVue : MonoBehaviour
{
    ChatManager ChatManager;
    public InputField userSendText;
    public Image sendTextBtn;
    Text placeHolder;

    public bool endChat = false;

    [DllImport("__Internal")]
    private static extern void UnityEvent(string message);

    void Start()
    {
        SendUserMessage("0[SceneNumber]안녕하세요 지은씨! 처음 뵙겠습니다. 윤서한테 연락처 받고 연락드려요. 당신과 소개팅하게 된 이호준이라고 해요!");
        
        ChatManager = GameObject.Find("GameManager").GetComponent<ChatManager>();

        // InputField의 onEndEdit 이벤트에 메서드 연결
        userSendText.onEndEdit.AddListener(HandleInputFieldEndEdit);

        placeHolder = GameObject.Find("Placeholder").GetComponent<Text>();

        // 시작할 때 InputField에 포커스 주기
        FocusInputField();
    }

    void Update()
    {
        ButtonActiveCtrl();
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
            SendUserMessage("0[SceneNumber]" + userSendText.text);

            // 퀘스트 만족 후 마무리 멘트 후 씬 이동
            if (userSendText.text.Contains("그럼 그"))
            {
                endChat = true;
            }

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

    private void ButtonActiveCtrl()
    {
        if (userSendText.text.Trim() == "")
        {
            sendTextBtn.color = new Color32(200, 200, 200, 255);
            sendTextBtn.gameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            sendTextBtn.color = new Color32(255, 228, 1, 255);
            sendTextBtn.gameObject.GetComponent<Button>().enabled = true;
        }
    }

    public void SendUserMessage(string message)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    try
    {
        // 두 매개변수를 JavaScript로 전달
        UnityEvent(message);
    }
    catch (System.Exception e)
    {
        Debug.LogError("Failed to call UnityEvent: " + e.Message);
    }
#else
        Debug.Log($"Sending to Vue message: {message}");
#endif
    }
}