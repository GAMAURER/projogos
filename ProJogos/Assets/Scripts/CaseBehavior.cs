using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaseBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetAssignedItem()
    {
        var menuMan = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        var player = GameObject.Find("Player").GetComponent<PlayerController>();
        if (menuMan.selectedItem!=null)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            var image = gameObject.transform.GetChild(0).GetComponent<Image>();
            image.sprite = player.itemsImgMap[menuMan.selectedItem];
            image.name = menuMan.selectedItem;

            menuMan.selectedItem = null;
            menuMan.holdtime = 0;

        }
    }
}
