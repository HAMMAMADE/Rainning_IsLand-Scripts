using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewAngle : MonoBehaviour
{
    public float viewAngle;
    public float viewDistance;
    public LayerMask targetMask;

    private WolfAI WolfEye;

    private void Start()
    {
        WolfEye = GetComponent<WolfAI>();
        StartCoroutine("Viewing");
    }

    IEnumerator Viewing()
    {
        yield return new WaitForSeconds(0.05f);
        View();
        StartCoroutine("Viewing");
    }


    private Vector3 BoundaryAngle(float angle)
    {
        angle += transform.eulerAngles.y;

        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad),0f,Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    private void View()
    {
        Vector3 _leftBoundary = BoundaryAngle(-viewAngle * 0.5f);
        Vector3 _rightBoundary = BoundaryAngle(viewAngle * 0.5f);

        Debug.DrawRay(transform.position + transform.up, _leftBoundary, Color.red);
        Debug.DrawRay(transform.position + transform.up, _rightBoundary, Color.red);

        Collider[] target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        for (int i = 0; i < target.Length; i++)
        {
            Transform targetTrans = target[i].transform;
            if (targetTrans.tag == "Player")
            {
                Vector3 direaction = (targetTrans.position - transform.position).normalized;
                float angle = Vector3.Angle(direaction, transform.forward);//플레이어와 자기 자신의 방향

                if (angle < viewAngle * 0.5f) //시야 범위 내에 들어왔을 때
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + transform.up, direaction, out hit, viewDistance))
                    {
                        if (hit.transform.tag == "Player")
                        {
                            Debug.DrawRay(transform.position + transform.up, direaction, Color.blue);
                            if (!WolfEye.meetPlayer)
                            {
                                WolfEye.ChaseAct(hit.transform.position);
                                //WolfEye.RandomAction();
                            }
                            else
                            {
                                WolfEye.LookPlayer(hit.transform.position);
                            }
                        }
                    }
                }
            }
        }
    }


}
 