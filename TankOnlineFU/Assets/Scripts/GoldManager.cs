using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GoldManager : MonoBehaviour
{
 
    public TextMeshProUGUI goldtxext;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        goldtxext.text = StaticManager.currentGold.ToString();

    }

    public int getGold()
    {
        return StaticManager.currentGold;
    }
    public void setGold()
    {
    }
    public void addGold(int gold)
    {
        StaticManager.currentGold += gold;
        goldtxext.text = StaticManager.currentGold.ToString();
    }

}

