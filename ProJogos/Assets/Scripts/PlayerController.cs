using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool useForce;

    public float speed;
    private BoxCollider2D boxcol;
    private Rigidbody2D rb;
    public GameObject dialogue;
    public int state = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        boxcol = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool interact = Input.GetButton("Fire1");
        Vector2 movement = new Vector2(horizontal, vertical)*speed;
        tryMove(movement);
        
        if(interact)
        {
            var canvas = GameObject.Find("Canvas");
            //Instantiate(dialogue,canvas.transform);
            var box = canvas.transform.GetChild(0).GetChild(0);
            box.GetComponent<TextMesh>();
            
        }

    }
    bool tryMove(Vector2 movement)
    {
        rb.velocity = movement;
        return true;

    }
}
