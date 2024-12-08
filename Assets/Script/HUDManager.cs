using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Train train;
    [SerializeField] Text passenger;
    [SerializeField] GameManager gameManager;
    [SerializeField] Text score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        passenger.text = "" + train.GetPassenger().ToString("D4");
        score.text = "" + gameManager.GetScore().ToString("D6");
    }
}
