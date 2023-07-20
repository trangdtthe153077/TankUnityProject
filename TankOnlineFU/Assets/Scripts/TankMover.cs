using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TankMover : MonoBehaviour
{
    // Start is called before the first frame update

    private MapPainter mapPainter;
    public float speed=1;
    public Direction currentMove;
    Rigidbody2D rb;
    public GameObject player;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mapPainter = FindObjectOfType<MapPainter>();
        speed = 1;
        var controller = gameObject.GetComponent<TankController>();
        if(controller != null && StaticManager.currentTank==2)
        {
            speed = 3;
        }
        if (controller != null && StaticManager.currentTank == 3)
        {
            rb.bodyType = RigidbodyType2D.Static;
          var  spriteRenderer = gameObject.GetComponent<Renderer>();

            // Set the sorting order to the desired value
            spriteRenderer.sortingOrder = 2;
        }
        if (controller != null && StaticManager.currentTank == 4)
        {
            controller._tank.Hp = 10000;
        }



    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetSpeed(int s)
    {
        speed = s;
    }    

    public Vector3 Move(Direction direction)
    {
        var currentPos = gameObject.transform.position;
        switch (direction)
        {
            case Direction.Down:
                currentPos.y -= speed * Time.deltaTime;
                currentMove = Direction.Down;
                break;
            case Direction.Left:
                currentPos.x -= speed * Time.deltaTime;
                currentMove = Direction.Left;
                break;
            case Direction.Right:
                currentPos.x += speed * Time.deltaTime;
                currentMove = Direction.Right;
                break;
            case Direction.Up:
                currentPos.y += speed * Time.deltaTime;
                currentMove = Direction.Up;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        gameObject.transform.position = currentPos;
        return currentPos;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
      /*  Debug.Log("Day la " + mapPainter.GetTile(gameObject.transform));*/
    /*    rb.constraints = RigidbodyConstraints2D.FreezeRotation;*/
    }
}