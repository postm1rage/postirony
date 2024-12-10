using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform targetPosition; // Target position for teleportation
    private Camera mainCamera; // Reference to the main camera

    private void Start()
    {
        // Find the main camera at the start
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if there's an object with the "Boss" tag in the scene
        if (GameObject.FindGameObjectWithTag("Boss") != null)
        {
            Debug.Log("Teleportation blocked: a Boss is present in the hierarchy.");
            return; // Do not teleport if a Boss exists
        }

        // Get the object entering the trigger
        GameObject enteringObject = other.gameObject;

        // Check if the object or its parent has the "Player" tag
        Transform rootObject = GetRootObjectWithTag(enteringObject, "Player");
        if (rootObject != null)
        {
            Debug.Log($"Player detected! Teleporting {rootObject.name}...");

            if (targetPosition != null)
            {
                // Teleport the root object (Player), which will also teleport all its children
                rootObject.position = targetPosition.position;
                Debug.Log($"Player teleported to position {targetPosition.position}");

                // Adjust the camera separately if necessary
                if (mainCamera != null)
                {
                    mainCamera.transform.position = new Vector3(
                        targetPosition.position.x,
                        targetPosition.position.y,
                        mainCamera.transform.position.z // Keep the camera's original Z position
                    );
                    Debug.Log("Camera teleported to follow the player.");
                }
            }
            else
            {
                Debug.LogWarning("Target position is not set! Check the Target Position field in the Inspector.");
            }
        }
        else
        {
            Debug.Log($"Unrelated object entered: {enteringObject.name}");
        }
    }

    // Helper method to find the root object with the specified tag
    private Transform GetRootObjectWithTag(GameObject obj, string tag)
    {
        // Check the object itself
        if (obj.CompareTag(tag)) return obj.transform;

        // Check the parent
        if (obj.transform.parent != null && obj.transform.parent.CompareTag(tag)) return obj.transform.parent;

        return null; // No matching tag found
    }
}