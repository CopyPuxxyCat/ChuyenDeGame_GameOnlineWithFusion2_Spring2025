using Fusion;
using UnityEngine;


public class PlayerMovement : NetworkBehaviour
{
    CharacterController _controller;
    public float speed = 5f;

    private Vector3 _velocity;
    private bool _jumpPressed;
    public float JumpForce = 5f;
    public float GravityValue = -9.81f;
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }
    public override void FixedUpdateNetwork()
    {
        if(!Object.HasInputAuthority) return;
        if (_controller.isGrounded)//khi nhan vat dang cham san
        {
            //tao 1 luc nho huong xuong giup nhan vat khong bi troi
            _velocity = new Vector3(0, -1, 0);
        }
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Runner.DeltaTime * speed;


        //tang van toc truc y theo trong luc
        //lam nhan vat roi xuong khi khong o tren mat dat
        _velocity.y += GravityValue * Runner.DeltaTime;
        if (_jumpPressed && _controller.isGrounded)
        {
            //tang van toc theo truc y de nhan vat nhay len
            _velocity.y += JumpForce;
        }
        _controller.Move(move + _velocity * Runner.DeltaTime);


        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        _jumpPressed = false;
    }

    void Update()
    {   //nhan nut jump thi bat co jump
        if (Input.GetButtonDown("Jump"))
        {
            _jumpPressed = true;
        }
    }

}
