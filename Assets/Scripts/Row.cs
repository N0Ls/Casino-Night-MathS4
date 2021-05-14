using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{

    private int randomValue;

    private float timeInterval;

    public bool rowStopped;
    public string stoppedSlot;
    // Start is called before the first frame update
    void Start()
    {
        rowStopped = true;
        GameControl.HandlePulled += StartRotating;
        
    }

    private void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine("Rotate");
    }

    private IEnumerator Rotate()
    {
        rowStopped = false;

        timeInterval = 0.025f;

        for(int i = 0; i<32; i++)
        {
            if (transform.position.y <= -9.4f)
            {
                transform.position = new Vector2(transform.position.x, 8.8125f);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, Mathf.Round((transform.position.y - 0.5875f) * 10000f) / 10000);
            }

            yield return new WaitForSeconds(timeInterval);
        }

        randomValue = Random.Range(0, 32);

        Debug.Log(randomValue);

        switch (randomValue % 4)
        {
            case 1:
                randomValue += 3;
                break;
            case 2:
                randomValue += 2;
                break;
            case 3:
                randomValue += 1;
                break;
        }

        for (int i = 0; i < randomValue; i++)
        {
            if (transform.position.y <= -9.4f)
            {
                transform.position = new Vector2(transform.position.x, 8.8125f);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, Mathf.Round((transform.position.y - 0.5875f) * 10000f) / 10000);
            }

            if (i > Mathf.RoundToInt(randomValue * 0.25f))
                timeInterval = 0.05f;
            if (i > Mathf.RoundToInt(randomValue * 0.5f))
                timeInterval = 0.1f;
            if (i > Mathf.RoundToInt(randomValue * 0.75f))
                timeInterval = 0.15f;
            if (i > Mathf.RoundToInt(randomValue * 0.95f))
                timeInterval = 0.2f;

            yield return new WaitForSeconds(timeInterval);
        }

        if (transform.position.y == -9.4f)
            stoppedSlot = "Watermelon";
        else if (transform.position.y == -7.05f)
            stoppedSlot = "Grapes";
        else if (transform.position.y == -4.7f)
            stoppedSlot = "Strawberry";
        else if (transform.position.y == -2.35f)
            stoppedSlot = "Lemon";
        else if (transform.position.y == 0f)
            stoppedSlot = "Orange";
        else if (transform.position.y == 2.35f)
            stoppedSlot = "Pear";
        else if (transform.position.y == 4.7f)
            stoppedSlot = "Cherry";
        else if (transform.position.y == 7.05f)
            stoppedSlot = "Banana";
        else if (transform.position.y == 9.4f)
            stoppedSlot = "Watermelon";

        rowStopped = true;
    }

    private void OnDestroy()
    {
        GameControl.HandlePulled -= StartRotating;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
