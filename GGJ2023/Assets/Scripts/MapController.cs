using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    [SerializeField]
    private GameObject tilePrefab;

    private int level = 2;

    private GameObject[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        CreateMap();
    }

    void CreateMap()
    {
        tiles = new GameObject[(level * 2) + 1, (level * 2) + 1];
        int x = 0;
        int y = 0;

        for(y = 0; y < ((level * 2) + 1); y++)
        {
            for (x = 0; x < ((level * 2) + 1); x++)
            {
                GameObject tile = Instantiate(tilePrefab);
                tile.transform.position = new Vector2(x, y);
                tiles[x, y] = tile;
            }
        }
    }

    public List<GameObject> ReturnTiles()
    {
        int i = 3;
        int j = 4;

        List<GameObject> nearTiles = new List<GameObject>();

        int x = 0;
        int y = 0;

        for (y = 0; y < ((level * 2) + 1); y++)
        {
            for (x = 0; x < ((level * 2) + 1); x++)
            {

                if((Mathf.Abs((x - i)) <= 1) && (Mathf.Abs((y - j)) <= 1))
                {
                    nearTiles.Add(tiles[x, y]);
                    Debug.Log("Counter = " + nearTiles.Count);
                    Debug.Log("X = " + x);
                    Debug.Log("Y = " + y);
                }
            }
        }

        return nearTiles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ReturnTiles();
        }
    }
}
