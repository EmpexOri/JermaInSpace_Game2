using UnityEngine;

public class EyeScript : MonoBehaviour
{
    private bool hasRotated = false;
    private Vector3 initial;
    public AudioSource source;
    public AudioClip clip;
    private float initialPitch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initial = transform.localEulerAngles;
        float initialPitch = source.pitch;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasRotated)
        {
            hasRotated = true;
            Invoke("RotateEye", Random.Range(3, 5));
        }
    }


    private void RotateEye()
    {
        Vector3 currentRotation = transform.localEulerAngles;
        CheckRotation(currentRotation);
        if (hasRotated)
        {
            float randY = Random.Range(-30, 30);
            float randX = Random.Range(-30, 30);
            float randZ = Random.Range(-30, 30);
            transform.Rotate(new Vector3(randX, randY, randZ));
            PitchClip();
            hasRotated = false;
        }

    }
    private void CheckRotation(Vector3 current)
    {
        if (initial.x - current.x < -90 || initial.x - current.x > 90)
        {
            transform.localEulerAngles = initial;
            PitchClip();
            hasRotated = false;
        }
        else if (initial.y - current.y < -90 || initial.y - current.y > 90)
        {
            transform.localEulerAngles = initial;
            PitchClip();
            hasRotated = false;
        }
        else if (initial.z - current.z < -90 || initial.z - current.z > 90)
        {
            transform.localEulerAngles = initial;
            PitchClip();
            hasRotated = false;
        }
        else
        {
            return;
        }
    }

    private void PitchClip()
    {
        source.pitch = initialPitch;
        float newPitch = Random.Range(0f, 2f);
        source.pitch = newPitch;
        source.PlayOneShot(clip);
    }
}
