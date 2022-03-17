using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private int speed = 10;
    private bool facingRight = true;
    private bool isJumping = false;
    public static int donuts = 0;
    public static bool won = false;

    private void Start()
    {
        StartCoroutine(waitForKeypress());
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal")*speed, body.velocity.y);
        if ((facingRight && Input.GetAxis("Horizontal") < 0) || (!facingRight && Input.GetAxis("Horizontal") > 0))
        {
            Flip();
            facingRight = !facingRight;
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space) && Input.GetAxis("Vertical") == 0)
        {
            if (!isJumping)
            {
                body.velocity = new Vector2(body.velocity.x, speed*2f);
                isJumping = true;
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (won && Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            donuts = 0;
            won = false;
        }

    }

    IEnumerator waitForKeypress()
    {
        while (!Input.GetKeyDown(KeyCode.S))
        {
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
    }
    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
}
