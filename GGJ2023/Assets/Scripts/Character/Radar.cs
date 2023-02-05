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
    [SerializeField] private SpriteRenderer _backlight;
    [SerializeField] private Color _onDetectLight, _onDetectDark;
    [SerializeField] private Color _onEmptyLight, _onEmptyDark;
    private Color _color;
    private Color _darkColor;
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
        
        var lerpedColor = Color.Lerp(_color, _darkColor, Mathf.PingPong(Time.time, 1));
        _light.color = lerpedColor;
        _backlight.color = lerpedColor;
    }

    public void SetTilesList(List<GameObject> tiles)
    {
        _darkColor = _onEmptyDark;
        _color = _onEmptyLight;

        foreach (var tile in tiles)
        {
            var tileObj = tile.GetComponent<Tile>();

            if (tileObj.TileType != TileType.Root) continue;
            if(tileObj.TileState == TileState.Opened) continue;
             
            _color = _onDetectLight;
            _darkColor = _onDetectDark;

            //todo : score function here or something
            //todo: play sfx
        }
    }



}
