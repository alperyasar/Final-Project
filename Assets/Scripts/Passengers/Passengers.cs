using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passengers
{
    private string stationName;
    private string destinationName;

    public Passengers(string stationName,string destinationName)
    {
        this.stationName = stationName;
        this.destinationName = destinationName;
    }

    public string getCurrentStation()
    {
        return stationName;
    }
    public string getDestinationStation()
    {
        return destinationName;
    }
}
