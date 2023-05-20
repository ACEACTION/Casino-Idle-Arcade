using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EmployeeCheker : MonoBehaviour
{
    public EmployeeType employeeType;
    public Employee employee;
    public bool isDealerAvailabe;
    public bool isPlayerAvailable;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag(employeeType.ToString()))
        {
            isDealerAvailabe = true;
            employee = other.gameObject.GetComponent<Employee>();
        }

        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerAvailable = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerAvailable = false;

        }
    }
}
