using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator cam, door;

    public void GameStart()
    {
        cam.SetTrigger("camTrigger");
        door.SetTrigger("Trig");
        StartCoroutine(End());
    }

    public IEnumerator End()
    {
        yield return new WaitForSeconds(9.07f);
        SceneManager.LoadScene(1);
    }
}
