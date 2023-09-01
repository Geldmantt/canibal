using System.Collections;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("------- Move Control -------")]                            //Передвижение
    [Space]

    [SerializeField] private Transform playerEmpty;
    [SerializeField] private Transform player;
    public bool canMove;

    [Tooltip("Мин: 0, Макс: 1")]
    [SerializeField][Range(0f, 1f)] private float speed, shiftSpeed, crouchSpeed;

    [Header("------- Camera Control -------")]                          //Камера
    [Space]

    [SerializeField] private Transform playerCamera;

    public bool canMoveCamera;

    [SerializeField][Range(1f, 20f)] private float senstivity;
    [SerializeField][Range(0.01f, 1f)] private float smooth;
    private float xRotCurrent;
    private float yRotCurrent;
    private float currenVelocityX;
    private float currenVelocityY;

    [SerializeField][Range(60f, 120f)] public float FOV;

    [Tooltip("На сколько высоко или низко может смотреть персонаж.")]
    [SerializeField] private float uThreshold, lThreshold;

    [Header("------- Other -------")]                                   //Остальное
    [Space]

    [Tooltip("Если вкл. курсор спрятан.")]
    public bool CursorIsHide;

    [SerializeField] private Vector3 defHeight;
    [SerializeField] private float crouchHeight;
    public PickNDrop pnd;
    public TextMeshProUGUI tip, tint;
    private string corLock;


    private float xRot, yRot;
    private float ad, ws;

    private void Start()
    {
        defHeight = player.localScale;

    }

    //РэйКастинг
    private void Update()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.collider.GetComponent<Door>())
                {
                    hit.collider.GetComponent<Door>().DoorOpen();
                }
            }


            if (Input.GetKeyDown(KeyCode.E) && hit.collider.tag == "Item")
            {
                pnd.PickUp(hit.collider.gameObject);
            }

            if (Input.GetKeyDown(KeyCode.E) && hit.collider.GetComponent<Water>())
            {
                if (hit.collider.GetComponent<Water>().canPickWater == true)
                {
                    pnd.obj.GetComponent<Bucket>().isFull = true;
                }
            }

            if (hit.collider.GetComponent<Lock>())
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (pnd.obj.GetComponent<Key>().keyId == hit.collider.GetComponent<Lock>().id)
                    {
                        hit.collider.GetComponent<Lock>().Unlock();
                    }
                    else
                    {
                        corLock = hit.collider.GetComponent<Lock>().description;
                        StartCoroutine(CantDoThis());
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            player.localScale = new Vector3(defHeight.x, crouchHeight, defHeight.z);
            playerEmpty.GetComponent<CapsuleCollider>().height = crouchHeight;

            playerEmpty.position = new Vector3(playerEmpty.position.x, Vector3.down.y + crouchHeight, playerEmpty.position.z);
            
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            player.localScale = defHeight;
            playerEmpty.GetComponent<CapsuleCollider>().height = 2f;
            playerEmpty.position = new Vector3(playerEmpty.position.x, Vector3.up.y - crouchHeight, playerEmpty.position.z);
        }
    }

    private void FixedUpdate()
    {
        playerCamera.GetComponent<Camera>().fieldOfView = FOV;

        if (canMove)
            Movement();

        if (canMoveCamera)
            CameraMovement();


        if (CursorIsHide)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void Movement()
    {
        ws = Input.GetAxis("Vertical");
        ad = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerEmpty.Translate(Vector3.right * ad * shiftSpeed);     //Если с шифотм
            playerEmpty.Translate(Vector3.forward * ws * shiftSpeed);
        }
        else
        {
            if (Input.GetKey(KeyCode.C))
            {
                playerEmpty.transform.Translate((Vector3.right * ad) * crouchSpeed);        //Если вприсяд
                playerEmpty.transform.Translate((Vector3.forward * ws) * crouchSpeed);
            }
            else
            {
                playerEmpty.transform.Translate((Vector3.right * ad) * speed);        //Если без шифта
                playerEmpty.transform.Translate((Vector3.forward * ws) * speed);
            }
        }
    }

    private void CameraMovement()
    {
        yRot += Input.GetAxis("Mouse Y") * senstivity;
        xRot += Input.GetAxis("Mouse X") * senstivity;

        yRot = Mathf.Clamp(yRot, lThreshold, uThreshold);

        xRotCurrent = Mathf.SmoothDamp(xRotCurrent, xRot, ref currenVelocityX, smooth);
        yRotCurrent = Mathf.SmoothDamp(yRotCurrent, yRot, ref currenVelocityY, smooth);

        playerEmpty.rotation = Quaternion.Euler(0, xRotCurrent, 0);           //Вращение Игрока
        playerCamera.rotation = Quaternion.Euler(-yRotCurrent, xRotCurrent, 0);      //Вращение Камеры

    }

    private IEnumerator CantDoThis()
    {   
        tint.text = "You need " + corLock;
        yield return new WaitForSeconds(4);
        tint.text = null;
    }
}
