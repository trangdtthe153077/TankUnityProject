using DefaultNamespace;
using Entity;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class TankController : MonoBehaviour
{
    // Start is called before the first frame update
    public Tank _tank;

    public Sprite tankUp;
    public Sprite tankDown;
    public Sprite tankLeft;
    public Sprite tankRight;
    private TankMover _tankMover;
    private CameraController _cameraController;
    private SpriteRenderer _renderer;
    public new GameObject camera;

    public TankManager tankManager;

    public GameObject shield;
    private GameObject shield1;

    private float time;
    private void Awake()
    {
        _tank = new Tank
        {
            Name = "Default",
            Direction = Direction.Down,
            Hp = 10,
            Point = 0,

           Position = new Vector3(0,-1.8f,0),
           
            Guid = GUID.Generate()
        };

        if(shield != null)
        {
            shield1 = Instantiate(shield, gameObject.transform.position, transform.rotation);
            Destroy(shield1, 3);
        }    
    

        Debug.Log("Position start"+ _tank.Position);
        gameObject.transform.position = _tank.Position;
        _tankMover = gameObject.GetComponent<TankMover>();
        _cameraController = camera.GetComponent<CameraController>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    /*    tankManager = GameObject.FindGameObjectWithTag("TankManager").GetComponent<TankManager>();
        tankManager.SetTank();*/
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(shield!=null)
        {
            time += Time.deltaTime;
            if (time < 3)
            {
                shield1.transform.position = gameObject.transform.position;
                _tank.Hp = 10;
            }
        }





        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Move(Direction.Left);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Move(Direction.Down);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Move(Direction.Right);
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Move(Direction.Up);
        }

        if (Input.GetKey(KeyCode.Space) && SceneManager.GetActiveScene().name == "Play")
        {
            
            Fire();
        }
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
            Tank = _tank,
            InitialPosition = _tank.Position
        };
        GetComponent<TankFirer>().Fire(b);
    }
}