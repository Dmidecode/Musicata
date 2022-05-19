using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoaderScene : MonoBehaviour
{
  public GameObject LoadingCanvas;
  public Image LoadingImage;
  public GameObject MainCanvas;

  private float _target;

  private static LoaderScene instance;
  public static LoaderScene Instance => instance;

  private void Awake()
  {
    if (instance == null)
      instance = this;
    else if (instance != this)
      Destroy(gameObject);

    DontDestroyOnLoad(gameObject);
  }

  public async void LoadLevel(string sceneName)
  {
    _target = 0;
    LoadingImage.fillAmount = 0;
    var scene = SceneManager.LoadSceneAsync(sceneName);
    scene.allowSceneActivation = false;

    LoadingCanvas.SetActive(true);

    do
    {
      await Task.Delay(150);
      _target = scene.progress;
    } while (scene.progress < 0.9f);
    await Task.Delay(300);

    scene.allowSceneActivation = true;

    if (MainCanvas != null)
      MainCanvas.SetActive(false);

    LoadingCanvas.SetActive(false);

    //var loadSceneObject = GameObject.Find("LoadScene");
    //if (loadSceneObject != null)
    //  loadSceneObject.GetComponent<LoadScene>().LoadConfigurationLevel();
  }

  private void Update()
  {
    LoadingImage.fillAmount = Mathf.MoveTowards(LoadingImage.fillAmount, _target, 3 * Time.deltaTime);
  }
}
