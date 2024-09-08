using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Runtime.InteropServices;

public class SceneController : MonoBehaviour
{
    public Image fadePanel;
    public Text description;
    //private LikabilityManager likabilityManager;
    //private MeetingManager meetingManager;


    void Start()
    {
        fadePanel.DOFade(1, 0f);
        fadePanel.DOFade(0, 1.5f); // Fade In
        //likabilityManager = GetComponent<LikabilityManager>();
        //meetingManager = GetComponent<MeetingManager>();
    }

    void Update()
    {
        //if (likabilityManager.GetNormalizedLikability() >= 0.5f && meetingManager.meetingPlace.text != "???" && meetingManager.meetingDay.text != "?요일")
        //{
        //    MoveToCafeScene();
        //}
    }

    public void MoveToChatScene()
    {
        StartCoroutine(MoveScene("ChatScene", ""));
    }

    public void MoveToCafeScene()
    {
        StartCoroutine(MoveScene("CafeScene", ""));
    }

    public void MoveToMenuScene()
    {
        StartCoroutine(MoveScene("MenuScene", ""));
    }


    IEnumerator MoveScene(string SceneName, string descriptionText)
    {
        fadePanel.DOFade(1, 1.5f); // Fade out
        description.DOFade(1, 1.5f);
        description.text = descriptionText;
        GameObject.Find("Main Camera").GetComponent<AudioSource>().DOFade(0f, 3f);
        yield return new WaitForSeconds(3f);  // 지체할 시간 (초)
        SceneManager.LoadScene(SceneName);
        yield break; // 코루틴 종료 
    }
}
