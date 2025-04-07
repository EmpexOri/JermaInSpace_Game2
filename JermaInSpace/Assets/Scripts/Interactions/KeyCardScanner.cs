using UnityEngine;

public class KeyCardScanner : MonoBehaviour, IInteractable
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip accessGrantedSound;
    public AudioClip accessDeniedSound;

    public void Interact()
    {
        PlayerItems playerItems = FindObjectOfType<PlayerItems>();

        if (playerItems != null && playerItems.hasKeycard && playerItems.currentItem == 3)
        {
            // Successful scan
            GlobalVariables.Instance.KeyCardScanned = true;
            Debug.Log("Keycard scan successful!");

            if (audioSource && accessGrantedSound)
                audioSource.PlayOneShot(accessGrantedSound);
        }
        else
        {
            // Failed scan
            Debug.Log("Keycard scan failed - no card equipped!");

            if (audioSource && accessDeniedSound)
                audioSource.PlayOneShot(accessDeniedSound);
        }
    }
}
