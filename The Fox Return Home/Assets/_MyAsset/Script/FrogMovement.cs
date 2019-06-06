using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{

    float speed = 2.5f;
    public Transform pos1, pos2;
    public Vector3 startPos;
    Vector3 nextPos;

    void Start()
    {
        startPos = transform.position;
        nextPos = pos1.position;
    }

    // Update is called once per frame
    void Update()
    { 
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
            Flip();
        }
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
            Flip();
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void Flip()
    {
        //facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
