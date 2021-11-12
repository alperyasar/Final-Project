using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BusLine 
{
    protected List<string> stationNames;
    protected List<string> departureTimes;
    public string LineNumber;


    public BusLine()
    {
        StreamReader str = new StreamReader("Assets/Resources/BusSchedules/133N/133NKartalKalkisDuraklar.csv");
        string dataStr = str.ReadLine();
        while (dataStr != null){
            Debug.Log(dataStr);
            /*   var dataValues = dataStr.Split(';');
               for (int d = 0; d < dataValues.Length; d++)
               {
                   print(dataValues[d]); // what you get is split sequential data that is column-first, then row
               }
               */
            dataStr = str.ReadLine();
        }

        str.Close();
    }
}
