using UnityEngine;
//Automaticly add rigidbody to the gameobject
[RequireComponent (typeof (Rigidbody))]
public class MovementController : MonoBehaviour {
    public float _speed = 6;
    public float _jumpForce = 6;
    private Rigidbody _rig;
    private Vector2 _input;
    private Vector3 _movementVector;
    private void Start () {
        _rig = GetComponent<Rigidbody> ();
        //Need to freez rotation so the player do not flip over
        _rig.freezeRotation = true;
    }
    private void Update () {
        //Cleanerway to get input
        _input = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
        if (Input.GetButtonDown ("Jump") && IsGrounded ()) {
            _rig.AddForce (Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
    private void FixedUpdate () {
        //Keep the movement vector aligned with the player rotation
        _movementVector = _input.x * transform.right * _speed + _input.y * transform.forward * _speed;
        //Apply the movement vector to the rigidbody without effecting gravity
        _rig.velocity = new Vector3 (_movementVector.x, _rig.velocity.y, _movementVector.z);
    }
    private bool IsGrounded () {
        //Simple way to check for ground
        if (Physics.Raycast (transform.position, Vector3.down, 1.5f)) {
            return true;
        } else {
            return false;
        }
    }
}