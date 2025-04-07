using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static GlobalVariables Instance { get; private set; }

    public bool GeneratorPower = false;
    public GameObject WindowToHide; // Assign in Inspector
    public bool KeyCardScanned = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        HandleObjectVisibility();
    }

    void HandleObjectVisibility()
    {
        if (WindowToHide != null)
        {
            WindowToHide.SetActive(!GeneratorPower); // Hide when true, show when false
        }
    }
}
