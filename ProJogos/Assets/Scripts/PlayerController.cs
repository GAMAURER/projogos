﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class PlayerController : MonoBehaviour
{
    public bool useForce;
    public bool respawn = false;
    public string nextSpawn="PlayerSpawn";

    private string currScene;
    private MenuManager menuMan;
    private float inputDelay = 0;
    public const float DELAY = 0.3f;

    public float speed;
    private BoxCollider2D boxcol;
    private Rigidbody2D rb;
    public GameObject dialogue;
    public bool active = true;

    public HashSet<string> itemsobtained;
    public Dictionary<string,Sprite> itemsImgMap;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        SceneManager.LoadScene("Level1", LoadSceneMode.Additive);
        currScene = "Level1";

        boxcol = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        itemsobtained = new HashSet<string>();
        itemsImgMap = new Dictionary<string, Sprite>();

        menuMan = GameObject.Find("MenuManager").GetComponent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {

        

        if (respawn&&active)
        {
            transform.position = GameObject.Find(nextSpawn).transform.position;
            respawn = false;
        }
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        bool interact1 = false;
        bool interact2 = false;
        if (inputDelay > 0)
        {
            inputDelay -= Time.deltaTime;
            return;

        }
        else
        {
            interact1 = Input.GetButton("Fire1");
            interact2 = Input.GetButton("Fire2");

            if(interact1 || interact2)
            {
                inputDelay = DELAY;
            }
        }

        Vector2 movement = new Vector2(horizontal, vertical)*speed;



        if (!active)
        {

            //Interaction(interact1, interact2);
            //active = menuMan.CutsceneInteraction(horizontal, vertical,interact1, interact2);
            return;
        }

        Interaction(interact1, interact2);
        TryMove(movement);


        Animate();
        
    }
    bool TryMove(Vector2 movement)
    {
        rb.velocity = movement;
        return true;

    }


    void Interaction(bool interact1, bool interact2)
    {
        

        if (interact1)
        {

            var hit = Physics2D.OverlapCircle(transform.position, 1.5f, LayerMask.GetMask("Items"));
            if (hit != null)
            {
                var a = hit.gameObject;
                itemsobtained.Add(a.name);
                itemsImgMap.Add(a.name, a.GetComponent<SpriteRenderer>().sprite);
                Debug.Log("Got " + a.name);
                Debug.Log("Item Count = " + itemsobtained.Count);
                a.SetActive(false);
                return;
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
                return;
            }

            hit = Physics2D.OverlapCircle(transform.position, 1.5f, LayerMask.GetMask("Characters"));

            if (hit != null)
            {

                Debug.Log("Hit Character");
                var diagEvent = hit.gameObject.GetComponent<DialogueEvent>();
                if(diagEvent != null)
                {
                    
                    menuMan.loadDiag(diagEvent);
                    active = false;
                    return;
                }
            }

        }

        if (interact2)
        {

            menuMan.loadMenu();
            active = false;
        }


    }

    public bool Activate()
    {
        active = true;
        inputDelay = DELAY;
        return true;

    }

    private void Animate()
    {
        anim.SetFloat("hspeed", rb.velocity.x);
        anim.SetFloat("vspeed", rb.velocity.y);
    }

}
