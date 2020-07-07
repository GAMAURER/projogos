using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> diags;
    public List<int> portraits;
    public int type = 0;// 0 = regular, 1 = itemTest
    public string item;
    public int itemTestDiag;
    public List<string> faildiags;
    public List<int> failportraits;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
