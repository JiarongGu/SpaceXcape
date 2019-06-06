using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Guide: MonoBehaviour
{
    public GameObject earthGuide;
    public GameObject launchGuide;
    public GameControl gameControl;
    public Earth earth;
    public LaunchBase launchBase;

    private void Start()
    {
        launchGuide.GetComponent<Renderer>().enabled = false;
        earthGuide.GetComponent<Renderer>().enabled = false;
        gameControl.launchBase.enabled = false;

        Invoke(nameof(EarthBlink), 0);
        Invoke(nameof(LaunchBaseBlink), 1.5f);
        Invoke(nameof(StartGame), 3f);
    }

    private void EarthBlink() {
        earthGuide.GetComponent<Renderer>().enabled = true;
        earth.StartBlink(1.5f);
    }

    private void LaunchBaseBlink() {
        launchGuide.GetComponent<Renderer>().enabled = true;
        launchBase.StartBlink(1.5f);
    }

    private void StartGame() {
        launchGuide.GetComponent<Renderer>().enabled = false;
        earthGuide.GetComponent<Renderer>().enabled = false;
        gameControl.StartGame();
    }
}
