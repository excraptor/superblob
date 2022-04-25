using System.Transactions;

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

    public bool powerUpped = false;

    public int hp = 1;

    public bool immune = false;

    public static bool canMoveToNext = false;

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
            Debug.Log("escape pressed");
            Application.Quit();
        }

        if (won && Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(1); // level 1
            donuts = 0;
            won = false;
        }
        if(canMoveToNext && Input.GetKey(KeyCode.N)) {
            moveToNextMap();
        }
        void moveToNextMap() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    public IEnumerator PowerUp(float time) {
        Debug.Log("started powerup: " + time);
        this.hp = 2;
        this.powerUpped = true;
        transform.localScale = new Vector2(transform.localScale.x * 2, transform.localScale.y * 2);
        yield return new WaitForSeconds(time);
        Debug.Log("after waitforseconds: " + time);
        this.PowerDown();
    }
    public void PowerDown() {
        Debug.Log("started powerdown: ");
        this.hp = 1;
        this.powerUpped = false;
        transform.localScale = new Vector2(transform.localScale.x * 0.5f, transform.localScale.y * 0.5f);
    }
    public IEnumerator Immune(float time) {
        this.immune = true;
        Color objectColor = this.GetComponent<Renderer>().material.color;
        float fadeAmount = objectColor.a - 0.2f;
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        this.GetComponent<Renderer>().material.color = objectColor;
        yield return new WaitForSeconds(time);
        this.NotImmune();

    }
    public void NotImmune() {
        this.immune = false;
        Color objectColor = this.GetComponent<Renderer>().material.color;
        float fadeAmount = objectColor.a + 0.2f;
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        this.GetComponent<Renderer>().material.color = objectColor;
    }

}
