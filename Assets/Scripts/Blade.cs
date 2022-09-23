using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    private bool slicing;
    private TrailRenderer bladeTrial;
    public float minSliceVelocity = 0.05f;
    public Vector3 direction { get; private set; }
    public float slicedForce = 5f;

    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrial = GetComponentInChildren<TrailRenderer>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        } else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }else if (slicing)
        {
            ContinueSlicing();
        }
        
    }
    
    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void StartSlicing()
    {
        bladeCollider.enabled = true;
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        transform.position = newPosition;
        
        slicing = true;
        
        bladeTrial.enabled = true;
        bladeTrial.Clear();
    }

    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;
        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        // bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }
    
    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrial.enabled = false;
    }
}