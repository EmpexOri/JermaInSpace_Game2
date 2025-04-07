using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [Header("Item Objects")]
    public GameObject voiceRecorder;
    public GameObject glowStick;
    public GameObject keycard;

    [Header("Item States")]
    public bool hasVoiceRecorder = false;
    public bool hasGlowStick = false;
    public bool hasKeycard = false;

    public int currentItem { get; private set; }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // Press Q to switch items
        {
            ToggleItem();
        }
    }

    void ToggleItem()
    {
        int maxItems = 4; // 0 = None, 1 = Voice Recorder, 2 = Glow Stick, 3 = Keycard
        int attempts = 0;

        do
        {
            currentItem = (currentItem + 1) % maxItems;
            attempts++;
        }
        while (!IsItemAvailable(currentItem) && attempts < maxItems);

        UpdateItemState();
    }

    bool IsItemAvailable(int itemIndex)
    {
        return itemIndex switch
        {
            0 => true, // None is always available
            1 => hasVoiceRecorder,
            2 => hasGlowStick,
            3 => hasKeycard,
            _ => false
        };
    }

    public void UpdateItemState()
    {
        // Disable all items first
        voiceRecorder?.SetActive(false);
        glowStick?.SetActive(false);
        keycard?.SetActive(false);

        // Activate selected item
        if (currentItem == 1) voiceRecorder?.SetActive(true);
        if (currentItem == 2) glowStick?.SetActive(true);
        if (currentItem == 3) keycard?.SetActive(true);

        // Update UI logic for recorder only
        AudioLogPlayer audioLogPlayer = FindObjectOfType<AudioLogPlayer>();
        if (audioLogPlayer != null)
        {
            audioLogPlayer.ForceUIUpdate(currentItem == 1);
        }
    }
}
