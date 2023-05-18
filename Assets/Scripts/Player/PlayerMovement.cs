using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public CharacterController playerHeight;
    public float gravity = -75f;
    public float jumpHeight = 3f;
    public float normalHeight, crouchHeight;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource walkingSoundEffect;
    [SerializeField] private AudioSource HSoundEffect;

    Vector3 velocity;
    bool isGrounded;
    bool isMoving;
    bool isSprinting;

    void Update()
    {
        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Player grounded velocity / limiter
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -3f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        float speed = 3f;

        // Crouch code
        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed *= 0.4f;
            playerHeight.height = crouchHeight;
        }
        else
        {
            playerHeight.height = normalHeight;
        }

        // Sprint code
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 3f;
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        // Check if the player is moving
        isMoving = move.magnitude > 0.1f;

        controller.Move(move * speed * Time.deltaTime);

        // Activate walking sound effect when grounded and moving
        if (isGrounded && isMoving)
        {
            if (!walkingSoundEffect.isPlaying)
            {
                walkingSoundEffect.Play();
                walkingSoundEffect.loop = true;
            }
        }
        else
        {
            walkingSoundEffect.Stop();
        }

        // Adjust audio pitch based on sprinting
        if (isSprinting)
        {
            walkingSoundEffect.pitch = 1.5f; // Adjust the value as desired for the sprinting audio speed
        }
        else
        {
            walkingSoundEffect.pitch = 1f; // Reset the pitch to normal if not sprinting
        }

        // Jump code
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpSoundEffect.Play();
        }

        // H key sound effect
        if (Input.GetKeyDown(KeyCode.H))
        {
            HSoundEffect.Play();
        }

        // Equations for jumping
        velocity.y += gravity * Time.deltaTime;

        // Movement controller
        controller.Move(velocity * Time.deltaTime);
    }
}
