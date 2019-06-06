using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    [SerializeField]
    private Vector3 velocity;

    float speed = 2f;
    public Transform pos1, pos2;
    public Vector3 startPos;
    Vector3 nextPos;

    private bool moving;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            moving = true;
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(null);
        moving = false;
    }

    // Start is called before the first frame update
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
        }
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }

    private void FixedUpdate()
    {
        if(moving)
        {
            transform.position += (velocity * Time.deltaTime);
        }
    }
}
