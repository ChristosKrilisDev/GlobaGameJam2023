
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile : MonoBehaviour
{
    
    
    public enum State
    {
        Open,
        Hide
    }

    public State TileState = State.Hide;

    //todo : move them to a asset collection script
    [SerializeField] private TileBase _tileBase;
    [SerializeField] private Sprite _showTile;
    [SerializeField] private Sprite _hideTile;

    
    private SpriteRenderer _mSprite;

    private void Awake()
    {
        _mSprite = GetComponent<SpriteRenderer>();
    }
    
    
    //call this to show/hide tiles
    public void OnClick()
    {
        //
    }
    
    public void ChangeState(State newState)
    {
        TileState = newState;

        if(TileState == State.Hide) Hide();
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


}
