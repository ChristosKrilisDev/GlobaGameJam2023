using System;
using Enums;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public TileState TileState = TileState.Hidden;
    public TileType TileType = TileType.None;

    //todo : move them to a asset collection script
    [SerializeField] private Sprite _showTile;
    [SerializeField] private Sprite _hideTile;

    private SpriteRenderer _mSprite;

    private void Awake()
    {
        _mSprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        ChangeState(TileState.Hidden);
    }
    

    private void ChangeState(TileState newState)
    {
        TileState = newState;

        if (TileState == TileState.Hidden) Hide();
        else Show();
    }

    private void Show()
    {
        _mSprite.sprite = _showTile;
    }

    private void Hide()
    {
        _mSprite.sprite = _hideTile;
    }
    
    //call this to show/hide tiles
    public void OnClick()
    {
        if (TileState == TileState.Opened) return; //todo : maybe play some sfx here

        //todo : check tile type
        ChangeState(TileState.Opened);
    }

    public void PlaceObject(GameObject obj)
    {
        obj.transform.localPosition = transform.localPosition;
    }
}
