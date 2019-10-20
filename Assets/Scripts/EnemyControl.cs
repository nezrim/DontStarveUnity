using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private Rigidbody2D rBody;
    private Animator anim;
    private new Renderer renderer;

    private Color originalColor;

    private Vector2 velocity;
    public Transform player;
    public float maxSpeed = 50f;
    public float speed = 50f;

    public float reactionSpeed = 0.1f;

    public float life = 3f;

    public bool reachedPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rBody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponent<SpriteRenderer>();

        originalColor = renderer.material.color;
        reachedPlayer = false;
    }


    private void Update()
    {
        anim.SetBool("reachedPlayer", reachedPlayer);
        anim.SetFloat("speed", Mathf.Abs(rBody.velocity.x));

        if (rBody.velocity.x < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (rBody.velocity.x > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //Debug.Log(reachedPlayer);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, reactionSpeed);
        //transform.position = Vector2.MoveTowards(transform.position, new Vector2(posX, transform.position.y), 0.01f);

        if(Mathf.Abs(transform.position.x - player.transform.position.x) < 8 && this.reachedPlayer==false)
        {
            rBody.AddForce((Vector2.right * 50f) * getDirection());
        }
        

        if (rBody.velocity.x > maxSpeed)
        {
            rBody.velocity = new Vector2(maxSpeed, rBody.velocity.y);
        }

        if (rBody.velocity.x < -maxSpeed)
        {
            rBody.velocity = new Vector2(-maxSpeed, rBody.velocity.y);
        }

        if (reachedPlayer)
        {
            rBody.velocity = Vector2.zero;
        }

        renderer.material.color = originalColor;
    }

    int getDirection()
    {
        //Debug.Log(transform.position.x - player.transform.position.x);
        if (transform.position.x - player.transform.position.x > 0)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            renderer.material.color= Color.red;
            minusLife();
        }

        if (collision.gameObject.tag == "Campfire")
        {
            renderer.material.color = Color.black;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            renderer.material.color = originalColor;
        }
    }

    void minusLife()
    {
        life -= 1;
        if (life < 1)
        {
            this.gameObject.SetActive(false);
        }
    }
}
