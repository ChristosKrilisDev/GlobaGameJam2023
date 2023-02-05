using System;
using Enums;
using Settings;
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


        switch (TileType)
        {
            case TileType.Ground:
                GameController.Instance.GroundTilesLeft--;
                MusicSettings.PlayOneShotOver(GameController.Instance.AssetsData.Dig);

                break;
            case TileType.Root:
                GameController.Instance.RootTilesLeft--;
                GameController.Instance.ScoreManager.DecreaseScoreValue(-5);
                MusicSettings.PlayOneShotOver(GameController.Instance.AssetsData.DigRoot);

                break;
            case TileType.Gem:
                MusicSettings.PlayOneShotOver(GameController.Instance.AssetsData.GemSfx);
                GameController.Instance.ScoreManager.IncreaseScoreValue(25);

                GameController.Instance.GroundTilesLeft--;
                GameController.Instance.GemTilesLeft--;
                break;
        }

        GameController.Instance.IncreaseCounters(TileType);
        // if tile is ground type
        if(TileType == TileType.Ground || TileType == TileType.Gem)
        {
            //GameController.Instance.GroundTilesLeft--;
            if (GameController.Instance.GroundTilesLeft <= 0)
            {
                GameController.Instance.gameState = GameState.Transition;
                GameController.Instance.MapController.ShowAllTiles();
            }
            //Debug.Log(GameController.Instance.GroundTilesLeft);
        }
        else
        {
            //GameController.Instance.RootTilesLeft--;
            if (GameController.Instance.RootTilesLeft <= 0)
            {
                GameController.Instance.gameState = GameState.Transition;
                GameObject.Find("ScreenLoader").GetComponent<ScreenLoader>().LoadScene(0);
            }
        }

        // if tile is root type
        //GameController.Instance.rootTilesLeft--;
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
