using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    [SerializeField] private TittleView tittleView;
    [SerializeField] private InGameView inGameView;
    [SerializeField] private CreateMapView createMapView;

    private void Start()
    {
        tittleView.gameObject.SetActive(true);
        inGameView.gameObject.SetActive(false);
        createMapView.gameObject.SetActive(false);
        tittleView.OnClickAdjustMap = ShowCreateMapView;
        createMapView.OnDoneExport = ShowTittleView;

    }

    public void UpdateTimeView(int time)
    {
        inGameView.UpdateTimeView(time);
    }

    public void ShowIngameView()
    {
        tittleView.gameObject.SetActive(false);
        inGameView.gameObject.SetActive(true);
        createMapView.gameObject.SetActive(false);
    }

    public void UpdateSttView(PlayerController player_1, PlayerController player_2)
    {
        inGameView.UpdateSttView(player_1, player_2);
    }

    public void ShowCreateMapView()
    {
        tittleView.gameObject.SetActive(false);
        inGameView.gameObject.SetActive(false);
        createMapView.gameObject.SetActive(true);
        createMapView.OpenShowCreateMap();
    }

    public void ShowTittleView()
    {
        tittleView.gameObject.SetActive(true);
        inGameView.gameObject.SetActive(false);
        createMapView.gameObject.SetActive(false);
    }
}
