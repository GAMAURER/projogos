using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public float sightdistance = 5;
    public Vector2 facing;
    // Start is called before the first frame update
    void Start()
    {
        facing = new Vector2(-1, 0);
    }

    // Update is called once per frame
    void Update()
    {

        Transform player = GameObject.Find("Player").transform;
        float dist = Vector2.Distance(transform.position, player.position);
        if (dist < sightdistance)
        {
            if (Vector2.Angle(facing, player.position - transform.position) < 60)
            {

                GetComponent<BoxCollider2D>().enabled = false;
                RaycastHit2D hitR = Physics2D.Raycast(transform.position, player.position - transform.position);
                GetComponent<BoxCollider2D>().enabled = true;

                if (hitR.collider.name == "Player")
                {
                    Debug.Log("You were seen");
                }
            }
        }

    }
}