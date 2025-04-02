using UnityEngine;
using System.Collections;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [Header("Door Settings")]
    public Vector3 slideDirection = Vector3.right; // Default: Moves right
    public float openDistance = 3f; // How far the door moves
    public float slideDuration = 1f; // Time to slide
    public float autoCloseTime = 5f; // Time before auto-close

    [Header("Power Requirements")]
    public bool requiresGeneratorPower = false; // If true, requires GeneratorPower to open
    public bool locksWhenPowered = false; // If true, door won't open when GeneratorPower is ON

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;
    private bool isMoving = false;
    private Coroutine autoCloseCoroutine;

    private void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + (slideDirection.normalized * openDistance);
    }

    public void Interact()
    {
        // Check power conditions before allowing interaction
        if (locksWhenPowered && GlobalVariables.Instance.GeneratorPower) return; // Locked when power is on
        if (requiresGeneratorPower && !GlobalVariables.Instance.GeneratorPower) return; // Needs power to open

        if (!isMoving)
        {
            StopAllCoroutines(); // Prevent multiple coroutines running
            StartCoroutine(isOpen ? MoveDoor(closedPosition) : MoveDoor(openPosition));
            isOpen = !isOpen;

            // If opened, start auto-close timer
            if (isOpen)
            {
                if (autoCloseCoroutine != null) StopCoroutine(autoCloseCoroutine);
                autoCloseCoroutine = StartCoroutine(AutoCloseAfterTime(autoCloseTime));
            }
        }
    }

    private IEnumerator MoveDoor(Vector3 targetPosition)
    {
        isMoving = true;
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < slideDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / slideDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }

    private IEnumerator AutoCloseAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (isOpen)
        {
            StartCoroutine(MoveDoor(closedPosition));
            isOpen = false;
        }
    }
}
