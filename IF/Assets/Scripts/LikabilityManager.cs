using UnityEngine;
using System.Collections.Generic;

public class LikabilityManager : MonoBehaviour
{
    public float currentLikability = 50f; // 0 to 100
    public float minLikability = 0f;
    public float maxLikability = 100f;

    private Dictionary<string, float> keywordLikability = new Dictionary<string, float>()
    {
        {"안녕", 2f},
        {"고마워", 5f},
        {"좋아해", 10f},
        {"싫어", -5f},
        {"바보", -10f}
        // 더 많은 키워드와 호감도 변화값 추가
    };

    public void UpdateLikability(string message)
    {
        foreach (var keyword in keywordLikability.Keys)
        {
            if (message.Contains(keyword))
            {
                currentLikability += keywordLikability[keyword];
                currentLikability = Mathf.Clamp(currentLikability, minLikability, maxLikability);
                break; // 첫 번째 일치하는 키워드에 대해서만 호감도 변경
            }
        }
    }

    public float GetNormalizedLikability()
    {
        return (currentLikability - minLikability) / (maxLikability - minLikability);
    }
}