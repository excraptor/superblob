using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AIPatrol : MonoBehaviour
{

    [HideInInspector] public bool mustPatrol;

    public Rigidbody2D rb;

    public float walkSpeed;

    public Transform groundCheckPos;

    public LayerMask groundLayer;

    public Text text;

    private bool mustFlip;

    private bool lost = false;
    void Start()
    {
        mustPatrol = true;
        System.Random random = new System.Random();
        if (random.NextDouble() <= 0.5d)
        {
            Flip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }

        if (lost && Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
       if (col.gameObject.tag == "player" && !text.gameObject.activeSelf)
       {
           text.text = "You lost, try again!\nPress R to restart.";
               PlayerMovement.donuts = 0;
               text.gameObject.SetActive(true);
               Destroy(col.gameObject);
               lost = true;
       }
   }
}
