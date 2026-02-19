using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;


public class OrbitCamera : MonoBehaviour
{
    [SerializeField] Transform focus = default;
    [SerializeField] PlayerInputReader input;
    private Camera CameraComponent;
    private float distance = 20f;
    [SerializeField] float verticalOffset = 1f;
    [SerializeField] float rotationSpeed = 0.001f;


    private bool UICam = false;





    private Vector2 crossHairPosition;
    private Vector2 lookDirection;
    private Vector2 lookAngles = new Vector2(40f, 0f);
    private Quaternion lookRotation;

    // private Vector3 lookDirection;
    private Vector3 focusPoint;


    void Awake()
    {

        focusPoint = focus.position;
        input.LookEvent += HandleLook;
        input.ScrollCameraEvent += HandleScroll;
        //input.OpenQuestLogEvent += HandleOpenQuestLog;
        //input.ShowInvEvent += HandleShowInv;
        //input.CloseInvEvent += HandleCloseInv;/*
 
        CameraComponent = GetComponent<Camera>();
        // lookDirection = focus.forward;

    }

    void LateUpdate()
    {


        UpdateCameraRotation();
        //focusPoint = focus.position + Vector3.up * verticalOffset;
        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = (focus.position + new Vector3(0, 2, 0)) - lookDirection * distance;

        //TODO: fix
        /*
        if (Physics.Raycast(focus.position, -lookDirection, out RaycastHit hit, distance)) { 
            //lookPosition = focus.position - lookDirection * hit.distance;
            lookPosition = hit.point + lookDirection * 0.5f;
        }
        */

        if (lookPosition.y < 0.6f)
        {
            lookPosition.y = 0.6f;
        }
        if (UICam == false)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            transform.SetPositionAndRotation(lookPosition, lookRotation);
        }


    }

    void HandleInv(bool stateOfInv)
    {
        UICam = stateOfInv;
    }

    void HandleLook(Vector2 input)
    {
        lookAngles.y += input.x * rotationSpeed;
        lookAngles.x += input.y * rotationSpeed;

        lookAngles.x = Mathf.Clamp(lookAngles.x, -90f, 90f);
    }

    void HandleScroll(float scrollMagnitude)
    {

        //Debug.Log($"scrolling woth magnitude: {scrollMagnitude}  and distance {distance}");
        distance = distance -= scrollMagnitude;
        if (distance <= 0)
        {
            distance = 0;
            return;
        }
        else if (distance >= 20)
        {
            distance = 20;
        }
        //Debug.Log($"new distance: {distance}");

    }

    /*
    void HandleInteract(Item itemInCrossHair) {
        if (onInteractable) {
            itemInCrossHair.OnInteract();
        }
        
    }*/

    void UpdateCameraRotation()
    {

        lookRotation = Quaternion.Euler(lookAngles.x, lookAngles.y, 0f);
    }

    void HandleShowInv()
    {
        UICam = !UICam;
    }

    void HandleCloseInv()
    {
        UICam = !UICam;
    }




}