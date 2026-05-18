using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbitUI : MonoBehaviour
{
    [Header("Customizability")]
    [SerializeField] private float speed = 10f;
    [SerializeField] public bool rotate = false;

    void Update()
    {
        if (rotate)
            transform.Rotate(0f, speed * Time.deltaTime, 0f);
    }

    public void UpdateRotation(float time)
    {
        transform.Rotate(0f, speed * time, 0f);

        print("rotation updated to: " + time);
    }
}
