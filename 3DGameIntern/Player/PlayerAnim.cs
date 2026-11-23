using Controller;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-700)]
[DisallowMultipleComponent]
public class PlayerAnim : MonoBehaviour
{
    public static PlayerAnim instance { get; set; }

    [Header("Main Components")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform playerTF;
    [SerializeField] private Transform enemyTF;

    [Header("VFX Settings")]
    [SerializeField] private ParticleSystem sk1_attack;
    [SerializeField] private ParticleSystem sk2_attack;
    [SerializeField] private ParticleSystem sk3_attack;
    [SerializeField] private ParticleSystem sk4_attack;

    [Header("Skill Cooldowns (Seconds)")]
    private float sk1_cd = 0f;
    private float sk2_cd = 0f;
    private float sk3_cd = 0f;
    private float sk4_cd = 0f;

    [Header("Skill Cooldowns (UI)")]
    [SerializeField] Image sk1;
    [SerializeField] Image sk2;
    [SerializeField] Image sk3;
    [SerializeField] Image sk4;
    [SerializeField] TextMeshProUGUI sk1_Text;
    [SerializeField] TextMeshProUGUI sk2_Text;
    [SerializeField] TextMeshProUGUI sk3_Text;
    [SerializeField] TextMeshProUGUI sk4_Text;

    // Animators & States
    private Animator playerAnim;
    private Animator enemyAnim;
    private string currentAnim = "";
    public string enemyCurrentAnim = "";
    public bool enemyIsAttack = false;
    public bool enemyIdle = false;

    // Combat Logic
    private bool isProcessing;
    private int bsComboIndex = 0;
    private Coroutine resetComboCoroutine;
    private bool run_Front, run_Back = false;
    public bool hit = false;
    public bool checkParry = false;

    public float distPlyerToEnemy;


    // Cache WaitForSeconds เพื่อป้องกัน Garbage Collection (GC Spikes)
    private readonly Dictionary<float, WaitForSeconds> waitCache = new Dictionary<float, WaitForSeconds>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }

