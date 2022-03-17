using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerCollider : MonoBehaviour
{
    public Text text;

    public Text donutsCollectedText;


    // Start is called before the first frame update
    void Start()
    {
        text.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "player")
        {
            Destroy(this.gameObject);
            PlayerMovement.donuts++;
            donutsCollectedText.text = donutsCollectedText.text.Split(':')[0] + ": " + PlayerMovement.donuts;
            if (this.gameObject.tag == "lastDonut")
            {
                text.text = text.text.Split(':')[0] + ":" + PlayerMovement.donuts;
                text.gameObject.SetActive(true);
                PlayerMovement.won = true;
            }
            
        }
    }
}
