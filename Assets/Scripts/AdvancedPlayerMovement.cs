using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AdvancedPlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 7f;
    public float dashSpeed = 20f;
    public float crouchHeight = .5f;
    public LayerMask whatIsGround;
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;

    public AudioClip jumpSound;
    public AudioClip dashSound;
    public AudioClip footstepSound;

    private Rigidbody2D body;
    private Animator anim;
    private AudioSource audioPlayer;

    private bool grounded;
    private bool canDoubleJump = false;
    private bool isDashing = false;
    private bool isCrouching = false;
    private bool facingRight = true;
    // Start is called before the first frame update
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
        
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        anim.SetBool("walk", horizontalInput !=0);

        if(horizontalInput != 0 && grounded){
            PlaySound(footstepSound);
        }
        if((horizontalInput>0 && !facingRight) || (horizontalInput<0 && facingRight)){
            Flip();
        }

        if(Input.GetKey(KeyCode.Space) && grounded){
            Jump();
        }

    }
    private void PlaySound(AudioClip clip){
        audioPlayer.clip = clip;
        audioPlayer.Play();
    }
    private void Flip(){
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }

    private void Jump(){
        body.velocity = new Vector2(body.velocity.x, jumpHeight);
        grounded = false;
        anim.SetTrigger("jump");
        PlaySound(jumpSound);
    }
}
