using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Death : MonoBehaviour
{
    public int days = 1, lastDay = 5;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private GameObject panel;
    [SerializeField] private PlayerController playerCont;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) Died();
    }

    private void Died()
    {
        if (days < lastDay)
        {
            days++;
            txt.text = $"{days} day";
            StartCoroutine(PanelFade());
            transform.position = spawnPos.position;
        }
        else
        {
            txt.text = "Смерть";
            panel.SetActive(true);
        }
    }

    private IEnumerator PanelFade()
    {
        panel.SetActive(true);
        playerCont.canMove = false;
        playerCont.canMoveCamera = false;

        yield return new WaitForSeconds(3f);
        playerCont.canMove = true;
        playerCont.canMoveCamera = true;
        panel.SetActive(false);
    }
}
