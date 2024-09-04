using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LikabilityDisplay : MonoBehaviour
{
    public Image gaugeImage;
    public Text guageNumber;
    public Image UserImage;
    private Texture2D profileSprite;
    private LikabilityManager likabilityManager;

    void Start()
    {
        likabilityManager = GetComponent<LikabilityManager>();
        profileSprite = Resources.Load<Texture2D>("aiGirl");
        UserImage.sprite = Sprite.Create(profileSprite, new Rect(0, 0, profileSprite.width, profileSprite.height), new Vector2(0.5f, 0.5f));
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