using UnityEngine;

public class KillBox : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform teleportDestination;  // The empty GameObject to teleport to

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name); // Add this to check if trigger is working

        if (other.CompareTag("Player"))  // Check if the colliding object is the player
        {
            TeleportPlayer(other.gameObject);  // Call the teleport function
        }
    }

    void TeleportPlayer(GameObject player)
    {
        if (teleportDestination != null)
        {
            CharacterController playerController = player.GetComponent<CharacterController>();
            if (playerController != null)
            {
                // Use the CharacterController to move the player
                playerController.enabled = false; // Temporarily disable the controller to change position
                player.transform.position = teleportDestination.position;  // Move player
                playerController.enabled = true; // Re-enable the controller after teleportation
            }
            else
            {
                // For regular transforms (if no CharacterController)
                player.transform.position = teleportDestination.position;
            }

            Debug.Log("Player teleported to: " + teleportDestination.position);
        }
        else
        {
            Debug.LogWarning("Teleport destination not assigned!");
        }
    }
}
