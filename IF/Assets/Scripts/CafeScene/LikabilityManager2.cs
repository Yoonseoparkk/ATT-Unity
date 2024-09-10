using UnityEngine;
using System.Collections.Generic;

public class LikabilityManager2 : MonoBehaviour
{
    public float currentLikability = 50f; // 0 to 100
    public float minLikability = 0f;
    public float maxLikability = 100f;

    private AudioManager AM;

    void Start()
    {
        AM = GetComponent<AudioManager>();
    }

    private Dictionary<string, float> keywordLikability = new Dictionary<string, float>()
    {
        // 호감도 증가 키워드
        {"사랑", 5f},
        {"설레", 5f},
        {"멋져", 5f},
        {"이뻐", 5f},
        {"예뻐", 5f},
        {"기대", 5f},
        {"따뜻", 5f},
        {"믿어", 5f},
        {"행복", 5f},
        {"고마워", 5f},
        {"감사", 5f},
        {"좋", 5f},
        {"신나", 5f},
        {"같이", 5f},
        {"가자", 5f},
        {"가실", 5f},
        {"친절", 5f},
        {"재미", 5f},
        {"공감", 5f},
        {"맞아", 5f},
        {"그래~", 5f},
        {"편안", 5f},
        {"덕분", 5f},
        {"웃음", 5f},
        {"웃겨", 5f},
        {"ㅎㅎ", 5f},
        {"~", 5f},
        {"!", 5f},

        // 호감도 감소 키워드
        {"최악", -5f},
        {"무례", -5f},
        {"후회", -5f},
        {"싫어", -5f},
        {"별로", -5f},
        {"짜증", -5f},
        {"나빠", -5f},
        {"불쾌", -5f},
        {"피곤", -5f},
        {"따분", -5f},
        {"지루", -5f},
        {"슬퍼", -5f},
        {"어색", -5f},
        {"막막", -5f},
        {"더럽", -5f},
        {"귀찮", -5f},
        {"얄미", -5f},
        {"부담", -5f},
        {"불편", -5f},
        {"열받", -5f},
        {"속상", -5f},
        {"지겹", -5f},
        {"지겨", -5f},
        {"불만", -5f},
        {"서운", -5f},
        {"우울", -5f},
        {"쓸쓸", -5f},
        {"난처", -5f},
        {"답답", -5f},
        {"당황", -5f},
        {"성급", -5f},
        {"착각", -5f},
    };

    public void UpdateLikability(string message)
    {
        foreach (var keyword in keywordLikability.Keys)
        {
            if (message.Contains(keyword))
            {
                if (keywordLikability[keyword] < 0)
                {
                    AM.audio.clip = AM.ac[2];
                    AM.audio.Play();    // 호감도 감소 사운드
                }
                else
                {
                    AM.audio.clip = AM.ac[1];
                    AM.audio.Play();    // 호감도 상승 사운드
                }
                currentLikability += keywordLikability[keyword];
                currentLikability = Mathf.Clamp(currentLikability, minLikability, maxLikability);
                break; // 첫 번째 일치하는 키워드에 대해서만 호감도 변경
            }
        }

        if (currentLikability >= 70f)   // 식사 메뉴 정하기 퀘스트 활성화
        {
            GetComponent<DiningManager>().letsEat = true;
        }
    }

    public float GetNormalizedLikability()
    {
        return (currentLikability - minLikability) / (maxLikability - minLikability);
    }
}