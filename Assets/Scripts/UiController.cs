using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text minionText;
    public Text DayNightText;
    public GameObject lair;
    // Start is called before the first frame update
    void Start()
    {
        DayNightText.text = "Day";
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetMinionText(int minionCount)
    {
        minionText.text = string.Format("Minions: {0}", minionCount);
    }

    public void ChangeDayNight(DayNight dayNight)
    {
        if (dayNight == DayNight.Day)
        {
            DayNightText.text = "Night";
        }
        else
        {
            DayNightText.text = "Day";
        }
    }
    public void ToggleLairUI()
    {
        lair.SetActive(!lair.activeSelf);
    } 

    public void DisableLairUI()
    {
        lair.SetActive(false);
    }
    public void EnableLairUI()
    {
        lair.SetActive(true);
    }
}