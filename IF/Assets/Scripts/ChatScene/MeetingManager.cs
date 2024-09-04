using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeetingManager : MonoBehaviour
{
    public Text guidance;
    public Text meetingPlace;
    public Text meetingDay;

    public bool letsMeet = false;

    void Update()
    {
        if (letsMeet)
        {
            guidance.text = "약속 장소와 시간을 정해\n실제 만남으로 나아가보세요!";
            meetingPlace.gameObject.SetActive(true);
            meetingDay.gameObject.SetActive(true);
            letsMeet = false;
        }
    }


    private Dictionary<string, string> keywordMeetingPlace = new Dictionary<string, string>()
    {
        // 약속 장소 결정 키워드
        {"에서 보는 거 어때요?", "광화문"},
        {"에서 만나요", "광화문"},
    };
    
    private Dictionary<string, string> keywordMeetingDay = new Dictionary<string, string>()
    {
        // 약속 날짜 결정 키워드
        {"요일이 편해요", "토요일"},
        {"요일 좋", "토요일"},
        {"요일 어때", "토요일"},
        {"요일에 봐요", "토요일"},
    };

    public void UpdateMeetingPlace(string message)
    {
        foreach (var keyword in keywordMeetingPlace.Keys)
        {
            if (message.Contains(keyword))
            {
                meetingPlace.text = keywordMeetingPlace[keyword];
                break; // 첫 번째 일치하는 키워드에 대해서만 약속 장소 반영
            }
        }
    }
    
    public void UpdateMeetingDay(string message)
    {
        foreach (var keyword in keywordMeetingDay.Keys)
        {
            if (message.Contains(keyword))
            {
                meetingDay.text = keywordMeetingDay[keyword];
                break; // 첫 번째 일치하는 키워드에 대해서만 약속 날짜 반영
            }
        }
    }
}
