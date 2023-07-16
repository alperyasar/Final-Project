using UnityEngine;
using System.Collections.Generic;


public class Stations
{
    private string stationName;
    private List<Passengers> pass;
    private List<int> busses;
 //   public List<GameObject> showPassenger;

    private void Start()
    {
        
    }
    public Stations(string stationName)
    {
        this.stationName = stationName;
        pass = new List<Passengers>();
        busses = new List<int>();
    }

    public string getName()
    {
        return stationName;
    }
    public int getNumber()
    {
        return pass.Count;
    }

    public void addPassenger(Passengers pass)
    {
        this.pass.Add(pass);
    }
    public void setBus(int i)
    {
        busses.Add(i);
    }

    public void removeBus(int i)
    {
        busses.Remove(i);
    }
    public int busInStation()
    {
        return busses.Count;
    }

    public int passInStation()
    {
        return pass.Count;
    }
    public List<int> bussesPlate()
    {
        return busses;
    }
    public void listPassengers(GameObject stationList)
    {
        GameObject temp = stationList;
        temp.SetActive(true);
        for (int i = 0; i < pass.Count; i++)
        {
            temp.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void closeList(GameObject stationList)
    {
        
        for (int i = 0; i < pass.Count; i++)
        {
            stationList.transform.GetChild(i).gameObject.SetActive(false);
        }
        stationList.SetActive(false);
    }
    public Passengers removePass()
    {
        Passengers temp = pass[0];
        pass.RemoveAt(0);
        return temp;
    }
}
