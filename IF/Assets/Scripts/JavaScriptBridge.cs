using UnityEngine;
using System.Runtime.InteropServices;

public class JavaScriptBridge : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void UnityEvent(string message);

    public void Start()
    {
        SendMessageToVue("Hello from Unity!");
    }

    public void SendMessageToVue(string message)
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            UnityEvent(message);
        #else
            Debug.Log("Sending to Vue: " + message);
        #endif
    }
}