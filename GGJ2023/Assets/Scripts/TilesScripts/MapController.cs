using System.Collections.Generic;
using DG.Tweening;
using Enums;
using UnityEngine;

public class MapController : MonoBehaviour
{

    [SerializeField]
    private Tile tilePrefab;

    [HideInInspector]
    private Tile[,] tiles;

    public void CreateMap()
    {
        int x = 0;
        int y = 0;
        int tilesOfLevel = (GameController.Instance.Level * 2 + 1);
        tiles = new Tile[tilesOfLevel, tilesOfLevel];
        x = 0;
        y = 0;

        for(y = 0; y < tilesOfLevel; y++)
        {
            for (x = 0; x < tilesOfLevel; x++)
            {
                var tile = Instantiate(tilePrefab);
                tile.transform.position = new Vector2(x, y);
                tiles[x, y] = tile;
                tile.Init();
            }
        }
        SetMiddleTile();
        DistributeGems(Random.Range(1, 1 + GameController.Instance.Level));
        Distribute(0.3f);
        GameController.Instance.GroundTilesLeft = ((tilesOfLevel * tilesOfLevel)  - 1) - GameController.Instance.RootTilesLeft;
        Debug.Log(GameController.Instance.GroundTilesLeft);
    }

    public void ShowAllTiles()
    {

        GameObject obj = null;

        Vector3 startPos = new Vector3(0, 0, 0);
        Vector3 endPos = new Vector3(0, 0, 0);
        float timePassed = 0.0f, duration = 3.0f;
        bool iconsChanged = false;
        List<Tile> rootTilesLeft = new List<Tile>();

        Tweener tweener = DOTween.To(() => startPos, x => startPos = x, endPos, duration)
            .OnStart(() =>
            {
                foreach (Tile tile in tiles)
                {
                    if (tile.TileState == TileState.Hidden && tile.TileType == TileType.Root)
                    {
                        rootTilesLeft.Add(tile);
                    }
                }
            })
            .OnUpdate(() =>
            {
                if ((!iconsChanged) && (timePassed > (duration / 2.0f)))
                {
                    foreach (Tile tile in rootTilesLeft)
                    {
                        tile.ChangeStateToShow(TileState.Opened);
                    }
                    iconsChanged = true;
                }
                foreach (Tile tile in rootTilesLeft)
                {
                    tile.transform.Rotate(0.0f, ((180.0f) / duration) * Time.deltaTime, 0.0f);
                }
                timePassed += Time.deltaTime;
            })
            .OnComplete(() =>
            {
                GameController.Instance.GoToNextLevel();
            });
    }

    public void SetMiddleTile()
    {
        var tile = tiles[GameController.Instance.Level, GameController.Instance.Level].GetComponent<Tile>();

        tile.GetComponent<BoxCollider2D>().enabled = false;
        tile.SetShowTile = GameController.Instance.AssetsData.MiddleTile;
        tile.ForceChangeTile(GameController.Instance.AssetsData.MiddleTile);
        tile.TileState = TileState.Opened;
    }

    public void CleanMap()
    {
        for (int j = 0; j < ((GameController.Instance.Level * 2) + 1); j++)
        {
            for (int i = 0; i < ((GameController.Instance.Level * 2) + 1); i++)
            {
                try
                {
                    Destroy(tiles[i, j].gameObject);
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

        for (y = 0; y < ((GameController.Instance.Level * 2) + 1); y++)
        {
            for (x = 0; x < ((GameController.Instance.Level * 2) + 1); x++)
            {

                if((Mathf.Abs((x - i)) <= 1) && (Mathf.Abs((y - j)) <= 1))
                {
                    nearTiles.Add(tiles[x, y].gameObject);
                }
            }
        }

        return nearTiles;
    }

    public void DistributeGems(int number)
    {

        bool finish = false;
        int selectedCount = 0;

        List<GameObject> tileList = new List<GameObject>();
        foreach (Tile tile in tiles)
        {
            if ((tile.transform.position.x == GameController.Instance.Level))
            {
                if ((tile.transform.position.y == GameController.Instance.Level)) continue;
            }

            tileList.Add(tile.gameObject);
        }

        while (!finish)
        {
            int randomIndex = Random.Range(0, tileList.Count);
            
            if (tileList[randomIndex].GetComponent<Tile>().TileType == Enums.TileType.Ground) 
            {
                Sprite s = GameController.Instance.AssetsData.Gem;
                tileList[randomIndex].GetComponent<Tile>().SetShowTile = s;
                tileList[randomIndex].GetComponent<Tile>().TileType = Enums.TileType.Gem;
                tileList.Remove(tileList[randomIndex]);
                selectedCount++;
                if (selectedCount >= number) finish = true; 
            }
        }

    }

    public void Distribute(float perc)
    {
        bool finish = false;
        int selectedCount = 0;

        List<GameObject> tileList = new List<GameObject>();
        foreach (Tile tile in tiles)
        {
            if ((tile.transform.position.x == GameController.Instance.Level))
            {
                if ((tile.transform.position.y == GameController.Instance.Level)) continue;
            }

            tileList.Add(tile.gameObject);
        }

        GameController.Instance.RootTilesLeft = (int)(perc * tileList.Count);
        
        while (!finish)
        {
            int randomIndex = Random.Range(0, tileList.Count);
            Sprite s = GameController.Instance.AssetsData.Root;
            tileList[randomIndex].GetComponent<Tile>().SetShowTile = s;
            tileList[randomIndex].GetComponent<Tile>().TileType = Enums.TileType.Root;
            //Debug.Log("X = " + tileList[randomIndex].transform.position.x + " Y = " + tileList[randomIndex].transform.position.y);
            tileList.Remove(tileList[randomIndex]);
            selectedCount++;

            if (selectedCount >= GameController.Instance.RootTilesLeft) finish = true;
        }
    }
}
