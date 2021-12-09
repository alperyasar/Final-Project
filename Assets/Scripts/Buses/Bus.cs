using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : BusLine
{
    private int sittingNumber;
    private int standingNumber;
    List<Passengers> pass;

    public Bus()
    {
        sittingNumber = 0;
        standingNumber = 0;
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

    public bool increasePassengers(Passengers pass)
    {
        if (standingNumber == 45)
            return false;
        if (sittingNumber != 30)
            sittingNumber++;
        else
            standingNumber++;

        this.pass.Add(pass);
        return true;
    }


}
