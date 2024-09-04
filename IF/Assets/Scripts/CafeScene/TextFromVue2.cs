using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System;
using DG.Tweening;
using static System.Net.Mime.MediaTypeNames;

public class TextFromVue2 : MonoBehaviour
{
    public GameObject loadingUI;
    public UnityEngine.UI.Text chatBotMessage;
    string chatMessage;

    private LikabilityManager2 likabilityManager;

    void Start()
    {
        likabilityManager = GameObject.Find("Gauge").GetComponent<LikabilityManager2>();
    }

    // JavaScript에서 호출할 수 있는 Unity 메서드
    public void VueEvent(string message)
    {
        Debug.Log("Message received from JavaScript: " + message);
        chatMessage = message;

        try
        {
            loadingUI.SetActive(false);
            chatBotMessage.DOText(chatMessage, 10f).SetSpeedBased();

            likabilityManager.UpdateLikability(chatMessage);    // 호감도 업데이트
        }
        catch (NullReferenceException ex)
        { 
            print(ex);
        }

        chatMessage = "";
    }
}
