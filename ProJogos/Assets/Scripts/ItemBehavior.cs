using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAsSelectedItem()
    {
        var name = gameObject.transform.GetChild(0).name;
        var menuMan = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        menuMan.selectedItem = name;
        menuMan.holdtime = 2;
    }

}
