using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //Variables
    public bool selected;
    public bool effectStatus; //false = negative, true = positive
    public int happinessBoost;
    public GameObject manager;
    public GameObject[] allPogs;

    // Start is called before the first frame update
    void Start()
    {
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        //Find all pogs
        allPogs = GameObject.FindGameObjectsWithTag("Pog");

        for(int i = 0; i < allPogs.Length; i++)
        {
            if(allPogs[i].GetComponent<PogumonoScript>().selected == true)
            {
                if(effectStatus)
                {
                    allPogs[i].GetComponent<PogumonoScript>().happiness += happinessBoost;
                    allPogs[i].GetComponent<PogumonoScript>().selected = false;
                }              
                else
                {
                    allPogs[i].GetComponent<PogumonoScript>().happiness -= happinessBoost;
                    allPogs[i].GetComponent<PogumonoScript>().selected = false;
                }
            }
        }
    }
}
