using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    private GameObject joystickk;
    private Joystick joystick;
    public AudioClip[] jumpSounds;
    private AudioSource myAudioSource;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 16f;
    private float horizontal;


    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        joystickk = GameObject.FindGameObjectWithTag("Moving");
        joystick = joystickk.GetComponent<Joystick>();
    }

    void Update()
    {
        if (joystick.Horizontal >= 0.55f)
        {
            horizontal = speed;
        }
        else if (joystick.Horizontal <= -0.55f)
        {
            horizontal = -speed;
        }
        else
        {
            horizontal = 0f;
        }

        float vertical = joystick.Vertical;

        if (vertical >= 0.58f && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            myAudioSource.clip = jumpSounds[Random.Range(0, jumpSounds.Length)];
            myAudioSource.Play();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal, rb.velocity.y);
    }

    private bool isGrounded()
    {
         return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

}
