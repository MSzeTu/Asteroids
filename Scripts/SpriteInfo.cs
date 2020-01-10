using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : MonoBehaviour
{
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public Vector3 size;
    public SpriteRenderer sprite;
    public float radius;
    public Vector3 center;
    // Start is called before the first frame update
    void Start()
    {
        //Gathers all information from attached sprites spriterenderer
        sprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        minX = sprite.bounds.min.x;
        maxX = sprite.bounds.max.x;
        minY = sprite.bounds.min.y;
        maxY = sprite.bounds.max.y;
        size = sprite.bounds.size;
        center = sprite.bounds.center;
        radius = Mathf.Sqrt(Mathf.Pow(maxX-center.x,2) + Mathf.Pow((0),2));  
    }
}
