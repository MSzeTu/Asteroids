using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Vector3 position;
    public Vector3 velocity;
    Vector3 direction;
    public SpriteRenderer asteroidRenderer;
    float velX;
    float velY;
    SpriteInfo info;
    bool isFrag = false;

    //Accessors for varibles
    public Vector3 Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!isFrag) //Only initializes the position if the asteroid is not a fragment
        {
            asteroidRenderer = this.GetComponent<SpriteRenderer>();
            velX = Random.Range(-0.01f, 0.02f);
            velY = Random.Range(-0.01f, 0.02f);
            velocity = new Vector3(velX, velY, 0);
            direction = (velocity / velocity.magnitude);
        }
        info = gameObject.GetComponent<SpriteInfo>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //Checks if the asteroid is visible and returns false if it's not and should be deleted.
    public bool LeaveScene()
    {
        if (asteroidRenderer != null)
        {
            bool isVisible = asteroidRenderer.isVisible;
            if (isVisible)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    //Moves the asteroid
    public void Move()
    {
        transform.position += velocity;
    }

    //Allows the setting of the fragment velocities 
    public void SetFragVelocity(Vector3 parentDirection)
    {
        direction = parentDirection;
        float randRotation = Random.Range(-20, 20);
        direction = Quaternion.Euler(0, 0, randRotation)*direction;
        float speedMod = Random.Range(30f, 150f);
        velocity = direction/speedMod;
        isFrag = true;
    }
}
