using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemDetection : MonoBehaviour
{
    // Start is called before the first frame update
    HashSet<string> itemsobtained;
    public GameObject[] items;
    public GameObject[] doors;
    public float detectionrange;
    void Start()
    {
        //items = new GameObject[10];
        itemsobtained = new HashSet<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            foreach (GameObject a in items)
            {

                if (a.activeSelf && Vector3.Distance(a.transform.position, transform.position) < detectionrange)
                {
                    itemsobtained.Add(a.name);
                    Debug.Log("Got " + a.name);
                    a.SetActive(false);
                }

            }


            foreach (GameObject a in doors)
            {

                if (a.activeSelf && Vector3.Distance(a.transform.position, transform.position) < detectionrange)
                {
                    SceneManager.LoadScene("Level2", LoadSceneMode.Additive);
                    Debug.Log("loadedscene");
                }

            }
        }
    }
}
