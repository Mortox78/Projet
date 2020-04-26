using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowAttack : MonoBehaviour
{
    [SerializeField] private GameObject arrow = null;
    [SerializeField] private float bowPower = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Camera cam = FindObjectOfType<Camera>();
            Vector2 playerScreenPos = cam.WorldToScreenPoint(this.transform.position);
            Vector2 mousePos = Input.mousePosition;

            Vector2 playerToMouse = mousePos - playerScreenPos;

            GameObject arrowShot = Instantiate(arrow);
            Arrow arrowShotScript = GetComponent<Arrow>();
            arrowShot.transform.position = this.transform.position;
            arrowShot.GetComponent<Rigidbody2D>().velocity = playerToMouse.normalized * bowPower;
        }
    }
}
