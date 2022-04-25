
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeadDetect : MonoBehaviour
{
    private GameObject enemy;


    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(!col.gameObject.GetComponent<PlayerMovement>().immune) {
            GetComponent<BoxCollider2D>().enabled = false;
            enemy.GetComponent<SpriteRenderer>().flipY = true;
            enemy.GetComponent<BoxCollider2D>().enabled = false;
            enemy.GetComponent<AIPatrol>().mustPatrol = false;
            enemy.GetComponent<AIPatrol>().rb.velocity = new Vector2(UnityEngine.Random.Range(10, 30), UnityEngine.Random.Range(-40, -35)) * Time.deltaTime*10;
        }
        
        // Vector3 movement = new Vector3(UnityEngine.Random.Range(40, 70), UnityEngine.Random.Range(-40, 40), 0f);
        // enemy.transform.position += movement * Time.deltaTime;
    }
}
