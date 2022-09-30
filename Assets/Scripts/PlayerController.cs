using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Collider2D collider2d;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundlayer;
    private bool istouchingGround;
    private float maxYvel;
    public float speed;
    public float jump;
    bool crouch = false;
    private Vector3 respawn;
    private Rigidbody2D rigidbody2d;
    private void Awake() 
    {
        Debug.Log("Player controller awake");
        respawn = transform.position;
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        
        istouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundlayer);
        float horizontalspeed = Input.GetAxisRaw("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        maxYvel = rigidbody2d.velocity.y ;
        //PlayerAnimations
        PlayerAnimations(horizontalspeed, VerticalInput);
        if(maxYvel <= -10 && istouchingGround != true )
        {
            Debug.Log("falling");
            maxYvel = 0 ;
            Death();
            transform.position = respawn;
            Debug.Log("Player respawned");
        }
        //Character movement
        MoveCharacter(horizontalspeed, VerticalInput);
    }
    private void PlayerAnimations(float horizontal, float vertical)
    {
        //PlayerAnimations
        //WALK
        //RUN
        if (Input.GetKey(KeyCode.LeftShift))
            {
                horizontal = horizontal*4 ;
            }
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        //JUMP
        Jump(vertical);

        //CROUCH
        if (Input.GetKey(KeyCode.LeftControl) && crouch == false)
        {
            Crouch(true);
            crouch = true;
        }
        else if (Input.GetKey(KeyCode.LeftControl) && crouch == true)
        {
            Crouch(false);
            crouch = false;
        }

        //STAFF ATTACK
        if (Input.GetKey(KeyCode.E))
        {
            animator.SetTrigger("Attack");
        }

        //FLIPPING
        Vector3 scale = transform.localScale;
        if(horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if(horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }
    public void Crouch(bool crouch)
    {
        animator.SetBool("Crouch", crouch);
    }
    public void Jump(float vertical)
    {    
		if (vertical > 0 && istouchingGround )
        {
            animator.SetTrigger("Jump");            
        }
    }
    public void Death()
    {    
            animator.SetTrigger("Death");   
            Debug.Log("Player died");         
    }
    private void MoveCharacter(float horizontal, float vertical)
    {
        //Move charachter Horizontally
        Vector3 position = transform.position;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            position.x += horizontal * speed * 3 * Time.deltaTime;
        }
        else
        {
            position.x += horizontal * speed * Time.deltaTime;
        }
        
        transform.position = position ;

        //Move charachter vertically
        if(vertical > 0 && istouchingGround)
        {   
            maxYvel = 0;
            rigidbody2d.AddForce(new Vector2(0f, jump),ForceMode2D.Force); 
        }
    }
}