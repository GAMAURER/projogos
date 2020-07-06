using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [Range(0f, 10f)]
    public float speed;
    //public Vector3 from;
    private Vector3 target;
    //public Vector3 to;
    public GameObject[] positions;
    public int currentpos;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        currentpos = 0;
        target = positions[currentpos].transform.position;

    }


    void Update()
    {
        if (!player.GetComponent<PlayerController>().active)
        {
            return;
        }
        if (transform.position == target)
        {
            if(currentpos==positions.Length-1){
                currentpos = 0;
                target = positions[currentpos].transform.position;
            }
            else
            {
                currentpos += 1;
                target = positions[currentpos].transform.position;
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
