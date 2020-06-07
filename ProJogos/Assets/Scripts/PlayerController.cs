using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool useForce = false;

    public float speed = 2f;
    public float halfsize =0.5f;
    private BoxCollider2D boxcol;
    private Rigidbody2D rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        boxcol = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical)*speed;
        tryMove(movement);
        


    }
    bool tryMove(Vector2 movement)
    {
        if(useForce)
        {
            rigidbody.AddForce(movement);

            return true;


        }
        else
        {

            if (!boxcol.IsTouchingLayers(LayerMask.GetMask("Blocking")))

            {
                rigidbody.velocity = movement;

                return true;
            }
            return false;

        }


        /*
            Vector2 step = movement * Time.deltaTime * speed;
            int blocklayer = 1 << 8;

            Collider2D hit = Physics2D.OverlapBox((Vector2)transform.position + step, boxcol.size, 0, blocklayer);
            //RaycastHit2D hitR = Physics2D.Raycast((Vector2)transform.position, movement,step.magnitude, blocklayer);
            if (hit == null)
            {
                transform.Translate(step);


                return true;
            }
            Debug.Log("hit!=null");
            return false;
        */
    }
}
