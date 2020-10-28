using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

//TODO: make this work with different lenght per seconds
public class TimeIndicatorBehaviour : MonoBehaviour
{
    public GameObject tenSecondsPrefab;

    public GameObject indicatorSliderGO;

    public Slider indicatorSlider;

    public Text timeIndication;

    private float totalTime = 0;

    public RectTransform followObject;
    public RectTransform thisTrans;

    float deltapos = 0;

    public PageManager manager;

    public float xoffset = 0;//-222.34f;
    private bool xoffsetApplied = false;

    public float currentTime;

    private void Start()
    {
        StartCoroutine(startSetup());
    }

    IEnumerator startSetup() 
    {
        yield return new WaitForEndOfFrame();
        Setup(100.0f);
    }

    public void Setup(float duration) 
    {
        totalTime = duration;
        int segments = Mathf.CeilToInt(duration / 10.0f);

        for (int i = 0; i < segments; i++)
        {
            GameObject temp = GameObject.Instantiate(tenSecondsPrefab, this.transform);
            RectTransform tempTrans = temp.GetComponent<RectTransform>();
            float xpos = i * 200;
            tempTrans.localPosition = new Vector3(xpos, -22.5f, 0);
            temp.GetComponent<TimelineIndicatorSecond>().Setup(i * 10);
        }

        float width = segments * 200;
        thisTrans.sizeDelta = new Vector2(width, thisTrans.sizeDelta.y);

        indicatorSliderGO.transform.SetAsLastSibling();

        //set slider lenght
        RectTransform sliderRect = indicatorSliderGO.GetComponent<RectTransform>();
        sliderRect.sizeDelta = new Vector2(duration * 20 , sliderRect.sizeDelta.y);
        sliderRect.localPosition = new Vector2(5, sliderRect.localPosition.y);

    }

    public float getCurrentTime()
    {
        return totalTime * indicatorSlider.value;
    }

    public void TimeChaned() 
    {
        float timePercentage = indicatorSlider.value;
        float actualTime = totalTime * timePercentage;
        currentTime = actualTime;
        timeIndication.text = SecondsToString(actualTime);
    }

    private string SecondsToString(float time)
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string timeText = minutes.ToString("00") + ":" + seconds.ToString("00");

        return timeText;
    }

    private void Update()
    {
        if (!xoffsetApplied) 
        {
            thisTrans.transform.localPosition += new Vector3(xoffset, 0, 0);

            xoffsetApplied = true;
        }
        if (!manager.busy) 
        {
            thisTrans.transform.localPosition += new Vector3(followObject.transform.position.x - deltapos, 0, 0);
            deltapos = followObject.transform.position.x;
        }
    }
}
