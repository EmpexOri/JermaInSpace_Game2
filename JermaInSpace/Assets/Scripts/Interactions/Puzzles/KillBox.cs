using UnityEngine;

public class KillBox : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform teleportDestination;  // The empty GameObject to teleport to

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Check if the colliding object is the player
        {
            TeleportPlayer(other.gameObject);  // Call the teleport function
        }
    }

    void TeleportPlayer(GameObject player)
    {
        if (teleportDestination != null)
        {
            player.transform.position = teleportDestination.position;  // Move player to teleport destination
            Debug.Log("Player teleported to: " + teleportDestination.position);
        }
        else
        {
            Debug.LogWarning("Teleport destination not assigned!");
        }
    }
}
