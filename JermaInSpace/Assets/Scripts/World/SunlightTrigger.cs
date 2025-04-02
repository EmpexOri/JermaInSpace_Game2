using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public Light light1;
    public Light light2;
    private bool playerInside = false;

    void Update()
    {
        // If GeneratorPower is false, turn off the lights regardless of player presence
        if (GlobalVariables.Instance != null && !GlobalVariables.Instance.GeneratorPower)
        {
            ToggleLights(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            if (GlobalVariables.Instance != null && GlobalVariables.Instance.GeneratorPower)
            {
                ToggleLights(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            ToggleLights(false); // Always turn off lights when player leaves
        }
    }

    void ToggleLights(bool state)
    {
        if (light1 != null) light1.enabled = state;
        if (light2 != null) light2.enabled = state;
    }
}
