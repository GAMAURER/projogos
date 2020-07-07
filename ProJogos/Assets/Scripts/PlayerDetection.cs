using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDetection : MonoBehaviour
{

    public float sightdistance = 5;
    public float visionangle = 40;
    public Vector2 facing;
    public bool isfacingforward = false;
    private GameObject player;
    public bool reqitem;
    public string item;
    private MenuManager menuMan;
    public bool pacify = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        facing = new Vector2(-1, 0);
        menuMan = GameObject.Find("MenuManager").GetComponent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Transform player = GameObject.Find("Player").transform;

        if(GameObject.Find("Player").GetComponent<PlayerController>().active == false)
        {
            return;
        }

        float dist = Vector2.Distance(transform.position, player.position);
        if (isfacingforward)
        {
            facing = transform.right;
            //Debug.Log("Facing:  " + facing);
        }
        if (dist < sightdistance)
        {

            if (Vector2.Angle(facing, player.position - transform.position) < visionangle)
            {

                GetComponent<BoxCollider2D>().enabled = false;
                RaycastHit2D hitR = Physics2D.Raycast(transform.position, player.position - transform.position);
                GetComponent<BoxCollider2D>().enabled = true;

                if (hitR.collider != null && hitR.collider.name == "Player")
                {
                    Debug.Log("You were seen");
                    if (pacify)
                    {
                         return;
                    }
                    else
                    {
                        PlayerController pc = player.GetComponent<PlayerController>();
                        pc.active = false;
                        var diagEvent = GetComponent<DialogueEvent>(); 
                        menuMan.loadDiag(diagEvent);
                    }
                }
            }
        }

    }
}