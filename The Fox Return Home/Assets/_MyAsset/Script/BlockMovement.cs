using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{

    //[SerializeField] Transform[] blockPoints;
    //public GameObject block;
    float speed = 3f;
    //int index = 0;
    public Transform pos1, pos2;
    public Vector3 startPos;
    Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        //index = Random.Range(0, blockPoints.Length);
        //transform.position = blockPoints[index].position;
        startPos = transform.position;
        nextPos = pos1.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Vector3.Distance(transform.position, blockPoints[index].position) < 0)
        //{
        //    index = (index + 1) % blockPoints.Length;
        //}

        //transform.position = Vector3.MoveTowards(transform.position, blockPoints[index].position, speed * Time.deltaTime);

        if(transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        if(transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
