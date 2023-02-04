using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    
    private void Update()
    {
        
        CheckMouseClick();
    }


    
    private void CheckMouseClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit == null || hit.collider == null) return;
        
        var tile = hit.collider.GetComponent<Tile>();
        if (tile)
        {
            Debug.Log("You click a tile");
        }
    }
}
