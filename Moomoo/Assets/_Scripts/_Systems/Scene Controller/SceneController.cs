using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image progressBar;

    [Header("Parameters")]
    [SerializeField] private float progressBarSpeed = 1f;
    [SerializeField] private float progressBarFadeDuration = 1f;

    private float target;
    AsyncOperation scene;

    private GameManager gameManager;


    private void Awake()
    {
        gameManager = GameManager.instance;
        gameManager.onGoToScene += LoadScene;
    }

    private void Start()
    {
        scene = null;
    }

    private void Update()
    {
        // progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, progressBarSpeed * Time.deltaTime);
        progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, target, progressBarSpeed * Time.deltaTime);
    }


    public async void LoadScene(string sceneName)
    {
        target = 0f;
        progressBar.fillAmount = 0f;
        progressBar.color = new Vector4(progressBar.color.r, progressBar.color.g, progressBar.color.b, 1f);

        scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        do
        {
            await Task.Delay(100);

            target = scene.progress;

        } while (scene.progress < 0.9f);

        target = 1f;

        StartCoroutine(AllowSceneActivation());
    }

    IEnumerator AllowSceneActivation()
    {
        yield return new WaitForSeconds(progressBarFadeDuration*2);
        progressBar.GetComponent<Image>().DOFade(0f, progressBarFadeDuration).OnComplete(() => scene.allowSceneActivation = true);
    }

    private void OnDestroy()
    {
        gameManager.onGoToScene -= LoadScene;
    }
}
