using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Key : MonoBehaviour
{
    public string name;
    public int keyId;
}
