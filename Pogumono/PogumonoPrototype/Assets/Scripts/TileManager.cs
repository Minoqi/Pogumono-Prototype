using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    //Variables
    public bool taken;
    public GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
        taken = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(!taken && manager.GetComponent<TrainingManager>().eggSelected != null)
        {
            if(manager.GetComponent<TrainingManager>().eggSelected.GetComponent<EggManager>().set == false)
            {
                taken = true;
                manager.GetComponent<TrainingManager>().eggSelected.transform.position = this.transform.position;
                manager.GetComponent<TrainingManager>().eggSelected.GetComponent<EggManager>().set = true;
                manager.GetComponent<TrainingManager>().eggSelected.GetComponent<EggManager>().SpawnPog();
            }
        }
    }
}
