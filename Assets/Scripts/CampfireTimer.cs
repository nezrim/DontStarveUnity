using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireTimer : MonoBehaviour
{
    // Start is called before the first frame update

    private float step;

    //12 h
    private float maxTime = 0.5f;

    private float lifecycle;

    public TimeControl time;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("TimeControl").GetComponent<TimeControl>().timeSpeed.Equals("Fast"))
            {
                step = GameObject.FindGameObjectWithTag("TimeControl").GetComponent<TimeControl>().fast;
            }
        else
        {
            step = GameObject.FindGameObjectWithTag("TimeControl").GetComponent<TimeControl>().slow;
        }

        lifecycle = 0;

    }

    // Update is called once per frame
    void Update()
    {
        lifecycle += step;

        if (lifecycle > maxTime)
        {
            Destroy(this.gameObject);
        }
    }
}
