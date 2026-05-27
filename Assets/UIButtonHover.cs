using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameManager;
using UnityEngine.UI;

public class UIButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private RectTransform underline;
    [SerializeField] private Image underlineImg;

    [Header("Toggle")]
    [SerializeField] private bool enableHoverScale;
    [SerializeField] private bool enableHoverColor;
    [SerializeField] private bool enableUnderline;

    [Header("Underline")]
    [SerializeField] private float normalUnderlineScale;
    [SerializeField] private float hoverUnderlineScale;
    [SerializeField] private float targetUnderlineScale;
    [SerializeField] private float underlineBaseHeight;
    [SerializeField] private float currentUnderlineScale;

    [Header("Colors")]
    [SerializeField] private Color normalColor = new Color32(176, 176, 176, 255);
    [SerializeField] private Color hoverColor = new Color32(170, 200, 255, 255);
    [SerializeField] private Color targetColor;
    [SerializeField] private Color normalUnderlineColor = new Color32(176, 176, 176, 255);
    [SerializeField] private Color hoverUnderlineColor = new Color32(170, 200, 255, 255);
    [SerializeField] private Color targetUnderlineColor;

    [Header("Scale")]
    [SerializeField] private float baseScale = 1f;
    [SerializeField] private float hoverScale = 1.1f;
    [SerializeField] private float targetScale = 1f;

    private void Start()
    {
        targetColor = normalColor;
        targetScale = baseScale;

        buttonText.color = normalColor;

        transform.localScale = Vector3.one * baseScale;

        underlineBaseHeight = underline.rect.height;
        hoverUnderlineScale = gameObject.GetComponent<RectTransform>().rect.width;
        targetUnderlineScale = 0f;

        underlineImg = underline.GetComponent<Image>();
        targetUnderlineColor = normalUnderlineColor;
        hoverUnderlineColor = hoverColor;
    }

    private void Update()
    {
        if (enableHoverColor)
            HandleButtonColor();
        if (enableHoverScale)
            HandleScale();
        if (enableUnderline)
            HandleUnderline();
    }

    private void HandleUnderline()
    {
        currentUnderlineScale = Mathf.Lerp(
            currentUnderlineScale,
            targetUnderlineScale,
            1f - Mathf.Exp(-UI_SMOOTH_SPEED * Time.deltaTime)
        );

        underlineImg.color = Color.Lerp(
            underlineImg.color,
            targetUnderlineColor,
            1f - Mathf.Exp(-UI_SMOOTH_SPEED * Time.deltaTime)
        );

        underline.sizeDelta = new Vector2(currentUnderlineScale, underlineBaseHeight);
    }

    private void HandleButtonColor()
    {
        buttonText.color = Color.Lerp(
            buttonText.color,
            targetColor,
            1f - Mathf.Exp(-UI_SMOOTH_SPEED * Time.deltaTime)
        );
    }

    private void HandleScale()
    {
        transform.localScale = Vector3.MoveTowards(
            transform.localScale,
            Vector3.one * targetScale,
            Time.deltaTime * UI_SMOOTH_SPEED
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetColor = hoverColor;
        targetScale = hoverScale;
        targetUnderlineScale = hoverUnderlineScale;
        targetUnderlineColor = hoverUnderlineColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetColor = normalColor;
        targetScale = baseScale;
        targetUnderlineScale = normalUnderlineScale;
        targetUnderlineColor = normalUnderlineColor;
    }
}