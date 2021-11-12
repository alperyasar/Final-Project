using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class test : MonoBehaviour
{
   // LineRenderer linerenderer;
    public Button testButton;
    // Start is called before the first frame update
    void Start()
    {
        BusLine bus = new BusLine();
        /*   linerenderer = GetComponent<LineRenderer>();
           var points = new Vector3[20];
           Vector3 temp = new Vector3(3f, 0.19f, 3f);
           linerenderer.SetPosition(0, temp);*/

        testButton.onClick.AddListener(buttonTest);
        /*        for (int i = 0; i < 20; i++)
                {
                    linerenderer.GetPosition(i);
                    linerenderer.SetPosition(0, temp);
                }
                linerenderer.SetPositions(points);*/
    }

    // Update is called once per frame
    private void buttonTest()
    {
        print("alper");
    }
    void Update()
    {
        
    }
}
