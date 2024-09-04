using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LikabilityDisplay2 : MonoBehaviour
{
    public Image gaugeImage;
    public Text guageNumber;
    private LikabilityManager2 likabilityManager;

    void Start()
    {
        likabilityManager = GetComponent<LikabilityManager2>();
    }

    void Update()
    {
        if (likabilityManager != null && gaugeImage != null)
        {
            gaugeImage.DOFillAmount(likabilityManager.GetNormalizedLikability(), 1f);
            guageNumber.text = string.Format("호감도 : {0}", (likabilityManager.GetNormalizedLikability()) * 100);
        }
    }
}