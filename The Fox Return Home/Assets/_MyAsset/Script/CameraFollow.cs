using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    public float speed;
    public float cameraDepth = -10f;
    public float minX, minY, maxX, maxY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var newPosition = Vector2.Lerp(transform.position, player.position, Time.deltaTime * speed);
        var camPosition = new Vector3(newPosition.x, newPosition.y, cameraDepth);
        var v3 = camPosition;
        var newX = Mathf.Clamp(v3.x, minX, maxX);
        var newY = Mathf.Clamp(v3.y, minY, maxY);
        transform.position = new Vector3(newX, newY, cameraDepth);
    }
}
