using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class LocationHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public Image image;                 // the icon Image
    public LocationModifier data;       // reference to its LocationModifier
    public float hoverScale = 1.1f;
    public float tweenSpeed = 10f;
    [Range(0,1)] public float normalAlpha = 0.7f;
    [Range(0,1)] public float hoverAlpha = 1f;

    RectTransform rect;
    Vector3 baseScale;
    bool hovering;

    void Reset()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        data = GetComponent<LocationModifier>();
    }

    void Awake()
    {
        if (rect == null) rect = GetComponent<RectTransform>();
        baseScale = rect.localScale;
        if (image != null) image.raycastTarget = true;
    }

    void Update()
    {
        // Smooth scale + alpha animation
        Vector3 target = hovering ? baseScale * hoverScale : baseScale;
        rect.localScale = Vector3.Lerp(rect.localScale, target, Time.unscaledDeltaTime * tweenSpeed);

        if (image != null)
        {
            Color c = image.color;
            c.a = Mathf.Lerp(c.a, hovering ? hoverAlpha : normalAlpha, Time.unscaledDeltaTime * tweenSpeed);
            image.color = c;
        }
    }

    public void OnPointerEnter(PointerEventData e)
    {
        hovering = true;
        if (data) TooltipManager.Instance?.Show(data, rect);
    }

    public void OnPointerExit(PointerEventData e)
    {
        hovering = false;
        TooltipManager.Instance?.Hide();
    }

    public void OnPointerMove(PointerEventData e)
    {
        if (hovering)
            TooltipManager.Instance?.PositionNear(rect);
    }
}
