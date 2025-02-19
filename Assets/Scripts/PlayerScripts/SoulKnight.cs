using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoulKnight : PlayerController
{
    [Header("Movement")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;

    [Header("Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] float dashCoolDown;

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
    }
}
