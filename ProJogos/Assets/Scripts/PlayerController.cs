using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public float halfsize =0.5f;
    private BoxCollider2D boxcol;
    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        boxcol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical);
        tryMove(movement);

    }
    bool tryMove(Vector2 movement)
    {
        Vector2 step = movement * Time.deltaTime * speed;
        int blocklayer= 1 << 8;
        
        Collider2D hit = Physics2D.OverlapBox((Vector2)transform.position + step, boxcol.size,0,blocklayer);
        //RaycastHit2D hitR = Physics2D.Raycast((Vector2)transform.position, movement,step.magnitude, blocklayer);
        if (hit== null)
        {
            transform.Translate(step);
            

            return true;
        }
        Debug.Log("hit!=null");
        return false;
    }
}
