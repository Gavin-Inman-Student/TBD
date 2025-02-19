using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : Player
{

    [Header("Movement")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;

    [Header("Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] float dashCoolDown;

    [Header("Look")]
    [SerializeField] Camera camera;
    [SerializeField] GameObject rotationPoint;
    void Start()
    {
        canDash = true;
    }

 
    void Update()
    {
        Movement(rb, moveSpeed);
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Dash(rb, dashSpeed, dashTime, dashCoolDown));
        }
        Look(camera, rotationPoint);
    }
}
