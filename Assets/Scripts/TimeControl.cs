using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    public float time;

    public float elapsedTime;

    public float startTime = 0.375f;

    public int daysCount;

    public bool isNight = false;


    public Text timeText;
    public Text dayText;

    public float slow = 0.0002f;
    public float fast = 0.0004f;

    public string timeSpeed;

    public Pause cam;
    public Player player;

    public AudioSource dayMusic;
    public AudioSource nightMusic;


    void Start()
    {
        Application.targetFrameRate = 60;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Pause>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dayMusic = GameObject.FindGameObjectWithTag("DayMusic").GetComponent<AudioSource>();
        nightMusic = GameObject.FindGameObjectWithTag("NightMusic").GetComponent<AudioSource>();
        time = startTime;
        daysCount = 1;
        timeSpeed = "Fast";
    }

    // Update is called once per frame
    void Update()
    {
        //Ha Pause-ba vagyunk, ne mennyen az idő
        if (!cam.paused && !player.isDead)
        {
            if (!isNight)
            {
                if (!dayMusic.isPlaying)
                {
                    dayMusic.Play();
                }
                if (nightMusic.isPlaying)
                {
                    nightMusic.Stop();
                }
            }
            else
            {
                if (dayMusic.isPlaying)
                {
                    dayMusic.Stop();
                }
                if (!nightMusic.isPlaying)
                {
                    nightMusic.Play();
                }
            }

            //Slow
            if (timeSpeed.Equals("Slow"))
            {
                time += slow;
                elapsedTime += slow;
            }
            else
            //Fast
            {
                if (timeSpeed.Equals("Fast"))
                {
                    time += fast;
                    elapsedTime += fast;
                }
            }

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
                dayText.color = Color.white;
                timeText.color = Color.white;
                dayText.text = ("Day " + daysCount + " (Night)");
                isNight = true;
            }
            else  //Day
            {
                dayText.color = Color.black;
                timeText.color = Color.black;
                dayText.text = ("Day " + daysCount + " (Day)");
                isNight = false;
            }
        }
    }
}
