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
    public float speed;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mapPainter = FindObjectOfType<MapPainter>();
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
    }


    public Vector3 Move(Direction direction)
    {
        var currentPos = gameObject.transform.position;
        switch (direction)
        {
            case Direction.Down:
                currentPos.y -= speed * Time.deltaTime;
                break;
            case Direction.Left:
                currentPos.x -= speed * Time.deltaTime;
                break;
            case Direction.Right:
                currentPos.x += speed * Time.deltaTime;
                break;
            case Direction.Up:
                currentPos.y += speed * Time.deltaTime;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        gameObject.transform.position = currentPos;
        return currentPos;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Day la " + mapPainter.GetTile(gameObject.transform));
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}