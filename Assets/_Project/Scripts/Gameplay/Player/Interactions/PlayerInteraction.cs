using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask interactMask;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private bool interact;

    private void Awake()
    {
        interactMask = LayerMask.GetMask("Interactable");
    }

    public void TryInteract(bool _interact)
    {
        interact = _interact;
    }

    private void Update()
    {

        if (interact)
        {
            
            RaycastHit hit;

            if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, interactDistance, interactMask))
            {
                Debug.DrawRay(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                Debug.Log("Did Hit");

                var interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    interactable.Interact(gameObject);
                }

                print("interacted with " + hit.transform.name);

            } 
            else
            {
                Debug.DrawRay(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                Debug.Log("Did Not Hit");
            }
        }
    }
}
