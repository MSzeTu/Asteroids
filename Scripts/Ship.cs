using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    Vector3 position;
    Vector3 velocity;
    Vector3 direction;
    Vector3 acceleration;
    float decelRate;
    float angleOfRotation;
    float maxSpeed;
    float accelRate;
    Camera cam;
    Vector3 screenPos;
    SpriteRenderer shipRenderer;
    bool wrapX;
    bool wrapY;
    SpriteInfo info;


    //Accessors for variables
    public Vector3 Position
    {
        get
        {
            return position;
        }
    }
    public Vector3 Direction
    {
        get
        {
            return direction;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        decelRate = 0.95f;
        maxSpeed = 0.5f;
        accelRate = 0.001f;
        direction = new Vector3(1, 0, 0);
        velocity = new Vector3(0, 0, 0);
        wrapX = false;
        wrapY = false;
        shipRenderer = gameObject.GetComponent<SpriteRenderer>();
        info = gameObject.GetComponent<SpriteInfo>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Sets proper transformation of vehicle rotation
    public void SetTransform()
    {
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);
        transform.position = position;
    }

    //Accelerates vehicle
    public void Drive()
    {
        acceleration = accelRate * direction;
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        position += velocity;
    }

    //Deccelerates Vehicle
    public void Slow()
    {
        acceleration = decelRate * direction;
        velocity = new Vector3(velocity.x * decelRate, velocity.y * decelRate, velocity.z * decelRate);
        position += velocity;
    }

    //Rotates vehicle to the right
    public void RotateRight()
    {
        angleOfRotation -= 2;
        direction = Quaternion.Euler(0, 0, -2) * direction;
    }

    //Rotates vehicle to the left
    public void RotateLeft()
    {
        angleOfRotation += 2;
        direction = Quaternion.Euler(0, 0, 2) * direction;
    }

    //Wraps vehicle across screen
    public void WrapVehicle()
    {
        bool isVisible = shipRenderer.isVisible;
        if (isVisible)
        {
            wrapX = false;
            wrapY = false;
            return;
        }

        if (wrapX && wrapY)
        {
            return;
        }
        screenPos = cam.WorldToViewportPoint(transform.position); //Converts coordinates to viewport points
        if (!wrapX && (screenPos.x > 1 || screenPos.x < 0)) //Wraps if crossing X
        {
            position.x = -position.x;
            wrapX = true;
        }

        if (!wrapY && (screenPos.y > 1 || screenPos.y < 0)) //Wraps if crossing Y
        {
            position.y = -position.y;
            wrapY = true;
        }
    }

    //Detects collision with asteroids using AABB method
    public bool CollisionDetection(GameObject asteroid, List<GameObject> aList)
    {
        SpriteInfo infoA = asteroid.GetComponent<SpriteInfo>();
        if (infoA.minX != 0 && infoA.maxX != 0 && infoA.minY != 0 && infoA.maxY != 0) //Prevents invalid collisions
        {
            if (info.minX < infoA.maxX && info.minY < infoA.maxY && info.maxY > infoA.minY && info.maxX > infoA.minX)
            {
                aList.Remove(asteroid);
                GameObject.Destroy(asteroid);
                return true;
            }
            else return false;
        }
        else return false;
    }

    //Detects collision with fragments using AABB method
    public bool FragmentCollisionDetection(GameObject fragment, List<GameObject> fList)
    {
        SpriteInfo infoF = fragment.GetComponent<SpriteInfo>();
        if (infoF.minX != 0 && infoF.maxX != 0 && infoF.minY != 0 && infoF.maxY != 0) //Prevents invalid collisions
        {
            if (info.minX < infoF.maxX && info.minY < infoF.maxY && info.maxY > infoF.minY && info.maxX > infoF.minX)
            {
                fList.Remove(fragment);
                GameObject.Destroy(fragment);
                return true;
            }
            else return false;
        }
        else return false;
    }

    //Stops the ship entirely for crashes
    public void HaltShip()
    {
        velocity = new Vector3(0,0,0);
    }


}
