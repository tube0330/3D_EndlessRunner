using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemyCtrl : MonoBehaviour
{
    Transform tr;
    Rigidbody rb;
    Animator ani;
    CapsuleCollider col;
    [SerializeField] Text score_txt;

    Vector3 initColCenter = new Vector3(0f, 1.9f, 0f);
    float initColHeight = 3.8f;
    float moveValue = 0.8f;
    float jumpForce = 10f;
    float deciamlScore = 0f;
    public int score = 0;
    public bool isGround = true;
    public bool isSlide = false;
    public bool isDie = false;
    public bool isPlatform = false;
    Vector3 targetPos;  // 목표 위치
    float damping = 5f;  // 부드럽게 이동할 속도

    void Start()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        score_txt = GameObject.Find("Canvas").transform.GetChild(0).GetChild(0).GetComponent<Text>();

        targetPos = tr.position;  // 초기 위치를 설정
    }

    void Update()
    {
        // 좌우 이동 처리
        MoveHorizontal();

        if (!isDie)
            IncreaseScore();

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGround && !isSlide)
            StartCoroutine(Jump());

        if (Input.GetKeyDown(KeyCode.DownArrow) && !isSlide && isGround)
            StartCoroutine(Slide());
    }


    void MoveHorizontal()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && targetPos.x > -0.8f)
            targetPos.x -= moveValue;

        if (Input.GetKeyDown(KeyCode.RightArrow) && targetPos.x < 0.8f)
            targetPos.x += moveValue;

        // x값을 제한 범위 내로 고정
        targetPos.x = Mathf.Clamp(targetPos.x, -0.8f, 0.8f);
        targetPos.z = 0f;

        // 현재 위치에서 목표 위치로 부드럽게 이동
        tr.position = Vector3.Lerp(tr.position, targetPos, Time.deltaTime * damping);
    }

    IEnumerator Jump()
    {
        ani.SetTrigger("Jump");

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        isGround = false;
        col.center = new Vector3(0f, 2.3f, 0f);
        col.height = initColHeight / 2f;

        yield return new WaitForSeconds(0.8f);

        col.center = initColCenter;
        col.height = initColHeight;
    }

    IEnumerator Slide()
    {
        ani.SetTrigger("Slide");
        isSlide = true;
        col.center = new Vector3(0f, 0.7f, 0f);
        col.height = 1.4f;

        yield return new WaitForSeconds(1f);

        col.center = initColCenter;
        col.height = initColHeight;

        isSlide = false;
    }

    void IncreaseScore()
    {
        deciamlScore += Time.deltaTime * 1; // 초당 1점씩 증가
        if (deciamlScore >= 1f)
        {
            score += (int)deciamlScore; // 정수 부분만큼 점수(score)에 더함
            deciamlScore -= Mathf.Floor(deciamlScore); // 정수로 더해진 값 만큼 deciamlScore에서 뺌
        }
        score_txt.text = $"{score}M";
    }

    void OnCollisionEnter(Collision col)
    {
        // 플랫폼과 충돌하면 isGround를 true로 설정
        if (col.gameObject.CompareTag("PLATFORM"))
            isGround = true;
    }

    // 이 코드로 인해 isGound가 깜빡여 정확한 점프가 불가능해 코드 삭제.
    // Jump()에서 isGound false 처리
    // void OnCollisionExit(Collision col)
    // {
    //     // 플랫폼에서 나가면 isGround를 false로 설정
    //     if (col.gameObject.CompareTag("PLATFORM"))
    //     {
    //         isGround = false;
    //     }
    // }
}
