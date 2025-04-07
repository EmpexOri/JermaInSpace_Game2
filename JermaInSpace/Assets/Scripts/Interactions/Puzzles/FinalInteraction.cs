using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalInteraction : MonoBehaviour, IInteractable
{
    [Header("Item Requirements")]
    public bool requiresGlowstick = true;
    public bool requiresVoiceRecorder = true;

    [Header("Scene Names")]
    public string successScene = "EndScene";
    public string failureScene = "EndJerma";

    public void Interact()
    {
        PlayerItems playerItems = FindObjectOfType<PlayerItems>();

        if (playerItems != null)
        {
            // Check if player has both required items
            if ((requiresGlowstick && playerItems.hasGlowStick) && (requiresVoiceRecorder && playerItems.hasVoiceRecorder))
            {
                // If player has the required items, load the success scene
                Debug.Log("Player has the required items. Proceeding to the EndScene.");
                SceneManager.LoadScene(successScene);
            }
            else
            {
                // If player is missing required items, load the failure scene
                Debug.Log("Player is missing required items. Proceeding to the EndJerma.");
                SceneManager.LoadScene(failureScene);
            }
        }
        else
        {
            Debug.LogWarning("PlayerItems not found!");
        }
    }
}
