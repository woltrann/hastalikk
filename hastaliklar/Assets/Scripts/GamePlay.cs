using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    public GameObject[] Stages;
    public int currentStage = 1;
    public int lastStage = 4;
    public RectTransform targetPoint;
    public UIStarBurst starBurst;
    public RectTransform starsOrigin;
    public int firstQuestion = 0;
    public GameObject nextStage;
    public AudioSource clap;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CorrectAnswer(Button btn)
    {
        RectTransform rect = btn.GetComponent<RectTransform>();

        Sequence seq = DOTween.Sequence();

        seq.Append(rect.DOMove(targetPoint.position, 0.5f).SetEase(Ease.InOutQuad));
        seq.Join(rect.DOScale(0f, 0.5f).SetEase(Ease.InBack));

        seq.OnComplete(() =>
        {
            rect.gameObject.SetActive(false);
            firstQuestion++;
            if (firstQuestion > 3)
            {
                Debug.Log("Game Over");
                clap.Play();
                Vector2 anchored = starsOrigin.anchoredPosition;
                starBurst.Burst(anchored);
                nextStage.SetActive(true);
            
            }
        });
        
    }
    public void WrongAnswer(Button btn)
    {
        RectTransform rect = btn.GetComponent<RectTransform>();

        rect.DOShakeAnchorPos(0.5f, 20f, 20, 90, false)
            .SetEase(Ease.Linear);
    }

    public void NextStage()
    {
        Stages[currentStage].SetActive(false);
        currentStage++;
        if (currentStage >= lastStage)
        {
            SceneManager.LoadScene(0);
        }
;
        Stages[currentStage].SetActive(true);
    }
}
