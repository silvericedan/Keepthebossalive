using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    // referenciamos un Slider para la barra de Loading
    public Slider sliderLoading;

    // Funcion simple que llama a una corutina con la variable sceneIndex seteada publica
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }


    // Corutina para cargar la escena enumerada de forma paralela sin congelar la escena actual.
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            sliderLoading.value = operation.progress;
            Debug.Log(operation.progress);
            if (sliderLoading.value == 0.9f)
            {
                sliderLoading.value = 1f;
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    // Funcion publica que luego llamamos con el boton de Exit para salir del juego.
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game!!");
    }
}
