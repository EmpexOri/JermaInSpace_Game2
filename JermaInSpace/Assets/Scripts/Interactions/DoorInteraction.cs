using UnityEngine;
using System.Collections;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [Header("Door Settings")]
    public Vector3 slideDirection = Vector3.right; // Default: Moves right
    public float openDistance = 3f;
    public float slideDuration = 1f;
    public float autoCloseTime = 5f;

    [Header("Power Requirements")]
    public bool requiresGeneratorPower = false;
    public bool locksWhenPowered = false;

    [Header("Keycard Settings")]
    public bool requiresKeycard = false; // NEW: Optional keycard requirement

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
        // Power logic
        if (locksWhenPowered && GlobalVariables.Instance.GeneratorPower) return;
        if (requiresGeneratorPower && !GlobalVariables.Instance.GeneratorPower) return;

        // Keycard logic
        if (requiresKeycard && !GlobalVariables.Instance.KeyCardScanned) return;

        if (!isMoving)
        {
            StopAllCoroutines();
            StartCoroutine(isOpen ? MoveDoor(closedPosition) : MoveDoor(openPosition));
            isOpen = !isOpen;

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
