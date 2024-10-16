using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffObstacle : MonoBehaviour
{
    Transform tr;

    void Start()
    {
        tr = transform;
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(tr.position + Vector3.up * 0.3f, Vector3.down, out hit, 2f))
        {            
            Debug.Log(hit.collider.name);

            if (hit.collider.CompareTag("PLATFORM") || hit.collider.CompareTag("Player"))
                gameObject.SetActive(true);

            //flag에서 Player가 슬라이딩 할 때 false되지 않도록 코드 추가
            else if (hit.collider.CompareTag("Player"))
                gameObject.SetActive(true);
        }

        else
            gameObject.SetActive(false);
    }
}
