using UnityEngine;

public class DoorExample : MonoBehaviour, IInteractable
{
    private bool isOpen = false;

    public void Interact()
    {
        isOpen = !isOpen;
        Debug.Log("Door " + (isOpen ? "opened!" : "closed!"));
        // Add animation or toggle visibility here
    }
}
