using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTutorialState : GameBaseState
{
    int position;
    Tutorial tutorial;
    public override void EnterState(GameController game)
    {
        tutorial = game.Tutorial.GetComponent<Tutorial>();
        position = 0;
        foreach (GameObject dialog in tutorial.dialogs)
        {
            dialog.GetComponent<SpriteRenderer>().enabled = false;
            dialog.transform.Find("OkButton").gameObject
                .GetComponent<SpriteRenderer>().enabled = false;
        }
        tutorial.dialogs[position].GetComponent<SpriteRenderer>().enabled = true;
        tutorial.dialogs[position].transform.Find("OkButton").gameObject
                .GetComponent<SpriteRenderer>().enabled = true;
    }

    public override void Update(GameController game)
    {
        if (position == 0)
        {
            if (tutorial.dialogs[position].transform.Find("OkButton").gameObject.GetComponent<okbutton1>().pressed)
            {
                tutorial.dialogs[position].GetComponent<SpriteRenderer>().enabled = false;
                tutorial.dialogs[position].transform.Find("OkButton").gameObject
                    .GetComponent<SpriteRenderer>().enabled = false;
                position += 1;
            }
        }
        if (position == 1)
        {
            tutorial.dialogs[position].GetComponent<SpriteRenderer>().enabled = true;
            tutorial.dialogs[position].transform.Find("OkButton").gameObject
                .GetComponent<SpriteRenderer>().enabled = true;
            if (tutorial.dialogs[position].transform.Find("OkButton").gameObject.GetComponent<okbutton1>().pressed)
            {
                tutorial.dialogs[position].GetComponent<SpriteRenderer>().enabled = false;
                tutorial.dialogs[position].transform.Find("OkButton").gameObject
                    .GetComponent<SpriteRenderer>().enabled = false;
                position += 1;
            }
        }
        if (position == 2)
        {
            tutorial.dialogs[position].GetComponent<SpriteRenderer>().enabled = true;
            tutorial.dialogs[position].transform.Find("OkButton").gameObject
                .GetComponent<SpriteRenderer>().enabled = true;
            if (tutorial.dialogs[position].transform.Find("OkButton").gameObject.GetComponent<okbutton1>().pressed)
            {
                tutorial.dialogs[position].GetComponent<SpriteRenderer>().enabled = false;
                tutorial.dialogs[position].transform.Find("OkButton").gameObject
                    .GetComponent<SpriteRenderer>().enabled = false;
                position += 1;
            }
        }
        if (position == 3)
        {
            game.TransitionToState(game.LairState);
        }
    }
}
