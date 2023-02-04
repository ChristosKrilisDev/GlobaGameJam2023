using System;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _areaSpriteRender;
    
    
    public void Activate()
    {

    }


    public void SetTilesList(List<Tile> tiles)
    {
        
    }
    
    private void OnEnable()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

}
