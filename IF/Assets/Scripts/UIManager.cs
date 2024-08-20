using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class UIManager : MonoBehaviour
{
    public GameObject chatPanel;

    [DllImport("__Internal")]
    private static extern void UnityEvent(string message);

    // Unity 시작시 호출되는 메소드
    public void Start()
    {
        chatPanel.gameObject.SetActive(false);
    }

    public void ButtonStartPressed() // '게임 시작' 버튼 눌렀을 때
    {
        SendMessageToVue("Start chat with your ideal type!");
        chatPanel.gameObject.SetActive(true);
    }

    // -----------------------------------------------------------------

    // JavaScript 함수를 호출하는 메소드
    public void SendMessageToVue(string message)
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
