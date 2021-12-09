using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeConvert : MonoBehaviour
{
    // Start is called before the first frame update
    private int minute=29;
    private int hour=6;

    /// <summary>
    /// increase time
    /// first convert hours and minute to string and 
    /// add 0 to left is number less than 10
    /// </summary>
    /// <returns>hour as like as 10:04</returns>
    public string watch()
    {
        minute++;
        if(minute == 60)
        {
            minute = 0;
            hour++;
        }
        if(hour == 24)
        {
            minute = 0;
            hour = 0;
        }

        return hour.ToString().PadLeft(2,'0') + ":" + minute.ToString().PadLeft(2,'0');
    }
}
