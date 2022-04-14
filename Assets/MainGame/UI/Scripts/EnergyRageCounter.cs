using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class EnergyRageCounter : MonoBehaviour
{
    public static EnergyRageCounter Instance {
        get
        {
            if(!m_instance)
            {
                m_instance = FindObjectOfType<EnergyRageCounter>();

                m_instance?.Init();
            }

            return m_instance;
        }
    }
    [Header("Variables")]
    public float SwapDuration = 1;

    [Header("References")]
    public RectTransform EnergyTransform;
    public RectTransform RageTransform;
    [Space(5)]
    public Image EnergyModal;
    public Image RageModal;
    [Space(5)]
    public Slider EnergySlider;
    public Slider RageSlider;
    [Space(5)]
    public TMP_Text EnergyCounter;
    public TMP_Text RageCounter;

    private float swapProgress = 0;
    private Vector2 activePos;
    private Vector2 inactivePos;
    private Vector3 largeScale;
    private Vector3 smallScale;
    private float inactiveModalColor;
    private Color activeTextColor;
    private Color inactiveTextColor;
    private DG.Tweening.Core.TweenerCore<float,float,DG.Tweening.Plugins.Options.FloatOptions> tweener = null;
    private PlayerController player = null;

    private static EnergyRageCounter m_instance = null;

    public void UpdateValues()
    {
        if(!player) player = PlayerController.Instance;

        EnergySlider.value = player.Energy/player.MaxEnergy;
        RageSlider.value = player.Rage/player.MaxRage;

        EnergyCounter.text = $"{(int) player.Energy}/{player.MaxEnergy}";
        RageCounter.text = $"{(int) player.Rage}/{player.MaxRage}";
    }

    public void ModeSwap()
    {
        if(!player) player = PlayerController.Instance;

        float currentProgress = player.isMelee ? swapProgress : 1 - swapProgress;
        float currentDuration = SwapDuration * (1 - currentProgress);

        if(tweener != null)
        {
            EnergyTransform.DOKill();
            RageTransform.DOKill();
            EnergyModal.DOKill();
            RageModal.DOKill();
            EnergyCounter.DOKill();
            RageCounter.DOKill();
            tweener.Kill();
            tweener = null;
        }

        (player.isMelee ? EnergyTransform : RageTransform).SetAsFirstSibling();

        EnergyTransform.DOAnchorPos(player.isMelee ? inactivePos : activePos,currentDuration).SetEase(Database.ElasticPopupCurve);
        RageTransform.DOAnchorPos(player.isMelee ? activePos : inactivePos,currentDuration).SetEase(Database.ElasticPopupCurve);

        EnergyTransform.DOScale(player.isMelee ? smallScale : largeScale,currentDuration).SetEase(Database.ElasticPopupCurve);
        RageTransform.DOScale(player.isMelee ? largeScale : smallScale,currentDuration).SetEase(Database.ElasticPopupCurve);

        EnergyModal.DOFade(player.isMelee ? inactiveModalColor : 0,currentDuration);
        RageModal.DOFade(player.isMelee ? 0 : inactiveModalColor,currentDuration);

        EnergyCounter.DOColor(player.isMelee ? inactiveTextColor : activeTextColor,currentDuration);
        RageCounter.DOColor(player.isMelee ? activeTextColor : inactiveTextColor,currentDuration);

        tweener = DOTween.To(() => swapProgress,(v) => swapProgress = v,player.isMelee ? 1 : 0, currentDuration);
    }

    private bool initted = false;

    private void Start()
    {
        if(m_instance == null)
        {
            m_instance = this;
            Init();
        }
        else
        {
            if(m_instance != this)
            {
                Destroy(this);
            }
        }
    }

    private void Init()
    {
        if(initted) return;

        initted = false;

        activePos = EnergyTransform.anchoredPosition;
        inactivePos = RageTransform.anchoredPosition;
        largeScale = EnergyTransform.localScale;
        smallScale = RageTransform.localScale;
        inactiveModalColor = RageModal.color.a;
        activeTextColor = EnergyCounter.color;
        inactiveTextColor = RageCounter.color;

        if(!player) player = PlayerController.Instance;
    }
}
