﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    // 초기화되지 않은 변수들은
    // 해당 스테이지에서의 변수로 직접 설정해서 사용 (prefab X)

    public bool isThrow;    // stageManager에서 조건에 따라 T/F 바꿔줌 

    public GameObject player;
    public Transform Projectile;    // stone의 tr

    public Transform pTr;    // player(target)의 tr
    private Transform mTr;  // stone의 Tr    
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    private int cnt = 0;  // player가 StoneZone 안에 있을 때 공이 무한정으로 던져지는 것을 방지하는 변수
    public PlayerCtrl playerCtrl;  // player가 던진 돌을 피하는 것 방지
    public GameObject findSign; // Kids 위의 느낌표

    void Start()
    {
        playerCtrl = player.GetComponent<PlayerCtrl>();
        pTr = player.transform;
        mTr = transform;
        isThrow = false;
        findSign.SetActive(false);
    }

    void Update()
    {
        if (isThrow && cnt == 0)  // cnt를 이용해 공이 던져진 적 없을 때 한 번만 던지게 됩니다
        {
            findSign.SetActive(true); // Kids 자식 오브젝트(=느낌표findMark) 비활성화
            playerCtrl.enabled = false;
            StartCoroutine(SimulateProjectile());
            cnt++;
            
        }
    }

    IEnumerator SimulateProjectile()
    {
        yield return new WaitForSeconds(0.5f);  // 투사체 던지기 전에 기다림

        mTr.transform.position += new Vector3(0, 0, 20);    // 숨겨놨다가 앞으로 당겨옴, 위치 정확히 조정

        float target_Distance = Vector3.Distance(Projectile.position, pTr.position); // taget(player)와의 거리 계산
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);   // 각도(45도)에서 물체를 던지는데 필요한 속도 계산

        // 속도의 X, Y 추출
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
        float flightDuration = target_Distance / Vx;    // 날아가는 시간 계산

        // taget까지 발사체 회전
        Projectile.rotation = Quaternion.LookRotation(pTr.position - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)    // 포물선 운동
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
        mTr.transform.position -= new Vector3(0, 0, 20);    // 뒤로 사라지게 함
    }

}

