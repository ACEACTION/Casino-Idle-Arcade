using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creative4_Controller : MonoBehaviour
{
    [SerializeField] GameObject[] spotCustomers;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < spotCustomers.Length; i++)
        {
            spotCustomers[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < spotCustomers.Length; i++)
            {
                spotCustomers[i].SetActive(true);
            }
        }
    }
}
