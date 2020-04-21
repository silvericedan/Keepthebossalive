using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public GameObject dialogBox;
    public GameObject okButton;
    public GameObject dialogBox2;
    public GameObject okButton2;
    public GameObject dialogBox3;
    public GameObject okButton3;
    // Start is called before the first frame update
    void Start()
    {
        dialogBox2.GetComponent<SpriteRenderer>().enabled = false;
        okButton2.GetComponent<SpriteRenderer>().enabled = false;
        dialogBox3.GetComponent<SpriteRenderer>().enabled = false;
        okButton3.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (okButton.GetComponent<okbutton1>().pressed)
        {
            dialogBox.GetComponent<SpriteRenderer>().enabled = false;
            okButton.GetComponent<SpriteRenderer>().enabled = false;
            dialogBox2.GetComponent<SpriteRenderer>().enabled = true;
            okButton2.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (okButton2.GetComponent<okbutton1>().pressed)
        {
            dialogBox2.GetComponent<SpriteRenderer>().enabled = false;
            okButton2.GetComponent<SpriteRenderer>().enabled = false;
            dialogBox3.GetComponent<SpriteRenderer>().enabled = true;
            okButton3.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (okButton3.GetComponent<okbutton1>().pressed)
        {
            dialogBox3.GetComponent<SpriteRenderer>().enabled = false;
            okButton3.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
