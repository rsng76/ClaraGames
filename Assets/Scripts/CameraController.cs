using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    public long trakingBuffer;
    private Vector2 offset;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        offset = player.transform.position - transform.position;
        if (offset.x > trakingBuffer)
        {
			transform.position = new Vector3(player.transform.position.x - trakingBuffer, transform.position.y, -10);
        }
        if (offset.x < -trakingBuffer)
        {
			transform.position = new Vector3(player.transform.position.x + trakingBuffer, transform.position.y, -10);
        }
        if (offset.y > trakingBuffer)
        {
			transform.position = new Vector3(transform.position.x, player.transform.position.y - trakingBuffer, -10);
        }
        if (offset.y < -trakingBuffer)
        {
			transform.position = new Vector3(transform.position.x, player.transform.position.y + trakingBuffer, -10);
        }

    }
}
