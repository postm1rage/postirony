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
        // Check if the object or its parent has the Player tag
        GameObject enteringObject = other.gameObject;
        if (enteringObject.CompareTag("Player") || (enteringObject.transform.parent != null && enteringObject.transform.parent.CompareTag("Player")))
        {
            Debug.Log("Player detected! Teleporting...");

            if (targetPosition != null)
            {
                // Teleport the player to the target position
                enteringObject.transform.position = targetPosition.position;
                Debug.Log($"Player teleported to position {targetPosition.position}");

                // Teleport the camera to follow the player
                if (mainCamera != null)
                {
                    mainCamera.transform.position = new Vector3(
                        targetPosition.position.x,
                        targetPosition.position.y,
                        mainCamera.transform.position.z // Keep the camera's original Z position
                    );
                    Debug.Log("Camera teleported to follow the player.");
                }
                else
                {
                    Debug.LogWarning("Main Camera not found!");
                }
            }
            else
            {
                Debug.LogWarning("Target position is not set! Check the Target Position field in the Inspector.");
            }
        }
        else
        {
            // Log if the object entering the trigger is not the player
            Debug.Log($"This is not the player, it's: {enteringObject.name}");
        }
    }
}