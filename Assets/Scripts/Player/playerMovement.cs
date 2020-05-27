using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    private bool grounded = false;

    Vector2 movement;
    void Update()
    {

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("MoveSpeed", movement.sqrMagnitude);
        if (movement.x >= 0.01f)
        {
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
        else if (movement.x <= -0.01f)
        {  
            transform.localScale = new Vector3(-0.4f, 0.4f, 0.4f);
        }
        
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            gameObject.GetComponentInParent<Animator>().SetTrigger("Jump");
            rb.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
        }
       
    }

    private void FixedUpdate()
    {
        if(GameManager.GetInstance().isPlatformer)
        {
            PlatformerMovement();
        } else
        {
            TopdownMovement();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("PlatformGround"))
        {
             grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlatformGround"))
        {
            grounded = false;
        }
    }

    private void PlatformerMovement()
    {
        Jump();
        Vector3 platformMovement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        movement.x = platformMovement.x;
        movement.y = platformMovement.y;
        transform.position += platformMovement * Time.deltaTime * moveSpeed;
    }

    private void TopdownMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
