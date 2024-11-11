using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyCtrl : MonoBehaviour
{
    RemyMove _move;

    Transform tr;
    internal Rigidbody rb;

    Vector3 initPos;

    float DieTimer = 0f;
    float timeToDie = 2f;
    public bool isGround = true;
    public bool isSlide = false;
    public bool isPlatform = true;

    void Start()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
        _move = GetComponent<RemyMove>();
        initPos = new Vector3(0f, tr.position.y, tr.position.z);
    }

    void Update()
    {
        // 플랫폼 또는 장애물 체크
        CheckPlatform();

        // PlayerDie 처리
        DieCheck();
    }

    RaycastHit GetPlatform(Vector3 dir, float distance)
    {
        Physics.Raycast(tr.position + Vector3.up * 0.3f, dir, out RaycastHit hit, distance);
        return hit;
    }

    void CheckPlatform()
    {
        RaycastHit hit = GetPlatform(Vector3.down, 50f);
        if (hit.collider == null)
        {
            isPlatform = false;
            isGround = false;
        }
        else
        {
            isPlatform = true;

            if (hit.distance < 0.31f)
                isGround = true;
            else
                isGround = false;
        }
    }

    void DieCheck()
    {
        if (!isPlatform && !GameManager.instance.isInvincible)
        {
            DieTimer += Time.deltaTime;
            if (DieTimer > timeToDie)
            {
                GameManager.instance.isDie = true;
                DieTimer = 0f;
            }
        }
        else DieTimer = 0f;
    }

    public void ResetRemy()
    {
        _move.curPos = Vector3.zero;
        tr.position = initPos;
        rb.velocity = Vector3.zero;
        isGround = true;
        isSlide = false;
        GameManager.instance.isDie = false;
        isPlatform = true;
    }
}
