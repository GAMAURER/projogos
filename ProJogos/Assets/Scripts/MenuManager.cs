using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float inputDelay = 0;
    private const float DELAY = 0.3f;
    private bool active = false;
    private int mode = 1;
    public GameObject canvas;
    public GameObject menuPanel;
    public GameObject textPanel;
    public TextMeshProUGUI textMesh;

    public DialogueEvent currDiag;
    public List<GameObject> portraits; 
    public int diagIndex;
    public bool checkItem = false;
    public bool changeDiag = false;
    public bool failDiag = false;
    public string selectedItem = null;
    public float holdtime = 0;
    public GameObject itemHold;
    PlayerController plyr;


    
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        menuPanel = canvas.transform.Find("MenuPanel").gameObject;
        textPanel = canvas.transform.Find("TextPanel").gameObject;
        textMesh = textPanel.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        plyr = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
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

            if (interact1 || interact2 )
            {
                inputDelay = DELAY;
            }
        }

        if (active)
        {
            if(mode ==1)
            {
                Debug.Log("Dialogue Activated");
                CutsceneInteraction(horizontal, vertical, interact1,interact2);
            }
            else if (mode == 2)
            {
                Debug.Log("Menu Open");
                MenuInteraction(horizontal, vertical, interact1,interact2);
            }
            
        }

        if(selectedItem != null && plyr.itemsobtained.Contains(selectedItem))
        {
            if (holdtime <= 0)
            {
                selectedItem = null;
                itemHold.SetActive(false);
            }
            else
            {
                itemHold.SetActive(true);
                itemHold.transform.position = Input.mousePosition;
                itemHold.GetComponent<Image>().sprite = plyr.itemsImgMap[selectedItem];
                holdtime -= Time.deltaTime;
            }
        }
        else
        {
            itemHold.SetActive(false);
        }

        
        

    }

   
    public bool Deactivate()
    {
        Debug.Log("Deactivating");
        active = false;
        textPanel.SetActive(false);
        menuPanel.SetActive(false);
        foreach (GameObject portrait in portraits)
        {
            portrait.SetActive(false);
        }
        return true;

    }

    public void loadDiag(DialogueEvent diagEvent)
    {
        
        textMesh.text = diagEvent.diags[diagIndex];
        portraits[diagEvent.portraits[diagIndex]].SetActive(true);
        changeDiag = false;
        mode = 1;
        currDiag = diagEvent;
        diagIndex = 0;


        Debug.Log("Activating");
        active = true;
        textPanel.SetActive(true);

    }

    public void loadMenu()
    {
        menuPanel.SetActive(true);
        mode = 2;
        active = true;
        failDiag = false;
        var itemset = plyr.itemsobtained;
        int count = menuPanel.transform.GetChild(0).childCount;
        int i = 0;
        foreach (string item in itemset)
        {
            if (i > count - 1)
            {
                Debug.Log("Too many items");
                break;
            }
            var imgObj = menuPanel.transform.GetChild(0).GetChild(i).GetChild(0);

            Debug.Log("Setting container" + i + "as" + item);

            var image = imgObj.GetComponent<Image>();
            image.sprite = plyr.itemsImgMap[item];
            
            image.name = item;

            imgObj.gameObject.SetActive(true);

            
            i++;
            
        }


    }


    void CutsceneInteraction(float hor, float vert, bool inter1,bool inter2)
    {
        if(changeDiag)
        {
            foreach (GameObject portrait in portraits)
            {
                portrait.SetActive(false);
            }

            if(failDiag)
            {
                portraits[currDiag.failportraits[diagIndex]].SetActive(true);
                textMesh.text = currDiag.faildiags[diagIndex];
                changeDiag = false;
            }
            else
            {
                portraits[currDiag.portraits[diagIndex]].SetActive(true);
                textMesh.text = currDiag.diags[diagIndex];
                changeDiag = false;

            }
            
        }

        if(checkItem)
        {

            if (selectedItem == currDiag.item)
            {
                Debug.Log("Right Item");
                menuPanel.SetActive(false);
                checkItem = false;
                changeDiag = true;
                diagIndex++;
            }
            else
            {
                if (inter2)
                {
                    Debug.Log("Wrong Item");
                    menuPanel.SetActive(false);
                    failDiag = true;
                    changeDiag = true;
                    diagIndex = 0;
                    checkItem = false;
                }
            }
            return;
        }
        



        if(inter1)
        {
            diagIndex++;

            if (failDiag)
            {
                if (diagIndex > currDiag.faildiags.Count - 1)
                {
                    Debug.Log("FAILED");
                    Deactivate();

                    plyr.Activate();
                    transform.position = GameObject.Find(plyr.nextSpawn).transform.position;
                }
                else
                {
                    changeDiag = true;
                }
                return;
            }


            if (!checkItem && !failDiag && currDiag.type == 1 && diagIndex == currDiag.itemTestDiag)
            {

                checkItem = true;
                loadMenu();
                mode = 1;
            }

            
            if (diagIndex > currDiag.diags.Count-1)
            {


                Deactivate();
                var enemy = currDiag.gameObject.GetComponent<PlayerDetection>();
                if (enemy != null)
                {
                    enemy.pacify = true;
                    plyr.respawn = false;
                }
                plyr.Activate();
                
            }
            else
            {
                
                changeDiag = true;
            }

            
        }

        



    }

    void MenuInteraction(float hor, float vert, bool inter1,bool inter2)
    {
        if(inter2)
        {
            Deactivate();
            plyr.Activate();
        }
    }


    public void SelectItem(string item)
    {
        selectedItem = item;
    }



}
