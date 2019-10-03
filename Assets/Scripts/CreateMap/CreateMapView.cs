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

    [SerializeField] private Button btnExport;

    [SerializeField] private Button btnCamera;

    public System.Action<TileType> BtnClickAction;
    public System.Action<PlayerInput> OnClickPlayerAction;
    public System.Action ClickExportAction;

    private void Start()
    {
        btnBrick.onClick.RemoveListener(OnClickBrick);
        btnBrick.onClick.AddListener(OnClickBrick);

        btnUnBreackBrick.onClick.RemoveListener(OnClickUnBreakBrick);
        btnUnBreackBrick.onClick.AddListener(OnClickUnBreakBrick);

        btnExport.onClick.RemoveListener(OnClickExport);
        btnExport.onClick.AddListener(OnClickExport);

        btnPlayer1.onClick.RemoveListener(OnClickPlayer1);
        btnPlayer1.onClick.AddListener(OnClickPlayer1);

        btnPlayer2.onClick.RemoveListener(OnClickPlayer2);
        btnPlayer2.onClick.AddListener(OnClickPlayer2);
    }

    void OnClickBrick()
    {
        if (BtnClickAction != null) BtnClickAction.Invoke(TileType.Brick);
    }

    void OnClickUnBreakBrick()
    {
        if (BtnClickAction != null) BtnClickAction.Invoke(TileType.UnBreakBrick);
    }

    void OnClickExport()
    {
        if (ClickExportAction != null) ClickExportAction.Invoke();
    }

    void OnClickPlayer1()
    {
        if (OnClickPlayerAction != null) OnClickPlayerAction.Invoke(PlayerInput.P1);
    }
    void OnClickPlayer2()
    {
        if (OnClickPlayerAction != null) OnClickPlayerAction.Invoke(PlayerInput.P2);
    }
}
