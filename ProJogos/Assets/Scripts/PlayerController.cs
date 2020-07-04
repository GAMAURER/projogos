using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool useForce;
    public bool respawn = false;
    private string nextSpawn;

    private string currScene;

    public float speed;
    private BoxCollider2D boxcol;
    private Rigidbody2D rb;
    public GameObject dialogue;
    public int state = 0;
    HashSet<string> itemsobtained;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Additive);
        currScene = "Level1";

        boxcol = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        itemsobtained = new HashSet<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (respawn)
        {
            transform.position = GameObject.Find(nextSpawn).transform.position;
            respawn = false;
        }
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool interact = Input.GetButton("Fire1");
        Vector2 movement = new Vector2(horizontal, vertical)*speed;
        tryMove(movement);
        
        if(interact)
        {
            /*
            var canvas = GameObject.Find("Canvas");
            //Instantiate(dialogue,canvas.transform);
            var box = canvas.transform.GetChild(0).GetChild(0);
            box.GetComponent<TextMesh>();
            */
            var hit = Physics2D.OverlapCircle(transform.position, 1.5f,LayerMask.GetMask("Items"));
            if (hit != null)
            {
                var a = hit.gameObject;
                itemsobtained.Add(a.name);
                Debug.Log("Got " + a.name);
                a.SetActive(false);
            }
            hit = null;
            hit = Physics2D.OverlapCircle(transform.position, 1.5f, LayerMask.GetMask("Doors"));
            if (hit != null)
            {
                var door = hit.gameObject.GetComponent<Door>();
                SceneManager.UnloadSceneAsync(currScene);
                var newscene = door.levelname;
                nextSpawn = door.spawnname;
                respawn = true;
                SceneManager.LoadScene(newscene, LoadSceneMode.Additive);
                currScene = newscene;

                
               
                Debug.Log("loadedscene " + newscene);
            }

        }

    }
    bool tryMove(Vector2 movement)
    {
        rb.velocity = movement;
        return true;

    }
}
