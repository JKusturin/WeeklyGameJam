using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    public GameObject text;
    public GameObject distanceText;
    public GameObject heightText;
    Text MyText;
    Text distanceType;
    Text heightType;
    public float points;
    bool restartInput;
    public float distance;
    public float height;
    double height2;
    double distance2;

    // Start is called before the first frame update
    void Start()
    {
        distance = 0;
        points = 0;
        restartInput = false;
    }

    // Update is called once per frame
    void Update()
    {
        restartInput = Input.GetKeyDown("r");
        distance = projectile.transform.position.x;
        height = projectile.transform.position.y - 17.8f;
        height2 = System.Math.Round(height, 0);
        distance2 = System.Math.Round(distance, 0);
        MyText = text.GetComponent<Text>();
        distanceType = distanceText.GetComponent<Text>();
        heightType = heightText.GetComponent<Text>();
        MyText.text = "Points:" + points;
        if (distance < -1)
        {
            distanceType.text = "Distance:" + -distance2;
        }
        heightType.text = " " + height2;
        if (restartInput == true)
        {
            SceneManager.LoadScene("level1", LoadSceneMode.Single);
        }
    }
}
