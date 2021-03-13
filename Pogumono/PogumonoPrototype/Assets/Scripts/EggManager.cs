using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggManager : MonoBehaviour
{
    //Variables
    public bool selected, set;
    public GameObject manager;
    public GameObject pogType;

    // Start is called before the first frame update
    void Start()
    {
        selected = false;
        set = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Move Egg
    private void OnMouseDown()
    {
        //Check If Egg Has Already Been Placed
        if (!set)
        {
            //Update Selection Status
            if (selected)
            {
                selected = false;
                manager.GetComponent<TrainingManager>().eggSelected = null;

            }
            else
            {
                //Unselect Previous Egg
                if (manager.GetComponent<TrainingManager>().eggSelected != null)
                {
                    manager.GetComponent<TrainingManager>().eggSelected = null;
                }

                //Get New Selected Egg
                selected = true;
                manager.GetComponent<TrainingManager>().eggSelected = this.gameObject;
            }
        }
    }

    //Replace Egg With Pog
    public void SpawnPog()
    {
        Instantiate(pogType, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
