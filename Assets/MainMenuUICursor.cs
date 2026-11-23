using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUICursor : MonoBehaviour
{
    [Header("Appearance")]
    [SerializeField] private RectTransform[] positions;
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

    public void ChangeIndex(int _index)
    {
        if(index + _index > positions.Length)
            index = 0;
        else if(index + _index < 0)
            index = positions.Length;
        else
            index += _index;
    }

    private void Update()
    {
        
    }

    private void OnValidate()
    {
        //change the color
        cursorImage.color = color;

        //change the position
        cursorTransform.position = positions[index].position;

        //change the size
        cursorTransform.sizeDelta = new Vector2(size.x/100, size.y/100);

    }
}
