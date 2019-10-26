using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public Player player;

    public TimeControl time;
    public bool isNight;

    public GameObject EnemyMale;
    public GameObject EnemyThief;

    public GameObject Weed;
    public GameObject DayBG;
    public GameObject NightBG;
    public List<GameObject> dayBGs = new List<GameObject>();
    public List<GameObject> nightBGs = new List<GameObject>();
    public GameObject Stone;
    public GameObject Wood;
    public GameObject Mushroom;

    public SpriteRenderer firstBGRenderer;

    public float minX = -11f;
    public float maxX = 11f;

    // Start is called before the first frame update
    void Start()
    {
        time = GameObject.FindGameObjectWithTag("TimeControl").GetComponent<TimeControl>();
        isNight = time.isNight;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        minX = player.transform.position.x;
 
        Instantiate(DayBG, DayBG.transform.position, DayBG.transform.rotation);
        Instantiate(NightBG, NightBG.transform.position, NightBG.transform.rotation);

        dayBGs.AddRange(GameObject.FindGameObjectsWithTag("BGDay"));
        nightBGs.AddRange(GameObject.FindGameObjectsWithTag("BGNight"));

        SpawnEnemy(1, minX, maxX);
        SpawnThief(1, minX, maxX);
        SpawnWeed(1, minX, maxX);
        SpawnStone(1, minX, maxX);
        SpawnWood(1, minX, maxX);
        SpawnMushroom(1, minX, maxX);

        CheckBGType();
    }

    void CheckBGType()
    {

        //Debug.Log("isNight: " + time.isNight);
        //Debug.Log("dayBGs: " + dayBGs.Count);
        //Debug.Log("nightBGs: " + nightBGs.Count);

        if (!time.isNight)

        {
            foreach (var item in dayBGs)
            {
                item.SetActive(true);
            }
            foreach (var item in nightBGs)
            {
                item.SetActive(false);
            }
        }
        else
        {
            foreach (var item in dayBGs)
            {
                item.SetActive(false);
            }
            foreach (var item in nightBGs)
            {
                item.SetActive(true);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        CheckBGType();
    }

    public void SpawnThief(int amount, float min, float max)
    {
        for (int i = 0; i < amount; i++)
        {
            float random = UnityEngine.Random.Range(min, max);
            if (Mathf.Abs(player.transform.position.x - random) > 2)
            {
                Instantiate(EnemyThief, new Vector3(random, -4.21f, 0), this.transform.rotation);
            }
            else
            {
                SpawnThief(amount, min, max);
            }
        }
    }

    void SpawnDefaultThief(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(EnemyThief, new Vector3(5.18f, -4.21f, 0), this.transform.rotation);
        }
    }

    public void SpawnEnemy(int amount, float min, float max)
    {
        for (int i = 0; i < amount; i++)
        {
            float random = UnityEngine.Random.Range(min, max);
            if (Mathf.Abs(player.transform.position.x - random) > 2)
            {
                Instantiate(EnemyMale, new Vector3(random, -4.21f, 0), this.transform.rotation);
            }
            else
            {
                SpawnEnemy(amount, min, max);
            }
        }
    }

    void SpawnDefaultEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(EnemyMale, new Vector3(5.18f, -4.21f, 0), this.transform.rotation);
        }
    }

    public void SpawnWeed(int amount, float min, float max)
    {
        float random = UnityEngine.Random.Range(min, max);
        if (Mathf.Abs(player.transform.position.x - random) > 2)
        {
            Instantiate(Weed, new Vector3(random, -4.75f, 0), this.transform.rotation);
        }
        else
        {
            SpawnWeed(amount, min, max);
        }
    }

    void SpawnDefaultWeed(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(Weed, new Vector3(4.278f, -4.75f, 0), this.transform.rotation);
        }
    }

    public void SpawnStone(int amount, float min, float max)
    {
        float random = UnityEngine.Random.Range(min, max);
        if (Mathf.Abs(player.transform.position.x - random) > 2)
        {
            Instantiate(Stone, new Vector3(random, -4.75f, 0), this.transform.rotation);
        }
        else
        {
            SpawnStone(amount, min, max);
        }
    }

    void SpawnDefaultStone(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(Stone, new Vector3(8.714f, -4.75f, 0), this.transform.rotation);
        }
    }

    public void SpawnWood(int amount, float min, float max)
    {
        float random = UnityEngine.Random.Range(min, max);
        if (Mathf.Abs(player.transform.position.x - random) > 2)
        {
            Instantiate(Wood, new Vector3(random, -4.75f, 0), this.transform.rotation);
        }
        else
        {
            SpawnWood(amount, min, max);
        }
    }

    void SpawnDefaultWood(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(Wood, new Vector3(6.414f, -4.75f, 0), this.transform.rotation);
        }
    }

    public void SpawnMushroom(int amount, float min, float max)
    {
        float random = UnityEngine.Random.Range(min, max);
        if (Mathf.Abs(player.transform.position.x - random) > 2)
        {
            Instantiate(Mushroom, new Vector3(random, -4.8f, 0), this.transform.rotation);
        }
        else
        {
            SpawnMushroom(amount, min, max);
        }
    }

    void SpawnDefaultMushroom(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(Mushroom, new Vector3(0.46f, -4.8f, 0), this.transform.rotation);
        }
    }
}
