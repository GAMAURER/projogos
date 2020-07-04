using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [Range(0.1f, 10f)]
    public float speed;
    public Vector3 from;
    private Vector3 target;
    public Vector3 to;
    public Vector3[] positions;
    public int currentpos;

    // Start is called before the first frame update
    void Start()
    {
        currentpos = 0;
        target = positions[currentpos];

    }


    void Update()
    {
        if (transform.position == target)
        {
            if(currentpos==positions.Length-1){
                currentpos = 0;
                target = positions[currentpos];
            }
            else
            {
                currentpos += 1;
                target = positions[currentpos];
            }
        }
        //if (transform.position == to)
        //{
        //    target = from;
        //}

        float step = speed * Time.deltaTime;
        transform.right = target - transform.position;
        // Move objeto na direção do target
        transform.position = Vector3.MoveTowards(transform.position,
            target, step);
    }
    // Update is called once per frame

}
