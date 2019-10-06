using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector2 velocity;
    public float smoothY;
    public float smoothX;

    public GameObject player;

    /*public bool camBounds;

    public Vector3 minPos;
    public Vector3 maxPos;
    */

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothX);
        //float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothY);

        transform.position = new Vector3(posX + 0.2f, transform.position.y, transform.position.z);
        /*
        if (camBounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x+0.2f, minPos.x, maxPos.x),
                Mathf.Clamp(transform.position.y, minPos.y, maxPos.y),
                Mathf.Clamp(transform.position.z, minPos.z, maxPos.z));
        }*/
    }
}
