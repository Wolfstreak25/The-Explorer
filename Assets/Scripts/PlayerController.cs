using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    
    bool crouch = false;

    float offX,offY,sizeX,sizeY;
    private BoxCollider2D boxCol;

	void Start()
    {
        boxCol = this.GetComponent<BoxCollider2D>();
    }
    public void Update()
    {
        
        float speed = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = speed*2 ;
            }
        animator.SetFloat("Speed", Mathf.Abs(speed));
        
        float VerticalInput = Input.GetAxis("Vertical");		

		    JumpAnimation(VerticalInput);
   
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

            if (Input.GetKey(KeyCode.E))
            {
                animator.SetTrigger("Attack");
            }

            Vector3 scale = transform.localScale;

            if(speed < 0)
            {
                scale.x = -1f * Mathf.Abs(scale.x);
            }
            else if(speed > 0)
            {
                scale.x = Mathf.Abs(scale.x);
            }
            transform.localScale = scale;
            
    }
     public void Crouch(bool crouch)
    {
        if (crouch == true)
        {
            offX = -0.1284856f;               //Offset X
            offY = 0.5952377f;                //Offset Y

            sizeX = 0.8512849f;               //Size X
            sizeY = 1.315794f;               //Size Y
        }

        else
        {
            offX = -0.002953231f;             //Offset X
            offY = 0.9699736f;               //Offset Y

            sizeX = 0.5884072f;              //Size X
            sizeY = 2.065266f;              //Size Y
        }
        animator.SetBool("Crouch", crouch);

        boxCol.size = new Vector2(sizeX, sizeY);
        boxCol.offset = new Vector2(offX, offY);
    }
    public void JumpAnimation(float vertical)
    {    
		if (vertical > 0 )
        {
            animator.SetTrigger("Jump");            
        }
    }
}