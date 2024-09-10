using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System;

public class TextFromVue : MonoBehaviour
{
    ChatManager ChatManager;
    MeetingManager MeetingManager;

    string chatMessage;
    string meetDate;
    string meetPlace;

    public void Start()
    {
        ChatManager = GetComponent<ChatManager>();
        MeetingManager = GetComponent<MeetingManager>();
    }

    // JavaScript에서 호출할 수 있는 Unity 메서드
    public void VueEvent(string message)
    {
        Debug.Log("Message received from JavaScript: " + message);
        chatMessage = message;

        try
        {
            ChatManager.Chat(false, chatMessage, "이지은", Resources.Load<Texture2D>("aiGirl"));
        }
        catch (NullReferenceException ex)
        { 
            print(ex);
        }

        chatMessage = "";
    }

    // JavaScript에서 호출할 수 있는 Unity 메서드
    public void DateEvent(string date)
    {
        Debug.Log("Message received from JavaScript: " + date);

        if (date == "" || date.Contains("없") || date.Contains("none"))
        {
            meetDate = "미정";   // 아직 약속 날짜가 정해지지 않고 데이터를 받아올 경우
        }
        else
        {
            if (MeetingManager.meetingDay.gameObject.activeSelf)   // 퀘스트 활성화 됐을 때만 적용
            {
                meetDate = "O";
            }
        }

        try
        {
            MeetingManager.meetingDay.text = meetDate;
        }
        catch (NullReferenceException ex)
        {
            print(ex);
        }

        meetDate = "";
    }

    // JavaScript에서 호출할 수 있는 Unity 메서드
    public void PlaceEvent(string place)
    {
        Debug.Log("Message received from JavaScript: " + place);

        if (place == "" || place.Contains("없") || place.Contains("none"))
        {
            meetPlace = "미정";   // 아직 약속 장소가 정해지지 않고 데이터를 받아올 경우
        }
        else
        {
            if (MeetingManager.meetingPlace.gameObject.activeSelf)   // 퀘스트 활성화 됐을 때만 적용
            {
                meetPlace = "O";
            }
        }

        try
        {
            MeetingManager.meetingPlace.text = meetPlace;
        }
        catch (NullReferenceException ex)
        {
            print(ex);
        }

        meetPlace = "";
    }
}
