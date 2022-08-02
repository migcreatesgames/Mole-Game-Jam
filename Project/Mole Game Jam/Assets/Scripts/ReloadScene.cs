using UnityEngine;
using UnityEngine.SceneManagement;
public class ReloadScene : MonoBehaviour
{
    private Scene _scene;
    private void Awake() => _scene = SceneManager.GetActiveScene();

    public void ReloadCurrentScene() => SceneManager.LoadScene(_scene.name);

}
