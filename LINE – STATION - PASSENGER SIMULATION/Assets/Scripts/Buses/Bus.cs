using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : BusLine
{
    private int sittingNumber, standingNumber;
    private int sittingCapacity, standingCapacity;
    List<Passengers> pass;
    private int stationNumber;
    

    public Bus(MainMenu mainmenu)
    {
        sittingNumber = 0;
        standingNumber = 0;
        stationNumber = 0;
        sittingCapacity = (int)mainmenu.sitNum;
        standingCapacity = (int)mainmenu.standNum;
        pass = new List<Passengers>();
    }

    public int getSittingNumber()
    {
        return sittingNumber;
    }
    public int getStandingNumber()
    {
        return standingNumber;
    }
    public int getSittingCapacity()
    {
        return sittingCapacity;
    }
    public int getStandingCapacity()
    {
        return standingCapacity;
    }
    public int getCapacity()
    {
        return standingCapacity + sittingCapacity;
    }
    public int getTotalPassengers()
    {
        return standingNumber + sittingNumber;
    }
    public int getStationNumber()
    {
        return stationNumber;
    }
    public void StationNumber(int stationNumber)
    {
        this.stationNumber = stationNumber;
    }
    public int increasePassengers(Passengers pass)
    {
        if (standingNumber == standingCapacity)
        {
            Debug.Log(standingCapacity + " " + standingNumber);
            return 2;
        }            
        if (sittingNumber != sittingCapacity)
        {
            sittingNumber++;
        }
        else
        {
            standingNumber++;
        }           

        this.pass.Add(pass);
        return color();
    }

    public int decreasePassenger(string DestinationName)
    {
        for (int i = pass.Count - 1; i >= 0; i--)
        {
            if (pass[i].getDestinationStation().Equals(DestinationName))
            {
                pass.Remove(pass[i]);
                if (standingNumber > 0)
                {
                    standingNumber--;
                }
                else
                {
                    sittingNumber--;
                }
                    
            }
                
        }
        return color();
    }
    private int color()
    {
        if (sittingNumber < sittingCapacity)
            return 0;
        else if (standingNumber < standingCapacity)
            return 1;
        else return 2;
    }

}
