using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public enum ItemType { VoiceRecorder, GlowStick, KeyCard }
    public ItemType itemType;

    public void Interact()
    {
        PlayerItems playerItems = FindObjectOfType<PlayerItems>();

        if (playerItems != null)
        {
            switch (itemType)
            {
                case ItemType.VoiceRecorder:
                    playerItems.hasVoiceRecorder = true;
                    break;

                case ItemType.GlowStick:
                    playerItems.hasGlowStick = true;
                    break;

                case ItemType.KeyCard:
                    playerItems.hasKeycard = true;
                    break;

                default:
                    Debug.LogWarning("Unhandled item type picked up.");
                    break;
            }

            playerItems.UpdateItemState(); // Refresh active items
            Destroy(gameObject); // Remove item from world
        }
        else
        {
            Debug.LogWarning("PlayerItems component not found!");
        }
    }
}
