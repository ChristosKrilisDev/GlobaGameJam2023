using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class Radar : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _areaSpriteRender;
    [SerializeField] private GameObject _graphics;
    [SerializeField] private SpriteRenderer _light;
    [SerializeField] private Color _onDetect, _onEmpty;
    private Color _color;
    private bool _isReady = false;

    private void OnEnable()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void Init(GameObject pinnedTile, List<GameObject> tiles)
    {
        transform.SetParent(pinnedTile.transform);
        transform.localPosition = Vector3.zero;


        _isReady = true;
        SetTilesList(tiles);
    }

    
    private void Update()
    {
        if(!_isReady) return;
        
        var lerpedColor = Color.Lerp(Color.white, _color, Mathf.PingPong(Time.time, 1));
        _light.color = lerpedColor;
    }

    public void SetTilesList(List<GameObject> tiles)
    {
        foreach (var tile in tiles)
        {
            var tileObj = tile.GetComponent<Tile>();

            if (tileObj.TileType == TileType.Root)
            {
                _color = _onDetect;
                //todo : score function here or something
                //todo: play sfx
            }
        }
    }



}
