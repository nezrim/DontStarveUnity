using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour
{
    public GameControl gameControl;

    private const int LEFT = -1;
    private const int RIGHT = 1;

    public int offsetX = 2;         // the offset so that we don't get any weird errors

    // these are used for checking if we need to instantiate stuff
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;

    public bool reverseScale = true;   // used if the object is not tilable

    private float spriteWidth = 0f;     // the width of our element
    private Camera cam;
    private Transform myTransform;

    void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    // Use this for initialization
    void Start()
    {
        gameControl = GameObject.FindGameObjectWithTag("GameControl").GetComponent<GameControl>();
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x* transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        // does it still need buddies? If not do nothing
        if (!hasALeftBuddy || !hasARightBuddy)
        {
            // calculate the cameras extend (half the width) of what the camera can see in world coordinates
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

            // calculate the x position where the camera can see the edge of the sprite (element)
            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;

            // checking if we can see the edge of the element and then calling MakeNewBuddy if we can
            if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && !hasARightBuddy)
            {
                makeNewBuddy(RIGHT);
                Debug.Log("Making new Copy of: " + this.gameObject.tag + " to RIGHT Side");
                hasARightBuddy = true;
            }
            else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && !hasALeftBuddy)
            {
                makeNewBuddy(LEFT);
                Debug.Log("Making new Copy of: " + this.gameObject.tag + " to LEFT Side");
                hasALeftBuddy = true;
            }
        }
    }

    // a function that creates a buddy on the side required
    void makeNewBuddy(int rightOrLeft)
    {
        // calculating the new position for our new buddy
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        // instantating our new body and storing him in a variable
        Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        // if not tilable let's reverse the x size og our object to get rid of ugly seams
        if (reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;
        if (rightOrLeft > 0)
        {
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        }
        else
        {
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
        }

        if (this.gameObject.CompareTag("BGDay"))
        {
            gameControl.dayBGs.Add(newBuddy.gameObject);
        }
        else if (this.gameObject.CompareTag("BGNight"))
        {
            gameControl.nightBGs.Add(newBuddy.gameObject);
        }

        float min = newPosition.x - spriteWidth / 2;
        float max = newPosition.x + spriteWidth / 2;

        //50% thief spawn
        int random = Random.Range(0, 10);
        if (random>=0 && random <= 5)
        {
            gameControl.SpawnThief(1, min, max);
        }

        //50% enemy spawn
        random = Random.Range(0, 10);
        if (random >= 0 && random <= 5)
        {
            gameControl.SpawnEnemy(1, min, max);
        }

        //25% weed spawn
        random = Random.Range(0, 10);
        if (random >= 0 && random <= 2.5f)
        {
            gameControl.SpawnWeed(1, min, max);
        }

        //25% stone spawn
        random = Random.Range(0, 10);
        if (random >= 0 && random <= 2.5f)
        {
            gameControl.SpawnStone(1, min, max);
        }

        //25% wood spawn
        random = Random.Range(0, 10);
        if (random >= 0 && random <= 2.5f)
        {
            gameControl.SpawnWood(1, min, max);
        }

        //30% mushroom spawn
        random = Random.Range(0, 10);
        if (random >= 0 && random <= 3)
        {
            gameControl.SpawnMushroom(1, min, max);
        }
    }
}
