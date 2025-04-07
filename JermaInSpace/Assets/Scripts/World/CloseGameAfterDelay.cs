using UnityEngine;
using System.Collections;

public class CloseGameAfterDelay : MonoBehaviour
{
    public float delayTime = 10f; // Time to wait before closing the game

    void Start()
    {
        // Start the coroutine to wait before closing the game
        StartCoroutine(CloseGameAfterTime());
    }

    IEnumerator CloseGameAfterTime()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delayTime);

        // Close the application
        Debug.Log("Closing the game...");
        Application.Quit();

        // If in the editor, stop the play mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
