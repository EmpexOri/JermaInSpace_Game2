using UnityEngine;
using TMPro;

public class AudioLogPlayer : MonoBehaviour
{
    public AudioClip[] audioLogs; // Assign 9 audio clips in the Inspector
    private AudioSource audioSource;
    public TextMeshProUGUI audioLogUI;
    private PlayerItems playerItems;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerItems = FindObjectOfType<PlayerItems>(); 
        UpdateUI(false); // Initially hide the UI
    }

    void Update()
    {
        bool isHoldingRecorder = playerItems.currentItem == 1; // Check if the recorder is selected

        if (!isHoldingRecorder)
        {
            if (audioSource.isPlaying) audioSource.Stop(); // Stop playback if the player switches items
            UpdateUI(false); // Hide UI when not holding the recorder
            return;
        }

        UpdateUI(true); // Show UI if holding the recorder

        for (int i = 0; i < audioLogs.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                PlayAudioLog(i);
            }
        }
    }

    void PlayAudioLog(int index)
    {
        if (index < 0 || index >= audioLogs.Length) return; // Prevent out-of-bounds errors

        if (audioSource.isPlaying)
            audioSource.Stop(); // Stop any currently playing audio

        audioSource.clip = audioLogs[index]; // Set the new audio clip
        audioSource.Play(); // Play the audio
    }

    void UpdateUI(bool show)
    {
        if (audioLogUI != null)
        {
            audioLogUI.gameObject.SetActive(show);
            if (show) audioLogUI.text = "Press 1-9 to play audio logs";
        }

        if (show != true)
        {
            audioLogUI.text = "";
        }
    }

    public void ForceUIUpdate(bool isHoldingRecorder)
    {
        UpdateUI(isHoldingRecorder);
    }
}
