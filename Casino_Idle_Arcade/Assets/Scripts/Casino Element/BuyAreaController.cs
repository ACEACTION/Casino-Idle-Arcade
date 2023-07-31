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
    [SerializeField] bool activeBoughtElement;
    [SerializeField] bool destroyAfterBought = true;
    public int price;
    //public int priceAmount;
    public int payTime;//time to buy anyelement
    bool isPlayerAvailabe;
    [SerializeField] float cooldown;
    [SerializeField] float cooldownAmount;
    [SerializeField] GameObject buyedElement;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] ParticleSystem buildEffect;
    [SerializeField] PriorityManager priorityManager;

    public float maxPlayerWaitingCd;
    float playerWaitingCd;

    public int paymentAmount = 0;
    int remainingPayment;
    float defaultScale;
    bool playSfx;
    private void Start()
    {
        payTime = 2;
        priceText.text = price.ToString();
        defaultScale = transform.localScale.x;
        playerWaitingCd = maxPlayerWaitingCd;
        //priceAmount = price;
        paymentAmount = (price / (payTime * 10));

    }
    private void Update()
    {
        if (price > 0 && isPlayerAvailabe && GameManager.GetTotalMoney() > 0 ) 
        {
            if (playerWaitingCd <= 0)
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                {
                    cooldown = cooldownAmount;
                    
                    Money money = StackMoneyPool.Instance.pool.Get();
                    money.transform.position = PlayerMovements.Instance.transform.position;
                    money.transform.eulerAngles = new Vector3(Random.Range(0, 359), 0, Random.Range(0, 359));
                    money.transform.DORotate(Vector3.zero, 0.5f);
                    money.transform.DOJump(transform.position + new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f)), 2f, 1, .5f).OnComplete(() =>
                    {
                        StackMoneyPool.Instance.OnReleaseMoney(money);
                    });
                    //paymentAmount = Mathf.Min(paymentAmount + Random.Range(2, 5), GameManager.GetTotalMoney());
                    paymentAmount = Mathf.Min(paymentAmount, GameManager.GetTotalMoney());

                    GameManager.MinusMoney(paymentAmount);
                    remainingPayment = price - paymentAmount;

                    if (remainingPayment <= 0)
                    {
                        GameManager.MinusMoney(-remainingPayment);
                    }

                    price -= paymentAmount;

                    AudioSourceManager.Instance.PlayFushSfx();

                    Money_UI.Instance?.SetTotalMoneyTxt();

                    priceText.text = price.ToString();
                }
            }
            else
                playerWaitingCd -= Time.deltaTime;
            
            
        }
        if (price <= 0)
        {
            //PriorityManager.Instance.OpenNextPriority();
            priorityManager.OpenNextPriority();
            buyedElement.SetActive(activeBoughtElement);
            buildEffect.gameObject.SetActive(true);
            buildEffect.Play();

            if (!playSfx)
            {
                playSfx = true;
                AudioSourceManager.Instance.PlayBuyAreaSfx();
            }

            if (destroyAfterBought)
                Destroy(this.gameObject, .1f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerAvailabe = true;
            if (GameManager.GetTotalMoney() > 0)
            {
                transform.DOScale(defaultScale + .2f, 0.5f);
                priceText.transform.DOScale
                (priceText.transform.localScale.x + 0.1f, 0.3f);


            }
            else 
                Money_UI.Instance.ShakeMoneyUI();

        }
    }




    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.DOScale(defaultScale, 0.5f);
            isPlayerAvailabe = false;
            priceText.transform.DOScale(0.6f, 0f);
            playerWaitingCd = maxPlayerWaitingCd;
        }
    }
}
