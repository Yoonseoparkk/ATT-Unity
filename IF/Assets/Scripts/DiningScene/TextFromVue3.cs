using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System;
using DG.Tweening;
using static System.Net.Mime.MediaTypeNames;

public class TextFromVue3 : MonoBehaviour
{
    public GameObject talkingAnim;
    public GameObject idleAnim;

    public UnityEngine.UI.Text chatBotMessage;
    string chatMessage;

    void Start()
    {
        chatBotMessage.DOText("식사 너무 맛있게 잘 먹었어요!\n다음에 또 만나고 싶어요.", 10f).SetSpeedBased();
        StartCoroutine(AnimCtrl());
    }

    // JavaScript에서 호출할 수 있는 Unity 메서드
    public void VueEvent(string message)
    {
        Debug.Log("Message received from JavaScript: " + message);
        chatMessage = message;

        try
        {
            StartCoroutine(AnimCtrl());
            chatBotMessage.DOText(chatMessage, 10f).SetSpeedBased();
        }
        catch (NullReferenceException ex)
        { 
            print(ex);
        }

        chatMessage = "";
    }

    IEnumerator AnimCtrl()
    {
        talkingAnim.SetActive(true);
        idleAnim.SetActive(false);
        yield return new WaitForSeconds(5f);  // 지체할 시간 (초)

        talkingAnim.SetActive(false);
        idleAnim.SetActive(true);
        yield return new WaitForSeconds(2f);  // 지체할 시간 (초)

        yield break; // 코루틴 종료 
    }
}
