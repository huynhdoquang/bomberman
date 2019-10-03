using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMapController : MonoBehaviour
{
    [SerializeField] private CreateMapView createMapView;
    [SerializeField] private SpriteRenderer spritePreview;

    [SerializeField] private Sprite[] sprites;

    private void Start()
    {
        createMapView.BtnClickAction = OnClickTile;
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
    }
}
