using System;
using DG.Tweening;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    [SerializeField] private CharacterParams _characterParams;
    [SerializeField] private Radar _radarPrefab;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GuiManager _guiManager; //remove from here

    private GameObject _focusedGameObject = null;

    private void Start()
    {
        _characterParams.Reset();
        Init();
    }

    private void Init()
    {
        _scoreManager = new ScoreManager(_characterParams.RadarsSpawnLimit);
        _focusedGameObject = null;
    }

    private void Update()
    {
        OnMouseOver();
        OnMouseExit();
        CheckMouseInput();
    }

    private void OnMouseOver()
    {
        if(_focusedGameObject) return;

        var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit == null || hit.collider == null) return;
        
        _focusedGameObject =  hit.collider.GetComponent<Tile>().gameObject;
        PopUpAnimation(_focusedGameObject.transform);
    }

    private void OnMouseExit()
    {
        _focusedGameObject = null;
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

    private void PopUpAnimation(Transform objTrasform) //todo : bug on multi clicks
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
