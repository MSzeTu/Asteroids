using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 velocity;
    Vector3 direction;
    SpriteRenderer bulletRenderer;
    SpriteInfo info;

    //Accesors for Variables
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
        bulletRenderer = gameObject.GetComponent<SpriteRenderer>();
        info = gameObject.GetComponent<SpriteInfo>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Moves the projectile across the screen
    public void Move()
    {
        gameObject.transform.position += velocity;
    }

    //Sets velocity using Direction
    public void SetVelocity()
    {
        velocity = direction * 0.3f;
    }

    //Checks if the bullet is visible and returns false if it's not and should be deleted.
    public bool LeaveScene()
    {
        if (bulletRenderer != null)
        {
            bool isVisible = bulletRenderer.isVisible;
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

    
}
