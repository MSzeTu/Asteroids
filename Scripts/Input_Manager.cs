using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Gets input and returns integers to determine what occurs
    public int returnInput()
    {
        if (Input.GetKey(KeyCode.UpArrow)) //Handles driving
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    return 14; //Driving, turning right, and shooting
                }
                return 4; //Driving and turning right
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    return 15; //Driving, turning left, and shooting
                }
                return 5; //Driving and turning left
            }
            if (Input.GetKey(KeyCode.Space))
            {
                return 10;
            }
            return 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                return 12; //turning right and shooting
            }
            return 2; //turning right
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                return 13; //turning left and shooting
            }
            return 3;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            return 0;
        }
        return 99;
    }
}
