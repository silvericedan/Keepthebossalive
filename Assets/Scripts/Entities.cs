﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entities : MonoBehaviour
{
    #region variables
    public int hitpoint;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        hitpoint = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void gettingHurt(int damage)
    {
        hitpoint -= damage;
    }

    public void heal(int hp)
    {
        hitpoint += hp;
    }
}