using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    public List<Sprite> shapes = new List<Sprite>();
    public Sprite endSprite;
    public GameObject tile;
    private GameObject[,] tiles;
    public int matchingTileCount; 

    public Camera mainCamera;
    private Vector3 cameraPos;

    public bool possibleMoves { get; set; }

    void Start()
    {
        instance = GetComponent<GridManager>();
        cameraPos = mainCamera.transform.position;
        endSprite = shapes[shapes.Count - 1];
        Level lvl = GameManager.instance.levels[GameManager.instance.index];
        InitGrid(lvl, 0.75f, 0.75f);
    }

    public void InitGrid(Level lvl, float xOffset, float yOffset)
    {
        int rows = lvl.grid_height;
        int columns = lvl.grid_width;
        string[] sequence = lvl.grid;
        GUIManager.instance.MoveCounter = lvl.move_count;
        GUIManager.instance.Level = lvl.level_number;
        
        matchingTileCount = columns - 1;
        possibleMoves = true;

        tiles = new GameObject[rows, columns];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                GameObject newTile = Instantiate(tile, new Vector3(transform.position.x + (xOffset * x), transform.position.y + (yOffset * y), 0), tile.transform.rotation);
                tiles[y, x] = newTile;
                newTile.transform.parent = transform;

                Sprite newSprite;
                if (sequence[columns * y + x] == "b")
                    newSprite = shapes[0];
                else if (sequence[columns * y + x] == "g")
                    newSprite = shapes[1];
                else if (sequence[columns * y + x] == "y")
                    newSprite = shapes[2];
                else //(sequence[columns * y + x] == "r")
                    newSprite = shapes[3];
                newTile.GetComponent<SpriteRenderer>().sprite = newSprite;
            }
        }

        cameraPos.x = tiles[rows - 1, columns - 1].gameObject.transform.position.x / 2;
        cameraPos.y = tiles[rows - 1, columns - 1].gameObject.transform.position.y / 2;
        mainCamera.transform.position = cameraPos;
    }
}
