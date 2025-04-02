using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    public Light blinkingLight; // Assign a Light component
    public float blinkInterval = 0.5f; // Time between blinks

    private void Start()
    {
        if (blinkingLight == null)
        {
            blinkingLight = GetComponent<Light>(); // Auto-assign if not set
        }

        StartCoroutine(Blink());
    }

    private System.Collections.IEnumerator Blink()
    {
        while (true)
        {
            blinkingLight.enabled = !blinkingLight.enabled; // Toggle light
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
