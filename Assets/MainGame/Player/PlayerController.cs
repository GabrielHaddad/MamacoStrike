using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : IHittable
{
    public static PlayerController Instance
    {
        get
        {
            if(!m_instance)
            {
                m_instance = FindObjectOfType<PlayerController>();

                m_instance?.Init();
            }

            return m_instance;
        }
    }


    public List<TempEffect> CurrentEffects = new List<TempEffect>();

    [Header("Variables")]
    public float MovementSpeed = 5;
    [Space(5)]
    public float MeleeBaseDamage = 8;
    public float MeleeBaseAPS = 2;
    public float MeleeSpawnRange = 3;
    [Space(5)]
    public float ProjBaseDamage = 6;
    public float ProjBaseAPS = 3;
    public float ProjSpawnRange = 1.5f;

    [Header("References")]
    public Collider2D PlayerCollider;
    public Rigidbody2D Rigidbody;

    [Header("Prefabs")]
    public GameObject BaseMelee;
    [Space(5)]
    public GameObject BaseProj;

    public Stat MeleeDamage = new Stat();
    public Stat MeleeAPS = new Stat();
    public Stat ProjDamage = new Stat();
    public Stat ProjAPS = new Stat();

    private bool playerInitted = false;

    private bool isMelee = true;

    private bool attackOnCD = false;

    private static PlayerController m_instance = null;

    public void ReceiveEffect(TempEffect effect)
    {
        if(effect != null)
        {
            CurrentEffects.Add(effect);
            effect.Init(this);
        }
    }

    private void Start()
    {
        if(!m_instance)
        {
            m_instance = this;
            Init();
        }
        else
        {
            if(m_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        foreach(TempEffect eff in CurrentEffects) eff?.Tick();

        Rigidbody.velocity = InputManager.MovementVector * MovementSpeed;
    }

    private void Init()
    {
        if(playerInitted) return;

        playerInitted = true;

        InputManager.Instance.OnShoot.AddListener(OnAttack);
        InputManager.Instance.OnSwap.AddListener(OnModeSwap);

        MeleeDamage.BaseValue = MeleeBaseDamage;
        MeleeAPS.BaseValue = MeleeBaseAPS;
        ProjDamage.BaseValue = ProjBaseDamage;
        ProjAPS.BaseValue = ProjBaseAPS;

        DamageColor = Color.red;
        HPBar = false;
    }

    private void OnAttack()
    {
        if(attackOnCD) return;

        Vector3 mouseVect = (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)) - transform.position).normalized;
        GameObject att;
        float range = 0;

        if(isMelee)
        {
            att = GameObject.Instantiate(BaseMelee);
            var melee = att.GetComponent<MeleeAttack>();

            melee.Damage = MeleeDamage.Value;
            melee.BlackList.Add(PlayerCollider.gameObject);
            range = MeleeSpawnRange;
            
            StartCoroutine(AttackCD(1/MeleeAPS.Value));
        }
        else
        {
            att = GameObject.Instantiate(BaseProj);
            var proj = att.GetComponent<ProjectileAttack>();

            proj.Damage = ProjDamage.Value;
            proj.BlackList.Add(PlayerCollider.gameObject);
            range = ProjSpawnRange;

            StartCoroutine(AttackCD(1/ProjAPS.Value));
        }

        att.transform.position = transform.position + mouseVect*range;
        att.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(mouseVect.y,mouseVect.x) * Mathf.Rad2Deg);
    }

    private void OnModeSwap()
    {
        isMelee = !isMelee;
    }

    private IEnumerator AttackCD(float CD)
    {
        attackOnCD = true;

        yield return new WaitForSeconds(CD);

        attackOnCD = false;
    }
}
