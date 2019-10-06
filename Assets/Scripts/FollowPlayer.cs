using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Rigidbody2D rBody;
    private Animator anim;

    private Vector2 velocity;
    public Transform player;
    public float maxSpeed = 0.01f;
    public float speed = 0.00001f;

    public float reactionSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rBody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }


    private void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(rBody.velocity.x));

        if (rBody.velocity.x < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (rBody.velocity.x > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, reactionSpeed);
        //transform.position = Vector2.MoveTowards(transform.position, new Vector2(posX, transform.position.y), speed * Time.deltaTime);

        if (rBody.velocity.x > maxSpeed)
        {
            rBody.velocity = new Vector2(maxSpeed, rBody.velocity.y);
        }

        if (rBody.velocity.x < -maxSpeed)
        {
            rBody.velocity = new Vector2(-maxSpeed, rBody.velocity.y);
        }
    }
}
