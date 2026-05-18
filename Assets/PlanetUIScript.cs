using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetUIScript : MonoBehaviour
{
    [SerializeField] private bool isHovered;

    [Header("Transforms")]
    [SerializeField] private Vector3 baseScale;
    [SerializeField] private float hoverScale = 1.3f;

        [Header("Hover Settings")]
    [SerializeField] private float hoverSpeed = 5f;

    public void Hover(bool hover)
    {
        isHovered = hover;

        if(isHovered)
        {
            GetComponentInParent<PlanetOrbitUI>().SetSpeed(hoverSpeed);
        }
        else
        {
            GetComponentInParent<PlanetOrbitUI>().SetSpeed();
        }
    }

    void Start()
    {
        baseScale = transform.localScale;
    }

    void Update()
    {
        if (isHovered)
        {
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                baseScale * hoverScale,
                Time.deltaTime * 5f
            );
        }
        else
        {
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                baseScale,
                Time.deltaTime * 5f
            );
        }
    }
}
