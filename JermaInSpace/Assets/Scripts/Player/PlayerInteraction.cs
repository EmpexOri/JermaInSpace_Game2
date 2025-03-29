using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public string interactableTag = "Interactable"; // Only objects with this tag will play sound

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if the collided object has the correct tag
        if (hit.gameObject.CompareTag(interactableTag))
        {
            AudioSource audioSource = hit.gameObject.GetComponent<AudioSource>();

            if (audioSource != null && !audioSource.isPlaying) //No ear bleeding
            {
                audioSource.Play();
            }
        }
    }
}
