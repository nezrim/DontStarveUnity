using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    public float time;

    public int daysCount;


    public Text timeText;
    public Text dayText;

    public Pause cam;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Pause>();
        time = 0.375f;
        daysCount = 1;
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
            time += 0.005f;

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
                dayText.text = ("Day " + daysCount + " (Night)");
            }
            else  //Day
            {
                dayText.text = ("Day " + daysCount + " (Day)");
            }
        }
    }
}
