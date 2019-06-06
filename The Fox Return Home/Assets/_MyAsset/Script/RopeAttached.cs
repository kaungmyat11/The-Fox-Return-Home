using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeAttached : MonoBehaviour
{

    public GameObject hook;
    public GameObject curHook;

    public bool ropeActive = false;

    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(!ropeActive)
        {
            Vector2 destiny = transform.position;
            curHook = (GameObject)Instantiate(hook, transform.position, Quaternion.identity);
            curHook.GetComponent<RopeScript>().destiny = destiny;
            ropeActive = true;
        }
        else
        {
            Destroy(curHook);
            ropeActive = false;
        }
    }
}
