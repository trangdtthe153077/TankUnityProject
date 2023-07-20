using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Unity.VisualScripting;

public class LoadHandler : MonoBehaviour
{
    Dictionary<string, Tilemap> tilemaps = new Dictionary<string, Tilemap>();

    [SerializeField] BoundsInt bounds;


    private void Start()
    {
        InitTilemaps();
        // InitTileReferences();
        OnLoad(DropdownManager.selectedFileName);
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


    public void OnLoad(string filename)
    {
        //string path = Path.Combine(Application.persistentDataPath, filename);

        // Clear all tilemaps first
        foreach (var map in tilemaps.Values)
        {
            map.ClearAllTiles();
        }

        List<TilemapData> data = FileHandler.ReadListFromJSON<TilemapData>(filename);

        foreach (var mapData in data)
        {
            // if key does NOT exist in dictionary, skip it
            if (!tilemaps.ContainsKey(mapData.key))
            {
                Debug.LogError("Found saved data for tilemap called '" + mapData.key + "', but Tilemap does not exist in scene.");
                continue;
            }

            // get corresponding map
            var map = tilemaps[mapData.key];

            // clear map
            map.ClearAllTiles();

            if (mapData.tiles != null && mapData.tiles.Count > 0)
            {
                foreach (TileInfo tile in mapData.tiles)
                {
                    map.SetTile(tile.position, tile.tile);
                }
            }
        }
    }
}

