using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    public float time;

    public int daysCount;

    public bool isNight;


    public Text timeText;
    public Text dayText;

    public Pause cam;

    public GameObject dayBG;
    public GameObject nightBG;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Pause>();
        dayBG = GameObject.FindGameObjectWithTag("BGDay");
        nightBG = GameObject.FindGameObjectWithTag("BGNight");
        time = 0.375f;
        daysCount = 1;

        nightBG.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Ha Pause-ba vagyunk, ne mennyen az idő
        if (!cam.paused)
        {
            //Real
            //time += 0.0001f;

            //Debug
            time += 0.001f;

            float dayNormalized = time % 1f;

            float hoursPerDay = 24f;
            float minutesPerHour = 60f;

            string hourString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");
            string minuteString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");

            if (time > 1)
            {
                time = 0f;
                daysCount++;
            }

            timeText.text = hourString + ":" + minuteString;


            if (time < 0.25f || time > 0.875f)
            { //Night
                nightBG.SetActive(true);
                dayBG.SetActive(false);
                dayText.color = Color.white;
                timeText.color = Color.white;
                dayText.text = ("Day " + daysCount + " (Night)");
                isNight = true;
            }
            else  //Day
            {
                dayBG.SetActive(true);
                nightBG.SetActive(false);
                dayText.color = Color.black;
                timeText.color = Color.black;
                dayText.text = ("Day " + daysCount + " (Day)");
                isNight = false;
            }
        }
    }
}
