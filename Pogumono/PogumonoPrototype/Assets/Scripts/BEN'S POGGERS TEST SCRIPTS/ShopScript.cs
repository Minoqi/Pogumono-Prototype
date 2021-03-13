using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    //Variables

    void Start()
    {

    }

    void Update()
    {
        
    }

    //Open/Close Shop
    public void OpenCloseShop()
    {
        //Close shop if open and vise-versa
        if (gameObject.GetComponent<Animator>().GetBool("isOpen"))
        {
            gameObject.GetComponent<Animator>().SetBool("isOpen", false);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("isOpen", true);
        }
    }

}
