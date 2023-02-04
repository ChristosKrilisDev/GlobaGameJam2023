using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    
    private void Update()
    {
        CheckMouseInput();
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
            Debug.Log("Left : You click a tile");
        }
        else
        {
            Debug.Log("Right : safe digging");
        }
    }
    
}
