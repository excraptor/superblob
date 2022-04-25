using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShroomMovement : MonoBehaviour
{
   [HideInInspector] public bool mustPatrol;

    public Rigidbody2D rb;

    public float walkSpeed;

    public Transform groundCheckPos;

    public LayerMask groundLayer;

    private bool mustFlip;

    public PlayerMovement player;
    void Start()
    {
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

   void Patrol()
    {
        if (mustFlip)
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

   void Flip()
   {
       mustPatrol = false;
       transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
       walkSpeed *= -1;
       mustPatrol = true;
   }

   void OnTriggerEnter2D(Collider2D col)
   {
       UnityEngine.Debug.Log("collided");
       if (col.gameObject.tag == "player" )
       {
           StartCoroutine(this.player.PowerUp(2.0f));
           Destroy(this.gameObject);
       }
   }
}
