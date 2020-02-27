using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Rotation Variables

    public GameObject cannon;
    public GameObject projectile;
    public Camera cannonCamera;
    public GameObject cameraHolder;
    public GameObject positionArrow;
    public GameObject height;
    public GameObject audioBoom;
    public GameObject audioWind;
    public GameObject audioFall;
    public GameObject tutorial;
    public GameObject clickTut;
    public float groundTimer;
    float barDisplay;
    Vector2 pos;
    Vector2 size;
    Texture2D progressBarEmpty;
    Texture2D progressBarFull;
    float projectilePos;
    float xAngle;
    float timer;
    float launchPower;
    float chargetimer;
    bool isCharging;
    bool initialShot;
    bool upCharge;
    float cannonAimInput;
    bool cannonFireInput;
    bool playerSlamInput;
    bool cannonFireDelay;
    public bool hasSlammed;
    bool readyToFire;
    float xForce;
    float yForce;

    Vector2 launchForce;
    


    // Start is called before the first frame update
    void Start()
    {
        initialShot = false;
        isCharging = false;
        readyToFire = false;
        upCharge = true;
        hasSlammed = false;
        pos = new Vector2(300, 500);
        size = new Vector2(800, 50);
        timer = 0.0f;
    }

    void OnGUI()
    {
        if (isCharging == true)
        {
            // draw the background:
            GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
            GUI.Box(new Rect(0, 0, size.x, size.y), progressBarEmpty);

            // draw the filled-in part:
            GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
            GUI.Box(new Rect(0, 0, size.x, size.y), progressBarFull);
            GUI.EndGroup();

            GUI.EndGroup();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        xAngle = cannon.transform.rotation.x * (180 / Mathf.PI);
        cannonAimInput = Input.GetAxis("Vertical");
        cannonFireInput = Input.GetKeyDown("space");
        cannonFireDelay = Input.GetKeyUp("space");
        playerSlamInput = Input.GetButton("Fire1");
        projectilePos = projectile.transform.position.x;
        positionArrow.transform.localPosition = new Vector3(projectilePos, 16.77f, 0.0f);
        cameraHolder.transform.localPosition = new Vector3(0.0f, 0.0f, -6.0f);
        if (xAngle < 28.5f && cannonAimInput < 0.0f)
        {
            cannon.transform.Rotate(0.0f, -cannonAimInput, 0.0f, Space.Self);
        }
        if (xAngle > 1.4f && cannonAimInput > 0.0f)
        {
            cannon.transform.Rotate(0.0f, -cannonAimInput, 0.0f, Space.Self);
        }


        if (cannonFireInput == true && isCharging == false && initialShot == false)
        {
            isCharging = true;
        }
        if(cannonFireDelay == true && isCharging == true && initialShot == false)
        {
            readyToFire = true;
        }

        if (isCharging == true)
        {
            if (upCharge == true)
            {
                chargetimer = 0.0f;
                chargetimer += Time.deltaTime;
                launchPower += chargetimer;
            }
            if (launchPower >= 5.0f || upCharge == false)
            {
                chargetimer = 0.0f;
                chargetimer += Time.deltaTime;
                launchPower -= chargetimer;
                upCharge = false;
            }
            if (launchPower <= 0.0f && upCharge == false)
            {
                chargetimer = 0.0f;
                upCharge = true;
            }
            barDisplay = launchPower / 5.0f;
        }

        if (cannonFireInput == true && isCharging == true && readyToFire == true)
        {
            audioBoom.SetActive(true);
            audioWind.SetActive(true);
            tutorial.SetActive(false);
            clickTut.SetActive(true);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = cannonCamera.transform.forward * launchPower * 10.0f;
            initialShot = true;
            isCharging = false;
            readyToFire = false;
        }
        if (playerSlamInput == true && initialShot == true && hasSlammed == false)
        {
            audioFall.SetActive(true);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0.0f, -40.0f, 0.0f);
            hasSlammed = true;
        }

        if (projectile.transform.position.y > 17.8f)
        {
            positionArrow.SetActive(true);
            height.SetActive(true);
        }
        else
        {
            positionArrow.SetActive(false);
            height.SetActive(false);
        }

        if (projectile.transform.position.y < 1.0f)
        {
            groundTimer += Time.deltaTime;
            audioFall.SetActive(false);
        }
        if (groundTimer >= 0.3f)
        {
            audioWind.SetActive(false);
        }

        if (projectile.transform.position.y > 2.0f)
        {
            groundTimer = 0.0f;
            audioWind.SetActive(true);
        }

        if (initialShot == true)
        {
            cameraHolder.transform.localPosition = new Vector3(projectilePos, 0.0f, -6.0f);
        }

        Debug.Log("launch power: " + launchPower);
    }
}
