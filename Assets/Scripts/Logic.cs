using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logic : MonoBehaviour
{
    public GameObject greenSquare;
    public GameObject redSquare;
    public GameObject blackSquare;

    public GameObject[] Squares;

    private Color[] colors = { Color.black, Color.red, Color.green };

    private GameObject[] instanciatedArray;

    List<GameObject> instanciatedList = new List<GameObject>();

    private float timeInterval;

    private float distanceGap = 2.5f;

    private bool stopped = true;
    private bool resultsChecked = true;

    private Color betColor = Color.black;
    private Color resultColor = Color.yellow;

    public GameObject blackButton;
    public GameObject redButton;
    public GameObject greenButton;

    public float geoParam = 0.7f;

    

    // Start is called before the first frame update
    void Start()
    {
        GenerateSquares();
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        //Debug.Log(stageDimensions);
        Probabilities.verifyGeometric();
        Debug.Log(Probabilities.verifyBernoulli(0.5f));
        Debug.Log(Probabilities.verifyBinomialFixedParameters());
        Probabilities.TestLoiUniformDiscrete();

    }

    // Update is called once per frame
    void Update()
    {
        if (!stopped)
        {
            resultsChecked = false;

        }
        if (stopped && !resultsChecked)
        {
            //Checking results
            CheckResults();
        }
    }



    void GenerateSquares()
    {
        
        for (int i = 0; i < 15; i++)
        {
            int randomValue = Random.Range(0, Squares.Length);

            instanciatedList.Add(Instantiate(Squares[randomValue], new Vector3(-17.5f + i * distanceGap, 0.0f,0f), new Quaternion()));
        }
    }

    void destroyInstanciated()
    {
        for (int i = 0; i < instanciatedList.Count; i++)
        {
            Destroy(instanciatedList[i]);
        }
    }

    void RemoveAllEnemies()
    {
        for(int i= 0; i < instanciatedList.Count; i++)
        {
            GameObject.Destroy(instanciatedList[i].gameObject);
            instanciatedList.RemoveAt(i);

        }
    }

    private void RotateAllIncrement()
    {
        for (int i = 0; i < instanciatedList.Count; i++)
        {

            if (instanciatedList[i].gameObject.transform.position.x >= 17.5f)
            {
                GameObject.Destroy(instanciatedList[i].gameObject);
                instanciatedList.RemoveAt(instanciatedList.Count - 1);

                int randomValue = Probabilities.pickFromBinomial(2, 0.3f);
                if (randomValue > 2) randomValue = 2;
                instanciatedList.Insert(0, Instantiate(Squares[randomValue], new Vector3(-17.5f - (3 * distanceGap) / 4, 0.0f, 0f), new Quaternion()));

                UserStats.rouletteColorSquares[randomValue]++;
            }
            else
            {
                instanciatedList[i].gameObject.transform.position = new Vector2(instanciatedList[i].gameObject.transform.position.x + distanceGap / 4, instanciatedList[i].gameObject.transform.position.y);
            }
        }
    }

    private IEnumerator Rotate()
    {
        stopped = false;

        timeInterval = 0.025f;

        //Disabling buttons
        greenButton.GetComponent<Button>().interactable = false;
        blackButton.GetComponent<Button>().interactable = false;
        redButton.GetComponent<Button>().interactable = false;

        for (int y=0; y < 15*4; y++)
        {
            RotateAllIncrement();
            yield return new WaitForSeconds(timeInterval);
        }

        int randomValue = Probabilities.GeometricPick(geoParam) + 1;

        UserStats.RouletteSpins += randomValue;

        for (int y = 0; y < 15 * 4 * randomValue; y++)
        {
            RotateAllIncrement();

            yield return new WaitForSeconds(timeInterval);
        }

        for (int i = 0; i < instanciatedList.Count; i++)
        {
            if(instanciatedList[i].transform.position.x >= -0.01 && instanciatedList[i].transform.position.x <= 0.01)
            {
                resultColor = instanciatedList[i].gameObject.GetComponent<SpriteRenderer>().color;
            }
        }

        stopped = true;

        //Enabling back buttons
        greenButton.GetComponent<Button>().interactable = true;
        blackButton.GetComponent<Button>().interactable = true;
        redButton.GetComponent<Button>().interactable = true;
    }


    public void move(int bet)
    {
        betColor = colors[bet];

        StartCoroutine("Rotate");

    }

    private void CheckResults()
    {

        if(betColor == resultColor)
        {
            Debug.Log("Win");
            UserStats.RouletteWins++;
        }

        resultsChecked = true;
    }

    private void OnDestroy()
    {
        RemoveAllEnemies();
        //destroyInstanciated();
    }
}
