using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Manager : MonoBehaviour
{
    public List<GameObject> asteroids;
    public List<GameObject> fragments;
    float xPos;
    float yPos;
    int randAst;
    // Start is called before the first frame update
    void Start()
    {
        asteroids = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (asteroids.Count < 8)
        {
            randAst = Random.Range(1, 4);
            xPos = Random.Range(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x);
            yPos = Random.Range(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y);
            switch (randAst)
            {
                case 1:
                    asteroids.Add(Instantiate((GameObject)Resources.Load("Prefabs/asteroid1"), new Vector3(xPos, yPos, 0), Quaternion.identity));
                    break;
                case 2:
                    asteroids.Add(Instantiate((GameObject)Resources.Load("Prefabs/asteroid2"), new Vector3(xPos, yPos, 0), Quaternion.identity));
                    break;
                case 3:
                    asteroids.Add(Instantiate((GameObject)Resources.Load("Prefabs/asteroid4"), new Vector3(xPos, yPos, 0), Quaternion.identity));
                    break;
            }
        }
        MoveAsteroids();
        MoveFragments();
        RemoveAsteroids();
        RemoveFragments();
    }

    //Moves each asteroid
    void MoveAsteroids()
    {
        foreach (GameObject g in asteroids)
        {
            Asteroid aScript = g.GetComponent<Asteroid>();
            aScript.Move();
        }
    }

    //Moves each Fragment
    void MoveFragments()
    {
        foreach (GameObject f in fragments)
        {
            Asteroid aScript = f.GetComponent<Asteroid>();
            aScript.Move();
        }
    }

    //Removes asteroids if they leave the screen
    void RemoveAsteroids()
    {
        for (int i = 0; i < asteroids.Count; i++)
        {
            Asteroid aScript = asteroids[i].GetComponent<Asteroid>();
            if (asteroids[i] != null)
            {
                if (!aScript.LeaveScene())
                {
                    GameObject removeThis;
                    removeThis = asteroids[i];
                    asteroids.RemoveAt(i);
                    GameObject.Destroy(removeThis);
                }
            }
        }
    }

    //Removes Fragments if they leave the screen
    void RemoveFragments()
    {
        for (int i = 0; i < fragments.Count; i++)
        {
            Asteroid aScript = fragments[i].GetComponent<Asteroid>();
            if (fragments[i] != null)
            {
                if (!aScript.LeaveScene())
                {
                    GameObject removeThis;
                    removeThis = fragments[i];
                    fragments.RemoveAt(i);
                    GameObject.Destroy(removeThis);
                }
            }
        }
    }

    //Detects collisions with bullets using AABB method
    public int CollisionDetection(GameObject bullet, List<GameObject> bList)
    {
        SpriteInfo infoB = bullet.GetComponent<SpriteInfo>();
        for (int i = 0; i < asteroids.Count; i++)
        {
            SpriteInfo infoA = asteroids[i].GetComponent<SpriteInfo>();
            if (infoB.minX < infoA.maxX && infoB.minY < infoA.maxY && infoB.maxY > infoA.minY && infoB.maxX > infoA.minX)
            {
                GenerateFragments(asteroids[i]);
                return 1;
            }
        }
        for (int i = 0; i < fragments.Count; i++) //Detects fragment collision
        {
            SpriteInfo infoF = fragments[i].GetComponent<SpriteInfo>();
            if (infoB.minX < infoF.maxX && infoB.minY < infoF.maxY && infoB.maxY > infoF.minY && infoB.maxX > infoF.minX)
            {
                GameObject target = fragments[i];
                fragments.RemoveAt(i);
                GameObject.Destroy(target);;
                return 2;
            }
        }
        return 0;
    }

    //Creates two asteroids when one is destroyed, using the parent location, direction and velocity
    void GenerateFragments(GameObject parent)
    {
        Asteroid parentA = parent.GetComponent<Asteroid>();
        GameObject frag1 = (Instantiate((GameObject)Resources.Load("Prefabs/asteroid3"), new Vector3(parent.transform.position.x, parent.transform.position.y, 0), Quaternion.identity));
        GameObject frag2 = (Instantiate((GameObject)Resources.Load("Prefabs/asteroid3"), new Vector3(parent.transform.position.x+0.2f, parent.transform.position.y+0.2f, 0), Quaternion.identity));
        Asteroid frag1A = frag1.GetComponent<Asteroid>();
        Asteroid frag2A = frag2.GetComponent<Asteroid>();
        frag1A.SetFragVelocity(parentA.Direction);
        frag2A.SetFragVelocity(parentA.Direction);
        fragments.Add(frag1);
        fragments.Add(frag2);
        asteroids.Remove(parent);
        GameObject.Destroy(parent);
    }

}
