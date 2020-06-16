using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [Range(0.1f, 10f)]
    public float speed;
    public Vector3 from;
    private Vector3 target;
    public Vector3 to;

    // Start is called before the first frame update
    void Start()
    {
        target = from;

    }


    void Update()
    {
        if (transform.position == from)
        {
            target = to;
        }
        if (transform.position == to)
        {
            target = from;
        }

        float step = speed * Time.deltaTime;
        transform.right = target - transform.position;
        // Move objeto na direção do target
        transform.position = Vector3.MoveTowards(transform.position,
            target, step);
    }
    // Update is called once per frame

}
