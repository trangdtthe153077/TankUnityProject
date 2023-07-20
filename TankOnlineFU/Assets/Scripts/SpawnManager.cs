using Entity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    [HideInInspector] public int liveEnemy;
    [HideInInspector] public float bulletTime;

    const int totalEnemy = 15;
    int[] enemyTanks = { 5, 5, 5, 5 };
    int[] enemyQueue = new int[totalEnemy];

    int prizePerBattle = 3;
    bool[] prizeQueue = new bool[totalEnemy];
    int enemyBorn;
    public GameObject enemy;

    public int tankLives = 3;

    public TankController tank;

    public float countTime;
    bool born=false;
   public Canvas looseScreen;
    public GameObject baseDestroyed;
    private void Awake()
    {
      
    }


    Vector3 Pos1 = new Vector3(0, 1.5f, 0);
    Vector3 Pos2 = new Vector3(3.8f, 1.5f, 0);
    Vector3 Pos3 = new Vector3(-3.8f, 1.5f, 0);
    // Start is called before the first frame update
    void Start()
    {

        looseScreen.gameObject.SetActive(false);
        tank = GameObject.FindGameObjectWithTag("Player").GetComponent<TankController>();
        enemyBorn = 0;
       /* FormEnemyQueue();*/
        SpawnEnemyTank();

   /*    Pos1 = new Vector3(0, 1.5f, 0);
         Pos2 = new Vector3(4, 1.5f, 0);
       Pos3 = new Vector3(10, 10, 10);*/
    }


    private void Update()
    {
        /*     bulletTime -= Time.deltaTime;*/
        SpawnEnemyTank();
        countTime += Time.deltaTime;
        if(countTime > 3f)
        {
            born = true;

        }    
    }



    public void SpawnEnemyTank()
    {

        if(liveEnemy == 0)
        {
            Instantiate(enemy, Pos1, Quaternion.identity);
            Instantiate(enemy, Pos2, Quaternion.identity);
            Instantiate(enemy, Pos3, Quaternion.identity);
        }    


        if (enemyBorn < totalEnemy)
        {
            //print("Tank No " + enemyBorn + " type " + enemyQueue[enemyBorn] + " born at " + enemyBorn % 3);
            
            if(born==true)
            {
                var a = Instantiate(enemy, RandomTransform(), Quaternion.identity);
                countTime = 0;
                enemyBorn++;
                born = false;
            
            }
           
            liveEnemy++;




        }
        else
        {
            WinGame();
        }    

           
        
    }

    public Vector3 RandomTransform()
    {
     
        var ran = Random.Range(0, 3);
        if (ran == 1)
        {
            return Pos1;
        }
        if (ran == 2)
        {
            return Pos2;
        }
        if (ran == 3)
        {
            return Pos3;
        }
        return Pos1;
    }

    public void TankDie()
    {
        if (tankLives>0)
        {
            tank.transform.position = new Vector3(0, -1.8f, 0);
           
            tank._tank.Hp = 10;
            tankLives--;
        }    
        else
        {
            Destroy(tank);
            LooseGame();
        }    
    }

    public void KillBase()
    {
        Instantiate(baseDestroyed, new Vector3(0, -1.80f, 0),Quaternion.identity);
        LooseGame();
    }    
    public void LooseGame()
    {
        Debug.Log("Thua ket thuc game");
        looseScreen.gameObject.SetActive(true);
    }

    public void WinGame()
    {
        Debug.Log("Da giet het quai");
    }



}


