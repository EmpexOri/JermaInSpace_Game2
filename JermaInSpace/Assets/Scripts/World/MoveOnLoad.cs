using UnityEngine;

public class MoveOnLoad : MonoBehaviour
{
    public float moveDistance = 40f; // Distance to move across the X axis
    public float moveDuration = 10f; // Time to move the distance

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timeElapsed = 0f;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(moveDistance, 0f, 0f);
    }

    void Update()
    {
        // Move the object over time
        if (timeElapsed < moveDuration)
        {
            timeElapsed += Time.deltaTime;
            float lerpFactor = timeElapsed / moveDuration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, lerpFactor);
        }
    }
}
