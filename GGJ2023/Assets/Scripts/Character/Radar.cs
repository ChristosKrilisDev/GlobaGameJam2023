using System.Collections.Generic;
using Enums;
using UnityEngine;

public class Radar : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _areaSpriteRender;
    
    
    private void OnEnable()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void SetTilesList(List<GameObject> tiles)
    {
        foreach (var tile in tiles)
        {
            var tileObj = tile.GetComponent<Tile>();

            if (tileObj.TileType == TileType.Root)
            {
                //todo : score function here or something
                //todo: play sfx
            }
        }
    }



}
