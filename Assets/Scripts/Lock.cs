using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Lock : MonoBehaviour
{
    public string description;
    public int id;
    public bool locked = true;
    public Door door;

    public void Unlock()
    {
        locked = false;
        transform.GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
        door.locker = null;
        Destroy(this.gameObject, 10f);
    }
}
