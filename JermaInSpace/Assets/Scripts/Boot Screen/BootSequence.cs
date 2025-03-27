using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BootSequence : MonoBehaviour
{
    private float timer = 0f;
    private bool hasSwitchedImage = false;
    public AudioClip sfx_load;
    public AudioClip sfx_boot;
    public Texture newBootTexture; 
    private AudioSource audioSource;
    private bool hasPlayedLoadSound = false;

    void Start()
    {
        // Create an AudioSource (Idk what I'll do here later)
        audioSource = gameObject.AddComponent<AudioSource>();
        if (sfx_boot != null)
        {
            audioSource.PlayOneShot(sfx_boot);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 7f && !hasPlayedLoadSound)
        {
            hasPlayedLoadSound = true; // Ensure it doesn't play again
            if (sfx_load != null)
            {
                audioSource.PlayOneShot(sfx_load);
            }
        }

        // At 8 seconds, change Boot's image & play the audio
        if (timer >= 8f && !hasSwitchedImage)
        {
            RawImage bootImage = GameObject.Find("Boot")?.GetComponent<RawImage>();

            if (bootImage != null && newBootTexture != null)
            {
                bootImage.texture = newBootTexture; // Change the RawImage texture
            }

            hasSwitchedImage = true; // Prevent repeated switching
        }

        // Load the 'Main' scene at 15 seconds
        if (timer >= 15f)
        {
            SceneManager.LoadScene("Main");
        }
    }
}
