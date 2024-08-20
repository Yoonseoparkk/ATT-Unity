using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class ChatManager : MonoBehaviour
{
    public Text chatMessage;

    // JavaScript에서 호출할 수 있는 Unity 메서드
    public void VueEvent(string message)
    {
        Debug.Log("Message received from JavaScript: " + message);
        chatMessage.text = message;
    }
}
