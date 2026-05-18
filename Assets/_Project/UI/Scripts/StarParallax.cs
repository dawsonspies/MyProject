using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Parallax : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform starLayer1;
    [SerializeField] private Transform starLayer2;
    [SerializeField] private Transform starLayer3;

    [Header("Speeds")]
    [SerializeField] private float layer1Speed = 0.1f; //slowest, smaller stars
    [SerializeField] private float layer2Speed = 0.25f; //medium, small stars
    [SerializeField] private float layer3Speed = 0.5f; //fastest, biggest stars

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Mouse.current.delta.ReadValue();

        //print("mouse delta: " + mousePos);

        MoveStars(mousePos);
    }

    void MoveStars(Vector2 mouseMovement)
    {
        starLayer1.Translate(mouseMovement * layer1Speed * Time.deltaTime);
        starLayer2.Translate(mouseMovement * layer2Speed * Time.deltaTime);
        starLayer3.Translate(mouseMovement * layer3Speed * Time.deltaTime);
    }
}
