using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image img;

    [Header("Customizability")]
    [SerializeField] private float speed;
    [SerializeField] private float offset;
    [SerializeField] private float minAlpha;
    [SerializeField] private float maxAlpha;

    [Header("COLORS")]
    [SerializeField] private Color32 softWhite = new Color32(237, 237, 237, 255);
    [SerializeField] private Color32 brightWhite = new Color32(245, 245, 245, 255);
    [SerializeField] private Color32 slightBlue = new Color32(191, 217, 255, 255);
    [SerializeField] private Color32 paleBlue = new Color32(170, 200, 255, 255);
    [SerializeField] private Color32 paleYellow = new Color32(255, 240, 179, 255);
    [SerializeField] private Color32 warmYellow = new Color32(255, 225, 140, 255);
    [SerializeField] private Color32 faintOrange = new Color32(255, 209, 166, 255);
    [SerializeField] private Color32 softOrange = new Color32(255, 179, 138, 255);
    [SerializeField] private Color32 dimRed = new Color32(255, 140, 140, 255);

    void Start()
    {
        img = GetComponent<Image>();

        speed = Random.Range(2f, 5f);
        offset = Random.Range(0f, 100f);

        minAlpha = 0.2f;
        maxAlpha = 1f;
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed + offset) + 1f) * 0.5f;

        Color c = AssignStarColor();

        c.a = Mathf.Lerp(minAlpha, maxAlpha, t);

        img.color = c;
    }

    private Color32 AssignStarColor()
    {
        int roll = Random.Range(0, 100);

        if (roll < 70)
        {
            return GetWhiteStar();
        }

        if (roll < 85)
        {
            return GetBlueStar();
        }

        if (roll < 95)
        {
            return GetYellowStar();
        }

        return GetWarmStar();
    }

    private Color32 GetWhiteStar()
    {
        bool bright = Random.value > 0.5f;

        if (bright)
        {
            return brightWhite;
        }

        return softWhite;
    }

    private Color32 GetBlueStar()
    {
        bool pale = Random.value > 0.5f;

        if (pale)
        {
            return paleBlue;
        }

        return slightBlue;
    }

    private Color32 GetYellowStar()
    {
        bool warm = Random.value > 0.5f;

        if (warm)
        {
            return warmYellow;
        }

        return paleYellow;
    }

    private Color32 GetWarmStar()
    {
        int roll = Random.Range(0, 3);

        switch (roll)
        {
            case 0:
                return faintOrange;

            case 1:
                return softOrange;

            default:
                return dimRed;
        }
    }
} 