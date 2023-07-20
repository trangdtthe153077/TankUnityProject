using Entity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyDropping : MonoBehaviour
{
    // Start is called before the first frame update
    public Tank _tank;

    private TankMover _tankMover;
    public new GameObject camera;
    public Transform position;


    public Transform frontLeft, frontRight, frontUp, frontDown;
    public GameObject explosion;

    [SerializeField] private TileData brickTileData;
    [SerializeField] private TileData rockTileData;
    Rigidbody2D rb;
    [SerializeField]

    SpawnDropping spawnManager;

    private int type;



    public TextMeshProUGUI text;

    void Start()
    {
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnDropping>();
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
        // Generate a random character between 'A' and 'Z'
        char randomChar = (char)Random.Range('a', 'z' + 1);

        // Display the random character using TextMeshProUGUI
        text.text = randomChar.ToString();




        /*  gameObject.transform.position = position.position;*/
        _tankMover = gameObject.GetComponent<TankMover>();

        Move(Direction.Down);
    }
    
    // Update is called once per frame
    void Update()
    {
        string charString = text.text;

        // Check if the user presses the key corresponding to the randomChar
        if (Input.GetKey(charString))
        {
            // Code to execute when the user presses the key matching the randomChar
            StaticManager.point++;
            StaticManager.currentGold += 10;
         var a =  Instantiate(explosion, transform.position, transform.rotation);
            spawnManager.PlaySound();
            Destroy(a, 1);
            Destroy(gameObject);
        }

        Move(Direction.Down);


       
    }

    private void Move(Direction direction)
    {
        _tank.Position = _tankMover.Move(direction);
        _tank.Direction = direction;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>();


        if (tilemap != null)
        {
            spawnManager.LooseGame();
        }
    }


}
