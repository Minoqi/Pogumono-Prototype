using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PogumonoScript : MonoBehaviour
{
    //Variables

    public GameObject[] allPogs;

    [Header("Stat Variables")]

    //Stat Variables
    public int health, attk, def, spd, happiness, maxStatsLv;
    public int attkBoost, defBoost, spdBoost, bonusBoost, negativeBoost;
    public int attkFocusBonus, defFocusBonus, spdFocusBonus;
    public bool isEnemy; //Set to true if it's an enemy pog
    public int tempSpeed;
    public bool startFight;
    public bool attack;
    public GameObject attackPog;

    public GameObject otherSide;

    public bool selected;
    public GameObject manager;

    [Header("Timers")]

    //Timer Variables
    public float happinessTimer, statTimer, attackTimer;
    public float currentHappinessTimer, currentStatTimer, currentAttackTimer;

    [Header("UI Elements")]
    [Space(10)]

    //UI Variables
    public GameObject textCanvas;
    public Text healthText, attkText, defText, spdText, happinessText;
    public Image faceSymbol;
    public Sprite happyFace, sadFace;

    [Header("Sprites")]
    public Sprite def1;
    public Sprite def2;
    public Sprite def3;

    // Start is called before the first frame update
    void Start()
    {
        //Initial deactivation of Canvas
        textCanvas.SetActive(false);

        currentHappinessTimer = happinessTimer;
        currentAttackTimer = attackTimer;

        selected = false;
        startFight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isEnemy && !startFight)
        {
            UpdateHappiness();
            UpdateUI();
            LevelStats();
            CheckEvolution();
        }

        if(startFight)
        {
            PogAI();
        }

        Death();
    }

    private void CheckEvolution()
    {

        if (happiness >= 100)
        {
            if (gameObject.GetComponent<SpriteRenderer>().sprite == def1)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = def2;
                happiness = 80;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = def3;
                happiness = 80;
            }
        }
    }

    //Update Happiness
    void UpdateHappiness()
    {
        if(currentHappinessTimer > 0)
        {
            currentHappinessTimer -= Time.deltaTime;
        }
        else
        {
            //Stop happiness going into negatives
            if (happiness <= 0)
            {
                happiness = 0;
                currentHappinessTimer = happinessTimer;
            }
            else
            {
                happiness -= 5;
                currentHappinessTimer = happinessTimer;
            }
        }
    }

    //Level Up Skills
    void LevelStats()
    {
        if (currentStatTimer > 0)
        {
            currentStatTimer -= Time.deltaTime;
        }
        else
        {
            if (happiness > 40)
            {
                attk = attkBoost + attkFocusBonus + attk; //Add base boost + pog special boost
                def = defBoost + defFocusBonus + def;
                spd = spdBoost + spdFocusBonus + spd;
            }
            else
            {
                attk -= attkBoost; //Subtract base boost (ignoring pog special boost)
                def -= defBoost;
                spd -= spdBoost;
            }

            currentStatTimer = statTimer;
        }
    }

    //Move AI
    public void PogAI()
    {
        if(attack && attackPog != null) //Attack otherwise keep moving
        {
            if(currentAttackTimer > 0) //Stop from spawm attacking
            {
                currentAttackTimer -= Time.deltaTime;
                this.transform.position = Vector3.MoveTowards(transform.position, attackPog.transform.position, tempSpeed * Time.deltaTime);
            }
            else
            {
                this.transform.position = Vector3.MoveTowards(transform.position, attackPog.transform.position, tempSpeed * Time.deltaTime);
                attackPog.GetComponent<PogumonoScript>().health -= attk;
                currentAttackTimer = attackTimer;
            }
        }

        if(isEnemy && attackPog == null)
        {
            allPogs = GameObject.FindGameObjectsWithTag("Pog");
            
            if(allPogs.Length >= 0)
            {
                int tempI = 0;
                while(allPogs[tempI] == null)
                {
                    tempI++;
                }
                attackPog = allPogs[tempI];
            }
        }
        else if(!isEnemy && attackPog == null)
        {
            allPogs = GameObject.FindGameObjectsWithTag("EnemyPog");

            if (allPogs.Length >= 0)
            {
                int temp2 = 0;
                while (allPogs[temp2] == null)
                {
                    temp2++;
                }
                attackPog = allPogs[temp2];
            }
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(transform.position, otherSide.transform.position, tempSpeed * Time.deltaTime);
        }
    }

    //Attack AI
    public void OnCollisionStay2D(Collision2D collision)
    {
        if(isEnemy)
        {
            //Make sure they don't collide with each other
            if(collision.gameObject.CompareTag("EnemyPog"))
            {
                //Debug.Log("Enemy: Hit Friend");
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }

            //Attack Enemy Pogs
            if (collision.gameObject.CompareTag("Pog"))
            {
                //Debug.Log("Enemy: Hit Enemy");
                attack = true;
                attackPog = collision.gameObject;
            }
            else
            {
                //Debug.Log("Enemy: Hit No One");
                attack = false;
                attackPog = null;
            }
        }
        else
        {
            //Make sure they don't collide with each other
            if (collision.gameObject.CompareTag("Pog") || collision.gameObject.CompareTag("PogTile"))
            {
                //Debug.Log("Player: Hit Friend");
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }

            //Attack Enemy Pogs
            if (collision.gameObject.CompareTag("EnemyPog"))
            {
                //Debug.Log("Player: Hit Enemy");
                attack = true;
                attackPog = collision.gameObject;
            }
            else
            {
                //Debug.Log("Player: Hit No One");
                attack = false;
                attackPog = null;
            }
        }
    }

    //Death
    void Death()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateUI()
    {
        healthText.text = health.ToString();
        attkText.text = attk.ToString();
        defText.text = def.ToString();
        spdText.text = spd.ToString();
        happinessText.text = happiness.ToString();
    }


    private void OnMouseDown()
    {
        if(selected)
        {
            selected = false;
        }
        else
        {
            selected = true;
        }
    }

    //Update UI and activate it
    public void OnMouseOver()
    {
        textCanvas.SetActive(true);

        //Change Happy image to sad if less than half
        if (happiness <= 10)
        {
            faceSymbol.sprite = sadFace;
        }
        else
        {
            faceSymbol.sprite = happyFace;
        }

        gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

    }

    public void OnMouseExit()
    {
        textCanvas.SetActive(false);

        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);


    }
}
