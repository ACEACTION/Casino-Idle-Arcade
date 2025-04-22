using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Localization.Components;

public class WorkerUpgradeEffect : MonoBehaviour
{
    [SerializeField] Transform upgradeMsgPanel;
    [SerializeField] TextMeshProUGUI upgradeTxt;
    [SerializeField] ParticleSystem upgradeEff;
    [SerializeField] LocalizeStringEvent localizeStringEvent;
    [SerializeField] WorkerUpgradeShowData data;

    private void Awake()
    {
        upgradeEff.Stop();        
    }

    private void Start()
    {
        data.upgradeMsgPanelOriginPos = upgradeMsgPanel.localPosition;
        upgradeMsgPanel.gameObject.SetActive(false);
    }

    public void UpgradeEffectProcess(string upgrade_localize_key)
    {
        // effect process
        upgradeEff.Play();

        // show process
        upgradeMsgPanel.gameObject.SetActive(true);

        localizeStringEvent.StringReference.TableEntryReference = upgrade_localize_key;
        upgradeTxt.text = localizeStringEvent.StringReference.GetLocalizedString();

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
