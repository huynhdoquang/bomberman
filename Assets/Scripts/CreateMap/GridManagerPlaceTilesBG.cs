using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerPlaceTilesBG : MonoBehaviour
{
    public GameObject tile;
    public Sprite[] sprites;
    public int[,] Grid;
    float ScreenRatio;
    int Vertical, Horizontal, Columns, Rows;
    public int Resolution;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    private void SpawnTile(int x, int y, int value)
    {
        SpriteRenderer sr = Instantiate(tile, new Vector3(x - (Horizontal) + 1, y - (Vertical) + 1, tile.transform.position.z), Quaternion.identity, parent: tile.transform.parent).GetComponent<SpriteRenderer>();
        sr.name = "X: " + x + "Y: " + y;
        sr.sprite = value == 0 ? sprites[0] : sprites[1];
    }

    private Vector3 GetGridPosition(int x, int y)
    {
        return new Vector3(x - (Horizontal) + 1, y - (Vertical)+1);
    }

    public void Init()
    {
        Vertical = (int)Camera.main.orthographicSize;
        Horizontal = Vertical * Screen.width / Screen.height;
        Columns = Horizontal * 2;
        Rows = Vertical * 2 -1;
        Grid = new int[Columns, Rows];
        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                Grid[i, j] = (i+j)%2 == 0 ? 1 : 0;
                SpawnTile(i, j, Grid[i, j]);
            }
        }
    }

    void SetColumnsAndRows(int column, int row)
    {
        Columns = column;
        Rows = row;
    }
}