using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical);
        tryMove(movement);

    }
    bool tryMove(Vector2 movement)
    {
        Vector2 step = movement * Time.deltaTime * speed;
        int blocklayer=1 << 8;
        Collider2D hit = Physics2D.OverlapCircle(step,1f,blocklayer);
        if (hit == null)
        {
            transform.Translate(step);
            

            return true;
        }
        Debug.Log("hit!=null");
        return false;
    }
}
