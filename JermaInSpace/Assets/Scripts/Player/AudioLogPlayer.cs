using UnityEngine;
using TMPro;

public class AudioLogPlayer : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI audioPromptText; // Assign in Inspector

    [Header("Audio Logs")]
    public AudioSource[] audioLogs; // Assign audio sources in Inspector

    private PlayerItems playerItems; // Reference to PlayerItems
    private AudioSource currentPlayingAudio; // Track current audio playing

    void Start()
    {
        playerItems = GetComponent<PlayerItems>();

        if (audioPromptText != null)
        {
            audioPromptText.enabled = false; // Hide UI initially
        }
    }

    void Update()
    {
        // Check if the Voice Recorder is the active item
        bool isHoldingRecorder = playerItems.currentItem == 1; // 1 = Voice Recorder

        if (audioPromptText != null)
        {
            audioPromptText.enabled = isHoldingRecorder; // Show prompt when holding recorder
        }

        if (isHoldingRecorder)
        {
            HandleAudioLogInput();
        }
        else if (currentPlayingAudio != null && currentPlayingAudio.isPlaying)
        {
            currentPlayingAudio.Stop(); // Stop audio if item is switched
            currentPlayingAudio = null;
        }
    }

    void HandleAudioLogInput()
    {
        for (int i = 0; i < audioLogs.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // Detect keys 1-9
            {
                PlayAudioLog(i);
            }
        }
    }

    void PlayAudioLog(int index)
    {
        if (currentPlayingAudio != null)
        {
            currentPlayingAudio.Stop(); // Stop any currently playing log
        }

        if (index >= 0 && index < audioLogs.Length)
        {
            currentPlayingAudio = audioLogs[index];
            currentPlayingAudio.Play();
        }
    }
}
