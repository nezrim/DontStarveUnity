using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathText : MonoBehaviour
{
    public Text deathText;
    public Text continueText;

    public TimeControl time;

    // Start is called before the first frame update
    void Start()
    {
        time = GameObject.FindGameObjectWithTag("TimeControl").GetComponent<TimeControl>();

        float timeAmount = time.elapsedTime%1;
        float days = time.daysCount - 1;

        deathText.text = "You have survived for " + days + " days, " + Mathf.Floor(timeAmount % 1f * 24).ToString("00") + " hours, " + Mathf.Floor(((timeAmount % 1f * 24) % 1f) * 60).ToString("00") + " minutes.";
        continueText.text = "Press 'R' to start a new game, or 'E' to exit";

    }

    // Update is called once per frame
    void Update()
    {

    }
}
