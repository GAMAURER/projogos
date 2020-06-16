using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetection : MonoBehaviour
{
    // Start is called before the first frame update
    HashSet<string> itemsobtained;
    public GameObject[] items;
    public float detectionrange;
    void Start()
    {
        //items = new GameObject[10];
        itemsobtained = new HashSet<string>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject a in items){
            
            if (a.activeSelf&&Vector3.Distance(a.transform.position, transform.position) <detectionrange)
            {
                itemsobtained.Add(a.name);
                Debug.Log("Got " + a.name);
                a.SetActive(false);
            }

        }
    }
}
