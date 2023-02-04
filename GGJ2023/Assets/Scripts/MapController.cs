using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    [SerializeField]
    private GameObject tilePrefab;

    private int level = 1;

    private GameObject[] tiles;

    // Start is called before the first frame update
    void Start()
    {
        CreateMap();
    }

    void CreateMap()
    {
        tiles = new GameObject[(level * 2 + 1) * (level * 2 + 1)];
        int x = 0;
        int y = 0;
        for(int i = 0; i < tiles.Length; i++)
        {
            GameObject tile = Instantiate(tilePrefab);
            Debug.Log("I = " + i);
            Debug.Log("X = " + x);
            Debug.Log("Y = " + y);
            tile.transform.position = new Vector3(x, y, 0.0f);
            tiles[y * (level * 2 + 1) + x] = tile;
            x++;
            if (x >= (level * 2 + 1))
            {
                x = 0;
                y++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
