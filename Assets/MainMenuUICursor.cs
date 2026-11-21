using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUICursor : MonoBehaviour
{
    [Header("Appearance")]
    [SerializeField] private Vector2[] positions;
    [SerializeField] private Vector2 size; //w/h
    [SerializeField] private Color32 color;
    [SerializeField] private float flashInterval;

    [Header("Function")]
    [SerializeField] private int index; //0 is top
    [SerializeField] private int maxIndex = 3; //3 is bottom

    [Header("References")]
    [SerializeField] private Image cursorImage;
    [SerializeField] private RectTransform cursorTransform;

    private void Start()
    {
        index = 0;
    }

    public void ChangeIndex(int _index)
    {
        if(index + _index > maxIndex)
            index = 0;
        else if(index + _index < 0)
            index = maxIndex;
        else
            index += _index;
    }

    private void Update()
    {
        
    }

    private void OnValidate()
    {
        cursorImage.color = color;
        //cursorTransform.position = positions[0];

    }
}
