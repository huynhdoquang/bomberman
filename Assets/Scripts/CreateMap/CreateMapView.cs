using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TileType
{
    Brick = 1,
    UnBreakBrick = 2,

}

public class CreateMapView : MonoBehaviour
{
    [SerializeField] private Button btnBrick;
    [SerializeField] private Button btnUnBreackBrick;
    [SerializeField] private Button btnPlayer1;
    [SerializeField] private Button btnPlayer2;

    [SerializeField] private Button btnCamera;

    public System.Action<TileType> BtnClickAction;


    private void Start()
    {
        btnBrick.onClick.RemoveListener(OnClickBrick);
        btnBrick.onClick.AddListener(OnClickBrick);

        btnUnBreackBrick.onClick.RemoveListener(OnClickUnBreakBrick);
        btnUnBreackBrick.onClick.AddListener(OnClickUnBreakBrick);
    }

    void OnClickBrick()
    {
        if (BtnClickAction != null) BtnClickAction.Invoke(TileType.Brick);
    }

    void OnClickUnBreakBrick()
    {
        if (BtnClickAction != null) BtnClickAction.Invoke(TileType.UnBreakBrick);
    }



}
