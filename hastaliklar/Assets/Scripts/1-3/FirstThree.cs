using UnityEngine;

public class FirstThree : MonoBehaviour
{
    public GameObject[] images;   // img1, img2 ... hepsi
    public UIStarBurst starBurst;
    public RectTransform starsOrigin;
    public GameObject nextStage;

    private bool end = false;

    void Update()
    {
        if (end) return;

        // Tüm objeler aktif mi?
        if (AreAllImagesActive())
        {
            Vector2 anchored = starsOrigin.anchoredPosition;
            starBurst.Burst(anchored);

            nextStage.SetActive(true);

            end = true;
        }
    }

    // DİZİDEKİ TÜM OBJELERİ KONTROL EDEN SİSTEM
    bool AreAllImagesActive()
    {
        foreach (GameObject img in images)
        {
            if (!img.activeSelf)
                return false; // biri bile aktif değil → false
        }
        return true; // hepsi aktif → true
    }
}
