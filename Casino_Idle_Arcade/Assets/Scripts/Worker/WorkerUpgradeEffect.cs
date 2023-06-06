using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class WorkerUpgradeEffect : MonoBehaviour
{
    [SerializeField] Transform upgradeMsgPanel;
    [SerializeField] TextMeshProUGUI upgradeTxt;
    [SerializeField] ParticleSystem upgradeEff;
    [SerializeField] WorkerUpgradeShowData data;
    //Vector3 upgradeMsgPanelOriginPos;
    //[SerializeField] float yMoveTarget;
    //[SerializeField] float yMoveDuration;


    private void Start()
    {
        upgradeEff.Stop();
        data.upgradeMsgPanelOriginPos = upgradeMsgPanel.localPosition;
        upgradeMsgPanel.gameObject.SetActive(false);
    }

    public void UpgradeEffectProcess(string upgradeMessage)
    {
        // effect process
        upgradeEff.Play();

        // show process
        upgradeMsgPanel.gameObject.SetActive(true);
        upgradeTxt.text = upgradeMessage;

        // y move process
        upgradeMsgPanel.localPosition = data.upgradeMsgPanelOriginPos;
        upgradeMsgPanel.DOLocalMoveY(upgradeMsgPanel.localPosition.y + data.yMoveTarget, data.yMoveDuration)
            .OnComplete(() =>
            {
                upgradeMsgPanel.gameObject.SetActive(false);
            });

        // play sfx
        AudioSourceManager.Instance.PlayUpgradeWorkerSfx();

    }

}
