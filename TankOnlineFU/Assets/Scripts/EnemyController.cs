using DefaultNamespace;
using Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.WSA;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{

    public Tank _tank;

    public Sprite tankUp;
    public Sprite tankDown;
    public Sprite tankLeft;
    public Sprite tankRight;
    private TankMover _tankMover;
    private CameraController _cameraController;
    private SpriteRenderer _renderer;
    public new GameObject camera;
    public Transform position;


    public Transform frontLeft, frontRight, frontUp, frontDown;
    private Tilemap map;
    [HideInInspector] public static float bulletTime;
    private float directionChangeInteval;
    private float directionChangeTimer;
    private bool hasPrize;
    private MapPainter mapPainter;
    private readonly int[] healthArray = { 1, 2, 2, 3 };
    private readonly float[] speedArray = { 0.5f, 0.5f, 1f, 0.5f };

    [SerializeField] private TileData brickTileData;
    [SerializeField] private TileData rockTileData;
    Rigidbody2D rb;
    [SerializeField]
  

    private int type;
    bool changing;

    // Start is called before the first frame update
    void Start()
    {
        directionChangeTimer = 0;
        directionChangeInteval = 2;

        rb = GetComponent<Rigidbody2D>();
        _tank = new Tank
        {
            Name = "Default",
            Direction = Direction.Down,
            Hp = 10,
            Point = 0,
/*
            Position = position.position,*/

            Guid = GUID.Generate()
        };

     /*  gameObject.transform.position = position.position;*/
        _tankMover = gameObject.GetComponent<TankMover>();
        _cameraController = camera.GetComponent<CameraController>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        mapPainter = FindObjectOfType<MapPainter>();
        map = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        Move(Direction.Down);
    }



    private void FixedUpdate()
    {
        if (_tankMover.currentMove == Direction.Left)
        {
            Move(Direction.Left);
        }
        else if (_tankMover.currentMove == Direction.Down)
        {
            Move(Direction.Down);
        }
        else if (_tankMover.currentMove == Direction.Right)
        {
            Move(Direction.Right);
        }
        else if (_tankMover.currentMove == Direction.Up)
        {
            Move(Direction.Up);
        }


        Fire();

        directionChangeTimer += Time.deltaTime;
        if (directionChangeTimer > directionChangeInteval)
        {
           
                Debug.Log("Changing 1");
                changing = true;
                SelectDirection(false);
                directionChangeInteval = Random.Range(0.5f, 2f);
            directionChangeTimer = 0;


        }
    }

    private bool AtGrid(Vector2 v, float grid)
    {
        return (v.x % grid < grid * 0.15 || v.x % grid > grid * 0.85) && (v.y % grid < grid * 0.15 || v.y % grid > grid * 0.85);
    }
    private void Move(Direction direction)
    {
        _tank.Position = _tankMover.Move(direction);
        _tank.Direction = direction;
        /*      Debug.Log(_tank.Position);*/
        /*_cameraController.Move(_tank.Position);*/
        _renderer.sprite = direction switch
        {
            Direction.Down => tankDown,
            Direction.Up => tankUp,
            Direction.Left => tankLeft,
            Direction.Right => tankRight,
            _ => _renderer.sprite
        };
    }


    private void Fire()
    {
        var b = new Bullet
        {
            Direction = _tank.Direction,
            Tank = null,
            InitialPosition = _tank.Position
        };
        GetComponent<TankFirer>().Fire(b);
    }

    private void SelectDirection(bool mustChange)
    {
        if(changing==false)
        { return; }
     
        Debug.Log("Changing");
        float[] directChance = { 0.1f, 0.45f, 0.2f, 0.2f };

        if (System.Math.Abs(transform.position.x) < Mathf.Epsilon)
        {
            directChance[0] = 0.1f;
            directChance[1] = 0.45f;
            directChance[2] = 0.2f;
            directChance[3] = 0.2f;
        }
        else if (transform.position.x < 0)
        {
            directChance[2] = 0.15f;
            directChance[3] = 0.25f;
            if (transform.position.x < -2.75f)
                directChance[2] = 0f;
        }
        else if (transform.position.x > 0)
        {
            directChance[2] = 0.25f;
            directChance[3] = 0.15f;
            if (transform.position.x > -2.75f)
                directChance[3] = 0f;
        }
        if (transform.position.y < -2.75f)
            directChance[1] = 0;
        else if (transform.position.y > 2.75f)
            directChance[0] = 0;

        if (mustChange)
        {
            print("must change direction");
            if (_tankMover.currentMove == Direction.Up)
                directChance[0] = 0f;
            else if (_tankMover.currentMove == Direction.Down)
                directChance[1] = 0f;
            else if (_tankMover.currentMove == Direction.Left)
                directChance[2] = 0f;
            else if (_tankMover.currentMove == Direction.Right)
                directChance[3] = 0f;
        }

        Direction[] directChoice = { Direction.Up, Direction.Down, Direction.Left, Direction.Right };

        _tankMover.currentMove = directChoice[Choose(directChance)];

        changing = false;
    }

    public static int Choose(float[] probs)
    {

        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        Debug.Log("hello"+collision.gameObject.name);
        if (collision.collider.name == "Tilemap")
        {
            TileBase tile = null;
            Debug.Log("Va cham");
            if(_tankMover.currentMove == Direction.Left)
            {
                tile = mapPainter.GetTile(frontLeft);
            }       
            if(_tankMover.currentMove == Direction.Right)
            {
                tile = mapPainter.GetTile(frontRight);
            }
            if (_tankMover.currentMove == Direction.Up)
            {
                tile = mapPainter.GetTile(frontUp);
            }
            if (_tankMover.currentMove == Direction.Down)
            {
                tile = mapPainter.GetTile(frontDown);
            }
  
            if(tile==null)
            {
                SelectDirection(true);
            }    

            foreach (TileBase brick in brickTileData.tiles)
            {
                if (tile == brick)
                {
                    return;
                    break; 
                  
                }
            }
           
                SelectDirection(true);
         
                
       
        }
        else if (collision.gameObject.name == "Main Camera")
            { SelectDirection(true);}

    }

}

