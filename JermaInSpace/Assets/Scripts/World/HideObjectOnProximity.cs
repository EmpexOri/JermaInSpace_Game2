using UnityEngine;

public class HideObjectOnProximity : MonoBehaviour
{
    [Header("Settings")]
    public GameObject objectToHide;  // The object to hide when the player gets close
    public float hideDistance = 6f;  // Distance at which the object will hide
    public Transform player;  // The player's transform

    private void Update()
    {
        if (player == null) return;  // Ensure player is assigned

        // Calculate the distance between the player and the object
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Hide the object if the player is within the specified distance
        if (distanceToPlayer <= hideDistance)
        {
            if (objectToHide != null && objectToHide.activeSelf)
            {
                objectToHide.SetActive(false);  // Hide the object
            }
        }
        else
        {
            if (objectToHide != null && !objectToHide.activeSelf)
            {
                objectToHide.SetActive(true);  // Show the object if the player is outside the distance
            }
        }
    }
}
