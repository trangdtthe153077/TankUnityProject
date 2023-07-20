using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Unity.VisualScripting;

public class LoadHandler : MonoBehaviour
{
    Dictionary<string, Tilemap> tilemaps = new Dictionary<string, Tilemap>();

    [SerializeField] BoundsInt bounds;
    [SerializeField] string filename = "tilemapData.json";

    private void Start()
    {
        InitTilemaps();
        // InitTileReferences();
        OnLoad();
    }

    private void InitTilemaps()
    {
        // get all tilemaps from scene
        // and write to dictionary
        Tilemap[] maps = FindObjectsOfType<Tilemap>();

        // the hierarchy name must be unique
        // you might add some checks here to make sure
        foreach (var map in maps)
        {
            // if you have tilemaps you don't want to safe - filter them here
            tilemaps.Add(map.name, map);
        }
    }


    public void OnLoad()
    {
        // Clear all tilemaps first
        foreach (var map in tilemaps.Values)
        {
            map.ClearAllTiles();
        }
        List<TilemapData> data = FileHandler.ReadListFromJSON<TilemapData>(filename);

        foreach (var mapData in data)
        {
            // if key does NOT exist in dictionary skip it
            if (!tilemaps.ContainsKey(mapData.key))
            {
                Debug.LogError("Found saved data for tilemap called '" + mapData.key + "', but Tilemap does not exist in scene.");
                continue;
            }

            // get according map
            var map = tilemaps[mapData.key];

            // clear map
            map.ClearAllTiles();

            if (mapData.tiles != null && mapData.tiles.Count > 0)
            {
                foreach (TileInfo tile in mapData.tiles)
                {
                    map.SetTile(tile.position, tile.tile);
                    //Debug.LogError("position: " + tile.position + ", tile: "+ tile.tile);
                }
            }
        }
    }
}

