using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [Header("Item Objects")]
    public GameObject voiceRecorder;
    public GameObject glowStick;

    [Header("Item States")]
    public bool hasVoiceRecorder = false;
    public bool hasGlowStick = false;

    private int currentItem = 0; // 0 = None, 1 = Voice Recorder, 2 = Glow Stick

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // Press Q to switch items
        {
            ToggleItem();
        }
    }

    void ToggleItem()
    {
        if (hasVoiceRecorder && hasGlowStick)
        {
            currentItem = (currentItem + 1) % 3; // Cycle between 0 (None), 1 (Voice Recorder), 2 (Glow Stick)
        }
        else if (hasVoiceRecorder)
        {
            currentItem = currentItem == 1 ? 0 : 1; // Toggle between None & Voice Recorder
        }
        else if (hasGlowStick)
        {
            currentItem = currentItem == 2 ? 0 : 2; // Toggle between None & Glow Stick
        }

        UpdateItemState();
    }

    void UpdateItemState()
    {
        // Disable all items by default
        voiceRecorder?.SetActive(false);
        glowStick?.SetActive(false);

        // Activate the currently selected item
        if (currentItem == 1) voiceRecorder?.SetActive(true);
        if (currentItem == 2) glowStick?.SetActive(true);
    }
}
