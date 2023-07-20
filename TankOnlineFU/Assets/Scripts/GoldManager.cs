using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static int currnetGold;
    public TextMeshProUGUI goldtxext;
    // Start is called before the first frame update
    void Start()
    {
        currnetGold = 500;
    }

    // Update is called once per frame
    void Update()
    {
        goldtxext.text = currnetGold.ToString();
    }

    public int getGold()
    {
        return currnetGold;
    }
    public void setGold()
    {
    }
    public void addGold(int gold)
    {
        currnetGold += gold;
        goldtxext.text = currnetGold.ToString();
    }
}

