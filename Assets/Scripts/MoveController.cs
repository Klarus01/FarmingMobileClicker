using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 1f;


    private bool isFacingRight = true;
    private float stopTime;
    private float reachedPointTime;


    private Vector2 parentCenter;
    private bool IsStopped() => reachedPointTime + stopTime < Time.time;
    
    private void Update()
    {
        parentCenter = transform.parent.position;
        if (Vector2.Distance(target.position, transform.position) < .01f)
        {
            stopTime = Random.Range(1f, 10f);
            reachedPointTime = Time.time;
            float xPosition = Random.Range(parentCenter.x - .5f, parentCenter.x + .5f);
            float yPosition = Random.Range(parentCenter.y - .5f, parentCenter.y + .5f);
            target.position = new Vector2(xPosition, yPosition);
            
            

            if (xPosition < transform.position.x && isFacingRight)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            if (xPosition > transform.position.x && !isFacingRight)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }

        if (IsStopped())
            transform.position = Vector2.MoveTowards(transform.position,
                target.position, Time.deltaTime * speed);
    }
}