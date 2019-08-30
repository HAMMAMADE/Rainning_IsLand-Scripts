using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public MasterManager masterManager;

    public CameraMove Camera;

    public Animator animator;

    Rigidbody Playrigidbody;

    public float waterGrav;
    private float originGrav;
    bool isWater, isDive;

    public GameObject PauseSet;
    //------------------------------------------
    public GameObject EquipHand;

    public GameObject EquipHand1;

    public GameObject EquipHand2;

    public GameObject EquipHand3;

    public BoxCollider AttackRange;
    //------------------------------------------

    bool isEnding, inWater;

    public float HealthPoint;

    public float Stamina;

    public float Temper;

    public int BreathPoint = 8;

    public float speed;

    public float jumpPower = 5f;

    public float rotateSpeed = 2f;

    public Vector3 movement;
    Vector3 TurnSet;

    public int AddPower;

    public int FishingTime;

    public float AddHealth;

    //------------------------------------
    public GameObject PopUpEffect;
    GameObject Effects;

    public GameObject HitEffect;
    public Transform TargetPos;
    GameObject Hits;
    //------------------------------------

    public float AxeDurability;
    public float HammerDurability;
    public float RodDurability;

    //------------------------------------
    float HorizontalMove;
    float VerticalMove;

    public int keyCount;
    private int InvKeyCount;
    private int InterKeyCount;

    bool isJumping, isMoving, firstJump, isRunning, isCanFishing;
    bool staminadec;


    public bool isFelling, isInterAct;
    public bool isSmallFell, isbigFell;

    public bool isFishing;
    public bool canBaiting;

    public bool isBuilding;
    public bool isAttack;

    public int coldstack;
    bool recoverCold, getCold;

    public bool isWithFire;
    public bool isWithHaze;
    public bool interActFire;
    public bool interActHaze;
    public bool ishaveAttack;
    public bool isPaused;
    public bool isSetting;

    public bool isGameover;

    bool nowFelling;

    // Use this for initialization
    void Start()
    {
        //masterManager.soundCheck.BGMPlay("DayBGM");
        isGameover = false;
        keyCount = 0;
        InvKeyCount = 0;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        isDive = false;

        originGrav = 0;

        speed = 3.5f;

        Stamina = 100f;

        animator = GetComponent<Animator>();

        Playrigidbody = GetComponent<Rigidbody>();

        Playrigidbody.position = movement;
    }

    void Run()
    {
        if (!isMoving)
        {
            animator.SetBool("Move", false);
            return;
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                animator.SetBool("Move", true);
                animator.SetBool("MoveRight", true);
                animator.SetBool("MoveLeft", false);
            }
            else if (Input.GetAxisRaw("Horizontal") == 1)
            {
                animator.SetBool("Move", true);
                animator.SetBool("MoveLeft", true);
                animator.SetBool("MoveRight", false);
            }
            else
            {
                animator.SetBool("Move", true);
                animator.SetBool("MoveRight", false);
                animator.SetBool("MoveLeft", false);
            }
            Turn();
            Vector3 inputMoveXZ = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                float inputMoveXZMgnitude = inputMoveXZ.sqrMagnitude;
                inputMoveXZ = transform.TransformDirection(inputMoveXZ);
               if (inputMoveXZMgnitude <= 1)
                inputMoveXZ *= speed;
               else
                inputMoveXZ = inputMoveXZ.normalized * speed;
            
            movement = inputMoveXZ;
            movement = movement * Time.deltaTime;
            Playrigidbody.MovePosition(transform.position + movement);
        }
    }
       
    void Jump()
    {
        if (!isJumping)
            return;
        firstJump = true;
        animator.SetTrigger("Jump");
        Playrigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isJumping = false;
    }

    void Turn()
    {
        if (!isBuilding&&!isWithFire)
        {
            Quaternion cameraRotation = Camera.transform.rotation;

            cameraRotation.x = cameraRotation.z = 0;    //y축만 필요하므로 나머지 값은 0으로 바꾼다.

            //자연스러움을 위해 Slerp로 회전시킨다.
            if (movement != Vector3.zero)
            {
                Playrigidbody.rotation = Quaternion.Slerp(Playrigidbody.rotation, cameraRotation, 10.0f * Time.deltaTime);
            }
        }
    }

    void Sprint()
    {
        if (isRunning)
        {
            animator.SetBool("Run", true);
            speed = 5.5f - masterManager.BuffCheck.Debuff1Effect1;
        }
        else
        {
            animator.SetBool("Run", false);
            speed = 3.5f - masterManager.BuffCheck.Debuff1Effect1;
        }
    }

    void FixedUpdate()
    {
        Run();
        Sprint();
        Jump();
    }
    // Update is called once per frame
    void Update()
    {
        if (TutorialManager.EndTutorial)
        {
            if (!isGameover)
            {
                if(Input.GetKeyDown(KeyCode.E) && isEnding)
                {
                    masterManager.EndCheck.StartGameClear();
                }

                //----player State------------------------------------
                if (Temper > 0.71 && !recoverCold && masterManager.BuffCheck.itHaveCold)
                {
                    recoverCold = true;
                    StartCoroutine("RecoverCold");
                }
                else if (Temper < 0.5 && recoverCold && masterManager.BuffCheck.itHaveCold)
                {
                    recoverCold = false;
                    StopCoroutine("RecoverCold");
                }

                if (coldstack > 6)
                {
                    masterManager.BuffCheck.SetDebuff1();
                }
                else if (coldstack < 5)
                {
                    getCold = false;
                    masterManager.BuffCheck.OffDebuff1();
                }
                //----------------------------------------------------
                if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && !isSetting && !isWithFire && !isWithHaze && !masterManager.BuildCheck.openBuildMenu)
                {
                    PauseSet.SetActive(true);
                    masterManager.soundCheck.SFXPlay("PauseSound");
                    if (!Cursor.visible)
                    {
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                    }
                    isPaused = true;
                    Time.timeScale = 0;
                }
                else if (Input.GetKeyDown(KeyCode.Escape) && isPaused && !isSetting && !isWithFire && !isWithHaze && !masterManager.BuildCheck.openBuildMenu)
                {
                    PauseSet.SetActive(false);
                    masterManager.soundCheck.SFXPlay("PauseSound");
                    if (Cursor.visible)
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                    isPaused = false;
                    Time.timeScale = 1;
                }

                if (isBuilding || isFishing || isFelling || isAttack || isWithFire || isWithHaze)
                {
                    isMoving = false;
                }

                if (Input.GetMouseButtonDown(0) && !isAttack && !isJumping && !isFelling && !isFishing && !interActFire && !isWithHaze && !isPaused && !masterManager.BuildCheck.openBuildMenu)
                {
                    Effects = Instantiate(PopUpEffect, transform);
                    //speed = 0.5f;
                    isAttack = true;
                    StartCoroutine("DoAttak");
                }

                if (Input.GetKeyDown(KeyCode.I) && !isPaused && InvKeyCount == 0)
                {
                    masterManager.soundCheck.SFXPlay("OpenInvenSound");
                    masterManager.UiCheck.InvClose();
                    masterManager.UiCheck.StartInvIcon();
                    InvKeyCount += 1;
                }
                else if (Input.GetKeyDown(KeyCode.I) && !isPaused && InvKeyCount == 1)
                {
                    masterManager.soundCheck.SFXPlay("OpenInvenSound");
                    masterManager.UiCheck.InvOpen();
                    masterManager.UiCheck.EndInvMain();
                    InvKeyCount = 0;
                }
                //---------미끼뿌리기-----------------
                if (Input.GetKeyDown(KeyCode.F) && canBaiting)
                {
                    if (masterManager.InvenCheck.HaveItems.ContainsKey(8))
                    {
                        if (masterManager.InvenCheck.HaveItems[8] >= 1)
                        {
                            masterManager.soundCheck.SFXPlay("ClickSound");
                            masterManager.InvenCheck.DecreaseItems(8, 1);

                            masterManager.FishingCheck.BaitingZoneSpone(gameObject.transform);

                        }
                    }
                }

                //--채집하기-----------------------------
                if (Input.GetKeyDown(KeyCode.E) && !isCanFishing && !isRunning && !isMoving && !isJumping && !interActFire && !interActHaze && isInterAct && !isAttack && !isFelling && !isPaused && !ishaveAttack && !masterManager.BuildCheck.nowMaking && keyCount == 0)
                {
                    if (!isSmallFell && !isbigFell && AxeDurability > 0)
                    {
                        //Debug.Log("채집가동");
                        masterManager.UiCheck.ShowDurability(1);
                        masterManager.UiCheck.StartFellUI();
                        masterManager.UiCheck.showCencel();

                        Effects = Instantiate(PopUpEffect, transform);

                        EquipHand.SetActive(true);
                        animator.SetBool("ReadyFell", true);
                        isFelling = true;
                        keyCount = 1;
                    }
                    else if (!isSmallFell && isbigFell && HammerDurability > 0)
                    {
                        //Debug.Log("채집가동");
                        masterManager.UiCheck.ShowDurability(2);
                        masterManager.UiCheck.StartFellUI();
                        masterManager.UiCheck.showCencel();

                        Effects = Instantiate(PopUpEffect, transform);

                        EquipHand1.SetActive(true);
                        animator.SetBool("ReadyFell", true);
                        isFelling = true;
                        keyCount = 1;
                    }
                    else
                    {
                        //Debug.Log("채집가동");
                        masterManager.UiCheck.StartFellUI();
                        masterManager.UiCheck.showCencel();

                        Effects = Instantiate(PopUpEffect, transform);
                        animator.SetBool("ReadyLightFell", true);
                        isFelling = true;
                        keyCount = 1;
                    }

                }
                else if (Input.GetKeyDown(KeyCode.E) && isFelling && !nowFelling && !isPaused && keyCount == 1)
                {
                    nowFelling = true;
                    keyCount = 2;
                    masterManager.ObjectCheck.StartFelling();
                }
                else if (Input.GetKeyDown(KeyCode.E) && isFelling && !isPaused  && Stamina > 0 && keyCount == 2)
                {
                    keyCount = 1;
                    if (!isSmallFell)
                    {
                        animator.SetTrigger("DoFell");
                        if (!isbigFell)
                        {
                            AxeDurability -= 2f;
                            masterManager.UiCheck.AxeUpdateDur();
                        }
                        else
                        {
                            HammerDurability -= 2f;
                            masterManager.UiCheck.HamUpdateDur();
                        }
                    }
                    else
                    {
                        animator.SetTrigger("DoLightFell");
                    }

                    StartCoroutine("FellEffect");
                    masterManager.ObjectCheck.StopFelling();

                    Stamina -= (10f - (masterManager.BuffCheck.StaminaCover * 10));
                    masterManager.UiCheck.StaminaCheck(-10f);
                    if (Stamina <= 0)
                    {
                        masterManager.UiCheck.DeBuff2On();
                        ishaveAttack = true;
                    }
                    StartCoroutine("Delay");
                }
                //---낚시하기--------------------------
                if (Input.GetKeyDown(KeyCode.E) && RodDurability > 0 && !isEnding && !isRunning && !isMoving && !isJumping && !isAttack && isCanFishing && !isPaused && !ishaveAttack && !masterManager.BuildCheck.nowMaking && keyCount == 0)
                {
                    masterManager.UiCheck.ShowDurability(3);
                    masterManager.UiCheck.DoFishing();
                    masterManager.UiCheck.showCencel();
                    masterManager.BuildCheck.CloseBuildWindow();
                    masterManager.UiCheck.OnBaitText();
                    canBaiting = true;

                    Effects = Instantiate(PopUpEffect, transform);
                    EquipHand2.SetActive(true);
                    animator.SetBool("ReadyFishing", true);
                    isFishing = true;
                    keyCount += 1;
                }
                else if (Input.GetKeyDown(KeyCode.E) && isFishing && !isPaused && keyCount == 1)
                {
                    masterManager.UiCheck.StartFishUI();
                    //masterManager.UiCheck.showCencel();
                    masterManager.UiCheck.CatchFish();

                    masterManager.FishingCheck.StartFishing();
                    animator.SetTrigger("DoFishing");
                    //isFishing = true;
                    keyCount += 1;
                }
                else if (Input.GetKeyDown(KeyCode.E) && isFishing && Stamina > 0 && !isPaused && keyCount == 2)
                {
                    masterManager.ArchiveCheck.FishingCount += 1;
                    MainArchiveManager.StaticFishingCount += 1;
                    masterManager.ArchiveCheck.CheckArchive();

                    animator.SetTrigger("CatchFish");
                    masterManager.UiCheck.EndFishUI();
                    masterManager.UiCheck.DoFishing();

                    masterManager.FishingCheck.StopFishing();
                    masterManager.FishingCheck.CheckCatch();

                    keyCount = 1;

                    Stamina -= (15f - (masterManager.BuffCheck.StaminaCover * 10));
                    masterManager.UiCheck.StaminaCheck(-10f);

                    if (Stamina <= 0)
                    {
                        masterManager.UiCheck.DeBuff2On();
                        ishaveAttack = true;
                    }
                    //masterManager.FishingCheck.StopFishing();
                }
                //---모닥불 메뉴열기----------------------------------

                if (Input.GetKeyDown(KeyCode.E) && interActFire && !interActHaze && !ishaveAttack && !isPaused && !masterManager.BuildCheck.nowMaking && InterKeyCount == 0)
                {
                    masterManager.soundCheck.SFXPlay("OpenMenu");
                    masterManager.UiCheck.StartFireUI();
                    masterManager.UiCheck.FireBaseCloseReady();
                    masterManager.BuildCheck.CloseBuildWindow();

                    isWithFire = true;

                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;


                    InterKeyCount = 1;
                }
                else if (Input.GetKeyDown(KeyCode.E) && interActFire && !isPaused && InterKeyCount == 1)
                {

                    masterManager.soundCheck.SFXPlay("OpenMenu");
                    masterManager.UiCheck.EndFireUI();
                    masterManager.UiCheck.FireBaseReady();

                    isWithFire = false;

                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;

                    InterKeyCount = 0;
                }

                //---건조대 메뉴열기----------------------------------

                if (Input.GetKeyDown(KeyCode.E) && interActHaze && !interActFire && !ishaveAttack && !isPaused && !masterManager.BuildCheck.nowMaking && InterKeyCount == 0)
                {
                    masterManager.soundCheck.SFXPlay("OpenMenu");
                    masterManager.HazeCheck.HazeUIOn();
                    masterManager.BuildCheck.CloseBuildWindow();

                    isWithHaze = true;

                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;


                    InterKeyCount = 1;
                }
                else if (Input.GetKeyDown(KeyCode.E) && interActHaze && !isPaused && InterKeyCount == 1)
                {

                    masterManager.soundCheck.SFXPlay("OpenMenu");
                    masterManager.HazeCheck.HazeUIOff();

                    isWithHaze = false;

                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;

                    InterKeyCount = 0;
                }

                //----------------------------------------------
                if (Input.GetKeyDown(KeyCode.Q) && !isPaused && isFelling)
                {
                    masterManager.UiCheck.EndFellUI();
                    masterManager.UiCheck.EndCencel();
                    masterManager.UiCheck.HideDurability();
                    masterManager.ObjectCheck.StopCoroutine("FellingPower");

                    EquipHand.SetActive(false);
                    EquipHand1.SetActive(false);

                    animator.SetBool("ReadyFell", false);
                    animator.SetBool("ReadyLightFell", false);

                    nowFelling = false;
                    isFelling = false;
                    keyCount = 0;
                    Destroy(Effects);
                }
                else if (Input.GetKeyDown(KeyCode.Q) && !isPaused && isFishing)
                {
                    masterManager.FishingCheck.StopFishing();
                    masterManager.UiCheck.HideDurability();
                    masterManager.UiCheck.EndFishUI();
                    masterManager.UiCheck.FishingReady();
                    masterManager.UiCheck.BuildReady();

                    canBaiting = false;
                    isCanFishing = true;

                    masterManager.UiCheck.EndCencel();

                    EquipHand2.SetActive(false);
                    animator.SetBool("ReadyFishing", false);
                    isFishing = false;
                    keyCount = 0;
                    Destroy(Effects);
                }

                if (Input.GetKey(KeyCode.LeftShift) && !isPaused && Stamina > 0 && !isFelling)
                {
                    isRunning = true;
                    Stamina -= 0.5f;
                    MainArchiveManager.RunningTime += 0.1f;
                    masterManager.UiCheck.StaminaCheck(-0.5f);
                }
                else
                {
                    isRunning = false;
                }

                if (Input.GetButtonDown("Jump") && Stamina > 0 && !isFelling && !isFishing && !isAttack && !isWithFire && !isWithHaze && !isPaused)
                    if (!firstJump || masterManager.WaterCheck.isWater)
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;

                        isJumping = true;
                        Stamina -= 1f;
                        masterManager.UiCheck.StaminaCheck(-1f);
                    }

                if (!isFelling && !isFishing && !isBuilding && !isWithFire && !isWithHaze)
                {
                    if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                    {
                        isMoving = true;
                    }
                    else
                    {
                        isMoving = false;
                    }
                }

                if (!isJumping && !isPaused && !isMoving && !isRunning && !isFishing && !isFelling && Stamina < 100)
                {
                    Stamina = Stamina + 0.25f + masterManager.BuffCheck.StaminaCover;
                    masterManager.UiCheck.StaminaCheck(0.25f + masterManager.BuffCheck.StaminaCover);
                }
                else if (Stamina >= 100)
                {
                    Stamina = 100f;
                }
                else if (Stamina < 0)
                {
                    Stamina = 0f;
                }

                if (ishaveAttack && Stamina >= 100)
                {
                    masterManager.UiCheck.DeBuff2Off();
                    ishaveAttack = false;
                }

                if (!isWithFire && !isPaused && HealthPoint >= 0)
                {
                    HealthPoint -= (0.005f - masterManager.BuffCheck.HpCover - AddHealth);
                    masterManager.UiCheck.HealthCheck();
                }

                if (!isJumping && !isMoving && !isRunning && !isPaused && !ishaveAttack && HealthPoint < 100)
                {
                    if (isWithFire)
                    {
                        HealthPoint += (0.005f + masterManager.BuffCheck.HpCover + AddHealth);
                    }
                    masterManager.UiCheck.HealthCheck();
                }
                else if (HealthPoint >= 100)
                {
                    HealthPoint = 100f;
                    masterManager.UiCheck.HealthCheck();
                }
                else if (HealthPoint <= 0)
                {
                    HealthPoint = 0f;
                    masterManager.UiCheck.HealthCheck();
                }

                if (HealthPoint <= 0)
                {
                    if (masterManager.InvenCheck.HaveItems.ContainsKey(199))
                    {
                        if (masterManager.InvenCheck.HaveItems[199] >= 1)
                        {
                            masterManager.InvenCheck.DecreaseItems(199, 1);
                            HealthPoint += 50f;
                            masterManager.UiCheck.HealthCheck();
                        }
                        else
                        {
                            isGameover = true;
                            animator.SetTrigger("isGameOver");
                            masterManager.EndCheck.StartGameOver();
                        }
                    }
                    else
                    {
                        isGameover = true;
                        animator.SetTrigger("isGameOver");
                        masterManager.EndCheck.StartGameOver();
                    }
                }

                if(BreathPoint <= 0)
                {
                    isGameover = true;
                    animator.SetTrigger("isGameOver");
                    masterManager.EndCheck.StartGameOver();
                }

                //----------낚시 취소-------------------
                if (isFishing && RodDurability <= 0)
                {
                    masterManager.FishingCheck.StopFishing();
                    masterManager.UiCheck.EndFishUI();
                    masterManager.UiCheck.FishingReady();
                    masterManager.UiCheck.BuildReady();
                    masterManager.UiCheck.HideDurability();

                    canBaiting = false;
                    isCanFishing = true;

                    masterManager.UiCheck.EndCencel();

                    EquipHand2.SetActive(false);
                    animator.SetBool("ReadyFishing", false);
                    isFishing = false;
                    keyCount = 0;
                    Destroy(Effects);
                }
                //--------채집 취소---------------------
                if (isFelling && AxeDurability <= 0)
                {
                    masterManager.UiCheck.EndFellUI();
                    masterManager.UiCheck.EndCencel();
                    masterManager.UiCheck.HideDurability();

                    EquipHand.SetActive(false);
                    EquipHand1.SetActive(false);
                    animator.SetBool("ReadyFell", false);

                    animator.SetBool("ReadyLightFell", false);
                    isFelling = false;
                    keyCount = 0;
                    Destroy(Effects);
                }
                //--------큰채집 취소---------------------
                if (isFelling && isbigFell && HammerDurability <= 0)
                {
                    masterManager.UiCheck.EndFellUI();
                    masterManager.UiCheck.EndCencel();
                    masterManager.UiCheck.HideDurability();

                    EquipHand.SetActive(false);
                    EquipHand1.SetActive(false);
                    animator.SetBool("ReadyFell", false);
                    animator.SetBool("ReadyLightFell", false);
                    isFelling = false;
                    keyCount = 0;
                    Destroy(Effects);
                }



                if (ishaveAttack)
                {

                    //----------낚시 취소-------------------
                    if (isFishing)
                    {
                        masterManager.FishingCheck.StopFishing();
                        masterManager.UiCheck.EndFishUI();
                        masterManager.UiCheck.FishingReady();
                        masterManager.UiCheck.BuildReady();
                        masterManager.UiCheck.HideDurability();

                        canBaiting = false;
                        isCanFishing = true;

                        masterManager.UiCheck.EndCencel();

                        EquipHand2.SetActive(false);
                        animator.SetBool("ReadyFishing", false);
                        isFishing = false;
                        keyCount = 0;
                        Destroy(Effects);
                    }
                    //--------채집 취소---------------------
                    if (isFelling)
                    {
                        masterManager.UiCheck.EndFellUI();
                        masterManager.UiCheck.EndCencel();
                        masterManager.UiCheck.HideDurability();

                        EquipHand.SetActive(false);
                        EquipHand1.SetActive(false);
                        animator.SetBool("ReadyFell", false);

                        animator.SetBool("ReadyLightFell", false);
                        isFelling = false;
                        keyCount = 0;
                        Destroy(Effects);
                    }
                    //-------모닥불 취소---------------------
                    if (isWithFire)
                    {
                        masterManager.UiCheck.EndFireUI();
                        masterManager.UiCheck.FireBaseReady();

                        isWithFire = false;
                        InterKeyCount = 0;
                    }

                    //-------건조대 취소---------------------
                    if (isWithHaze)
                    {
                        masterManager.HazeCheck.HazeUIOff();
                        masterManager.UiCheck.HazeReady();

                        isWithHaze = false;
                        InterKeyCount = 0;
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Boat")
        {
            masterManager.UiCheck.EndingReady();
            isEnding = true;
            //isCanFishing = false;
        }

        if (collision.gameObject.tag == "Fire")
        {
            masterManager.UiCheck.SetTemperature(0.8f);
        }

        if (collision.gameObject.tag == "Baited")
        {
            masterManager.FishingCheck.PlusPercentage = 20;
        }

        if (collision.gameObject.tag == "Ground")
        {
            firstJump = false;
        }

        if (collision.gameObject.tag == "Fishing"&& keyCount == 0)
        {
            masterManager.UiCheck.FishingReady();
            isCanFishing = true;
            inWater = true;
        }

        if (collision.gameObject.tag == "Water")
        {
            masterManager.UiCheck.SetTemperature(0.4f);
            GetWater();
            if (Temper <= 0.5f)
            {
                if (!getCold)
                {
                    getCold = true;
                    StartCoroutine("GetCold");
                }
                coldstack += 1;
            }
        }

        if (collision.gameObject.tag == "WaterBreath")
        {
            if (!isDive && !masterManager.BuffCheck.inWaterBreath)
            {
                StopCoroutine("BreathIn");
                StartCoroutine("BreathOut");
                isDive = true;
            }
        }

        if (collision.gameObject.tag == "Tree")
        {
            isInterAct = true;
            masterManager.UiCheck.InterActObject(collision);
        }

        if (collision.gameObject.tag == "Rock")
        {
            isInterAct = true;
            isbigFell = true;
            masterManager.UiCheck.InterActObject(collision);
        }

        if (collision.gameObject.tag == "Plants")
        {
            isInterAct = true;
            isSmallFell = true;
            masterManager.UiCheck.SmallInterActObject(collision);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Boat")
        {
            if (isCanFishing && inWater)
            {
                keyCount = 0;
                masterManager.UiCheck.FishingReady();
                isEnding = false;
            }
            else
            {
                masterManager.UiCheck.WaitInteract();
                isCanFishing = false;
                isEnding = false;
            }
        }

        if (collision.gameObject.tag == "Fire")
        {
            masterManager.UiCheck.SetTemperature(0.7f);
        }

        if (collision.gameObject.tag == "Baited")
        {
            masterManager.FishingCheck.PlusPercentage = 0;
        }

        if (collision.gameObject.tag == "Fishing")
        {
            masterManager.UiCheck.WaitInteract();
            isCanFishing = false;
        }

        if (collision.gameObject.tag == "Water")
        {
            masterManager.UiCheck.SetTemperature(0.7f);
            GetOutWater();
            inWater = false;
        }
        if (collision.gameObject.tag == "WaterBreath")
        {
            if (isDive)
            {
                StopCoroutine("BreathOut");
                StartCoroutine("BreathIn");
                isDive = false;
            }
        }
        if (collision.gameObject.tag == "Tree")
        {
            isInterAct = false;
            if (isFelling)
            {
                masterManager.UiCheck.EndFellUI();
                masterManager.UiCheck.EndCencel();
                masterManager.UiCheck.HideDurability();

                EquipHand.SetActive(false);
                EquipHand1.SetActive(false);
                animator.SetBool("ReadyFell", false);
                animator.SetBool("ReadyLightFell", false);
                isFelling = false;
                keyCount = 0;
                Destroy(Effects);
            }
            masterManager.UiCheck.OutInterAct();
        }

        if (collision.gameObject.tag == "Rock")
        {
            isInterAct = false;
            if (isFelling)
            {
                masterManager.UiCheck.EndFellUI();
                masterManager.UiCheck.EndCencel();
                masterManager.UiCheck.HideDurability();

                EquipHand.SetActive(false);
                EquipHand1.SetActive(false);

                animator.SetBool("ReadyFell", false);
                animator.SetBool("ReadyLightFell", false);
                isFelling = false;
                keyCount = 0;
                Destroy(Effects);
            }
            masterManager.UiCheck.OutInterAct();
            isbigFell = false;
        }


        if (collision.gameObject.tag == "Plants")
        {
            isInterAct = false;
            if (isFelling)
            {
                masterManager.UiCheck.EndFellUI();
                masterManager.UiCheck.EndCencel();

                animator.SetBool("ReadyFell", false);
                animator.SetBool("ReadyLightFell", false);
                isFelling = false;
               // isSmallFell = false;
                keyCount = 0;
                Destroy(Effects);
            }
            masterManager.UiCheck.OutInterAct();
            isSmallFell = false;
        }
    }

    public void GetWater()
    {
        isWater = true;
        transform.GetComponent<Rigidbody>().drag = waterGrav;
    }

    public void GetOutWater()
    {
        if (isWater)
        {
            isWater = false;
            transform.GetComponent<Rigidbody>().drag = originGrav;
        }
    }

    IEnumerator BreathOut()
    {
        yield return new WaitForSeconds(2f);
        if (BreathPoint > 0)
        {
            BreathPoint -= 1;
            masterManager.UiCheck.BreathCheck();
            StartCoroutine("BreathOut");
        }
        else
        {
            StopCoroutine("BreathOut");
        }
    }

    IEnumerator BreathIn()
    {
        yield return new WaitForSeconds(0.7f);
        if (BreathPoint < 8)
        {
            BreathPoint += 1;
            masterManager.UiCheck.BreathCheck();
            StartCoroutine("BreathIn");
        }
        else
        {
            StopCoroutine("BreathIn");
        }
    }

    public void EndFell()
    {
        //masterManager.ObjectCheck.StopFelling();
        //masterManager.UiCheck.EndFellUI();
        masterManager.UiCheck.EndCencel();
        masterManager.UiCheck.HideDurability();

        EquipHand.SetActive(false);
        EquipHand1.SetActive(false);

        animator.SetBool("ReadyFell", false);
        animator.SetBool("ReadyLightFell", false);
        isFelling = false;
        keyCount = 0;
        Destroy(Effects);
    }

    IEnumerator DoAttak()
    {
        EquipHand3.SetActive(true);
        AttackRange.enabled = true;
        animator.SetTrigger("DoAttack");
        //isMoving = false;
        //speed = 1f;
        yield return new WaitForSeconds(0.5f);
        masterManager.soundCheck.SFXPlay("FailSound");
        yield return new WaitForSeconds(0.7f);
        //speed = 3.5f;
        isAttack = false;
        Destroy(Effects);
        EquipHand3.SetActive(false);
        AttackRange.enabled = false;
    }

    IEnumerator FellEffect()
    {
        yield return new WaitForSeconds(1.2f);
        masterManager.soundCheck.SFXPlay("HitObjectSound");
        Hits = Instantiate(HitEffect, TargetPos);
        
        yield return new WaitForSeconds(0.5f);
        Destroy(Hits);
    }

    IEnumerator RecoverCold()
    {
        yield return new WaitForSecondsRealtime(10f);
        if (coldstack > 0)
        {
            coldstack -= 1;
            StartCoroutine("RecoverCold");
        }
        else
        {
            recoverCold = false;
            StopCoroutine("RecoverCold");
        }
    }

    IEnumerator GetCold()
    {
        yield return new WaitForSecondsRealtime(15f);
        if (coldstack < 7)
        {
            coldstack += 1;
            StartCoroutine("GetCold");
        }
        else 
        {
            StopCoroutine("GetCold");
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        nowFelling = false;
    }

    public void EndPause()
    {
        PauseSet.SetActive(false);
        masterManager.soundCheck.SFXPlay("PauseSound");
        if (Cursor.visible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        isPaused = false;
        Time.timeScale = 1;
    }
}
