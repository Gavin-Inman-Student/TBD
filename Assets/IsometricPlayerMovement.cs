using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class IsometricPlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public bool isMoving;
    float movX;
    float movY;
    Vector2 moveDirection;
        
    bool canDash = true;
    bool isDashing;
    public float dashSpeed;
    public float dashTime;
    public float dashCoolDown;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        Movement();
        
        if (Input.GetKey(KeyCode.Space) && canDash == true)
        {
            StartCoroutine(Dash());
        }
    }

    
    void Movement()
    {
        if (isDashing == true)
        {
            return;
        }

        movY = Input.GetAxisRaw("Vertical");
        movX = Input.GetAxisRaw("Horizontal");
        moveDirection = new Vector2(movX, movY).normalized;
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
           
    }

    IEnumerator Dash()
    {
        movY = Input.GetAxisRaw("Vertical");
        movX = Input.GetAxisRaw("Horizontal");
        moveDirection = new Vector2(movX, movY).normalized;

        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    
    void Look()
    {
        
    }
}
