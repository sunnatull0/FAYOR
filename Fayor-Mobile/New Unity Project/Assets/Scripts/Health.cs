using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Text damaged;
    private int killed = 0;

    private void Start()
    {
        damaged = GameObject.FindGameObjectWithTag("Damage").GetComponent<Text>();
    }

    public void Damage()
    {
        killed++;
        damaged.text = "DEATHS: " + killed.ToString();
    }
}
