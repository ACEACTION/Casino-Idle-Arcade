using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteSpiner : MonoBehaviour
{
    [SerializeField] GameObject staticBalls;
    [SerializeField] GameObject animatedBalls;
    public void RouletteSpinerAnimBeginning()
    {
        staticBalls.SetActive(false);
        animatedBalls.SetActive(true);
    }

    public void RouletteSpinerAnimEnd()
    {
        staticBalls.SetActive(true);
        animatedBalls.SetActive(false);
    }
}
