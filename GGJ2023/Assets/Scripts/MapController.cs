using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    [SerializeField]
    private GameObject tilePrefab;


    private GameObject[,] tiles;

    public void CreateMap()
    {
        int x = 0;
        int y = 0;

        GameController.Instance.groundTilesLeft = (GameController.Instance.level * 2 + 1) * (GameController.Instance.level * 2 + 1);
        tiles = new GameObject[(GameController.Instance.level * 2) + 1, (GameController.Instance.level * 2) + 1];
        x = 0;
        y = 0;

        for(y = 0; y < ((GameController.Instance.level * 2) + 1); y++)
        {
            for (x = 0; x < ((GameController.Instance.level * 2) + 1); x++)
            {
                GameObject tile = Instantiate(tilePrefab);
                tile.transform.position = new Vector2(x, y);
                tiles[x, y] = tile;
            }
        }
    }

    public void CleanMap()
    {
        for (int j = 0; j < ((GameController.Instance.level * 2) + 1); j++)
        {
            for (int i = 0; i < ((GameController.Instance.level * 2) + 1); i++)
            {
                try
                {
                    Destroy(tiles[i, j]);
                }
                catch
                {
                    Debug.Log("Cannot destroy game object.");
                    break;
                }
            }
        }
    }

    public List<GameObject> ReturnTiles(int i, int j)
    {
        List<GameObject> nearTiles = new List<GameObject>();

        int x = 0;
        int y = 0;

        for (y = 0; y < ((GameController.Instance.level * 2) + 1); y++)
        {
            for (x = 0; x < ((GameController.Instance.level * 2) + 1); x++)
            {

                if((Mathf.Abs((x - i)) <= 1) && (Mathf.Abs((y - j)) <= 1))
                {
                    nearTiles.Add(tiles[x, y]);
                }
            }
        }

        return nearTiles;
    }
}
