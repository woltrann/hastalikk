using UnityEngine;

public class FoodLevelManager : MonoBehaviour
{
    public FoodChecker[] foodCheckers;   // Inspector’dan atıyorsun
    public UIStarBurst starBurst;
    public RectTransform starsOrigin;
    public GameObject nextStage;
    public bool end = false;
    private void Update()
    {
        if (end)
            return;
        CheckAllFoods();
    }
    public void CheckAllFoods()
    {
        foreach (FoodChecker fc in foodCheckers)
        {
            if (!fc.IsCorrect())
            {
                Debug.Log("En az bir besin yanlış!");
                return;
            }
        }

        // Buraya ulaştıysa her şey doğru
        OnAllCorrect();
    }

    private void OnAllCorrect()
    {
        Debug.Log("TÜM BESİNLER DOĞRU!");
        // 🔥 BURADA İSTEDİĞİN FONKSİYONU ÇALIŞTIR
        // konfeti, alkış sesi, panel açma, level geçme vs.
        Vector2 anchored = starsOrigin.anchoredPosition;
        starBurst.Burst(anchored);
        nextStage.SetActive(true);
        end = true;
    }
}
