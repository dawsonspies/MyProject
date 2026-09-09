using UnityEngine;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class InteractableObjectLabel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        _rectTransform.pivot = new Vector2(0.5f, 0.5f);
    }

    public void SetLabelText(string _text)
    {
        text.text = _text;
    }
}
