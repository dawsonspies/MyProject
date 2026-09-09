using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InteractableLabelManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject labelPrefab;

    [Header("References")]
    [SerializeField] private RectTransform interactUIMask; // The empty UI RectTransform defining the tracking box

    [Header("Scene Data (Layer 3)")]
    [SerializeField] private List<GameObject> interactableObjectsInScene = new List<GameObject>();

    // Maps each active 3D game object to its instantiated 2D UI label
    private Dictionary<GameObject, GameObject> _activeLabels = new Dictionary<GameObject, GameObject>();
    private Camera _mainCamera;
    private Canvas _parentCanvas;

    private void Start()
    {
        _mainCamera = Camera.main;

        // Find the parent canvas to determine the proper camera reference for UI calculations
        _parentCanvas = interactUIMask.GetComponentInParent<Canvas>();

        // Cache all Layer 3 objects currently in the scene
        RefreshInteractableObjects();
    }

    // Call this if objects are dynamically spawned or destroyed in the scene later on
    public void RefreshInteractableObjects()
    {
        interactableObjectsInScene = FindObjectsByType<GameObject>(FindObjectsSortMode.None)
               .Where(go => go.layer == 3)
               .ToList();
    }

    private void Update()
    {
        MoveDiageticUI();
    }

    private void MoveDiageticUI()
    {
        if (_mainCamera == null || interactUIMask == null) return;

        // Use a camera reference ONLY if canvas is set to Screen Space - Camera or World Space
        Camera uiCamera = (_parentCanvas != null && _parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : _mainCamera;

        // 1. Clean up labels for 3D objects that were destroyed elsewhere in the game engine
        var activeKeys = _activeLabels.Keys.ToList();
        for (int i = activeKeys.Count - 1; i >= 0; i--)
        {
            GameObject trackedObj = activeKeys[i];

            // Unity checks if an object is destroyed by comparing it to null
            if (trackedObj == null)
            {
                GameObject labelToDestroy = _activeLabels[trackedObj];
                if (labelToDestroy != null)
                {
                    Destroy(labelToDestroy);
                }
                _activeLabels.Remove(trackedObj);
            }
        }

        // 2. Process active objects inside your scene tracker list
        for (int i = 0; i < interactableObjectsInScene.Count; i++)
        {
            GameObject obj = interactableObjectsInScene[i];

            // If an object in the master scene list was destroyed, clean up the cache tracking index
            if (obj == null)
            {
                interactableObjectsInScene.RemoveAt(i);
                i--; // Step back to prevent skipping an element on the next loop iteration
                continue;
            }

            // Convert World Space to Screen Space
            Vector3 screenPoint = _mainCamera.WorldToScreenPoint(obj.transform.position);

            // Is it in front of the camera and inside our UI RectTransform boundary?
            bool isInScreenSpaceZone = screenPoint.z > 0 && RectTransformUtility.RectangleContainsScreenPoint(interactUIMask, screenPoint, uiCamera);

            if (isInScreenSpaceZone)
            {
                // If it just entered the space, create its UI piece
                if (!_activeLabels.ContainsKey(obj))
                {
                    CreateLabel(obj);
                }

                // Update its position to sit dead-center on top of the 3D target
                if (_activeLabels.TryGetValue(obj, out GameObject label))
                {
                    if (label != null)
                    {
                        // Use transform.position to cleanly handle both Overlay and Camera Canvas space
                        label.transform.position = screenPoint;
                    }
                }
            }
            else
            {
                // If it left the space or went behind the camera, remove and delete its UI piece
                if (_activeLabels.ContainsKey(obj))
                {
                    RemoveLabel(obj);
                }
            }
        }
    }

    private void CreateLabel(GameObject targetObject)
    {
        if (labelPrefab == null) return;

        // Instantiate the UI element as a direct child of your tracking area (interactUIMask)
        GameObject newLabel = Instantiate(labelPrefab, interactUIMask);

        // Safety checks to ensure scripts exist on objects before calling them
        InteractableObjectLabel labelScript = newLabel.GetComponent<InteractableObjectLabel>();
        Interactable interactableScript = targetObject.GetComponent<Interactable>();

        if (labelScript != null && interactableScript != null)
        {
            labelScript.SetLabelText(interactableScript.GetInteractionText());
        }

        _activeLabels.Add(targetObject, newLabel);
    }

    private void RemoveLabel(GameObject targetObject)
    {
        if (_activeLabels.TryGetValue(targetObject, out GameObject labelToDestroy))
        {
            if (labelToDestroy != null)
            {
                Destroy(labelToDestroy);
            }
            _activeLabels.Remove(targetObject);
        }
    }

    // Cleanup active UI objects if this manager is destroyed
    private void OnDestroy()
    {
        foreach (var label in _activeLabels.Values)
        {
            if (label != null) Destroy(label);
        }
        _activeLabels.Clear();
    }
}
