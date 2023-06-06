using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapPainter : MonoBehaviour
{

    public Tile grass;
    public Tile water;
    public Tile brick;
    public Tile brick1;
    public Tile brick2;
    public Tile brick3;
    public Tile brick4;
    public Tile steel;
    public Tile steel1;
    public Tile steel2;
    public Tile steel3;
    public Tile steel4;

    public Vector3Int position;
    public Transform tank_pos;
    public Tilemap tileMap;

    private int currentTileIndex = 0;

    public List<Tile> tileTypes;
    public Tile selectedTile;
    void Start()
    {
        tileMap.SetTile(position, grass);
        tileTypes.Add(grass);
        tileTypes.Add(water);
        tileTypes.Add(brick);
        tileTypes.Add(brick1);
        tileTypes.Add(brick2);
        tileTypes.Add(brick3);
        tileTypes.Add(brick4);

        tileTypes.Add(steel);
        tileTypes.Add(steel1);
        tileTypes.Add(steel2);
        tileTypes.Add(steel3);
        tileTypes.Add(steel4);


        selectedTile = tileTypes[currentTileIndex];
    }

    // Update is called once per frame
    void Update()
    {

    

        if (Input.GetKeyDown(KeyCode.Space))
        {
           Vector3Int cellPos = tileMap.WorldToCell(tank_pos.transform.position);
            TileBase currentTile = tileMap.GetTile(cellPos);
            if (currentTile != null)
            {
                // Tile exists at the position, switch it with a new tile
                CycleToNextTile();
                PlaceTile(cellPos);
                Debug.Log("The cho");
            }

            else
            {
                PlaceTile(cellPos);
            }
        }
    }

    private void CycleToNextTile()
    {
        currentTileIndex++;
        if (currentTileIndex >= tileTypes.Count)
        {
            currentTileIndex = 0; 
        }
        selectedTile = tileTypes[currentTileIndex];
        Debug.Log("Tile thu may" + currentTileIndex);
       
    }

    public void PlaceTile(Vector3Int position)
    {
        // Instantiate the selected tile at the given position
        tileMap.SetTile(position, selectedTile);
        TileBase tileBase = tileMap.GetTile(position);
        Debug.Log(tileBase);

    }

    public TileBase GetTile(Transform position)
    {

        Vector3Int cellPos = tileMap.WorldToCell(position.position);
        Debug.Log("Vector3" + cellPos);
        TileBase tileBase = tileMap.GetTile(cellPos);
        Debug.Log("Run this" +tileBase);
        return tileBase;
    }
    public TileBase GetTileVector(Vector3Int position)
    {

      
        TileBase tileBase = tileMap.GetTile(position);
        Debug.Log("Run this" + tileBase);
        return tileBase;
    }
    public void SetNullVector(Vector3Int position)
    {

        tileMap.SetTile(position, null);

    }


    public void SetNull(Transform position)
    {

        Vector3Int cellPos = tileMap.WorldToCell(position.position);
        tileMap.SetTile(cellPos,null);
     
    }
}
