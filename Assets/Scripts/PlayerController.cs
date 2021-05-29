using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float xSpeed = 7f;
    [Tooltip("In m")] [SerializeField] float xRange = 10f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 7f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;

    [Header("Screen-position factors")]
    [SerializeField] float pitchPositionFactor = -6f;
    [SerializeField] float yawPositionFactor = 3f;

    [Header("Rotation-control factors")]
    [SerializeField] float ControlPitchFactor = -30f;    // to control and add a feel in the changing of pitch of the y throw value
                                                         // essentially a factor to be multiplied with yThrow before we add it to the actual pitch value.
    [SerializeField] float ControlRollFactor = -30f;

    [SerializeField] GameObject[] bullets;

    float xThrow, yThrow;
    bool isControllEnabled = true;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isControllEnabled)
        {
            processTranslation();
            processRotation();
            processFiring();
        }
    }

    private void processFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunActive(true);
        }
        else
        {
            SetGunActive(false);
        }
    }

    private void SetGunActive(bool isActive)
    {
        foreach (GameObject bullet in bullets)
        {
            ParticleSystem.EmissionModule emissionModule = bullet.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }


    void DiableControlls()      // called by string reference
    {
        isControllEnabled = false;
    }

    private void processRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * pitchPositionFactor;
        float pitchDueToControlThrow = yThrow * ControlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yawDueToPosition = transform.localPosition.x * yawPositionFactor;
        float yaw = yawDueToPosition;

        float rollDueToThrow = xThrow * ControlRollFactor;
        float roll = rollDueToThrow;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void processTranslation()
    {
        xMovement();
        yMovement();
    }

    private void xMovement()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");    //here we are accessing the CrossplatformInputmanager
        // (which has axes instead of keys and buttons, and they are by default sey to some values like for a PC the keys for "Horizontal"
        // input are "a" and "d") and asking to to tell us if a key set for horizontal axis is being pressed, and how much is it pressed 
        // (between 0 and 1).
        //print(horizontalThrow);

        float xOffset = xSpeed * xThrow * Time.deltaTime;
        float rawXpos = xOffset + transform.localPosition.x;
        float xPosChange = Mathf.Clamp(rawXpos, -xRange, xRange);
        transform.localPosition = new Vector3(xPosChange, transform.localPosition.y, transform.localPosition.z);
    }

    private void yMovement()
    {
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float yOffset = ySpeed * yThrow * Time.deltaTime;
        float rawYpos = yOffset + transform.localPosition.y;
        float yPosChange = Mathf.Clamp(rawYpos, -yRange, yRange);
        transform.localPosition = new Vector3(transform.localPosition.x, yPosChange, transform.localPosition.z);
    }

}
