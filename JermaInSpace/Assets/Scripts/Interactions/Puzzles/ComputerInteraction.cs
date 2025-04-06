using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComputerInteraction : MonoBehaviour, IInteractable
{
    [Header("UI Elements")]
    public GameObject computerScreenUI;
    public Button generator1Button;
    public Button generator2Button;
    public Button generator3Button;

    [Header("Generator Objects")]
    public GameObject generator1;
    public GameObject generator2;
    public GameObject generator3;

    [Header("Audio")]
    public AudioClip normalGeneratorSound;
    public AudioClip differentGeneratorSound;
    public AudioClip correctChoiceSound;
    public AudioClip wrongChoiceSound;
    public AudioClip finalGeneratorSound;
    public AudioClip unisonSound;

    private MouseLook mouseLook;
    private PlayerMovement playerMovement;

    private GameObject[] generators;
    private int correctGeneratorIndex = -1;
    private bool isUsingTerminal = false;
    private bool isReshuffling = false;
    private bool isTerminalDisabled = false;

    void Start()
    {
        computerScreenUI.SetActive(false);
        generators = new GameObject[] { generator1, generator2, generator3 };
        ShuffleGenerators();

        generator1Button.onClick.AddListener(() => HandleButtonPress(0));
        generator2Button.onClick.AddListener(() => HandleButtonPress(1));
        generator3Button.onClick.AddListener(() => HandleButtonPress(2));

        mouseLook = FindFirstObjectByType<MouseLook>();
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    void ShuffleGenerators()
    {
        correctGeneratorIndex = Random.Range(0, generators.Length);

        for (int i = 0; i < generators.Length; i++)
        {
            AudioSource audioSource = generators[i].GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.clip = (i == correctGeneratorIndex) ? differentGeneratorSound : normalGeneratorSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }

        Debug.Log($"New correct generator: {correctGeneratorIndex + 1}");
    }

    public void Interact()
    {
        if (!isReshuffling && !isTerminalDisabled)
        {
            OpenComputerScreen();
        }
    }

    void OpenComputerScreen()
    {
        Debug.Log("Opening computer UI");
        computerScreenUI.SetActive(true);

        if (computerScreenUI.activeSelf)
        {
            Debug.Log("Canvas Element On");
        }

        LockCursor(false);

        if (playerMovement != null) playerMovement.enabled = false;
        if (mouseLook != null) mouseLook.enabled = false;

        isUsingTerminal = true;
    }

    public void CloseComputerScreen()
    {
        computerScreenUI.SetActive(false);
        LockCursor(true);

        if (playerMovement != null) playerMovement.enabled = true;
        if (mouseLook != null) mouseLook.enabled = true;

        isUsingTerminal = false;
    }

    void HandleButtonPress(int buttonIndex)
    {
        if (isReshuffling || isTerminalDisabled) return;

        AudioSource terminalAudio = GetComponent<AudioSource>();

        if (buttonIndex == correctGeneratorIndex)
        {
            if (terminalAudio != null && correctChoiceSound != null)
            {
                terminalAudio.PlayOneShot(correctChoiceSound);
            }

            GlobalVariables.Instance.GeneratorPower = true;
            Debug.Log($"Correct! Generator {buttonIndex + 1} activated.");

            if (GlobalVariables.Instance.GeneratorPower)
            {
                Debug.Log("Final state reached! Playing final generator sound, then switching to unison sound.");
                StartCoroutine(PlayFinalSoundThenUnison());
                isTerminalDisabled = true;
            }

            CloseComputerScreen();
        }
        else
        {
            if (terminalAudio != null && wrongChoiceSound != null)
            {
                terminalAudio.PlayOneShot(wrongChoiceSound);
            }

            Debug.Log($"Wrong choice! Generator {buttonIndex + 1} was incorrect.");
            StartCoroutine(ReshuffleAfterDelay());
        }
    }

    IEnumerator PlayFinalSoundThenUnison()
    {
        // Stop all generator sounds
        foreach (GameObject generator in generators)
        {
            AudioSource audioSource = generator.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Stop();
                audioSource.clip = finalGeneratorSound;
                audioSource.loop = false;
                audioSource.Play();
            }
        }

        // Wait for final generator sound to finish
        yield return new WaitForSeconds(finalGeneratorSound.length);

        // Now switch to the unison sound
        foreach (GameObject generator in generators)
        {
            AudioSource audioSource = generator.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Stop();
                audioSource.clip = unisonSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }

    IEnumerator ReshuffleAfterDelay()
    {
        isReshuffling = true;

        foreach (GameObject generator in generators)
        {
            AudioSource audioSource = generator.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }

        Debug.Log("Generators are silent for 5 seconds...");
        yield return new WaitForSeconds(5);

        ShuffleGenerators();
        isReshuffling = false;
    }

    void LockCursor(bool lockCursor)
    {
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;
    }

    void Update()
    {
        if (isUsingTerminal && (Input.GetKeyDown(KeyCode.Escape)))
        {
            CloseComputerScreen();
        }
    }
}
