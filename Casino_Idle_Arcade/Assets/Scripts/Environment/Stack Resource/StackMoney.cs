using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackMoney : MonoBehaviour
{
    [SerializeField] int xSize = 5; // the size of the 3D array in x
    [SerializeField] int ySize = 5; // the size of the 3D array in y
    [SerializeField] int zSize = 5; // the size of the 3D array in z
    [SerializeField] float xOffset; // the custom offset between the game objects
    [SerializeField] float yOffset;
    [SerializeField] float zOffset;
    [SerializeField] float moneyMoveSpeed;
    [SerializeField] Ease moneyMoveEase;
    [SerializeField] float goToPlayerDelay;
    public int stackCounter;


    [SerializeField] StackMoneySlot prefab; // the prefab to create
    [SerializeField] List<StackMoneySlot> slots;
    [SerializeField] List<Money> moneyList;


    private void Start()
    {
        MakeSlots();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MakeSlots();
        }
    }

    void MakeSlots()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                for (int z = 0; z < zSize; z++)
                {
                    StackMoneySlot slot =
                        Instantiate(prefab,
                        transform.position + new Vector3
                        (y * xOffset, x * yOffset, z * zOffset),
                        Quaternion.identity);
                    slot.transform.SetParent(transform);
                    slots.Add(slot);
                }
            }
        }
    }


    public void AddToStack(Money money)
    {
        money.transform.SetParent(slots[stackCounter].transform);
        money.transform.DOLocalMove(Vector3.zero, moneyMoveSpeed)
            .SetEase(moneyMoveEase);        
        moneyList.Add(money);
        stackCounter++;
        if (stackCounter == slots.Count) stackCounter = 0;
    }

    public void GetFromStack()
    {
        stackCounter = 0;
        StartCoroutine(GoToPlayer());
    }

    IEnumerator GoToPlayer()
    {
        for (int i = 0; i < moneyList.Count; i++)
        {
            yield return new WaitForSeconds(goToPlayerDelay);
            moneyList[i].SetGoToPlayer();
        }
        moneyList.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetFromStack();
        }
    }

}
