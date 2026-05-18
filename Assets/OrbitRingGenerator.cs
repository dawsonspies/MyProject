using UnityEngine;

public class OrbitRingGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform planet;

    [Header("Orbit")]
    [SerializeField] private float radius = 5f;

    [Header("Trail")]
    [SerializeField] private int segments = 64;
    [SerializeField] private float trailLength = 20f; // degrees
    [SerializeField] private float currentAngle = 0f;

    void Update()
    {
        Vector3 dir = (planet.position - transform.position).normalized;

        currentAngle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;

        GenerateTrail();
    }

    [ContextMenu("Generate Trail")]
    void GenerateTrail()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            // 0 -> 1 across trail
            float t = (float)i / (segments - 1);

            // newest point = currentAngle
            // oldest point = currentAngle - trailLength
            float angle = Mathf.Lerp(
                currentAngle,
                currentAngle + trailLength,
                t
            );

            // degrees -> radians
            float radians = angle * Mathf.Deg2Rad;

            float x = Mathf.Cos(radians) * radius;
            float z = Mathf.Sin(radians) * radius;

            Vector3 point = new Vector3(x, 0f, z);

            lineRenderer.SetPosition(i, point);
        }
    }
}