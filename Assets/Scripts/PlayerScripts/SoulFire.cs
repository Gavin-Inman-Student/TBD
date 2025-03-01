using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;

public class SoulFire : MonoBehaviour
{
    
    [SerializeField] float projectileSpeed;
    [SerializeField] Vector3 mousePos;
    Camera camera;
    void Start()
    {
        
        //sets spawn and velocity of projetile on spawn
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * projectileSpeed;
        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

    }


    //Destroys on contact, uses trigger instead of collision due to lack of realistic colliders
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Terrian"))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}

