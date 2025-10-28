using System.Collections;
using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance { get; private set; }

    public Canvas canvas;
    public RectTransform tooltipRoot;
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI multiplierText;

    public Vector2 offset = new Vector2(0, 60f);
    public float fadeSpeed = 12f;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        if (!canvas) canvas = GetComponentInParent<Canvas>();
        HideImmediate();
    }

    public void Show(LocationModifier data, RectTransform anchor)
    {
        if (!data || !anchor) return;
        titleText.text = data.displayName;
        multiplierText.text = $"Cash x{Mathf.Max(0f, data.rewardMultiplier):0.##}";
        tooltipRoot.gameObject.SetActive(true);
        PositionNear(anchor);
        StopAllCoroutines();
        StartCoroutine(FadeTo(1f));
    }

    public void PositionNear(RectTransform anchor)
    {
        var screen = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, anchor.position);
        var canvasRect = (RectTransform)canvas.transform;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screen, canvas.worldCamera, out var local))
            tooltipRoot.anchoredPosition = local + offset;
    }

    public void Hide() { StopAllCoroutines(); StartCoroutine(FadeTo(0f)); }
    public void HideImmediate() { canvasGroup.alpha = 0; tooltipRoot.gameObject.SetActive(false); }

    IEnumerator FadeTo(float target)
    {
        tooltipRoot.gameObject.SetActive(true);
        while (Mathf.Abs(canvasGroup.alpha - target) > 0.01f)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, target, Time.unscaledDeltaTime * fadeSpeed);
            yield return null;
        }
        canvasGroup.alpha = target;
        if (Mathf.Approximately(target, 0f)) tooltipRoot.gameObject.SetActive(false);
    }
}
