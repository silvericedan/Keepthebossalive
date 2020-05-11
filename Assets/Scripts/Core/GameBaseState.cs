using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*CORE*/
public abstract class GameBaseState
{
    public abstract void EnterState(GameController game);
    public abstract void Update(GameController game);
}
