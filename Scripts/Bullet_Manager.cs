using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Manager : MonoBehaviour
{
    public List<GameObject> bulletList;
    // Start is called before the first frame update
    void Start()
    {
        bulletList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullets();
        RemoveBullets();
    }

    //"Fires" a bullet by instantiating one, the sets its direction and velocity realtive to the players. 
    public void Fire(GameObject player)
    {
        Ship playerScript = player.GetComponent<Ship>();
        bulletList.Add(Instantiate((GameObject)Resources.Load("Prefabs/projectile"), new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity));
        Bullet bScript = bulletList[bulletList.Count - 1].GetComponent<Bullet>();        
        bScript.Direction = playerScript.Direction;
        bScript.SetVelocity();
    }

    //Removes asteroids if they leave the screen
    void RemoveBullets()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            Bullet bScript = bulletList[i].GetComponent<Bullet>();
            if (bulletList[i] != null)
            {
                if (!bScript.LeaveScene())
                {
                    GameObject removeThis;
                    removeThis = bulletList[i];
                    bulletList.RemoveAt(i);
                    GameObject.Destroy(removeThis);
                }
            }
        }
    }

    //Moves all bullets in the list
    void MoveBullets()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            Bullet bScript = bulletList[i].GetComponent<Bullet>();
            bScript.Move();
        }
    }

    //Deletes the entered bullet
    public void DeleteBullet(GameObject bullet)
    {
        bulletList.Remove(bullet);
        GameObject.Destroy(bullet);
    }

}
