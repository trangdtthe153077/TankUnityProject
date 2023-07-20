using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System.Net.NetworkInformation;

public class SaveHandler : MonoBehaviour
{
    Dictionary<string, Tilemap> tilemaps = new Dictionary<string, Tilemap>();

    [SerializeField] BoundsInt bounds;
    [SerializeField] public TMP_InputField stageInput;
    public int maxCharacterLimit = 12;
    private void Start()
    {
        InitTilemaps();
        // InitTileReferences();
        stageInput.characterLimit = maxCharacterLimit;

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
        //Debug.Log("Stage Inputfield: " + stageInput.text);
        if (stageInput != null && stageInput.text.Length > 0)
        {
            string filename = stageInput.text + ".json";
            FileHandler.SaveToJSON<TilemapData>(data, filename);
            string path = Path.Combine(Application.persistentDataPath, filename);
            //Debug.Log(path);
            Application.LoadLevel("MainMenu");
        }
    }
    public void OnReset()
    {
        // Clear all tilemaps first
        foreach (var map in tilemaps.Values)
        {
            map.ClearAllTiles();
        }

        //string stageName = PlayerPrefs.GetString("StageName");
        //string filename = stageName + ".json";
        //// Clear the JSON file: ghi một chuỗi rỗng vào file 
        //string path = Application.persistentDataPath + "/" + filename;
        //File.WriteAllText(path, "");

        //OnLoad();
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