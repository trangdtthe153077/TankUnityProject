using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button tank1;
    public Button tank2;
    public Button tank3;
    public Button tank4;

    public Sprite tankUp1, tankUp2, tankUp3, tankUp4;
    public Sprite tankDown1, tankDown2, tankDown3, tankDown4;
    public Sprite tankLeft1, tankLeft2, tankLeft3, tankLeft4;
    public Sprite tankRight1, tankRight2, tankRight3, tankRight4;

    public TankController controller;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetColor()
    {
        if (StaticManager.bool1 == true)
        {
            tank1.GetComponent<Image>().color = Color.white;
        }
        if (StaticManager.bool2 == true)
        {
            tank2.GetComponent<Image>().color = Color.white;
        }
        if (StaticManager.bool3 == true)
        {
            tank3.GetComponent<Image>().color = Color.white;
        }
        if (StaticManager.bool4 == true)
        {
            tank4.GetComponent<Image>().color = Color.white;
        }
        Debug.Log("Current " + StaticManager.currentTank);
        switch (StaticManager.currentTank)
        {
            case 1:
                tank1.GetComponent<Image>().color = Color.cyan;
                break;
            case 2:
                tank2.GetComponent<Image>().color = Color.cyan;
                break;
            case 3:
                tank3.GetComponent<Image>().color = Color.cyan;
                break;
            case 4:
                tank4.GetComponent<Image>().color = Color.cyan;
                break;
        }
    }

    public void SetTank()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<TankController>();
        if (StaticManager.currentTank == 1)
        {
            controller.tankUp = tankUp1;
            controller.tankDown = tankDown1;
            controller.tankLeft = tankLeft1;
            controller.tankRight = tankRight1;

        }
        else if (StaticManager.currentTank == 2)
        {


            controller.tankUp = tankUp2;
            controller.tankDown = tankDown2;
            controller.tankLeft = tankLeft2;
            controller.tankRight = tankRight2;


        }
        else if (StaticManager.currentTank == 3)
        {


            controller.tankUp = tankUp3;
            controller.tankDown = tankDown3;
            controller.tankLeft = tankLeft3;
            controller.tankRight = tankRight3;
        }


        else if (StaticManager.currentTank == 4)
        {


            controller.tankUp = tankUp4;
            controller.tankDown = tankDown4;
            controller.tankLeft = tankLeft4;
            controller.tankRight = tankRight4;


        }

    }

    public void Tank1OnClick()
    {
        StaticManager.bool1 = true;
        StaticManager.currentTank = 1;
        SetColor();

    }

    public void Tank2OnClick()
    {
        if (StaticManager.bool2 != true)
        {
            if (GoldManager.currnetGold >= 100)
            {
                GoldManager.currnetGold -= 100;
                StaticManager.bool2 = true;
            }
          
        }
        else
        {
            StaticManager.currentTank = 2;
        }
        SetColor();
    }

    public void Tank3OnClick()
    {
        if (StaticManager.bool3 != true)
        {
            if (GoldManager.currnetGold >= 200)
            {
                GoldManager.currnetGold -= 200;
                StaticManager.bool3 = true;
            }
       
        }
        else
        {
            StaticManager.currentTank = 3;
        }
        SetColor();
    }
    public void Tank4OnClick()
    {
        if (StaticManager.bool4 != true)
        {
            if (GoldManager.currnetGold >= 400)
            {
                GoldManager.currnetGold -= 400;
                StaticManager.bool4 = true;
            }
       
        }
        else
        {
            StaticManager.currentTank = 4;
        }
        SetColor();

    }
}
