using System;
using Enums;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public TileState TileState = TileState.Hidden;
    public TileType TileType = TileType.Ground;

    //todo : move them to a asset collection script
    [SerializeField] private Sprite _setShowTile;
    [SerializeField] private Sprite _setHideTile;
    [SerializeField] private Sprite _brokenTile;
    [SerializeField] private Color _brokenColor;
    public Sprite SetShowTile
    {
        set => _setShowTile = value;
    }
    public Sprite SetHideTile
    {
        set => _setHideTile = value;
    }

    private SpriteRenderer _mSprite;

    private void Awake()
    {
        _mSprite = GetComponent<SpriteRenderer>();
    }

    
    public void Init()
    {
        ChangeState(TileState.Hidden);
    }


    private void ChangeState(TileState newState)
    {
        TileState = newState;

        if (TileState == TileState.Hidden) Hide();
        else Show();
    }

    public void ChangeStateToShow(TileState newState)
    {
        TileState = newState;
        _mSprite.sprite = _setShowTile;
    }

    private void Show()
    {
        if (TileType == TileType.Root && TileState == TileState.Opened)
        {
            //hit by player
            _mSprite.sprite = _brokenTile;
            _mSprite.color = _brokenColor;
        }
        else
        {
            _mSprite.sprite = _setShowTile;
        }
    }

    private void Hide()
    {
        _mSprite.sprite = _setHideTile;
    }
    
    //call this to show/hide tiles
    public void OnClick()
    {
        if (TileState == TileState.Opened) return; //todo : maybe play some sfx here

        //todo : check tile type
        ChangeState(TileState.Opened);
        IncreaseCounters();
        // if tile is ground type
        if(TileType == TileType.Ground || TileType == TileType.Gem)
        {
            GameController.Instance.GroundTilesLeft--;
            if (GameController.Instance.GroundTilesLeft <= 0)
            {
                GameController.Instance.MapController.ShowAllTiles();
            }
            //Debug.Log(GameController.Instance.GroundTilesLeft);
        }

        // if tile is root type
        //GameController.Instance.rootTilesLeft--;
    }

    private void IncreaseCounters()
    {
        switch (TileType)
        {

            // case TileType.Ground:
            //     GameController.Instance.GroundTilesLeft--;
            //     GameController.Instance.OnGemChanged(GameController.Instance.GroundTilesLeft);
            //     break;
            // case TileType.Root:
            //     GameController.Instance.RootTilesLeft--;
            //     GameController.Instance.OnGemChanged(GameController.Instance.RootTilesLeft);
            //     break;
            // case TileType.Gem:
            //     GameController.Instance.GemTilesLeft--;
            //     GameController.Instance.OnGemChanged(GameController.Instance.GemTilesLeft);
            //     break;
        }
    }

    public void PlaceObject(GameObject obj)
    {
        obj.transform.localPosition = transform.localPosition;
    }

    public void ForceChangeTile(Sprite sprite)
    {
        _mSprite.sprite = sprite;
    }
}
