using System;
using DG.Tweening;
using Enums;
using Settings;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
     public CharacterParams CharacterParams;
    [SerializeField] private Radar _radarPrefab;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GuiManager _guiManager; //remove from here

    private GameObject _focusedGameObject = null;
    
    
    private void Start()
    {
        CharacterParams.Reset();
        Init();
    }

    private void Init()
    {
        _scoreManager = new ScoreManager(CharacterParams.RadarsSpawnLimit);
        GameController.Instance.ScoreManager = _scoreManager;
        _focusedGameObject = null;
    }

    private void Update()
    {
        OnMouseOver();
        CheckMouseInput();
    }

    private void OnMouseOver()
    {
        // if(_focusedGameObject) return;

        //todo : performance issue incomingggg
        var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit == null || hit.collider == null)
        {
            _focusedGameObject = null;
            return;
        }

        if (hit.collider.gameObject == _focusedGameObject) return;

        var tile = hit.collider.GetComponent<Tile>();
        if(tile != null && tile.TileState == TileState.Opened) return;
        
        _focusedGameObject =  tile.gameObject;
        PopUpAnimation(_focusedGameObject.transform);
    }

 

    private void CheckMouseInput()
    {
        CheckLeftMouseClick();
        CheckRightMouseClick();
    }

    private void CheckLeftMouseClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        RayCastDetection(true);
    }

    private void CheckRightMouseClick()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        RayCastDetection(false);
    }

    private void RayCastDetection(bool isLeftClick)
    {
        var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit == null || hit.collider == null) return;

        var tile = hit.collider.GetComponent<Tile>();

        if (tile == null) return;

        if (isLeftClick)
        {
            tile.OnClick();
            //todo : play sfx
            MusicSettings.PlayOneShot(GameController.Instance.AssetsData.MouseClick[0]);
            if(tile.TileState == TileState.Opened) return;
            PopUpAnimation(tile.transform);
        }
        else
        {
            var hasChild = tile.transform.childCount;
            if(hasChild >= 1) return;
            
            MusicSettings.PlayOneShot(GameController.Instance.AssetsData.Dig);
            SpawnRadar(tile);
        }
    }

    private void SpawnRadar(Tile tile)
    {
        if (!CharacterParams.CanSpawnRadar())
        {
            Debug.Log("No more radars available");

            return;
        }

        var newRadar = Instantiate(_radarPrefab);
        CharacterParams.IncreaseRadarCounter();
        GameController.Instance.OnRadarChanged?.Invoke(CharacterParams.CurrentRadarsSpawned);
        var tilesList = GameController.Instance.MapController.ReturnTiles((int)tile.transform.position.x, (int)tile.transform.position.y);
        newRadar.Init(tile.gameObject,tilesList);
        // tile.PlaceObject(newRadar.gameObject);
    }

    public int GetUsedRadarsCount()
    {
        return CharacterParams.CurrentRadarsSpawned;
    }
    
#region Animations

    private void PopUpAnimation(Transform objTrasform)
    {
        var scale = Vector3.one;
        
        objTrasform.transform.DOKill();
        objTrasform.transform.DOScale(new Vector3(1.05f, 1.05f, 1), 0.25f).SetEase(Ease.OutFlash).OnComplete(() =>
        {
            objTrasform.transform.DOScale(scale, 0.25f);
        });
        
        objTrasform.transform.localScale = scale;
    }

#endregion

}
