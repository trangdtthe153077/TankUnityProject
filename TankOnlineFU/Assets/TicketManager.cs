using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ticket1OnClick()
    {
        if (StaticManager.currentGold>=300)
        {
            StaticManager.currentGold -= 300;
            Application.LoadLevel("Dropping");
        }
     
    }
    public void Ticket2OnClick()
    {
        if (StaticManager.currentGold >= 300)
        {
            StaticManager.currentGold -= 300;
            Application.LoadLevel("Boss");
        }

    }
}
