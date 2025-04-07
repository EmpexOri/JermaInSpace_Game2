using UnityEngine;

public class EnableObjectOnPower : MonoBehaviour
{
    [Header("Settings")]
    public GameObject objectToEnable;  // The object to enable when power is true
    private bool hasEnabled = false;  // Track if the object has already been enabled

    private void Start()
    {
        // Initialize based on current GeneratorPower state at the start
        if (GlobalVariables.Instance.GeneratorPower && !hasEnabled)
        {
            EnableObject();
        }
    }

    private void Update()
    {
        // Check only once when the power is first turned on
        if (GlobalVariables.Instance.GeneratorPower && !hasEnabled)
        {
            EnableObject();
        }
    }

    private void EnableObject()
    {
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);  // Enable the object
            hasEnabled = true;  // Mark the object as enabled
            Debug.Log("Object Enabled: " + objectToEnable.name); // Debug log to check enabling
        }
    }

    private void DisableObject()
    {
        if (objectToEnable != null && objectToEnable.activeSelf)
        {
            objectToEnable.SetActive(false);  // Disable the object
            hasEnabled = false;  // Reset the flag if you want to re-enable later
            Debug.Log("Object Disabled: " + objectToEnable.name); // Debug log to check disabling
        }
    }
}
