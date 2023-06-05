using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SaveHandler : MonoBehaviour
{
    Dictionary<string, Tilemap> tilemaps = new Dictionary<string, Tilemap>();

    [SerializeField] BoundsInt bounds;
    [SerializeField] string filename = "tilemapData.json";

    private void Start()
    {
        InitTilemaps();
       // InitTileReferences();
    }

    //private void InitTileReferences()
    //{
    //    BuildingObjectBase[] buildables = Resources.LoadAll<BuildingObjectBase>("Scriptables/Buildables");

    //    foreach (BuildingObjectBase buildable in buildables)
    //    {
    //        if (!tileBaseToBuildingObject.ContainsKey(buildable.TileBase))
    //        {
    //            tileBaseToBuildingObject.Add(buildable.TileBase, buildable);
    //            guidToTileBase.Add(buildable.name, buildable.TileBase);
    //        }
    //        else
    //        {
    //            Debug.LogError("TileBase " + buildable.TileBase.name + " is already in use by " + tileBaseToBuildingObject[buildable.TileBase].name);
    //        }
    //    }
    //}

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

    public void OnSave()
    {
        // List that will later be safed
        List<TilemapData> data = new List<TilemapData>();

        // foreach existing tilemap
        foreach (var mapObj in tilemaps)
        {
            TilemapData mapData = new TilemapData();
            mapData.key = mapObj.Key;


            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    Vector3Int pos = new Vector3Int(x, y, 0);
                    TileBase tile = mapObj.Value.GetTile(pos);

                    if (tile != null)
                    {

                        TileInfo ti = new TileInfo(tile, pos);
                        // Add "TileInfo" to "Tiles" List of "TilemapData"
                        mapData.tiles.Add(ti);
                    }
                }
            }

            // Add "TilemapData" Object to List
            data.Add(mapData);
        }
        FileHandler.SaveToJSON<TilemapData>(data, filename);
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


[Serializable]
public class TilemapData
{
    public string key; // the key of your dictionary for the tilemap - here: the name of the map in the hierarchy
    public List<TileInfo> tiles = new List<TileInfo>();
}

[Serializable]
public class TileInfo
{
    public TileBase tile;
    public Vector3Int position;

    public TileInfo(TileBase tile, Vector3Int pos)
    {
        this.tile = tile;
        position = pos; 
    }
}