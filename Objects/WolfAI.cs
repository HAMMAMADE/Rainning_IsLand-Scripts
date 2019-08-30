using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : MonoBehaviour
{
    public ObjectParmeter WolfParm;
    //public int WolfHP;

    public float walkSpeed;
    public float RunSpeed;

    public Vector3 direaction;

    public float walkTime;
    public float RunTime;
    public float waitTime;
    float currentTime;

    bool isAction;
    bool isWalking;
    bool isRunning;
    public bool isChase;
    bool inWater;
    bool isDeath;
    bool isAttacking;
    bool outAttack;

    bool haveDamage;
    bool GiveDamage;

    public bool DodgeWater;
    public bool meetPlayer;

    //필요한 컴포넌트
    public Animator Wolfanimation;
    public Rigidbody rigid;

    public GameObject HitEffect;
    public Transform HitEffectPos;

    void Start()
    {
        currentTime = waitTime;
        isAction = true;
        isDeath = false;
    }

    void Update()
    {
        ElapseTIme();

        if (isWalking)
        {
            rigid.MoveRotation(Quaternion.Euler(direaction));
            rigid.MovePosition(transform.position + (transform.forward * walkSpeed * Time.deltaTime));
        }
        else if (isRunning)
        {
            rigid.MoveRotation(Quaternion.Euler(direaction));
            rigid.MovePosition(transform.position + (transform.forward * RunSpeed * Time.deltaTime));
        }
        else if (isChase && !meetPlayer)
        {
            rigid.MoveRotation(Quaternion.Euler(direaction));
            rigid.MovePosition(transform.position + (transform.forward * RunSpeed * Time.deltaTime));
        }


        if (WolfParm.ObjectHealth <= 0 && !inWater && !isDeath)
        {
            isWalking = false;
            isRunning = false;
            isChase = false;
            isAction = false;
            meetPlayer = false;
            isDeath = true;
            WolfParm.DropItems();
            StopCoroutine("StartAttack");
            WolfParm.masterManager.PlayerCheck.ishaveAttack = false;

            WolfParm.ObjectAnim.SetTrigger("ObjectDestroy");
            WolfParm.ObjectDestroy();
            MainArchiveManager.StaticWolfCount += 1;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "Attack")
        {
            isWalking = false;
            isRunning = false;
            isAction = false;

            //StartCoroutine("HaveDamage");
            if (!haveDamage)
            {
                haveDamage = true;
                StartCoroutine("HaveDamage");
               // Debug.Log("데미지");
                rigid.MovePosition(transform.position - (transform.forward * 0.25f));
                Instantiate(HitEffect, HitEffectPos);
                WolfParm.ObjectHealth -= 20;
                WolfParm.masterManager.soundCheck.SFXPlay("HitSound");
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "OnWater")
        {
            OutAct();
            inWater = true;
            StartCoroutine("BreathDemage");
        }

      /*  if (collision.tag == "Attack")
        {
            isWalking = false;
            isRunning = false;
            isAction = false;
            haveDamage = true;
            StartCoroutine("HaveDamage");
        }
        */

        if (collision.tag == "Player")
        {
            if (!isDeath)
            {
                meetPlayer = true;
                DoAttack();
                if (outAttack)
                {
                    if (!GiveDamage)
                    {
                        GiveDamage = true;
                        StartCoroutine("GiveDemage");
                    }
                }
            }
        }
    }

    IEnumerator GiveDemage()
    {
        if (GiveDamage)
        {
            WolfParm.masterManager.PlayerCheck.animator.SetTrigger("Hit");
            WolfParm.masterManager.soundCheck.SFXPlay("HitSound");
            WolfParm.masterManager.soundCheck.SFXPlay("ShootSound");
            WolfParm.masterManager.PlayerCheck.HealthPoint -= 5f;
            WolfParm.masterManager.PlayerCheck.ishaveAttack = true;
            WolfParm.masterManager.UiCheck.HealthCheck();
            yield return new WaitForSeconds(1.5f);
            GiveDamage = false;
        }
    }

    IEnumerator HaveDamage()
    {
        yield return new WaitForSeconds(1.5f);
        haveDamage = false;
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "OnWater")
        {
            OutAct();
            inWater = false;
            DodgeWater = false;
            StopCoroutine("BreathDemage");
        }

        if (collision.tag == "Player")
        {
            StartCoroutine("DelayMov");
            //haveDamage = false;
            GiveDamage = false;
            currentTime = 0;
            isAction = true;
            WolfParm.masterManager.PlayerCheck.ishaveAttack = false;
        }
    }

    IEnumerator DelayMov()
    {
        yield return new WaitForSeconds(1f);
        meetPlayer = false;
    }

    IEnumerator BreathDemage()
    {
        yield return new WaitForSeconds(25f);

        WolfParm.ObjectAnim.SetTrigger("ObjectDestroy");
        isWalking = false;
        isRunning = false;
        isAction = false;
        meetPlayer = false;
        WolfParm.ObjectDestroy();
    }

    public void ElapseTIme()
    {
        if (isAction && !isChase)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                ReadyToMove();
            }
        }
    }

    public void ReadyToMove()
    {
        DodgeWater = false;
        isRunning = false;
        isWalking = false;
        isAction = false;

        Wolfanimation.SetBool("DoMove", false);
        Wolfanimation.SetBool("LookPlayer", false);
        RandomAction();
    }

    public void RandomAction()
    {

        int randomAct = Random.Range(0, 3);

        switch (randomAct) {
            case 0:
                WaitAct();
                break;
            case 1:
                WalkAct();
                break;
            default:
                WalkAct();
                break;
        }
    }

    public void WaitAct()
    {
        direaction.Set(0f, Random.Range(0f, 360f), 0f);
        currentTime = waitTime;
        isAction = true;
    }

    public void WalkAct()
    {
        direaction.Set(0f, Random.Range(0f, 360f), 0f);
        isRunning = false;
        isWalking = true;
        Wolfanimation.SetBool("DoMove", true);
        currentTime = walkTime;
        isAction = true;
    }

    public void ChaseAct(Vector3 target)
    {
        if (!DodgeWater && !isDeath)
        {
            direaction = Quaternion.LookRotation((transform.position - target) * -1).eulerAngles;
            isWalking = false;
            isRunning = false;
            isChase = true;
            Wolfanimation.SetBool("LookPlayer", true);
        }
    }

    public void OutAct()
    {
        direaction = Quaternion.LookRotation(transform.position * -1).eulerAngles;
        isRunning = false;
        isWalking = true;
        isChase = false;
        DodgeWater = true;
        Wolfanimation.SetBool("DoMove", true);
        currentTime = walkTime;
        isAction = true;
    }

    public void LookPlayer(Vector3 target)
    {
        direaction = Quaternion.LookRotation((transform.position - target) * -1).eulerAngles;
        isWalking = false;
        isRunning = false;
        //isChase = true; 
        Wolfanimation.SetBool("DoMove", false);
        Wolfanimation.SetBool("LookPlayer", false);
    }

    public void DoAttack()
    {
        //Debug.Log("공격명령");
        if (!outAttack)
        {
            StartCoroutine("StartAttack");
        }
    }

    IEnumerator StartAttack()
    {
        if (!isDeath)
        {
            Wolfanimation.SetTrigger("PlayerAttack");
            outAttack = true;
            yield return new WaitForSeconds(1f);
            outAttack = false;
            yield return new WaitForSeconds(1.5f);
            StartCoroutine("StartAttack");
        }
    }

}
