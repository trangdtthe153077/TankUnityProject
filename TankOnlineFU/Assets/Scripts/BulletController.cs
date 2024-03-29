using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class BulletController : MonoBehaviour
{
    [SerializeField] private TileData brickTileData;
    [SerializeField] private TileData rockTileData;
    public Bullet Bullet { get; set; }
    public bool isEnemyBullet;

    public int MaxRange { get; set; }

    private MapPainter mapPainter;

    public GameObject coinPrefab;
    public int coinCount = 1;
 



    SpawnManager spawnManager;
    SpawnTarget spawnTarget;
    public GameObject explosiveAnimation;
    // Start is called before the first frame update

    private void Awake()
    {
        mapPainter = FindObjectOfType<MapPainter>();
        spawnManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<SpawnManager>();
     
        spawnTarget = GameObject.FindGameObjectWithTag("Manager").GetComponent<SpawnTarget>();

    }

    // Update is called once per frame
    private void Update()
    {
        DestroyAfterRange();
    }

    private void DestroyAfterRange()
    {
        var currentPos = gameObject.transform.position;
        var initPos = Bullet.InitialPosition;
        switch (Bullet.Direction)
        {
            case Direction.Down:
                if (initPos.y - MaxRange >= currentPos.y)
                {
                    Destroy(gameObject);
                }

                break;
            case Direction.Up:
                if (initPos.y + MaxRange <= currentPos.y)
                {
                    Destroy(gameObject);
                }

                break;
            case Direction.Left:
                if (initPos.x - MaxRange >= currentPos.x)
                {
                    Destroy(gameObject);
                }

                break;
            case Direction.Right:
                if (initPos.x + MaxRange <= currentPos.x)
                {
                    Destroy(gameObject);
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    
        Debug.Log("Tank pos: " + gameObject.transform.position+ " and Collision pos "+collision.gameObject.transform.position);

     /*   if(collision.gameObject.GetComponent<TankController>()==null)
        {
            Destroy(gameObject);
        }   */ 

        Debug.Log("Collision with " + collision.gameObject.name);

        Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>();
       

        if (tilemap != null)
        {
            var position = tilemap.WorldToCell(gameObject.transform.position);

            switch (Bullet.Direction)
            {
              case Direction.Down:
                    position.y = position.y - 1;
                    break;
                case Direction.Up:
                    position.y = position.y + 1;

                    break;
                case Direction.Left:
                    position.x = position.x - 1;
                    break;
                case Direction.Right:
                    position.x = position.x + 1;
                    break;
            }

            Debug.Log("Null or not");
            TileBase tile = mapPainter.GetTileVector(position);

           
      
            Debug.Log(tile);
            if (tile != null)
            {
              
                Debug.Log("Check if tile existed");
                foreach (TileBase rockTile in rockTileData.tiles)
                {
                    if (tile == rockTile)
                    {
                        Debug.Log("Hit the rock");
                        var a = Instantiate(explosiveAnimation, gameObject.transform.position, Quaternion.identity);
                        Destroy(a, 1);
                        Destroy(gameObject);
                        break; // No need to check further if a match is found
                    }
                }
                // Check if the collided tile is one of the rock tile variations
                foreach (TileBase rockTile in brickTileData.tiles)
                {
                    if (tile == rockTile)
                    {
                        Debug.Log("Collided with a rock tile");
                        mapPainter.SetNullVector(position); // Remove the tile
                        var a = Instantiate(explosiveAnimation, gameObject.transform.position, Quaternion.identity);
                        Destroy(a, 1);
                        Destroy(gameObject);
                        break; // No need to check further if a match is found
                    }
                }
             
            }
        }

       else if(collision.gameObject.tag=="Enemy")
        {
            var tank = Bullet.Tank;
            if(tank!=null)
            {

                var a = Instantiate(explosiveAnimation, gameObject.transform.position, Quaternion.identity);
                Destroy(a, 1);
                Debug.Log("Dang ban enemy");

            var enemy = collision.gameObject.GetComponent<EnemyController>();
            Debug.Log("Quai mau: " + enemy._tank.Hp);
                enemy._tank.Hp -= 5;
         
            if (enemy._tank.Hp <=0)
            {
                coinCount = 10;
                var b = Instantiate(coinPrefab, transform.position, Quaternion.identity);
        
               
                Destroy(b, 0.8f);
                StaticManager.point++;
                    StaticManager.currentGold += coinCount;
                Debug.Log("Quai chet");
                Destroy(collision.gameObject);
                spawnManager.liveEnemy -= 1;
                    Debug.Log("Live enemy: " + spawnManager.liveEnemy);
            }

            }
        }
        else if (collision.gameObject.tag == "EnemyTarget")
        {
            var tank = Bullet.Tank;
            if (tank != null)
            {

                var a = Instantiate(explosiveAnimation, gameObject.transform.position, Quaternion.identity);
                Destroy(a, 1);
                Debug.Log("Dang ban enemy");

                var enemy = collision.gameObject.GetComponent<EnemyTarget>();
                Debug.Log("Quai mau: " + enemy._tank.Hp);
                enemy._tank.Hp -= 5;

                if (enemy._tank.Hp <= 0)
                {
                    coinCount = 10;
                    var b = Instantiate(coinPrefab, transform.position, Quaternion.identity);


                    Destroy(b, 0.8f);
                    StaticManager.point++;
                    StaticManager.currentGold += coinCount;
                    Debug.Log("Quai chet");
                    Destroy(collision.gameObject);
                    spawnTarget.liveEnemy -= 1;
              
                }

            }
        }

        else  if (collision.gameObject.tag == "Player")
        {

            Debug.Log("Ban trung xe tang");
            var tank = Bullet.Tank;
            Debug.Log("Tank 123 " + tank);
            if (tank !=null)
            {
          
            }
            else
            {
                var a = Instantiate(explosiveAnimation, gameObject.transform.position,Quaternion.identity);
                Destroy(a, 1);

                var enemy = collision.gameObject.GetComponent<TankController>();
                Debug.Log("Tank mau: " + enemy._tank.Hp);
                enemy._tank.Hp -= 2;

                if (enemy._tank.Hp <= 0)
                {

                    Debug.Log("Tank chet");
                    if(spawnManager!=null)
                    {
                        spawnManager.TankDie();

                    }
                    if(spawnTarget!=null)
                    { spawnTarget.TankDie(); }

                }
            } 
                

        }
      else  if (collision.gameObject.tag == "Bullet")
        {
             Destroy(collision.gameObject);
            Destroy(gameObject);

            var a = Instantiate(explosiveAnimation, gameObject.transform);
            Destroy(a, 1);

        }

        else if (collision.gameObject.tag == "base")
        {

            Debug.Log("Kill base");
       
                var a = Instantiate(explosiveAnimation, gameObject.transform.position, Quaternion.identity);
                Destroy(a, 1);
            spawnManager.KillBase();
            Destroy(collision.gameObject);


        }

        else if (collision.gameObject.tag == "Boss")
        {
            var tank = Bullet.Tank;
            if (tank != null)
            {

                var a = Instantiate(explosiveAnimation, gameObject.transform.position, Quaternion.identity);
                Destroy(a, 1);
                Debug.Log("Dang ban enemy");

                var enemy = collision.gameObject.GetComponent<Boss>();
                Debug.Log("Quai mau: " + enemy._tank.Hp);
                enemy._tank.Hp -= 5;

                if (enemy._tank.Hp <= 0)
                {
                    coinCount = 1000;
                    var b = Instantiate(coinPrefab, transform.position, Quaternion.identity);


                    Destroy(b, 0.8f);
                    StaticManager.point++;
                    StaticManager.currentGold += coinCount;
                    Debug.Log("Quai chet");
                    Destroy(collision.gameObject);
                    spawnTarget.WinGame();
                    Debug.Log("Live enemy: " + spawnManager.liveEnemy);
                }

            }
        }


    }

}