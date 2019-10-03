using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerPlaceTiles : MonoBehaviour
{
    [SerializeField] private GameObject tileBrick;
    [SerializeField] private GameObject tileUnBreakBrick;

    [SerializeField] private CreateMapView createMapView;
    [SerializeField] private SpriteRenderer spritePreview;

    [SerializeField] private Sprite[] sprites;
    public int[,] Grid;
    float ScreenRatio;
    int Vertical, Horizontal, Columns, Rows;
    public int Resolution;
    // Start is called before the first frame update
    void Start()
    {
        tileBrick.SetActive(false);
        tileUnBreakBrick.SetActive(false);

        createMapView.BtnClickAction = OnClickTile;

        //Init();
    }
    
    void OnClickTile(TileType tileType)
    {
        switch (tileType)
        {
            case TileType.Brick:
                spritePreview.sprite = sprites[0];
                break;
            case TileType.UnBreakBrick:
                spritePreview.sprite = sprites[1];
                break;
            default:
                spritePreview.sprite = sprites[0];
                break;
        }
    }

    private void Update()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spritePreview.transform.position = new Vector3(pos.x + 0.5f, pos.y + 0.5f, spritePreview.transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {

        }

        if (Input.GetMouseButtonDown(1))
        {
            //Delete
        }

    }



    private void SpawnTile(int x, int y, int value)
    {
        var tile = value == 0 ? tileBrick : tileUnBreakBrick;
        SpriteRenderer sr = Instantiate(tile, new Vector3(x - (Horizontal) + 1, y - (Vertical) + 1, tile.transform.position.z), Quaternion.identity, parent: tile.transform.parent).GetComponent<SpriteRenderer>();
        sr.name = "X: " + x + "Y: " + y;
        sr.gameObject.SetActive(true);
        //sr.sprite = GetSprite(value);
    }

    private Sprite GetSprite(int value)
    {
        return sprites[value];
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