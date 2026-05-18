using Unity.VisualScripting;
using UnityEngine;
using static GameManager;

public class StarSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private Transform starLayer1;
    [SerializeField] private Transform starLayer2;
    [SerializeField] private Transform starLayer3;

    [Header("Customizability")]
    [SerializeField] private float starSizeMin = 0.01f;
    [SerializeField] private float starSizeMax = 0.3f;
    [SerializeField] private int starCount = 100;
    [SerializeField] private float spawnPaddingX;
    [SerializeField] private float spawnPaddingY;

    private enum StarSize
    {
        Small,
        Medium,
        Large
    }

    public void SpawnStars()
    {
        spawnPaddingX = 0.1f * CAMERA_WIDTH_PX;
        spawnPaddingY = 0.1f * CAMERA_HEIGHT_PX;

        for (int i = 0; i < starCount; i++)
        {
            GameObject star = Instantiate(
                starPrefab,
                CalcSpawnLocation(),
                Quaternion.Euler(28.3f, 0f, 0f)
            );

            StarSize tier = AssignStarSize();

            star.transform.localScale = GetScale(tier);
            AssignStarLayer(star, tier);
        }
    }

    private StarSize AssignStarSize()
    {
        float rand = Random.Range(0f, 1f);

        if (rand <= 0.6f)
            return StarSize.Small;

        if (rand <= 0.9f)
            return StarSize.Medium;

        return StarSize.Large;
    }

    Vector3 CalcSpawnLocation()
    {
        Camera cam = Camera.main;

        float z = 10f;

        Vector3 min = cam.ViewportToWorldPoint(new Vector3(-2.5f, -2.5f, z));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(2.5f, 2.5f, z));

        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);

        return new Vector3(x, y, z);
    }

    private Vector3 GetScale(StarSize tier)
    {
        switch (tier)
        {
            case StarSize.Small:
                return Vector3.one * Random.Range(starSizeMin, starSizeMax / 3f);

            case StarSize.Medium:
                return Vector3.one * Random.Range(starSizeMax / 3f, starSizeMax * 2f / 3f);

            default:
                return Vector3.one * Random.Range(starSizeMax * 2f / 3f, starSizeMax);
        }
    }

    private void AssignStarLayer(GameObject star, StarSize tier)
    {
        switch (tier)
        {
            case StarSize.Small:
                star.transform.parent = starLayer3;
                star.transform.localRotation = Quaternion.identity;
                break;

            case StarSize.Medium:
                star.transform.parent = starLayer2;
                star.transform.localRotation = Quaternion.identity;
                break;

            case StarSize.Large:
                star.transform.parent = starLayer1;
                star.transform.localRotation = Quaternion.identity;
                break;
        }
    }
}
