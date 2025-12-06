using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] pieces;       // Puzzle parçaları
    public RectTransform[] areas;     // Parçaların yerleşeceği alanlar
    private int selectedPieceID = -1; // Şu an seçili olan parça yok

    private int correctPlacedCount = 0;   // Doğru yerleşen parça sayısı

    public UIStarBurst starBurst;
    public RectTransform starsOrigin;
    public GameObject nextStage;

    // Parça butonundan çağrılır
    public void SelectPiece(int pieceID)
    {
        selectedPieceID = pieceID;
        Debug.Log("Seçilen Parça: " + pieceID);
    }

    // Alan butonundan çağrılır
    public void SelectArea(int areaID)
    {
        if (selectedPieceID == -1)
        {
            Debug.Log("Hiç parça seçili değil.");
            return;
        }

        if (selectedPieceID == areaID)
        {
            // Doğru eşleşme → parçayı alana yerleştir
            pieces[selectedPieceID].transform.position = areas[areaID].position;

            Debug.Log("Parça doğru alana yerleşti!");

            correctPlacedCount++; // doğru yerleşme sayısını artır
            selectedPieceID = -1;

            CheckComplete(); // tüm parçalar tamam mı?
        }
        else
        {
            Debug.Log("Yanlış eşleşme!");
        }
    }

    // Tüm parçalar doğru yerleştirildiğinde tetiklenen fonksiyon
    private void CheckComplete()
    {
        if (correctPlacedCount == pieces.Length)
        {
            Debug.Log("✨ TÜM PARÇALAR TAMAMLANDI! ✨");
            PuzzleCompleted();
        }
    }

    // → Bölüm bitince yapılacaklar
    private void PuzzleCompleted()
    {
        Debug.Log("BÖLÜM BİTTİ! Fonksiyon çalıştı!");

        Vector2 anchored = starsOrigin.anchoredPosition;
        starBurst.Burst(anchored);
        nextStage.SetActive(true);
    }
}
