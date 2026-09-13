using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement_Controller : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector2 _Movement_Input;
    private bool _Facing_Right = true;

    [SerializeField] private float _Move_Speed = 5f; 

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
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
            Debug.Log("Attack!!!");
            //play actiack animation and open colition if hit change scene and disable movement
        }
    }

    public void OnMovenent_Action(InputAction.CallbackContext context)
    {
        _Movement_Input = context.ReadValue<Vector2>();
    }

    private void FlipCharactior()
    {
        _Facing_Right = !_Facing_Right;

        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    private void Update()
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

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(_Movement_Input.x, 0f, _Movement_Input.y);
        _rb.MovePosition(_rb.position + moveDirection * _Move_Speed * Time.deltaTime);
    }


}
