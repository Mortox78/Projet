using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject cart;
    [SerializeField] GameObject player;
    [SerializeField] float range;
    [SerializeField] float smoothTime;

    //bool followPlayer = false;
    Vector3 velocity;

    private void Awake()
    {
        velocity = Vector3.zero;
    }
    void Update()
    {
        //Centred_Camera_Between_Player_And_Cart();
    }
    private void FixedUpdate()
    {
        Follow_Player();
    }

    void Centred_Camera_Between_Player_And_Cart()
    {
        float centredPosX = (player.transform.position.x + cart.transform.position.x) * 0.5f;
        transform.position = new Vector3(centredPosX, transform.position.y, transform.position.z);
    }

    void Follow_Player()
    {
        float distance = cart.transform.position.x - player.transform.position.x;
        if (Mathf.Abs(distance) > range)
        {
            Vector3 newPos = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
        }
        else
        {
            Vector3 newPos = new Vector3(cart.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
        }
            
    }
}
