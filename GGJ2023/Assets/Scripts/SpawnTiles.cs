using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnTiles : MonoBehaviour
{

    public Tilemap tilemap;
    public TileBase tile;
    // Start is called before the first frame update
    void Start()
    {
        tilemap.SetTile(new Vector3Int(0, 0, 0), tile);
        //tilemap.GetTile(new Vector3Int(0, 0, 0));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
