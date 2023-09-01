using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private GameObject valve;
    public bool canPickWater;

    void Update()
    {
        if (valve.GetComponent<Valve>().isTurnedOn == true)
            canPickWater = true;
    }
}
