using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public Player player;

    public GameObject EnemyMale;

    public GameObject Weed;
    public GameObject Stone;
    public GameObject Wood;
    public GameObject Mushroom;
    public float minX = -11f;
    public float maxX = 11f;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        minX = player.transform.position.x + 2;
        SpawnEnemy(2, minX, maxX);
        SpawnWeed(3, minX, maxX);
        SpawnStone(4, minX, maxX);
        SpawnWood(2, minX, maxX);
        SpawnMushroom(10, minX, maxX);
    }

    // Update is called once per frame
    void Update()
    {
        minX = player.transform.position.x + 2;

        counter += 1;
        if (counter % 500 == 0)
        {
            SpawnEnemy(1, minX, maxX);
        }
        /*
        if (counter % 100 == 0)
        {
            SpawnWeed(1, minX, maxX);
        }
        if (counter % 100 == 0)
        {
            SpawnStone(1, minX, maxX);
        }
        if (counter % 100 == 0)
        {
            SpawnWood(1, minX, maxX);
        }
        if (counter % 100 == 0)
        {
            SpawnMushroom(1, minX, maxX);
        }
        if (counter > 1000)
        {
            counter = 0;
        }*/
    }

    void SpawnEnemy(int amount, float min, float max)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(EnemyMale, new Vector3(Random.Range(min, max), -4.21f, 0), this.transform.rotation);
        }
    }

    void SpawnDefaultEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(EnemyMale, new Vector3(5.18f, -4.21f, 0), this.transform.rotation);
        }
    }

    void SpawnWeed(int amount, float min, float max)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(Weed, new Vector3(Random.Range(min, max), -4.75f, 0), this.transform.rotation);
        }
    }

    void SpawnDefaultWeed(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(Weed, new Vector3(4.278f, -4.75f, 0), this.transform.rotation);
        }
    }

    void SpawnStone(int amount, float min, float max)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(Stone, new Vector3(Random.Range(min, max), -4.75f, 0), this.transform.rotation);
        }
    }

    void SpawnDefaultStone(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(Stone, new Vector3(8.714f, -4.75f, 0), this.transform.rotation);
        }
    }

    void SpawnWood(int amount, float min, float max)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(Wood, new Vector3(Random.Range(min, max), -4.75f, 0), this.transform.rotation);
        }
    }

    void SpawnDefaultWood(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(Wood, new Vector3(6.414f, -4.75f, 0), this.transform.rotation);
        }
    }

    void SpawnMushroom(int amount, float min, float max)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(Mushroom, new Vector3(Random.Range(min, max), -4.8f, 0), this.transform.rotation);
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
