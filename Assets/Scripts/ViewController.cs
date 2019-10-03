using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    [SerializeField] private TittleView tittleView;
    [SerializeField] private InGameView inGameView;

    private void Start()
    {
        tittleView.gameObject.SetActive(true);
        inGameView.gameObject.SetActive(false);
    }

    public void UpdateTimeView(int time)
    {
        inGameView.UpdateTimeView(time);
    }

    public void ShowIngameView()
    {
        tittleView.gameObject.SetActive(false);
        inGameView.gameObject.SetActive(true);
    }

    public void UpdateSttView(PlayerController player_1, PlayerController player_2)
    {
        inGameView.UpdateSttView(player_1, player_2);
    }

}
