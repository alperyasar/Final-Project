using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System.Globalization;

public class SystemCall : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject bus, busTable, menu;
    List<GameObject> clone;
    List<Bus> buss;
    BusLine buses;
    TimeConvert timeConvert;
    MainMenu mainmenu;

    [Header("Bus Colors")]
    public Sprite redBus;
    public Sprite GreenBus,YellowBus;

    
    [Header("Lines")]
    public List<GameObject> lineDistance;
    public List<Button> stationNames;
    public Button closePassengerStation,busButton;
    public TextMeshProUGUI watchText, lineText, accelerationText;
    public TextMeshProUGUI busSitting, busStanding, busSitting1, busStanding1, stationName;
    public TextMeshProUGUI busNote1,busNote2;
    float acceleration = 60f;
    public GameObject showStation,BusPanel, BusPanel1;

    bool timeChanged = true;
    int mapScale = 1;
    private int i =0,stationList=-1;
    private int[] randomRatio = { 0, 0, 0, 0 };
    private int[] randomRatio1 = { 0, 0, 0, 0 };
    float runTime = 24f, virtualTime = 0f;

    void Awake()
    {
        
        timeConvert = new TimeConvert();
        buses = new BusLine();
        clone = new List<GameObject>();
        buss = new List<Bus>();
        buttonListener();
        lineText.text = buses.getLineNumber();
        watchText.text = "06:27";
        mainmenu = menu.GetComponent<MainMenu>();
        acceleration = mainmenu.acceNum;
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
        tempObject.transform.position = lineDistance[0].transform.position;
        tempObject.transform.position = new Vector3(tempObject.transform.position.x, tempObject.transform.position.y, 0);
        Button busButton = tempObject.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Button>();
        int buttontemp = i;
        busButton.onClick.AddListener(delegate () { buttonClickedBus(buttontemp); });
        Bus tempBus = new Bus(mainmenu);
        buss.Add(tempBus);        
        clone.Add(tempObject);
        
        tempObject.SetActive(true);
        int lenghtBusLine = buses.getlineLenght();
        int busnumber=0;
        int temp = 5;
        yield return new WaitForSeconds(runTime / acceleration);
        while (busnumber < lenghtBusLine)
        {
            if (temp == 5)
            {                
                busnumber++;
                temp = 0;
                clone[i].transform.position = lineDistance[busnumber-1].transform.position;
                clone[i].transform.position = new Vector3(clone[i].transform.position.x, clone[i].transform.position.y, 0);
                yield return StartCoroutine(getInToBus(busnumber-1,i));
                yield return new WaitForSeconds(runTime / acceleration);
            }
            else
            {
                clone[i].transform.position = clone[i].transform.position + ((lineDistance[busnumber].transform.position - lineDistance[busnumber-1].transform.position) / 5);
                clone[i].transform.position = new Vector3(clone[i].transform.position.x, clone[i].transform.position.y, 0);
                yield return new WaitForSeconds(runTime / acceleration);

            }
            temp++;
            
        }
        clone[i].SetActive(false);
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
        if(index != -1)
          stationName.text = buses.getStationName(index);

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
    public void buttonClickedBus(int index)
    {
        BusPanel.SetActive(true);
        string str = "sit " + buss[index].getSittingCapacity() + "/" + buss[index].getSittingNumber();
        busSitting.text = str;
        str = "stand " + buss[index].getStandingCapacity() + "/" + buss[index].getStandingNumber();
        busStanding.text = str;
        if(index > 0 && clone[index - 1] != null)
        {
            if(clone[index - 1].transform.position.x == clone[index].transform.position.x && 
               clone[index - 1].transform.position.y == clone[index].transform.position.y)
            {
                BusPanel1.SetActive(true);
                str = "sit " + buss[index - 1].getSittingCapacity() + "/" + buss[index - 1].getSittingNumber();
                busSitting1.text = str;
                str = "stand " + buss[index - 1].getStandingCapacity() + "/" + buss[index - 1].getStandingNumber();
                busStanding1.text = str;
            }
        }
        if(index < clone.Count-1 && clone[index + 1] != null)
        {
            if (clone[index + 1].transform.position.x == clone[index].transform.position.x &&
                clone[index + 1].transform.position.y == clone[index].transform.position.y)
            {
                BusPanel1.SetActive(true);
                str = "sit " + buss[index + 1].getSittingCapacity() + "/" + buss[index + 1].getSittingNumber();
                busSitting1.text = str;
                str = "stand " + buss[index + 1].getStandingCapacity() + "/"  + buss[index + 1].getStandingNumber();
                busStanding1.text = str;
            }
        }
    }
    public void busPanelClose()
    {
        BusPanel.SetActive(false);

    }
    public void busPanelClose1()
    {
        BusPanel1.SetActive(false);

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
        int randomNumberofPass = Random.Range(0, (int)mainmenu.passNumb);
        for(int j = 0; j< randomNumberofPass; j++)
        {
            /*   firstStation = Random.Range(0, lenghtBusLine - 1);
               secondStation = Random.Range(firstStation, lenghtBusLine - 1);*/
            firstStation = randomGenerator(lenghtBusLine);
            while (firstStation < 0)
            {
                if(firstStation == -1) {
                    firstStation = randomGenerator(30);
                }
                else if (firstStation == -2)
                {
                    firstStation = randomGenerator(20);
                }
                else
                    firstStation = randomGenerator(10);
            }
            secondStation = randomGeneratorFinish(firstStation);
            while (secondStation < 0)
            {
                   secondStation = randomGeneratorFinish(firstStation);                
            }
            Passengers pass = new Passengers(buses.getStationName(firstStation), buses.getStationName(secondStation));
            buses.addPassengertoStation(firstStation, pass);


        }
    }
    private int randomGenerator(int lenght)
    {
        randomGenerateCheck();
        int firstStation = Random.Range(0, lenght - 1);
        if (firstStation > 30 && randomRatio[3] == 5)
        {
            return -1;
        }
        else if(firstStation > 30 && randomRatio[3] != 5)
        {
            randomRatio[3]++;
            return firstStation;
        }
        else if (firstStation > 20 && firstStation <= 30 && randomRatio[2] == 15)
        {
            return -2;
        }
        else if (firstStation > 20 && firstStation <= 30 && randomRatio[2] != 15)
        {
            randomRatio[2]++;
            return firstStation;
        }
        else if (firstStation > 10 && firstStation <= 20 && randomRatio[1] == 30)
        {
            return -3;
        }
        else if (firstStation > 10 && firstStation <= 20 && randomRatio[1] != 30)
        {
            randomRatio[1]++;
            return firstStation;
        }
        else
            randomRatio[0]++;
        return firstStation;
    }
    private void randomGenerateCheck()
    {
        int sum = 0;
        foreach (int j in randomRatio)
            sum += j;
        if (sum == 100)
            for (int j = 0; j < randomRatio.Length; j++)
                randomRatio[j] = 0;
    }
    private int randomGeneratorFinish(int firstStation)
    {
        randomGenerateCheck1();
        int secondStation = Random.Range(firstStation, buses.getlineLenght() - 1);
        if (secondStation < 10 && randomRatio1[3] == 5)
        {
            return -1;
        }
        else if (secondStation < 10 && randomRatio1[3] != 5)
        {
            randomRatio1[3]++;
            return secondStation;
        }
        else if (secondStation >= 10 && secondStation <= 20 && randomRatio1[2] == 15)
        {
            return -2;
        }
        else if (secondStation >= 10 && secondStation <= 20 && randomRatio1[2] != 15)
        {
            randomRatio1[2]++;
            return secondStation;
        }
        else if (secondStation > 20 && secondStation <= 30 && randomRatio1[1] == 30)
        {
            return -3;
        }
        else if (secondStation > 20 && secondStation <= 30 && randomRatio1[1] != 30)
        {
            randomRatio1[1]++;
            return secondStation;
        }
        else
            randomRatio1[0]++;
        return secondStation;
    }
    private void randomGenerateCheck1()
    {
        int sum = 0;
        foreach (int j in randomRatio1)
            sum += j;
        if (sum == 100)
            for (int j = 0; j < randomRatio1.Length; j++)
                randomRatio1[j] = 0;
    }
    public void AccelerationChanged(TextMeshProUGUI text)
    {
        string strText = text.text.ToString();
        string result = strText.Substring(0, strText.Length - 1);
        acceleration = System.Single.Parse(result);
    }
    IEnumerator getInToBus(int stationNumber, int busNumber)
    {
        int busColor;
        buses.busGetInStation(stationNumber, busNumber);
        int passInStationNumber = buses.passInStation(stationNumber);
        busColor = buss[busNumber].decreasePassenger(buses.getStationName(stationNumber));
        if (busColor == 0)
            clone[busNumber].GetComponent<SpriteRenderer>().sprite = GreenBus;
        else if (busColor == 1)
            clone[busNumber].GetComponent<SpriteRenderer>().sprite = YellowBus;
        else if (busColor == 2)
            clone[busNumber].GetComponent<SpriteRenderer>().sprite = redBus;
        /*       string str = "30/" + buss[busNumber].getSittingNumber();
               busSitting.text = str;
               str = "45/" + buss[busNumber].getStandingNumber();
               busStanding.text = str;*/
        yield return new WaitForSeconds(3f / acceleration);
        if (passInStationNumber > 0)
        {
            for (int j = passInStationNumber; j > 0; j--)
            {
                if(buses.busInStation(stationNumber, busNumber) == 1)
                {
                    if (buss[busNumber].getTotalPassengers() < buss[busNumber].getCapacity())
                    {
                   //     Debug.Log("passengerNumber : " + passInStationNumber + " enter to bus : " + busNumber);
                        Passengers pass = buses.getInBus(stationNumber);
                        showStation.transform.GetChild(j-1).gameObject.SetActive(false);
                        busColor = buss[busNumber].increasePassengers(pass);
                        if(busColor == 0)
                            clone[busNumber].GetComponent<SpriteRenderer>().sprite = GreenBus;
                        else if (busColor == 1)
                            clone[busNumber].GetComponent<SpriteRenderer>().sprite = YellowBus;
                        else if (busColor == 2)
                            clone[busNumber].GetComponent<SpriteRenderer>().sprite = redBus;
                        yield return new WaitForSeconds(3f / acceleration);
                    }
                    else
                        j = 0;

                }
                else
                {
                    List<int> busses;
                    busses = buses.bussesPlate(stationNumber);
                    int otherBusPass = 0;
                    for(int k = 0;k < busses.Count; k++)
                    {
                        if(busses[k] != busNumber)
                        {
                            otherBusPass += buss[busses[k]].getTotalPassengers();
                        }
                    }
                    
                    
                    if(busses[0] == busNumber)
                    {
                        string str = passInStationNumber + " passenger in Station enter to bus : " + busNumber;
                        StartCoroutine(busNoteToScreen(busses,buses.getStationName(stationNumber),str));
                        Debug.Log("passengerNumber : " + passInStationNumber +" " + busses.Count + " enter to bus : " + busNumber);
                        Passengers pass = buses.getInBus(stationNumber);
                        showStation.transform.GetChild(j - 1).gameObject.SetActive(false);
                        busColor = buss[busNumber].increasePassengers(pass);
                        if (busColor == 0)
                            clone[busNumber].GetComponent<SpriteRenderer>().sprite = GreenBus;
                        else if (busColor == 1)
                            clone[busNumber].GetComponent<SpriteRenderer>().sprite = YellowBus;
                        else if (busColor == 2)
                            clone[busNumber].GetComponent<SpriteRenderer>().sprite = redBus;
                    }
                        
                    yield return new WaitForSeconds(3f / acceleration);
                    if (buss[busNumber].getTotalPassengers() - (otherBusPass / busses.Count - 1) > j)
                    {
                        j = 0;
                    }
                }
            }
        }
        

        buses.busGetOutStation(stationNumber, busNumber);
    }
    IEnumerator busNoteToScreen(List<int> busses,string station,string choose)
    {
        string str = "In " + station +" " + busses.Count +" bus : ";
        for(int k=0;k<busses.Count-1;k++){
            str = str + busses[k] + "-";
        }
        str = str + busses[busses.Count-1];
        busNote1.text = str;
        busNote2.text = choose;
        yield return new WaitForSeconds(60f / acceleration);
        busNote1.text = "";
        busNote2.text = ""  ;
    }
    public void exitButton()
    {
        Application.Quit();
    }
    public void fromMainMenu()
    {
        mainmenu = menu.GetComponent<MainMenu>();
        acceleration = mainmenu.acceNum;
        Debug.Log(acceleration);
    }
}
