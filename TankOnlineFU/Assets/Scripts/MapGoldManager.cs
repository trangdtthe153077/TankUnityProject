using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MapGoldManager : MonoBehaviour
{
    public static int mapgold;
    public TextMeshProUGUI mapgoldtxext;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mapgoldtxext.text = mapgold.ToString();

    }

    public int getGold()
    {
        return mapgold;
    }
    public void setGold()
    {
    }
    public void addGold(int gold)
    {
        mapgold += gold;
        mapgoldtxext.text = mapgold.ToString();
    }
}
