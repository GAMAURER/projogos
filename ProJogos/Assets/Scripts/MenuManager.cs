using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float inputDelay = 0;
    private const float DELAY = 0.3f;
    private bool active = false;
    public GameObject canvas;
    public GameObject menuPanel;
    public GameObject textPanel;
    public TextMeshProUGUI textMesh;

    public List<string> activeDiag;
    public int diagIndex;
    public bool changeDiag = false;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        menuPanel = canvas.transform.Find("MenuPanel").gameObject;
        textPanel = canvas.transform.Find("TextPanel").gameObject;
        textMesh = textPanel.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        activeDiag = null;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool interact1 = false;

        if (inputDelay > 0)
        {
            inputDelay -= Time.deltaTime;
            return;

        }
        else
        {
            interact1 = Input.GetButton("Fire1");

            if (interact1 )
            {
                inputDelay = DELAY;
            }
        }

        if (active)
        {
            Debug.Log("Dialogue Activated");
            CutsceneInteraction(horizontal, vertical, interact1);
        }

    }

    public bool Activate()
    {
        Debug.Log("Activating");
        active = true;
        textPanel.SetActive(true);
        return true;

    }
    public bool Deactivate()
    {
        Debug.Log("Deactivating");
        active = false;
        textPanel.SetActive(false);
        return true;

    }

    public void loadDiag(List<string> newDiag)
    {
        
        activeDiag = newDiag;
        diagIndex = 0;
        textMesh.text = activeDiag[diagIndex];
        changeDiag = false;
        Activate();
    }

    void CutsceneInteraction(float hor, float vert, bool inter)
    {
        if(changeDiag)
        {
            textMesh.text = activeDiag[diagIndex];
            changeDiag = false;
        }

        if(inter)
        {
            diagIndex++;
            if(diagIndex > activeDiag.Count-1)
            {
                Deactivate();
                GameObject.Find("Player").GetComponent<PlayerController>().Activate();
                
            }
            else
            {
                changeDiag = true;
            }

            
        }


        
    }
 


}
