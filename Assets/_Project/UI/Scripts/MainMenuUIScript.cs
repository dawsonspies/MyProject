using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class MainMenuUIScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private StarSpawner starSpawnerScript;
    [SerializeField] private Camera cam;

    [Header("SolarSystem")]
    [SerializeField] private int count = 8;
    [SerializeField] private GameObject[] planet;

    [Header("UI")]
    [SerializeField] private bool canInteract = true;
    [SerializeField] private GameObject currentHover;

    private void Start()
    {
        //set global vars
        CAMERA_HEIGHT_PX = Screen.height;
        CAMERA_WIDTH_PX = Screen.width;

        //set local references
        starSpawnerScript = GetComponent<StarSpawner>();

        //call local references
        starSpawnerScript.SpawnStars();

        ToggleRotate(true);

        cam = Camera.main;
    }

    private void Update()
    {
        //print("height: "+ CAMERA_HEIGHT_PX);
        //print("width: " + CAMERA_WIDTH_PX);

        if (canInteract)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.CompareTag("Planet"))
                {
                    if(currentHover != hitObject)
                    {
                        SetHover(hitObject);
                    }

                    return;
                }
            }

            ClearHover();
        }

    }

    void SetHover(GameObject newHover)
    {
        if (currentHover != null)
        {
            OnHoverExit(currentHover);
        }

        currentHover = newHover;

        OnHoverEnter(currentHover);
    }

    void ClearHover()
    {
        if (currentHover != null)
        {
            PlanetUIScript planetScript = currentHover.GetComponent<PlanetUIScript>();

            if (planetScript != null)
            {
                planetScript.Hover(false); //reset planets hover status
            }

            OnHoverExit(currentHover);
            currentHover = null;
        }
    }

    void OnHoverEnter(GameObject obj)
    {
        Debug.Log("Hover Enter: " + obj.name);

        PlanetUIScript planetScript = obj.GetComponent<PlanetUIScript>();

        if (planetScript != null)
        {
            planetScript.Hover(true);
        }

    }

    void OnHoverExit(GameObject obj)
    {
        Debug.Log("Hover Exit: " + obj.name);

        // revert effects here
    }

    void ToggleRotate(bool rotate)
    {
        for (int i = 0; i < count; i++)
        {
            print("toggled rotation[" + i + "] @ " + rotate);
            planet[i].GetComponent<PlanetOrbitUI>().rotate = rotate;
        }

        StartRotation();
    }

    void StartRotation()
    {
        float seconds = Random.Range(5, 65); //this is like seconds into the future

        for (int i = 0; i < count; i++)
        {
            print("started rotation[" + i + "] @ " + seconds);
            planet[i].GetComponent<PlanetOrbitUI>().UpdateRotation(seconds);
        }
    }
}
