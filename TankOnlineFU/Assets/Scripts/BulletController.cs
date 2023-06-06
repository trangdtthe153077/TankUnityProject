using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletController : MonoBehaviour
{
    [SerializeField] private TileData brickTileData;
    [SerializeField] private TileData rockTileData;
    public Bullet Bullet { get; set; }

    public int MaxRange { get; set; }

    private MapPainter mapPainter;
    // Start is called before the first frame update
    private void Start()
    {
        mapPainter = FindObjectOfType<MapPainter>();
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
        var position = gameObject.transform;
        Debug.Log("Tank pos: " + position.position+ " and Collision pos "+collision.gameObject.transform.position);

     /*   if(collision.gameObject.GetComponent<TankController>()==null)
        {
            Destroy(gameObject);
        }   */ 

        Debug.Log("Collision with " + collision.gameObject.name);

        Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>();
        if (tilemap != null)
        {
         
            Debug.Log("Null or not");
            TileBase tile = mapPainter.GetTile(position);

            Debug.Log(tile);
            if (tile != null)
            {
              
                Debug.Log("Check if tile existed");
                foreach (TileBase rockTile in rockTileData.tiles)
                {
                    if (tile == rockTile)
                    {
                        Debug.Log("Hit the rock");
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
                        mapPainter.SetNull(position); // Remove the tile
                        Destroy(gameObject);
                        break; // No need to check further if a match is found
                    }
                }
             
            }
        }
    }

}