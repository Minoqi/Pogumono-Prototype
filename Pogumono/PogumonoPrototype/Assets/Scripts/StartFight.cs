using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFight : MonoBehaviour
{
    //Variables

    [Header("Pogs")]
    public bool startFight;
    public GameObject[] playerPogs;
    public GameObject[] enemyPogs;
    public GameObject playerMarker, enemyMarker;

    [Header("Scene")]
    public GameObject tilesPlayer;
    public GameObject tilesEnemy;
    public GameObject shop;

    public int leftEnemyPogs, leftPlayerPogs;
    public GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
        leftEnemyPogs = enemyPogs.Length;
        leftPlayerPogs = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Start Fight
    private void OnMouseDown()
    {
        //Get all Player Pogs
        playerPogs = GameObject.FindGameObjectsWithTag("Pog");
        leftPlayerPogs = playerPogs.Length;
        manager.GetComponent<TrainingManager>().leftEnemyPogs = leftEnemyPogs;
        manager.GetComponent<TrainingManager>().leftPlayerPogs = leftPlayerPogs;

        //Move pogs to starting location
        for(int i = 0; i < playerPogs.Length; i++)
        {
            float tempX = Random.Range((float)-3.0, (float)3.0);
            playerPogs[i].transform.position = new Vector2(playerMarker.transform.position.x + tempX, playerMarker.transform.position.y);
            playerPogs[i].GetComponent<PogumonoScript>().startFight = true;
        }

        for(int j = 0; j < enemyPogs.Length; j++)
        {
            float tempX2 = Random.Range((float)-3.0, (float)3.0);
            enemyPogs[j].transform.position = new Vector2(enemyMarker.transform.position.x + tempX2, enemyMarker.transform.position.y);
            enemyPogs[j].GetComponent<PogumonoScript>().startFight = true;
        }

        //Reset Scene
        tilesEnemy.SetActive(false);
        tilesPlayer.SetActive(false);
        shop.SetActive(false);
        //gameObject.SetActive(false);

        startFight = true;
    }
}
