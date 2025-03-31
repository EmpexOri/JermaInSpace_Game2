using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Tags")]
    public string passiveTag = "PassiveInteractable";  // Objects that play sound when bumped
    public string activeTag = "ActiveInteractable";    // Objects that require pressing "F", we can change this later if needed

    [Header("Interaction Settings")]
    public float interactionRange = 2f;  // Range to detect active interactables, we're using 2 for now, but due to low light we may want to tweak
    public TextMeshProUGUI interactionText;  // UI prompt using TextMeshPro, painful

    private IInteractable currentInteractable;  // Stores nearby interactable, just simple

    void Update()
    {
        HandleActiveInteraction();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Play sound only for passive interactables, like hitting into things
        if (hit.gameObject.CompareTag(passiveTag))
        {
            AudioSource audioSource = hit.gameObject.GetComponent<AudioSource>();
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    void HandleActiveInteraction()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange);
        currentInteractable = null;

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag(activeTag))
            {
                IInteractable interactable = collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    currentInteractable = interactable;
                    ShowInteractionPrompt(true);
                    break;
                }
            }
        }

        if (currentInteractable == null)
        {
            ShowInteractionPrompt(false);
        }

        if (Input.GetKeyDown(KeyCode.F) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void ShowInteractionPrompt(bool show)
    {
        if (interactionText != null)
        {
            Color textColor = interactionText.color;
            textColor.a = show ? 1f : 0f; // Set alpha to 1 or 0, aka show or hide
            interactionText.color = textColor;
            if (show) interactionText.text = "Press F to interact";
        }
    }

    // Visualize interaction range in Scene View, for debugging mostly
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
