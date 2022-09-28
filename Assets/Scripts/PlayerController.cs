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
    public float speed;
    public float jump;
    bool crouch = false;
    private Rigidbody2D rigidbody2d;

    float offX,offY,sizeX,sizeY;
    private BoxCollider2D boxCol;
    private CapsuleCollider2D capsCol;
    private void Awake() 
    {
        Debug.Log("Player controller awake");
        boxCol = this.GetComponent<BoxCollider2D>();
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        istouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundlayer);
        float horizontalspeed = Input.GetAxisRaw("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        
        //PlayerAnimations
        PlayerAnimations(horizontalspeed, VerticalInput);
        
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
        /*if (crouch == true)
        {
            offX = -0.1284856f;               
            offY = 0.5952377f;                

            sizeX = 0.8512849f;               
            sizeY = 1.315794f;     
        }         

        else
        {
            offX = -0.002953231f;             
            offY = 0.9699736f;               

            sizeX = 0.5884072f;              
            sizeY = 2.065266f;              
        }
        */
        animator.SetBool("Crouch", crouch);

        //boxCol.size = new Vector2(sizeX, sizeY);
        //boxCol.offset = new Vector2(offX, offY);
    }
    public void Jump(float vertical)
    {    
		if (vertical > 0 && istouchingGround )
        {
            animator.SetTrigger("Jump");            
        }
    }
    private void MoveCharacter(float horizontal, float vertical)
    {
        //Move charachter Horizontally
        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position ;

        //Move charachter vertically
        if(vertical > 0 && istouchingGround)
        {
            rigidbody2d.AddForce(new Vector2(0f, jump),ForceMode2D.Force);
        }
    }
}