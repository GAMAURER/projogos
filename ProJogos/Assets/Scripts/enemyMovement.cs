using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [Range(0.1f, 10f)]
    public float speed;
    public float xmax;
    private Vector3 target;
    public float xmin;

    // Start is called before the first frame update
    void Start()
    {
        target = new Vector3(xmax, transform.position.y, transform.position.z);

    }


    void Update()
    {
        if (transform.position.x == xmax)
        {
            target = new Vector3(xmin, transform.position.y, transform.position.z);
        }
        if (transform.position.x == xmin)
        {
            target = new Vector3(xmax, transform.position.y, transform.position.z);
        }

        float step = speed * Time.deltaTime;
        transform.right = target - transform.position;
        // Move objeto na direção do target
        transform.position = Vector3.MoveTowards(transform.position,
            target, step);
    }
    // Update is called once per frame

}
