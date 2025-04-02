using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public enum ItemType { VoiceRecorder, GlowStick }
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
            }

            playerItems.UpdateItemState(); // Refresh items
            Destroy(gameObject); // Remove world item
        }
    }
}
