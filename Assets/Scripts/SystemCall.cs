using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class SystemCall : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bus, busTable;
    List<GameObject> clone;
    BusLine buses;
    TimeConvert timeConvert;

    public List<GameObject> lineDistance;
    public List<Button> stationNames;
    public Button closePassengerStation;
    public TextMeshProUGUI watchText, lineText, accelerationText;
    public float acceleration = 1f;
    public GameObject showStation;

    bool timeChanged = true;
    int mapScale = 1;
    private int i =0,stationList=-1;
    float runTime = 24f, virtualTime = 0f;

    void Start()
    {
        timeConvert = new TimeConvert();
        buses = new BusLine();
        clone = new List<GameObject>();
        buttonListener();
        lineText.text = buses.getLineNumber();
        watchText.text = "06:00";

    }

    /// <summary>
    /// station names buttons
    /// </summary>
    private void buttonListener()
    {
        int j;
        for ( j = 0; j < stationNames.Count; j++)
        {
            int k = j;
            stationNames[k].onClick.AddListener(delegate () { buttonClicked(k); });
        }

        closePassengerStation.onClick.AddListener(delegate () { buttonClicked2(); });
    }
    // Update is called once per frame
    void Update()
    {


        if (Timechecking())
        {
            StartCoroutine(SystemRun(i));
            i++;
        }
          
    }
    IEnumerator SystemRun(int i)
    {
        GameObject tempObject = Instantiate(bus, bus.transform.position, bus.transform.rotation);
        tempObject.transform.SetParent(busTable.transform);
        clone.Add(tempObject);
        tempObject.SetActive(true);
        int lenghtBusLine = buses.getlineLenght();
        int busnumber=0;
        int temp = 1;
        yield return new WaitForSeconds(runTime / acceleration);
        while (busnumber < lenghtBusLine)
        {
            
            if (temp == 5)
            {
                busnumber++;
                temp = 0;
                clone[i].transform.position = lineDistance[busnumber].transform.position;
                clone[i].transform.position = new Vector3(clone[i].transform.position.x, clone[i].transform.position.y, 0);
                yield return new WaitForSeconds(runTime / acceleration);
            }
            else
            {
                clone[i].transform.position = clone[i].transform.position + ((lineDistance[busnumber + 1].transform.position - lineDistance[busnumber].transform.position) / 5);
                clone[i].transform.position = new Vector3(clone[i].transform.position.x, clone[i].transform.position.y, 0);
                yield return new WaitForSeconds(runTime / acceleration);

            }
            temp++;
            
        }
    }
    IEnumerator ScaleMap(float scaleSize,float position,int index)
    {
        int timer = 0;
        while (timer < 5 && mapScale == 2)
        {
            timer++;
            busTable.transform.localScale = new Vector3(busTable.transform.localScale.x + scaleSize/5, busTable.transform.localScale.y + scaleSize/5, busTable.transform.localScale.z);
            busTable.transform.position = new Vector3(busTable.transform.position.x + position, busTable.transform.position.y + position, busTable.transform.position.z);
       /*     clone[0].transform.localScale = new Vector3(clone[0].transform.localScale.x + scaleSize/5, clone[0].transform.localScale.y + scaleSize/5, clone[0].transform.localScale.z);
            clone[0].transform.position = new Vector3(clone[0].transform.position.x + position, clone[0].transform.position.y + position, clone[0].transform.position.z);*/
            yield return new WaitForSeconds(0.1f);
        }

        buses.showList(index,stationList, showStation);

        while (timer < 5 && mapScale == 3)
        {
            timer++;
            busTable.transform.localScale = new Vector3(busTable.transform.localScale.x + scaleSize / 5, busTable.transform.localScale.y + scaleSize / 5, busTable.transform.localScale.z);
            busTable.transform.position = new Vector3(busTable.transform.position.x + position, busTable.transform.position.y + position, busTable.transform.position.z);
            /*     clone[0].transform.localScale = new Vector3(clone[0].transform.localScale.x + scaleSize/5, clone[0].transform.localScale.y + scaleSize/5, clone[0].transform.localScale.z);
                 clone[0].transform.position = new Vector3(clone[0].transform.position.x + position, clone[0].transform.position.y + position, clone[0].transform.position.z);*/
            yield return new WaitForSeconds(0.1f);
        }
        if(mapScale == 3)
        {
            mapScale = 1;
        }
        else if(mapScale == 2)
            mapScale = 0;
        stationList = index;
    }

    public void buttonClicked(int index)
    {
        if (mapScale == 1)
        {
            mapScale = 2;
        }

        StartCoroutine(ScaleMap(-0.3f * 0.02985489f,-0.4f,index));



    }
    public void buttonClicked2()
    {
            mapScale = 3;

        StartCoroutine(ScaleMap(0.3f * 0.02985489f, 0.4f,-1));


    }
    private bool Timechecking()
    {
        virtualTime = Time.time * acceleration;
        int tempVirtualTime = (int)virtualTime;
        if (tempVirtualTime % 60 == 0 && timeChanged)
        {
            timeChanged = false;
            string tempWatch = timeConvert.watch();
            createRandomPassengers();
            watchText.text = tempWatch;
            if(buses.timeCheck(tempWatch))
                return true;

        }
        else if (!timeChanged && tempVirtualTime % 60 != 0)
            timeChanged = true;
        return false;
    }
    /// <summary>
    /// create random person
    /// </summary>
    private void createRandomPassengers()
    {
        int firstStation;
        int secondStation;
        int lenghtBusLine = buses.getlineLenght();
        int randomNumberofPass = Random.Range(0, 10);
        for(int j = 0; j< randomNumberofPass; j++)
        {
            firstStation = Random.Range(0, lenghtBusLine);
            secondStation = Random.Range(firstStation, lenghtBusLine);
            Passengers pass = new Passengers(buses.getStationName(firstStation), buses.getStationName(secondStation));
            buses.addPassengertoStation(firstStation, pass);


        }
    }
    public void AccelerationChanged(TextMeshProUGUI text)
    {
        string strText = text.text.ToString();
        Debug.Log(strText);
            acceleration = float.Parse(strText, CultureInfo.InvariantCulture);
    }
}
