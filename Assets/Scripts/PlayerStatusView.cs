using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusView : MonoBehaviour
{
    [SerializeField] private Text txtBombNumber;
    [SerializeField] private Text txtBlastNumber;
    [SerializeField] private Text txtSpeed;

    public void Init(int bomb_number, int blast_number, int speed)
    {
        txtBombNumber.text = "x" + bomb_number.ToString();
        txtBlastNumber.text = "x" + blast_number.ToString();
        txtSpeed.text = "x" + speed.ToString();
    }
}