        InitializeComponents();
    }

    private void Update()
    {
        // อัปเดตลดเวลาคูลดาวน์สกิล 1-4 ทุกเฟรม
        UpdateSkillCooldowns();

        if (player != null) HandlePlayerAnimations();
        if (enemy != null) HandleEnemyAnimations();
    }

    #region Initialization & Cooldowns
    private void InitializeComponents()
    {
        if (player != null)
        {
            playerAnim = player.GetComponent<Animator>();
            PlayAnim("Sword And Shield Idle");
        }

        if (enemy != null)
        {
            enemyAnim = enemy.GetComponent<Animator>();
        }
    }

    private void UpdateSkillCooldowns()
    {
        if (sk1_cd > 0)
        {
            sk1_cd -= Time.deltaTime;
            sk1.color = Color.black;
            sk1_Text.text = "Cooldown";

        }
        else
        {
            sk1.color = Color.white;
            sk1_Text.text = "Slash";


        }
        if (sk2_cd > 0)
        {
            sk2_cd -= Time.deltaTime;
            sk2.color = Color.black;
            sk2_Text.text = "Cooldown";

        }
        else
        {
            sk2.color = Color.white;
            sk2_Text.text = "Triple Slash";

        }
        if (sk3_cd > 0)
        {
            sk3_cd -= Time.deltaTime;
            sk3.color = Color.black;
            sk3_Text.text = "Cooldown";

        }
        else
        {
            sk3.color = Color.white;
            sk3_Text.text = "Power Slash";

        }
        if (sk4_cd > 0)
        {
            sk4_cd -= Time.deltaTime;
            sk4.color = Color.black;
            sk4_Text.text = "Cooldown";

        }
        else
        {
            sk4.color = Color.white;
            sk4_Text.text = "Wave Slash";


        }

    }

    private WaitForSeconds GetWait(float time)
    {
        if (!waitCache.TryGetValue(time, out var wait))
        {
            wait = new WaitForSeconds(time);
            waitCache.Add(time, wait);
        }
        return wait;
    }
    #endregion

    #region Player Controls & Animation
    private void HandlePlayerAnimations()
    {
        if (isProcessing) return;

        // 1. เช็คการกดสกิลและการโจมตีก่อน (พร้อมเช็ค Cooldown ของสกิล 1-4)
        if (PlayerController.instance != null && PlayerController.instance.isGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && sk1_cd <= 0) { StartCoroutine(Combat("Sword Attack 1")); return; }
            if (Input.GetKeyDown(KeyCode.Alpha2) && sk2_cd <= 0) { StartCoroutine(Combat("Sword Attack 2")); return; }
            if (Input.GetKeyDown(KeyCode.Alpha3) && sk3_cd <= 0) { StartCoroutine(Combat("Sword Combo 3")); return; }
            if (Input.GetKeyDown(KeyCode.Alpha4) && sk4_cd <= 0) { StartCoroutine(Combat("Sword Combo 4")); return; }

            if (Input.GetKeyDown(KeyCode.Q)) { StartCoroutine(Combat("Q_Skill")); return; }
            if (Input.GetKeyDown(KeyCode.E)) { StartCoroutine(Combat("E_Skill")); return; }
            if (Input.GetKeyDown(KeyCode.Mouse0)) { StartCoroutine(Combat("Basic_Melee")); return; }

            if (Input.GetKey(KeyCode.Mouse1) && BossAI.instance != null && BossAI.instance.parryTime && !BossAI.instance.bossAgent.pathPending && BossAI.instance.bossAgent.hasPath && BossAI.instance.bossAgent.remainingDistance <= BossAI.instance.bossAgent.stoppingDistance)
            {
                StartCoroutine(ParrySequence());
                return;
            }
        }

        // 2. เช็คสถานะการลอย / ตกพื้น
        if (PlayerController.instance != null && !PlayerController.instance.isGrounded())
        {
            PlayAnim("Falling Idle");
            return;
        }

        // 3. รับค่า Input การเคลื่อนที่
        bool isShift = Input.GetKey(KeyCode.LeftShift);
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        // ระบบวิ่ง (Running)
        if (isShift)
        {
            if (vertical > 0)
            {
                if (PlayerController.instance != null) PlayerController.instance.RunSpeed();
                if (MouseMoveMent.instance != null) MouseMoveMent.instance.distance = 4f;

                run_Front = true;
                run_Back = false;
                PlayAnimAnyState("Run_Front");
                return;
            }
            else if (vertical < 0)
            {
                if (PlayerController.instance != null) PlayerController.instance.RunSpeed();
                if (MouseMoveMent.instance != null) MouseMoveMent.instance.distance = 4f;

                run_Front = false;
                run_Back = true;
                PlayAnimAnyState("Backward_Run");
                return;
            }
            else if (horizontal > 0 && vertical == 0)
            {
                if (PlayerController.instance != null) PlayerController.instance.RunSpeed();
                if (MouseMoveMent.instance != null) MouseMoveMent.instance.distance = 4f;

                run_Front = false;
                run_Back = false;
                PlayAnim("Sword And Shield Strafe Right");
                return;
            }
            else if (horizontal < 0 && vertical == 0)
            {
                if (PlayerController.instance != null) PlayerController.instance.RunSpeed();
                if (MouseMoveMent.instance != null) MouseMoveMent.instance.distance = 4f;

                run_Front = false;
                run_Back = false;
                PlayAnim("Sword And Shield Strafe Left");
                return;
            }
        }

        run_Front = false;
        run_Back = false;

        if (PlayerController.instance != null) PlayerController.instance.WalkSpeed();
        if (MouseMoveMent.instance != null) MouseMoveMent.instance.distance = MouseMoveMent.instance.originalDistance;

        // --- ระบบเดินปกติ / Idle ---
        if (vertical > 0) PlayAnim("Sword And Shield Walk Front");
        else if (vertical < 0) PlayAnim("Sword And Shield Walk Back");
        else if (horizontal > 0) PlayAnim("Sword And Shield Strafe Right");
        else if (horizontal < 0) PlayAnim("Sword And Shield Strafe Left");
        else PlayAnim("Sword And Shield Idle");
    }
    #endregion

    #region Enemy Animation Control
    private void HandleEnemyAnimations()
    {
        if (BossAI.instance == null || Status.instance == null) return;

        if (WeaponSet.hit != null && !Status.instance.bossIsDeath)
        {
            EnemyPlayAnimAnyState("RibHit");
            hit = true;
            WeaponSet.OnEndAttack();
            enemyIsAttack = false;
            BossAI.instance.bossAgent.speed = BossAI.instance.originalSpeed;
            BossAI.instance.checkWaitForAttack = false;
            StartCoroutine(HitToIdle());
            enemyIdle = false;
        }
        else
        {
            if (!BossAI.instance.bossAgent.pathPending && BossAI.instance.bossAgent.hasPath && BossAI.instance.bossAgent.remainingDistance > BossAI.instance.bossAgent.stoppingDistance && !BossAI.instance.checkWaitForAttack)
            {
                if (enemyCurrentAnim != "Walk") EnemyPlayAnim("Walk");
            }
            else if (!BossAI.instance.bossAgent.pathPending && BossAI.instance.bossAgent.hasPath && BossAI.instance.bossAgent.remainingDistance <= BossAI.instance.bossAgent.stoppingDistance && enemyIdle && !Status.instance.bossIsDeath)
            {
                EnemyPlayAnim("Idle");
            }
        }
    }
    #endregion

    #region Animation Playback Helpers
    private void PlayAnim(string newAnim)
    {
        if (currentAnim == newAnim || playerAnim == null) return;
        playerAnim.CrossFade(newAnim, 0.2f);
        currentAnim = newAnim;
    }

    private void PlayAnimAnyState(string newAnim)
    {
        if (playerAnim == null) return;
        playerAnim.Play(newAnim);
        currentAnim = newAnim;
    }

    public void EnemyPlayAnim(string newAnim)
    {
        if (enemyCurrentAnim == newAnim || enemyAnim == null) return;
        enemyAnim.CrossFade(newAnim, 0.2f);
        enemyCurrentAnim = newAnim;
    }

    public void EnemyPlayAnimAnyState(string newAnim)
    {
        if (enemyCurrentAnim == newAnim || enemyAnim == null) return;
        enemyAnim.Play(newAnim);
        enemyCurrentAnim = newAnim;
    }
    #endregion

    #region Combat Sequences (Coroutines)
    private IEnumerator Combat(string combatType)
    {
        isProcessing = true;

        if (resetComboCoroutine != null)
        {
            StopCoroutine(resetComboCoroutine);
        }

        switch (combatType)
        {
            case "Q_Skill":
                PlayAnimAnyState(combatType);
                yield return GetWait(0.17f);
                if (Status.instance != null) Status.instance.SetBossHealthBar(8);
                yield return GetWait(1.09f);
                break;

            case "E_Skill":
                PlayAnimAnyState(combatType);
                yield return GetWait(0.6f);
                if (Status.instance != null) Status.instance.SetBossHealthBar(5);
                yield return GetWait(0.73f);
                if (Status.instance != null) Status.instance.SetBossHealthBar(5);
                yield return GetWait(0.84f);
                if (Status.instance != null) Status.instance.SetBossHealthBar(5);
                yield return GetWait(0.96f);
                break;

            case "Basic_Melee":
                yield return StartCoroutine(HandleBasicMeleeCombo());
                break;

            case "Sword Attack 1":
                PlayAnimAnyState(combatType);
                yield return GetWait(0.4f);
                PlayVFX(sk1_attack);
                WeaponSet.OnAttack();
                if (Status.instance != null) Status.instance.SetBossHealthBar(10);
                yield return GetWait(0.67f);
                WeaponSet.OnEndAttack();
                sk1_cd = 3f;
                break;

            case "Sword Attack 2":
                PlayAnimAnyState(combatType);
                yield return GetWait(0.4f);
                PlayVFX(sk2_attack);
                WeaponSet.OnAttack();
                if (Status.instance != null) Status.instance.SetBossHealthBar(8);
                yield return GetWait(0.03f);
                WeaponSet.OnEndAttack();
                yield return GetWait(0.6f);
                WeaponSet.OnAttack();
                if (Status.instance != null) Status.instance.SetBossHealthBar(8);
                yield return GetWait(0.07f);
                WeaponSet.OnEndAttack();
                yield return GetWait(0.3f);
                WeaponSet.OnAttack();
                if (Status.instance != null) Status.instance.SetBossHealthBar(8);
                yield return GetWait(0.07f);
                WeaponSet.OnEndAttack();
                sk2_cd = 6f;
                break;

            case "Sword Combo 3":
                PlayAnimAnyState(combatType);
                yield return GetWait(1.17f);
                PlayVFX(sk3_attack);
                WeaponSet.OnAttack();
                if (Status.instance != null) Status.instance.SetBossHealthBar(12);
                yield return GetWait(0.063f);
                WeaponSet.OnEndAttack();
                sk3_cd = 3f;
                break;

            case "Sword Combo 4":
                PlayAnimAnyState(combatType);
                yield return GetWait(0.87f);
                WeaponSet.OnAttack();
                if (Status.instance != null) Status.instance.SetBossHealthBar(8);
                PlayVFX(sk4_attack);
                yield return GetWait(0.03f);
                WeaponSet.OnEndAttack();
                sk4_cd = 8f;
                break;
        }

        currentAnim = "";
        isProcessing = false;

        if (bsComboIndex != 0)
        {
            resetComboCoroutine = StartCoroutine(ResetComboTimer(2.0f));
        }
    }

    private IEnumerator HandleBasicMeleeCombo()
    {
        if (bsComboIndex == 0)
        {
            PlayAnimAnyState("BS_1");
            yield return GetWait(0.19f);
            if (Status.instance != null) Status.instance.SetBossHealthBar(3);
            yield return GetWait(1.10f);
            bsComboIndex = 1;
        }
        else if (bsComboIndex == 1)
        {
            PlayAnimAnyState("BS_2");
            yield return GetWait(0.24f);
            if (Status.instance != null) Status.instance.SetBossHealthBar(3);
            yield return GetWait(1.10f);
            bsComboIndex = 2;
        }
        else if (bsComboIndex >= 2)
        {
            PlayAnimAnyState("BS_3");
            yield return GetWait(1.11f);
            if (Status.instance != null) Status.instance.SetBossHealthBar(5);
            yield return GetWait(1.15f);
            bsComboIndex = 0;
        }
    }

    private IEnumerator ResetComboTimer(float delay)
    {
        yield return GetWait(delay);
        bsComboIndex = 0;
    }
    #endregion

    #region VFX Controls
    private void PlayVFX(ParticleSystem vfxPrefab)
    {
        if (vfxPrefab == null) return;

        if (WeaponSet.instance == null || WeaponSet.instance.bladePos == null)
        {
            Debug.LogError("WeaponSet instance หรือ bladePos ยังไม่ได้ใส่ใน Inspector!");
            return;
        }

        ParticleSystem vfxInstance = Instantiate(vfxPrefab, WeaponSet.instance.bladePos.position, WeaponSet.instance.bladePos.rotation);
        vfxInstance.Play();
        Destroy(vfxInstance.gameObject, 3f);

        if (vfxPrefab == sk4_attack && enemyTF != null)
        {
            StartCoroutine(MoveVFXToTarget(vfxInstance.transform, enemyTF, 0.35f));
        }
    }

    private IEnumerator MoveVFXToTarget(Transform vfxTransform, Transform target, float moveDuration)
    {
        if (vfxTransform == null || target == null) yield break;

        Vector3 startPosition = vfxTransform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            if (vfxTransform == null || target == null) yield break;

            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;

            vfxTransform.position = Vector3.Lerp(startPosition, target.position, t);
            vfxTransform.LookAt(target.position);

            yield return null;
        }

        if (vfxTransform != null && target != null)
        {
            vfxTransform.position = target.position;
        }
    }
    #endregion

    private IEnumerator HitToIdle()
    {
        // แก้ไขให้ใช้ GetWait แทน new WaitForSeconds เพื่อความสมบูรณ์ของระบบ Cache
        yield return GetWait(1f);
        enemyIdle = true;
    }

    private IEnumerator ParrySequence()
    {
        isProcessing = true;

        PlayAnimAnyState("Parry");
        checkParry = true;
        yield return GetWait(0.67f);
        checkParry = false;

        isProcessing = false;
    }
}