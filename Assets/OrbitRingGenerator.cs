using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitRingGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float radius;
    [SerializeField] private int segments;

    [ContextMenu("Generate Orbit")]
    void GenerateOrbit()
    {
        lineRenderer = GetComponent<LineRenderer>();

        segments = 256;

        lineRenderer.positionCount = segments;

        float interval = 360/segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = (float)i / segments * Mathf.PI * 2f;

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            Vector3 point = new Vector3(x, 0f, z);

            lineRenderer.SetPosition(i, point);
        }
    }
}
