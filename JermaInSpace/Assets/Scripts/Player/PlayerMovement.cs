using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Footstep Audio")]
    public AudioSource footstepAudioSource;
    public AudioClip defaultFootstep;
    public AudioClip metalFootstep;
    public AudioClip carpetFootstep;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isMoving;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        isMoving = move.magnitude > 0.1f;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        HandleFootstepAudio(move);
    }

    private void HandleFootstepAudio(Vector3 move)
    {
        if (isGrounded && isMoving)
        {
            RaycastHit hit;
            Vector3 rayOrigin = transform.position + Vector3.up * 0.1f; // Raise origin a bit
            float rayDistance = 2f;

            Debug.DrawRay(rayOrigin, Vector3.down * rayDistance, Color.green);

            if (Physics.Raycast(rayOrigin, Vector3.down, out hit, rayDistance))
            {
                string floorTag = hit.collider.tag;
                Debug.Log("Player is standing on: " + floorTag);

                AudioClip selectedClip = defaultFootstep;

                switch (floorTag)
                {
                    case "MetalFloor":
                        selectedClip = metalFootstep;
                        break;
                    case "CarpetFloor":
                        selectedClip = carpetFootstep;
                        break;
                }

                if (footstepAudioSource.clip != selectedClip)
                {
                    footstepAudioSource.clip = selectedClip;
                }

                if (!footstepAudioSource.isPlaying)
                {
                    footstepAudioSource.Play();
                }
            }
            else
            {
                Debug.Log("Raycast did not hit anything!");
            }
        }
        else
        {
            if (footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Stop();
            }
        }
    }
}