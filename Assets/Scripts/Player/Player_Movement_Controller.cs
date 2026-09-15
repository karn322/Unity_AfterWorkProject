using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement_Controller : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector2 _Movement_Input;
    private bool _Facing_Right = true;

    private Combat_Manager _Combat_Manager;

    [SerializeField] private float _Move_Speed = 5f;
    [SerializeField] private Attack_HitBox _HitBox;
    [SerializeField] private List<GameObject> _Child_Objects= new List<GameObject>();

    private bool _Is_Move_Able;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _HitBox = GetComponentInChildren<Attack_HitBox>();
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        _Combat_Manager = Object.FindAnyObjectByType<Combat_Manager>();

        foreach (MeshRenderer renderer in renderers)
        {
            _Child_Objects.Add(renderer.gameObject);
        }
        _Child_Objects.RemoveAt(0);
    }

    private void Start()
    {
        _Is_Move_Able = true;
    }

    public void OnInteract_Item(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Interact Someting");
            //play pick-up animation and get that item ininventory
        }
    }

    public void OnAttack_Action(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _HitBox.CallToAttack();
        }
    }

    public void OnMovenent_Action(InputAction.CallbackContext context)
    {
        _Movement_Input = context.ReadValue<Vector2>();
    }

    private void FlipCharactior()
    {
        _Facing_Right = !_Facing_Right;

        foreach (GameObject gameObject in _Child_Objects)
        {
            Vector3 temp = gameObject.transform.localPosition;
            temp.x *= -1;
            gameObject.transform.localPosition = temp;
        }
    }

    private void Update()
    {
        if (_Combat_Manager.Get_Current_State() == GameMode.Exploration)
        {
            _Is_Move_Able = true;
        }
        else
        {
            _Is_Move_Able = false;
        }

        if (_Is_Move_Able)
        {
            if (_Movement_Input.x > 0 && !_Facing_Right)
            {
                FlipCharactior();
            }
            else if (_Movement_Input.x < 0 && _Facing_Right)
            {
                FlipCharactior();
            }
        }
    }

    private void FixedUpdate()
    {
        if (_Is_Move_Able)
        {
            Vector3 moveDirection = new Vector3(_Movement_Input.x, 0f, _Movement_Input.y);
            _rb.MovePosition(_rb.position + moveDirection * _Move_Speed * Time.deltaTime);
        }
    }
}
