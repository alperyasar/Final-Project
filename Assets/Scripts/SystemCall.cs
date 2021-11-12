using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemCall : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bus;
    public List<GameObject> lineDistance;
    public List<Button> stationNames;
    public GameObject busTable;
    int i=0;
    void Start()
    {
        BusLine bus = new BusLine();
        stationNames[0].onClick.AddListener(delegate () { buttonClicked(); });
        stationNames[1].onClick.AddListener(delegate () { buttonClicked(); });
        stationNames[2].onClick.AddListener(delegate () { buttonClicked(); });
        stationNames[3].onClick.AddListener(delegate () { buttonClicked(); });
        stationNames[4].onClick.AddListener(delegate () { buttonClicked(); });
        stationNames[5].onClick.AddListener(delegate () { buttonClicked(); });
        stationNames[6].onClick.AddListener(delegate () { buttonClicked(); });
        stationNames[7].onClick.AddListener(delegate () { buttonClicked(); });
        stationNames[8].onClick.AddListener(delegate () { buttonClicked(); });
    }

    // Update is called once per frame
    void Update()
    {

        if(i == 0)
           StartCoroutine(SystemRun());
        i++;
    }
    IEnumerator SystemRun()
    {
        GameObject clone = Instantiate(bus, bus.transform.position, bus.transform.rotation);
        clone.SetActive(true);
        int busnumber=0;
        int temp = 0;
        while (busnumber < 40)
        {
            
            if (temp == 5)
            {
                busnumber++;
                temp = 0;
                clone.transform.position = lineDistance[busnumber].transform.position;
                clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, 0);
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                clone.transform.position = clone.transform.position + ((lineDistance[busnumber + 1].transform.position - lineDistance[busnumber].transform.position) / 5);
                clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, 0);
                
            }
            temp++;
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator Wait(float duration)
    {
        Debug.Log("Process() function calling Wait function at " + Time.time);
        Debug.Log("procDuration is " + duration);
        yield return new WaitForSeconds(duration);   //Wait
        Debug.Log("Process() function after returning from the Wait Function, the time is:" + Time.time);
    }
    public void buttonClicked()
    {
        
        busTable.transform.localScale = new Vector3(busTable.transform.localScale.x - 0.3f, busTable.transform.localScale.y - 0.3f, busTable.transform.localScale.z);
        busTable.transform.position = new Vector3(busTable.transform.position.x - 1.5f, busTable.transform.position.y - 1.5f, busTable.transform.position.z);
        
        Debug.Log(Time.time);

    }
}
