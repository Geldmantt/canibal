using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform door;
    public bool isOpen, isLocked;
    [SerializeField] private float angle, zeroAngle, speed;
    public GameObject locker;


    private void Update()
    {
        if (locker == null)
        {
            isLocked = false;
        }

        if (isOpen == true)
        {
            Vector3 open = new Vector3(door.rotation.x, door.rotation.y + angle, door.rotation.z);

            door.rotation = Quaternion.Slerp(door.rotation, Quaternion.Euler(open), Time.deltaTime * speed);
        }
        else
        {
            Vector3 close = new Vector3(door.rotation.x, door.rotation.y + zeroAngle, door.rotation.z);

            door.rotation = Quaternion.Slerp(door.rotation, Quaternion.Euler(close), Time.deltaTime * speed);
        }
    }

    public void DoorOpen()
    {

        if (isOpen == false && isLocked == false)
        {
            isOpen = true;
        }
        else
        {
            isOpen = false;
        }
    }
}
