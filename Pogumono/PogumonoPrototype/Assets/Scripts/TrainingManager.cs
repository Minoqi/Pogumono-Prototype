using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TrainingManager : MonoBehaviour
{
    //Variables
    public GameObject eggSelected;
    public int maxLevelPogs; //Num of pogs you can use
    public GameObject[] leftOverEggs;
    public GameObject[] allPogs;
    public GameObject[] allEnemyPogs;
    public bool pogsSet;
    public GameObject fightButton;
    public int leftEnemyPogs, leftPlayerPogs;

    // Start is called before the first frame update
    void Start()
    {
        pogsSet = false;
        leftEnemyPogs = 0;
        leftPlayerPogs = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!pogsSet)
        {
            CheckNumPogs();
        }

        if(fightButton.GetComponent<StartFight>().startFight)
        {
            CheckWinner();
        }
    }

    //Check for max Pogs
    void CheckNumPogs()
    {
        //Find all pogs
        allPogs = GameObject.FindGameObjectsWithTag("Pog");

        //Delete Eggs & Stop Looking for Pogs if allPogs = maxLevelPogs
        if(allPogs.Length >= maxLevelPogs)
        {
            //Delete Leftover Eggs
            leftOverEggs = GameObject.FindGameObjectsWithTag("Egg");
            for(int i = 0; i < leftOverEggs.Length; i++)
            {
                Destroy(leftOverEggs[i].gameObject);
            }

            //Stop Check
            pogsSet = true;
        }
    }

    void CheckWinner()
    {
        allPogs = GameObject.FindGameObjectsWithTag("Pog");
        allEnemyPogs = GameObject.FindGameObjectsWithTag("EnemyPog");
        if (allEnemyPogs.Length <= 0)
        {
            Debug.Log("Player Won");
            SceneManager.LoadScene("Win");
        }
        
        if(allPogs.Length <= 0)
        {
            Debug.Log("Enemy Won");
            SceneManager.LoadScene("Lose");
        }
    }
}
