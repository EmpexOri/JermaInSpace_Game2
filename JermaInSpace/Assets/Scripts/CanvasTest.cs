using UnityEngine;

public class CanvasTest : MonoBehaviour
{
    public GameObject testCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            testCanvas.SetActive(!testCanvas.activeSelf);
            Debug.Log("Toggled Canvas: " + testCanvas.activeSelf);
        }
    }
}
