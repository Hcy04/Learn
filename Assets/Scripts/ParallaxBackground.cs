using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private float xPosition;
    private float yPosition;
    private float length;

    [SerializeField] private float distanceMoved;
    [SerializeField] private float xDiatanceToMove;
    [SerializeField] private float yDiatanceToMove;

    void Start()
    {
        cam = GameObject.Find("Main Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;

        xPosition = transform.position.x;
        yPosition = transform.position.y; 
    }
    
    void Update()
    {
        distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        
        xDiatanceToMove = cam.transform.position.x * parallaxEffect;
        yDiatanceToMove = cam.transform.position.y * parallaxEffect;

        transform.position = new Vector3(xPosition + xDiatanceToMove, yPosition + yDiatanceToMove);

        if (distanceMoved > xPosition + length) xPosition += length;
        else if (distanceMoved < xPosition - length) xPosition -= length;
    }
}
