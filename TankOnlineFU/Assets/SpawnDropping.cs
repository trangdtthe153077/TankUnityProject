using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnDropping : MonoBehaviour
{
    public Canvas looseScreen;
    public GameObject enemyPrefab;
    public float initialSpawnInterval = 3f; // Initial time between enemy spawns
    public float spawnIntervalDecrease = 0.1f; // How much the spawn interval decreases each time
    public float speedIncrease = 0.1f; // How much the enemy speed increases each time

    private float currentSpawnInterval;
    private float currentSpeed;
    public float countTime;
    public Camera mainCamera;
    private float leftEdge;
    private float rightEdge;

    private float maxTime=3f;
    public int totalEnemy;
    bool loose = false;
    public float openNewSceneTime;
    public AudioSource audio;

    void Start()
    {////
        looseScreen.gameObject.SetActive(false);
        currentSpawnInterval = initialSpawnInterval;
        currentSpeed = 1f; // Initial enemy speed

        leftEdge = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).x;
        rightEdge = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, mainCamera.nearClipPlane)).x;
 


    }
    public void PlaySound()
    {
        audio.Play();
    }

    private void Update()
    {
        /*     bulletTime -= Time.deltaTime;*/
    
        countTime += Time.deltaTime;
        if (countTime > maxTime)
        {
            SpawnEnemyTank();
            countTime = 0;
          
        }
        if (loose == true)
        {
            openNewSceneTime += Time.deltaTime;
            if (openNewSceneTime > 10)
            { SwitchScene(); }

        }
    }



    public void SpawnEnemyTank()
    {


        var a = Instantiate(enemyPrefab, RandomTransform(), Quaternion.identity);

        totalEnemy++;
        if (totalEnemy%5==0)
        {
            maxTime = maxTime / 1.5f;
            if(maxTime<0.3f)
            {
                maxTime = 0.3f;
            }
        }
    }


    public Vector3 RandomTransform()
    {
        return new Vector3(Random.Range(leftEdge, rightEdge),4, 0);
    }

    public void LooseGame()
    {
        Debug.Log("Thua ket thuc game");
        looseScreen.gameObject.SetActive(true);
        loose = true;

    }
    public void SwitchScene()
    {
        Application.LoadLevel("SummaryScene");
    }
}


