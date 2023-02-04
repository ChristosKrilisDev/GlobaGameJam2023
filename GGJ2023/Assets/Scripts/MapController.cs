using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    [SerializeField]
    private GameObject tilePrefab;

    [HideInInspector]
    private GameObject[,] tiles;

    public Sprite rootDefault;

    public void CreateMap()
    {
        int x = 0;
        int y = 0;
        int tilesOfLevel = (GameController.Instance.level * 2 + 1);
        GameController.Instance.groundTilesLeft = tilesOfLevel * tilesOfLevel;
        tiles = new GameObject[tilesOfLevel, tilesOfLevel];
        x = 0;
        y = 0;

        for(y = 0; y < tilesOfLevel; y++)
        {
            for (x = 0; x < tilesOfLevel; x++)
            {
                GameObject tile = Instantiate(tilePrefab);
                tile.transform.position = new Vector2(x, y);
                tiles[x, y] = tile;
            }
        }

        Distribute(0.3f);
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

    public void Distribute(float perc)
    {
        
        bool finish = false;
        int selectedCount = 0;

        List<GameObject> tileList = new List<GameObject>();
        foreach (GameObject tile in tiles)
        {
            if ((tile.transform.position.x == GameController.Instance.level))
            {
                if ((tile.transform.position.y == GameController.Instance.level)) continue;
            }

            tileList.Add(tile);

        }

        int distributionNumber = (int)(perc * tileList.Count);
        
        while (!finish)
        {
            int randomIndex = Random.Range(0, tileList.Count);
            Sprite s = rootDefault;
            tileList[randomIndex].GetComponent<Tile>().SetShowTile = s;
            tileList[randomIndex].GetComponent<Tile>().TileType = Enums.TileType.Root;
            tileList.Remove(tileList[randomIndex]);
            selectedCount++;

            if (selectedCount >= distributionNumber) finish = true;
        }

    }
}
