using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class BuyAreaController : MonoBehaviour
{
    public int price;
    public float priceAmount;
    bool isPlayerAvailabe;
    [SerializeField] float cooldown;
    [SerializeField] float cooldownAmount;
    [SerializeField] GameObject buyedElement;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] ParticleSystem buildEffect;

    float remainingTime;
    int paymentAmount = 0;
    public int a;
    private void Start()
    {
        priceText.text = price.ToString();
    }
    private void Update()
    {
        if (price > 0 && isPlayerAvailabe && GameManager.totalMoney > 0 ) 
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                cooldown = cooldownAmount;

                Money money = StackMoneyPool.Instance.pool.Get();
                money.transform.position = PlayerMovements.Instance.transform.position;
                money.transform.eulerAngles = new Vector3(Random.Range(0,359), 0, Random.Range(0, 359));
                money.transform.DORotate(Vector3.zero,0.5f);
                money.transform.DOJump(transform.position + new Vector3(Random.Range(-0.2f ,0.2f), 0,Random.Range(-0.2f ,0.2f)), 2f, 1, .5f).OnComplete(() =>
                {
                   StackMoneyPool.Instance.OnReleaseMoney(money);
                });
                paymentAmount = Mathf.Min(paymentAmount + Random.Range(12, 63), GameManager.totalMoney);
                GameManager.totalMoney -= paymentAmount;
                a = price - paymentAmount;
                price -= paymentAmount;



                // GameManager.totalMoney -= (GameManager.totalMoney * 8) / 100;
                priceText.transform.DOShakeScale(0.5f, 0.1f).OnComplete(() =>
                { priceText.transform.DOScale(1f, 0f);  });


                if (a < 0)
                {

                    GameManager.totalMoney += -a;

                }

                Money_UI.Instance.SetMoneyTxt();

                priceText.text = price.ToString();

            }


        }
        if (price <= 0)
        {
            PriorityManager.Instance.OpenNextPriority();
            buyedElement.SetActive(true);
            buildEffect.gameObject.SetActive(true);
            buildEffect.Play();
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.DOScale(1.2f, 0.5f);
            isPlayerAvailabe = true;

        }
    }




    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.DOScale(1f, 0.5f);

            isPlayerAvailabe = false;
        }
    }
}
