using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIStarBurst : MonoBehaviour
{
    [Header("Prefab & Parent")]
    public RectTransform starsContainer;
    public GameObject starPrefab;
    public int poolSize = 40;

    [Header("Burst Settings")]
    public int burstCount = 30;
    public float spreadX = 500f;

    [Header("UP Movement")]
    public float upMin = 250f;
    public float upMax = 450f;

    [Header("DOWN Movement")]
    public float fallDistance = 600f;
    public float fallDuration = 0.8f;

    [Header("Durations")]
    public float popDuration = 0.15f;
    public float upDuration = 0.4f;
    public float scaleDownDuration = 0.3f;

    [Header("Easing")]
    public Ease popEase = Ease.OutBack;
    public Ease upEase = Ease.OutCubic;
    public Ease fallEase = Ease.InQuad;

    Queue<GameObject> pool;

    void Awake()
    {
        InitPool();
    }

    void InitPool()
    {
        pool = new Queue<GameObject>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            var go = Instantiate(starPrefab, starsContainer);
            go.SetActive(false);
            pool.Enqueue(go);
        }
    }

    GameObject GetFromPool()
    {
        if (pool.Count > 0)
        {
            var g = pool.Dequeue();
            g.transform.SetParent(starsContainer, false);
            g.SetActive(true);
            return g;
        }

        return Instantiate(starPrefab, starsContainer);
    }

    void ReturnToPool(GameObject g)
    {
        g.SetActive(false);
        pool.Enqueue(g);
    }

    // PUBLIC: Patlamayý baþlat
    public void Burst(Vector2 origin)
    {
        for (int i = 0; i < burstCount; i++)
            SpawnStar(origin);
    }

    void SpawnStar(Vector2 origin)
    {
        var go = GetFromPool();
        var rect = go.GetComponent<RectTransform>();
        var cg = go.GetComponent<CanvasGroup>();
        var img = go.GetComponent<Image>();

        if (cg != null) cg.alpha = 1f;
        if (img != null) img.enabled = true;

        rect.anchoredPosition = origin;
        rect.localScale = Vector3.zero;

        // YUKARI RASTGELE HEDEF
        float randomX = Random.Range(-spreadX, spreadX);
        float randomUp = Random.Range(upMin, upMax);
        Vector2 upTarget = origin + new Vector2(randomX, randomUp);

        // AÞAÐI DÜÞÜÞ HEDEFÝ
        Vector2 downTarget = upTarget + new Vector2(0, -fallDistance);

        float delay = Random.Range(0f, 0.1f);

        Sequence seq = DOTween.Sequence();

        // Hafif gecikme
        seq.AppendInterval(delay);

        // Pop-in
        seq.Append(rect.DOScale(1f, popDuration).SetEase(popEase));

        // Yukarý hareket
        seq.Append(rect.DOAnchorPos(upTarget, upDuration).SetEase(upEase));

        // Aþaðý düþme + küçülme ayný anda
        seq.Append(rect.DOAnchorPos(downTarget, fallDuration).SetEase(fallEase));
        seq.Join(rect.DOScale(0f, scaleDownDuration).SetEase(Ease.InQuad));

        // Fade out
        if (cg != null)
            seq.Join(cg.DOFade(0f, scaleDownDuration));

        // Bittiðinde pool'a dönsün
        seq.OnComplete(() =>
        {
            ReturnToPool(go);
        });
    }
}
