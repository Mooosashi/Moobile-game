using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.UI;

public class TransitionAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject mask;
    [SerializeField] private GameObject image;
    [SerializeField] private Image progressBar;

    [Header("Parameters")]
    [SerializeField] private float initialDelay = 1f;
    [SerializeField] private float fullScale = 5f;
    [SerializeField] private float halfScale = 2f;
    [SerializeField] private float firstAnimDuration = 1f;
    [SerializeField] private float secondAnimDuration = 0.5f;
    [SerializeField] private float timeBetweenAnims = 0.2f;

    private GameManager gameManager;


    private void Awake()
    {
        gameManager = GameManager.instance;
        gameManager.onTransitionOut += Out;
        //gameManager.onTransitionState += SetMaskPosition;
    }

    //private void Start()
    //{
    //    Vector3 maskPosition = gameManager.mainCamera.WorldToScreenPoint(gameManager.playerCharacter.transform.position);
    //    mask.transform.position = maskPosition;
    //}

    private void OnDisable()
    {
        mask.SetActive(false);
        image.SetActive(false);
    }

    private void Start()
    {
        mask.SetActive(true);
        image.SetActive(true);

        StartCoroutine(In());
    }

    private void OnLevelWasLoaded()
    {
        mask.SetActive(true);
        image.SetActive(true);

        StartCoroutine(In());
    }

    private void OnDestroy()
    {
        gameManager.onTransitionOut -= Out;
        //gameManager.onTransitionState -= SetMaskPosition;
    }


    //private void SetMaskPosition()
    //{
    //    Vector3 maskPosition = gameManager.mainCamera.WorldToScreenPoint(gameManager.playerCharacter.transform.position);
    //    mask.transform.position = maskPosition;
    //}


    IEnumerator In()
    {
        yield return new WaitForSeconds(initialDelay);
        StartCoroutine(NoneToHalf());
    }
    IEnumerator NoneToHalf()
    {
        yield return new WaitForSeconds(timeBetweenAnims);
        mask.transform.localScale = new Vector2(0f, 0f);
        mask.transform.DOScale(new Vector2(halfScale, halfScale), secondAnimDuration).SetEase(Ease.OutCubic).OnComplete(() => StartCoroutine(HalfToFull()));
    }
    IEnumerator HalfToFull()
    {
        yield return new WaitForSeconds(timeBetweenAnims);
        mask.transform.DOScale(fullScale, firstAnimDuration).OnComplete(() =>
        {
            gameManager.SetGameState(GameState.freeMovement);
        });
    }

    public void Out(string sceneName)
    {
        //Vector3 maskPosition = gameManager.mainCamera.WorldToScreenPoint(gameManager.playerCharacter.transform.position);
        //mask.transform.position = maskPosition;

        mask.transform.localScale = new Vector2(fullScale, fullScale);
        mask.transform.DOScale(new Vector2(halfScale, halfScale), firstAnimDuration).SetEase(Ease.OutCubic).OnComplete(() =>
            StartCoroutine(HalfToNone(sceneName)));
    }

    IEnumerator HalfToNone(string sceneName)
    {
        yield return new WaitForSeconds(timeBetweenAnims);
        mask.transform.DOScale(0f, secondAnimDuration).OnComplete(() => gameManager.GoToScene(sceneName));
    }
}
