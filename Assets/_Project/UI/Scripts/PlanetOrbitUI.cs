using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbitUI : MonoBehaviour
{
    [Header("Customizability")]
    [SerializeField] private float currentSpeed = 10f;
    [SerializeField] private float baseSpeed = 10f;
    [SerializeField] public bool rotate = false;

    void Update()
    {
        if (rotate)
            transform.Rotate(0f, currentSpeed * Time.deltaTime, 0f);
    }

    public void UpdateRotation(float time)
    {
        transform.Rotate(0f, currentSpeed * time, 0f);

        print("rotation updated to: " + time);
    }

    public void SetSpeed(float newSpeed = 0f)
    {
        if (newSpeed != 0f)
        {
            currentSpeed = newSpeed;
        }
        else
        {
            currentSpeed = baseSpeed;
        }
    }
}
