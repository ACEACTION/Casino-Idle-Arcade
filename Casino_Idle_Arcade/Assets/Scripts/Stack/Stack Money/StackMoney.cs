using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackMoney : MonoBehaviour
{
    [Header("Stack")]
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
    bool isPlayer;

    [Header("References")]
    [SerializeField] StackMoneyData stackData;
    [SerializeField] LootMoneyData lootData;
    [SerializeField] StackMoneySlot prefab; // the prefab to create
    [SerializeField] List<StackMoneySlot> slots;
    [SerializeField] List<Money> moneyList;


    private void Start()
    {
        MakeSlots();
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

        if (!isPlayer)
        {
            money.transform.SetParent(slots[stackCounter].transform);
            //money.transform.DOLocalJump(Vector3.zero, 2, 1, moneyMoveSpeed).SetEase(moneyMoveEase);
            money.transform.DOLocalMove(Vector3.zero, moneyMoveSpeed).SetEase(moneyMoveEase);
            money.transform.DORotate(new Vector3(0, Random.Range(-7, 7), 0), 1, RotateMode.FastBeyond360);
            moneyList.Add(money);
            stackCounter++;
            if (stackCounter == slots.Count) stackCounter = slots.Count - 1;
        }
        else
        {
            money.SetGoToPlayer();
        }
    }


   


    private void Update()
    {
        if (isPlayer)
        {
            foreach (Money money in moneyList)
            {
                money.transform.DOMove(money.transform.position + GetRandomLootOffset(), lootData.lootMoveUpDuration)
                    .OnComplete(() =>
                    {
                        money.SetGoToPlayer();
                    });
                    
                money.transform.DORotate(new Vector3(Random.Range(0, 360),
                    Random.Range(0, 360),
                    Random.Range(0, 360)), lootData.lootRotDuration);

                money.transform.DOScale(.4f, lootData.lootScaleDuration);
            }
            stackCounter = 0;
            moneyList.Clear();
        }
    }

    Vector3 GetRandomLootOffset()
    {
        return new Vector3
            (Random.Range(-lootData.lootXOffset, lootData.lootXOffset),
            Random.Range(lootData.lootMinYOffset, lootData.lootMaxYOffset),
            Random.Range(-lootData.lootZOffset, lootData.lootZOffset));
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
            isPlayer = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }

}
