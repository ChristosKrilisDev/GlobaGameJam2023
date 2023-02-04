using System;
using DG.Tweening;
using Enums;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    [SerializeField] private CharacterParams _characterParams;
    [SerializeField] private Radar _radarPrefab;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GuiManager _guiManager; //remove from here

    private GameObject _focusedGameObject = null;

    private void Awake()
    {

    }
    
    private void Start()
    {
        _characterParams.Reset();
        Init();
    }

    private void Init()
    {
        _scoreManager = new ScoreManager(_characterParams.RadarsSpawnLimit);
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
        Debug.Log(hit.collider);

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
            if(tile.TileState == TileState.Opened) return;
            PopUpAnimation(tile.transform);
        }
        else
        {
            SpawnRadar(tile);
        }
    }

    private void SpawnRadar(Tile tile)
    {
        if (!_characterParams.CanSpawnRadar())
        {
            Debug.Log("No more radars available");

            return;
        }

        var newRadar = Instantiate(_radarPrefab);
        _characterParams.IncreaseRadarCounter();
        var tilesList = GameController.Instance.mapController.ReturnTiles((int)tile.transform.position.x, (int)tile.transform.position.y);
        newRadar.SetTilesList(tilesList);
        tile.PlaceObject(newRadar.gameObject);
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
