using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapPainter : MonoBehaviour
{

    public Tile grass;
    public Tile rock;
    public Tile brick;
    public Tile brick1;
    public Tile brick2;
    public Tile brick3;
    public Tile brick4;

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
        tileTypes.Add(rock);
        tileTypes.Add(brick);
        tileTypes.Add(brick1);
        tileTypes.Add(brick2);
        tileTypes.Add(brick3);
        tileTypes.Add(brick4);
  

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
        tileMap.SetTile(tileMap.WorldToCell(tank_pos.transform.position), selectedTile);
       
    }
}
