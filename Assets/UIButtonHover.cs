using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI buttonText;

    [Header("Colors")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = Color.yellow;
    [SerializeField] private Color targetColor;

    [Header("Scale")]
    [SerializeField] private float baseScale = 1f;
    [SerializeField] private float hoverScale = 1.1f;
    [SerializeField] private float targetScale = 1f;

    [Header("Smoothing")]
    [SerializeField] private float smoothSpeed = 6f;

    private void Start()
    {
        targetColor = normalColor;
        targetScale = baseScale;

        buttonText.color = normalColor;

        transform.localScale = Vector3.one * baseScale;
    }

    private void Update()
    {
        HandleButtonColor();
        HandleScale();
    }

    private void HandleButtonColor()
    {
        buttonText.color = Color.Lerp(
            buttonText.color,
            targetColor,
            1f - Mathf.Exp(-smoothSpeed * Time.deltaTime)
        );
    }

    private void HandleScale()
    {
        transform.localScale = Vector3.MoveTowards(
            transform.localScale,
            Vector3.one * targetScale,
            Time.deltaTime * smoothSpeed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetColor = hoverColor;
        targetScale = hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetColor = normalColor;
        targetScale = baseScale;
    }
}