using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        Setup();
    }

    void Setup()
    {
        tileBrick.SetActive(false);
        tileUnBreakBrick.SetActive(false);

        createMapView.BtnClickAction = OnClickTile;
        createMapView.ClickExportAction = ExportMap;


        Vertical = (int)Camera.main.orthographicSize;
        Horizontal = Vertical * Screen.width / Screen.height;
        Columns = Horizontal * 2;
        Rows = Vertical * 2 - 1;
        Grid = new int[Columns, Rows];

        _curTileType = TileType.Brick;
        OnClickTile(_curTileType);

        _myWrapper = new MyWrapper();
    }

    TileType _curTileType;
    List<GameObject> gameObjects;
    void OnClickTile(TileType tileType)
    {
        _curTileType = tileType;
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

        Debug.Log("_curTileType pos " + (int)_curTileType);
    }

    bool onCreateMode;
    private void Update()
    {
        if (!onCreateMode) return;
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spritePreview.transform.position = new Vector3(pos.x + 0.5f, pos.y + 0.5f, spritePreview.transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("on GetMouseButtonDown ");

            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            AddTile(position);
        }

        if (Input.GetMouseButtonDown(1))
        {
            //Delete
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            AddTile(position, isDelete: true);
        }

    }

    void AddTile(Vector3 pos, bool isDelete = false)
    {
        var roundPos = new Vector3(Mathf.RoundToInt(pos.x - 0.5f),
                                   Mathf.RoundToInt(pos.y - 0.5f));

        Debug.Log("Horizontal pos " + Horizontal);
        Debug.Log("Vertical pos " + Vertical);
        Debug.Log("roundPos pos " + roundPos);
        //Change to grid pos
        var gridPos = new Vector3(roundPos.x  + (Horizontal), roundPos.y + (Vertical));

        Debug.Log("grid pos " + gridPos);

        if (gridPos.x > Columns || gridPos.x < 0 ||
            gridPos.y > Horizontal || gridPos.y < 0)
            return;

        if (Grid[(int)gridPos.x, (int)gridPos.y] != 0) {
            
            for(int i =0; i< gameObjects.Count; i++)
            {
                var go = gameObjects[i];
                if(go.name == "X: " + (int)gridPos.x + "Y: " + (int)gridPos.y)
                {
                    _myWrapper.gridDatas.RemoveAt(i);
                    gameObjects.Remove(go);
                    Destroy(go);

                    if (isDelete) break;

                    SpawnTile((int)gridPos.x, (int)gridPos.y, (int)_curTileType);
                    break;
                }
            }

        }
        else
        {
            if (isDelete) return;

            SpawnTile((int)gridPos.x, (int)gridPos.y, (int)_curTileType);
        }
    }


    private void SpawnTile(int x, int y, int value)
    {
        Grid[x,y] = (int)_curTileType;

        var tile = Grid[x, y] == 1 ? tileBrick : tileUnBreakBrick;
        SpriteRenderer sr = Instantiate(tile, new Vector3(x - (Horizontal) + 1, y - (Vertical) + 1, tile.transform.position.z), Quaternion.identity, parent: tile.transform.parent).GetComponent<SpriteRenderer>();
        sr.name = "X: " + x + "Y: " + y;
        sr.gameObject.SetActive(true);

        if (gameObjects == null)
            gameObjects = new List<GameObject>();

        if (!gameObjects.Contains(sr.gameObject))
            gameObjects.Add(sr.gameObject);

        if (_myWrapper.gridDatas == null)
            _myWrapper.gridDatas = new List<DTGridData>();

        DTGridData gridData = new DTGridData();
        gridData.posX = x;
        gridData.posY = y;
        gridData.value = value;

        if (!_myWrapper.gridDatas.Contains(gridData))
            _myWrapper.gridDatas.Add(gridData);
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

        var json = HandleTextFile.ReadString();
        var cleanJson = json.Replace('\\',' ');

        MyWrapper myObjects = JsonConvert.DeserializeObject(cleanJson) as MyWrapper;

        if (myObjects == null) return;
        foreach (var g in myObjects.gridDatas)
        {
            if (g.posX > Columns || g.posY < 0 ||
                g.posX > Horizontal || g.posY < 0)
            {
                continue;
            }
            _curTileType = (TileType)g.value;
            SpawnTile(g.posX, g.posY, g.value);
        }
    }

    void SetColumnsAndRows(int column, int row)
    {
        Columns = column;
        Rows = row;
    }


    MyWrapper _myWrapper;
    void ExportMap()
    {
        var serializedJson = JsonConvert.SerializeObject(_myWrapper);
        Debug.Log(serializedJson);

        HandleTextFile.WriteString(serializedJson);
    }
    
}

public class DTGridData
{
    public int posX;
    public int posY;
    public int value;
    
}

public class MyWrapper
{
    public List<DTGridData> gridDatas;
}
