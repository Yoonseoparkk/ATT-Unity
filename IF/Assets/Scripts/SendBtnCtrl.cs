using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SendBtnCtrl : MonoBehaviour
{
    InputField UserSendField;
    Button ButtonSend;

    void Start()
    {
        UserSendField = gameObject.GetComponent<InputField>();
        ButtonSend = GameObject.Find("ButtonSend").GetComponent<Button>();
    }

    private void Update()
    {
        if (UserSendField.text.Trim() == "")
        {
            ButtonSend.GetComponentInParent<Image>().color = new Color32(200, 200, 200, 255);
            ButtonSend.enabled = false;
        }
        else
        {
            ButtonSend.GetComponentInParent<Image>().color = new Color32(255, 228, 1, 255);
            ButtonSend.enabled = true;
        }
    }
}
