using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MapGoldManager : MonoBehaviour
{

    public TextMeshProUGUI mapgoldtxext;
    public TextMeshProUGUI pointtext;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        pointtext.text = StaticManager.point.ToString();
        mapgoldtxext.text = StaticManager.point * 10 + "";

    }


}
