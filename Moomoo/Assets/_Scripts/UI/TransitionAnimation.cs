using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransitionAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject mask;

    [Header("Parameters")]
    [SerializeField] private float fullScale = 5f;
    [SerializeField] private float halfScale = 2f;
    [SerializeField] private float halfToFullDuration = 1f;
    [SerializeField] private float noneToHalfDuration = 0.5f;
    [SerializeField] private float timeBetweenAnims = 0.2f;


    private void Start()
    {
        GameManager gameManager = GameManager.instance;

        Vector3 maskPosition = mask.transform.position;
        maskPosition = gameManager.mainCamera.WorldToScreenPoint(gameManager.playerCharacter.transform.position);
        mask.transform.position = maskPosition;
    }

    public void In()
    {
        mask.transform.localScale = new Vector2(0f, 0f);
        mask.transform.DOScale(new Vector3(halfScale, halfScale), noneToHalfDuration).SetEase(Ease.OutCubic).OnComplete(() =>
              StartCoroutine(HalfToFull()));
    }
    IEnumerator HalfToFull()
    {
        yield return new WaitForSeconds(timeBetweenAnims);
        mask.transform.DOScale(fullScale, halfToFullDuration);
    }


    public void Out()
    {
        mask.transform.localScale = new Vector2(fullScale, fullScale);
        // Falta el primer movimiento
    }
    IEnumerator HalfToNone()
    {
        yield return new WaitForSeconds(timeBetweenAnims);
        mask.transform.DOScale(0f, halfToFullDuration);
    }
}
