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

    private LikabilityManager likabilityManager;
    private SceneController sceneController;

    void Start()
    {
        likabilityManager = GetComponent<LikabilityManager>();
        sceneController = GetComponent<SceneController>();
    }

    void Update()
    {
        if (letsMeet)
        {
            guidance.text = "약속 장소와 시간을 정해\n실제 만남으로 나아가보세요!";
            meetingPlace.gameObject.SetActive(true);
            meetingDay.gameObject.SetActive(true);
            letsMeet = false;
        }

        // 모든 조건이 만족된 후 마무리 인사를 하면 다음 씬으로 이동
        if (likabilityManager.GetNormalizedLikability() >= 0.5f && meetingPlace.text != "미정" && meetingDay.text != "미정")
        {
            if (GameObject.Find("ButtonSend").GetComponent<SendMessageToVue>().endChat)
            {
                sceneController.MoveToCafeScene();
            }
        }
    }
}
