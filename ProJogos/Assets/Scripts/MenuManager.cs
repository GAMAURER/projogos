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

    public List<string> activeDiag;
    public List<int> portraitIndex;
    public List<GameObject> portraits; 
    public int diagIndex;
    public bool changeDiag = false;
    PlayerController plyr;


    
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        menuPanel = canvas.transform.Find("MenuPanel").gameObject;
        textPanel = canvas.transform.Find("TextPanel").gameObject;
        textMesh = textPanel.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        activeDiag = null;
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
                CutsceneInteraction(horizontal, vertical, interact1);
            }
            else if (mode == 2)
            {
                Debug.Log("Menu Open");
                MenuInteraction(horizontal, vertical, interact1,interact2);
            }
            
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

    public void loadDiag(List<string> newDiag, List<int> portIndex)
    {
        
        activeDiag = newDiag;
        portraitIndex = portIndex;
        diagIndex = 0;
        textMesh.text = activeDiag[diagIndex];
        portraits[portraitIndex[diagIndex]].SetActive(true);
        changeDiag = false;
        mode = 1;


        Debug.Log("Activating");
        active = true;
        textPanel.SetActive(true);

    }

    public void loadMenu()
    {
        menuPanel.SetActive(true);
        mode = 2;
        active = true;
        var itemset = plyr.itemsobtained;
        int count = menuPanel.transform.childCount;
        int i = 0;
        foreach (string item in itemset)
        {
            if (i > count - 1)
            {
                Debug.Log("Too many items");
                break;
            }
            var container = menuPanel.transform.GetChild(i);

            var image = container.transform.GetChild(0).GetComponent<Image>();
            


            i++;
            
        }


    }


    void CutsceneInteraction(float hor, float vert, bool inter)
    {
        if(changeDiag)
        {
            foreach (GameObject portrait in portraits)
            {
                portrait.SetActive(false);
            }
            portraits[portraitIndex[diagIndex]].SetActive(true);
            textMesh.text = activeDiag[diagIndex];
            changeDiag = false;
        }

        if(inter)
        {
            diagIndex++;
            if(diagIndex > activeDiag.Count-1)
            {
                Deactivate();
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



}
