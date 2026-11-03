using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUICursor : MonoBehaviour
{
    [Header("Appearance")]
    [SerializeField] private Transform[] positions;
    [SerializeField] private Vector2 size; //w/h
    [SerializeField] private Color32 color;
    [SerializeField] private float flashInterval;

    [Header("Function")]
    [SerializeField] private int index; //0 is top

    [Header("References")]
    [SerializeField] private Image cursorImage;
    [SerializeField] private RectTransform cursorTransform;

    private void Start()
    {
        index = 0;
    }

    private void Update()
    {
        
    }

    private void OnValidate()
    {
        cursorImage.color = color;
        cursorTransform.position = positions[0];

    }
}
