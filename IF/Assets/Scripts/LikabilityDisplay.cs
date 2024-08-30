using UnityEngine;
using UnityEngine.UI;

public class LikabilityDisplay : MonoBehaviour
{
    public Image gaugeImage;
    private LikabilityManager likabilityManager;

    void Start()
    {
        likabilityManager = FindObjectOfType<LikabilityManager>();
    }

    void Update()
    {
        if (likabilityManager != null && gaugeImage != null)
        {
            gaugeImage.fillAmount = likabilityManager.GetNormalizedLikability();
        }
    }
}