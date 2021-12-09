using UnityEngine;
using System.Collections.Generic;


public class Stations
{
    private string stationName;
    private int numberOfPassengers;
    private List<Passengers> pass;
 //   public List<GameObject> showPassenger;

    private void Start()
    {
        
    }
    public Stations(string stationName)
    {
        this.stationName = stationName;
        numberOfPassengers = 0;
        pass = new List<Passengers>();
    }

    public string getName()
    {
        return stationName;
    }
    public int getNumber()
    {
        return numberOfPassengers;
    }

    public void addPassenger(Passengers pass)
    {
        this.pass.Add(pass);
        numberOfPassengers++;
    }

    public void listPassengers(GameObject stationList)
    {
        GameObject temp = stationList;
        temp.SetActive(true);
        for (int i = 0; i < numberOfPassengers; i++)
        {
            temp.transform.GetChild(i).gameObject.SetActive(true);
            Debug.Log(i);
        }
    }

    public void closeList(GameObject stationList)
    {
        
        for (int i = 0; i < numberOfPassengers; i++)
        {
            stationList.transform.GetChild(i).gameObject.SetActive(false);
        }
        stationList.SetActive(false);
    }
}
