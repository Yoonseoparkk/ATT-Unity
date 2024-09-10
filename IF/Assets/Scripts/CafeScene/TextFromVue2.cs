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
    public GameObject talkingAnim;
    public GameObject idleAnim;

    string foodMenu;

    public UnityEngine.UI.Text chatBotMessage;
    string chatMessage;

    private DiningManager diningManager;
    private LikabilityManager2 likabilityManager;

    private AudioManager AM;

    void Start()
    {
        AM = GetComponent<AudioManager>();
        diningManager = GetComponent<DiningManager>();
        likabilityManager = GetComponent<LikabilityManager2>();
    }

    // JavaScript에서 호출할 수 있는 Unity 메서드
    public void VueEvent(string message)
    {
        Debug.Log("Message received from JavaScript: " + message);
        chatMessage = message;

        try
        {
            AM.audio.clip = AM.ac[0];
            AM.audio.Play();

            StartCoroutine(AnimCtrl());
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

    // JavaScript에서 호출할 수 있는 Unity 메서드
    public void FoodMenuEvent(string menu)
    {
        Debug.Log("Message received from JavaScript: " + menu);

        if (menu == "" || menu.Contains("없") || menu.Contains("none"))
        {
            foodMenu = "미정";   // 아직 식사 메뉴가 정해지지 않고 데이터를 받아올 경우
        }
        else
        {
            if (diningManager.foodMenu.gameObject.activeSelf)   // 퀘스트 활성화 됐을 때만 적용
            {
                foodMenu = "O";
            }
        }

        try
        {
            diningManager.foodMenu.text = foodMenu;
        }
        catch (NullReferenceException ex)
        {
            print(ex);
        }

        foodMenu = "";
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
