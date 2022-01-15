using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Button> lines;
    public Button play, exit;
    public Sprite highlighted,normal;
    public TextMeshProUGUI LineError;
    public float sitNum=30, standNum=45, acceNum=1,passNumb = 10;
    public GameObject busLine, system;
    bool accesLine = false;
    void Start()
    {
        buttonListener();
        play.onClick.AddListener(delegate () { StartCoroutine(playButton()); });
        exit.onClick.AddListener(delegate () { exitButton(); });
    }
    private void buttonListener()
    {
        int j;
        for (j = 0; j < lines.Count; j++)
        {
            int k = j;
            lines[k].onClick.AddListener(delegate () { buttonClicked(k); });
        }

    }

    public void buttonClicked(int index)
    {
        if (index == 0)
        {
            lines[0].GetComponent<Image>().sprite = highlighted;
            accesLine = true;
        }

        else
        {
            lines[0].GetComponent<Image>().sprite = normal;
            accesLine = false;

        }


    }
    // Update is called once per frame
    IEnumerator playButton()
    {
        if (!accesLine)
        {
            StartCoroutine(LineErrorDebug());
        }
        else
        {
            system.GetComponent<SystemCall>().fromMainMenu();
            busLine.SetActive(true);
            system.SetActive(true);
            
            yield return new WaitForSeconds(0.02f);
            this.gameObject.SetActive(false);
        }
        
    }
    public void AccelerationChanged(TextMeshProUGUI text)
    {
        string strText = text.text.ToString();
        string result = strText.Substring(0, strText.Length - 1);
        acceNum = System.Single.Parse(result);
    }
    public void sitChanged(TextMeshProUGUI text)
    {
        string strText = text.text.ToString();
        string result = strText.Substring(0, strText.Length - 1);
        sitNum = System.Single.Parse(result);
    }
    public void standChanged(TextMeshProUGUI text)
    {
        string strText = text.text.ToString();
        string result = strText.Substring(0, strText.Length - 1);
        standNum = System.Single.Parse(result);
    }
    public void passChanged(TextMeshProUGUI text)
    {
        string strText = text.text.ToString();
        string result = strText.Substring(0, strText.Length - 1);
        passNumb = System.Single.Parse(result);
    }
    IEnumerator LineErrorDebug()
    {
        LineError.text = "Please select green lines. \n" + "The grey Lines not ready!";
        yield return new WaitForSeconds(3f);
        LineError.text = "";
    }
    void exitButton()
    {
        Application.Quit();
    }
}
