﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneEvent : MonoBehaviour
{
    /* stageManager에서 조건 추가적으로 설정해서 사용하시면 됩니다

    public StoneEvent stoneEvent;    // player가 StoneZone에 있는지 전달받기 위해 사용
    public Stone stone;  // 조건 만족 후 투사체 던지기 위해 변수 전달
    public GameObject Kids;
    public GameObject Player;
    public SpriteRenderer KidsRenderer; // Kids가 쳐다보는 방향을 알려줌

    void Start()
    {
        KidsRenderer = Kids.GetComponent<SpriteRenderer>(); // 초기화
    }


    void Update()
    {
        // Kids
        if (stoneEvent.isStoneEvent == true)  // player가 StoneZone에 있을 때
        {
            if (KidsRenderer.flipX == true)    // kids가 Player를 쳐다볼 때 (왼쪽)
            {
                // * 조건 if로 추가

                stone.isThrow = true; // Stone 스크립트의 변수 수정
            }
        }

     */


    public bool isStoneEvent;   // player가 StoneZone에 들어갈 경우를 판단

    void Start()
    {
        isStoneEvent = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isStoneEvent = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isStoneEvent = false;
    }
}
