using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiningManager : MonoBehaviour
{
    public Text guidance;
    public Text foodMenu;

    public bool letsEat = false;

    private LikabilityManager2 likabilityManager;
    private SceneController sceneController;

    void Start()
    {
        likabilityManager = GetComponent<LikabilityManager2>();
        sceneController = GetComponent<SceneController>();
    }

    void Update()
    {
        if (letsEat)
        {
            guidance.text = "식사 메뉴를 정해\n다음 장소로 이동하세요!";
            foodMenu.gameObject.SetActive(true);
            letsEat = false;
        }

        if (likabilityManager.GetNormalizedLikability() >= 0.7f && foodMenu.text != "미정")
        {
            if (GetComponent<SendMessageToVue2>().endChat)
            {
                sceneController.MoveToDiningScene();
            }
        }
    }
}
