using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(Player))]
public class KBPlayerInputHandler : MonoBehaviour
{
    #region ABOUT
    /**
     * Handles player input from keyboard (or mouse)
     * Sets the parent camera (Main Camera) transform to a clicked object's position.
     * As such, the [CameraRig] object that this script is attached to will also have its position move.
     **/
    #endregion

    #region VARIABLES
    [Tooltip("Main Non-VR Game Camera")]
    public Camera cam;
    public Player player;

    [SerializeField] private MouseLook mouseLook;
    #endregion

    private void Awake()
    {
        if (!cam)
        {
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
        player = GetComponent<Player>();
    }

    private void Start()
    {
        mouseLook.Init(transform, cam.transform);
    }

    private void FixedUpdate()
    {
        mouseLook.UpdateCursorLock();
    }

    /// <summary>
    /// Listens for a mouse click and moves the Main Camera and [Camera Rig] to a raycast hit at a position to the click.
    /// </summary>
    void Update()
    {
        mouseLook.LookRotation(transform, cam.transform);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            player.TeleportTo(ray);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            player.SetSquadTarget(ray);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.SelectSquad(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.SelectSquad(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.SelectSquad(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            player.SelectSquad(4);
        }
    }
}
