using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Movement : MonoBehaviour
{

    private RaycastHit hit;
    private NavMeshAgent agent;
    public bool useClickToMove;
    private Rigidbody agentRigidBody;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject MoveToPointMarker;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(!useClickToMove)
        agentRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    private void FixedUpdate()
    {
        if(!useClickToMove)
        MoveWithJoystick();
        if (useClickToMove)
        {
            ClickToMove();
        }
    }

    private void MoveWithJoystick()
    {
        agentRigidBody.velocity = new Vector3(joystick.Horizontal * moveSpeed, agentRigidBody.velocity.y, joystick.Vertical * moveSpeed);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(agentRigidBody.velocity);
        
        }

    }

    private void ClickToMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    agent.SetDestination(hit.point);
                    MoveToPointMarker.transform.position= hit.point;
                }

            }
        }
    }


}
