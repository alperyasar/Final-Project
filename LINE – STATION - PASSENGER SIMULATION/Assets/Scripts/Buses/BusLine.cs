using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// Each busline has departure time and station name
/// read it.
/// 
/// </summary>
public class BusLine 
{
    protected List<Stations> stationNames;
    protected List<List<string>> departureTimes;
    private string LineNumber;

    /// <summary>
    /// constructer of busline
    /// </summary>
    public BusLine()
    {
        stationNames = new List<Stations>();
        departureTimes = new List<List<String>>();
        stationsNames();
        departureTime();
    }

    /// <summary>
    /// read departuretime from file to 2 dimensional list
    /// </summary>
    private void departureTime()
    {
        StreamReader str = new StreamReader("Assets/Resources/BusSchedules/133N/133NMaltepeKalkisSaatleri.csv");
        string dataStr = str.ReadLine();
        dataStr = str.ReadLine();
        while (dataStr != null)
        {
            var dataValues = dataStr.Split(';');
            List<String> temp  = new List<string>();
            for (int d = 0; d < dataValues.Length; d++)
            {
                temp.Add(dataValues[d]);
            }
            departureTimes.Add(temp);
            dataStr = str.ReadLine();
        }

        str.Close();
    }
    /// <summary>
    /// read lines from file and read line number
    /// </summary>
    private void stationsNames()
    {
        StreamReader str = new StreamReader("Assets/Resources/BusSchedules/133N/133NMaltepeKalkisDuraklar.csv");
        string dataStr = str.ReadLine();
        LineNumber = dataStr;
        dataStr = str.ReadLine();

        while (dataStr != null)
        {
            stationNames.Add(new Stations(dataStr));
            dataStr = str.ReadLine();
        }

        str.Close();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>How many station in line</returns>
    public int getlineLenght()
    {
        return stationNames.Count;
    }
    public string getLineNumber()
    {
        return LineNumber;
    }

    public bool timeCheck(string time)
    {
        for(int i = 0; i< departureTimes.Count; i++)
        {
            if (departureTimes[i][0].CompareTo(time) == 0)
                return true;
        }
        return false;
    }

    public string getStationName(int index)
    {
        return stationNames[index].getName();
    }

    public void addPassengertoStation(int index,Passengers pass)
    {
        stationNames[index].addPassenger(pass);
    }
    public void showList(int index,int stationList,GameObject showList)
    {
        if(index == -1)
            stationNames[stationList].closeList(showList);
        else {
            if(stationList != -1)
            {
                stationNames[stationList].closeList(showList);
            }
            stationNames[index].listPassengers(showList);
        }
    }
    public void busGetInStation(int stationNumber, int busNumber)
    {
        stationNames[stationNumber].setBus(busNumber);
       
    }
    public void busGetOutStation(int stationNumber, int busNumber)
    {
        stationNames[stationNumber].removeBus(busNumber);

    }
    public int busInStation(int stationNumber, int busNumber)
    {
        return stationNames[stationNumber].busInStation();
    }
    public int passInStation(int stationNumber)
    {
        return stationNames[stationNumber].passInStation();
    }
    public Passengers getInBus(int stationNumber)
    {
        return stationNames[stationNumber].removePass();
    }
    public List<int> bussesPlate(int stationNumber)
    {
        return stationNames[stationNumber].bussesPlate();
    }
}

