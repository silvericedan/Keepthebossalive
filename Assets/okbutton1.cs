using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class okbutton1 : MonoBehaviour
{
    public bool pressed;
    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        pressed = true;
    }
    private void OnMouseUp()
    {
        pressed = false;
    }
}
