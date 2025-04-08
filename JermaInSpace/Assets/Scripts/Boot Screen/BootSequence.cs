using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BootSequence : MonoBehaviour
{
    private float timer = 0f;
    private bool hasSwitchedImage = false;

    [Header("Sound Settings")]
    public AudioClip sfx_load;
    public AudioClip sfx_boot;
    [Range(0f, 1f)] public float bootVolume = 1f;
    [Range(0f, 1f)] public float loadVolume = 1f;

    [Header("Visuals")]
    public Texture newBootTexture;

    private AudioSource audioSource;
    private bool hasPlayedLoadSound = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        if (sfx_boot != null)
        {
            audioSource.PlayOneShot(sfx_boot, bootVolume);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 7f && !hasPlayedLoadSound)
        {
            hasPlayedLoadSound = true;
            if (sfx_load != null)
            {
                audioSource.PlayOneShot(sfx_load, loadVolume);
            }
        }

        if (timer >= 8f && !hasSwitchedImage)
        {
            RawImage bootImage = GameObject.Find("Boot")?.GetComponent<RawImage>();
            if (bootImage != null && newBootTexture != null)
            {
                bootImage.texture = newBootTexture;
            }

            hasSwitchedImage = true;
        }

        if (timer >= 15f)
        {
            SceneManager.LoadScene("Main");
        }
    }
}
