using Unity.VisualScripting;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public string description;
    public int id;
    public bool locked = true;
    [SerializeField] private bool isDoorLock, isValve;
    public Door door;
    [SerializeField] private GameObject boilerValve, valve;
    public PickNDrop pnd;

    public void Unlock()
    {
        if(isDoorLock == true)
        {
            locked = false;
            transform.GetComponent<Rigidbody>().isKinematic = false;
            transform.parent = null;
            door.locker = null;
            Destroy(this.gameObject, 10f);
        }

        if(isValve == true)
        {
            pnd.Destroy = true;
            Destroy(valve);
            boilerValve.SetActive(true);
        }
        
    }
}
