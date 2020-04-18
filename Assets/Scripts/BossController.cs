using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Entities
{
    #region variables
    private int hunger;
    [Range(1, 20)]
    public int hungerModificator = 1;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        hunger = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetFeeded(int foodModificator)
    {
        hunger = hunger + (1 * foodModificator);
        if (hunger > 100)
        {
            hunger = 100;
        }
    }
    public void GettingHungrier()
    {
        hunger = hunger - (1 * hungerModificator);
        if (hunger < 0)
        {
            hunger = 0;
        }
    }

    
}
