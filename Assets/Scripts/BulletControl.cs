using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public Player player;
    private Rigidbody2D rBody;

    private Vector3 startPosition;
    public float maxDistance = 5;
    public float speed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rBody = gameObject.GetComponent<Rigidbody2D>();
        startPosition = GetComponent<Transform>().position;

        Vector3 bulletSpeed;
        Quaternion bulletRotation;

        if (player.transform.localScale.x>0)
        {
            bulletSpeed = new Vector3(speed, 0, 0);
            bulletRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            bulletSpeed = new Vector3(-1 * speed, 0, 0);
            bulletRotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }

        rBody.velocity = bulletSpeed;
        this.transform.rotation = bulletRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if ((GetComponent<Transform>().position - startPosition).magnitude > maxDistance)
        {
            Destroy(this.gameObject);
        }
    }
}
