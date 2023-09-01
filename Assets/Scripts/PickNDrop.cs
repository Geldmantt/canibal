using UnityEngine;

public class PickNDrop : MonoBehaviour
{
    [SerializeField] private Transform hand;
    public bool isHold, Destroy;
    public GameObject obj, nullean;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && obj != nullean || Destroy == true)
        {
            obj.transform.parent = null;
            obj.GetComponent<Rigidbody>().isKinematic = false;
            obj.GetComponent<Collider>().enabled = true;
            obj = nullean;
            isHold = false;
            Destroy = false;
        }
    }

    public void PickUp(GameObject item)
    {
        if(isHold == false)
        {
            isHold = true;
            item.transform.SetParent(hand);
            item.transform.position = hand.position;
            obj = item;
            item.transform.rotation = Quaternion.identity;
            item.GetComponent<Rigidbody>().isKinematic = true;
            item.GetComponent<Collider>().enabled = false;
        }
    }
}
