using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{

    private int randomValue;

    private float timeInterval;

    public bool rowStopped;
    public string stoppedSlot;
    public string previousStoppedSlot;

    private float lambda = 2.7f;
    List<float> computedProbas;

    // Start is called before the first frame update
    void Start()
    {
        rowStopped = true;
        GameControl.HandlePulled += StartRotating;

        computedProbas = PoissonProbas(8, lambda);

        
        //Debug.Log(computedProbas.Count);

        for (int i = 0; i < computedProbas.Count; i++)
        {
            //Debug.Log(computedProbas[i]);
        }

        verifyPoisson();
    }

    private void StartRotating()
    {
        previousStoppedSlot = stoppedSlot;
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

        randomValue = PickFromPoisson();

        int correct = 0;

        switch (previousStoppedSlot)
        {
            case "Lemon":
                correct = 7;
                break;
            case "Strawberry":
                correct = 6;
                break;
            case "Grapes":
                correct = 5;
                break;
            case "Watermelon":
                correct = 4;
                break;
            case "Banana":
                correct = 3;
                break;
            case "Cherry":
                correct = 2;
                break;
            case "Pear":
                correct = 1;
                break;
        }

        //Debug.Log(randomValue);

        //switch (randomValue % 4)
        //{
        //    case 1:
        //        randomValue += 3;
        //        break;
        //    case 2:
        //        randomValue += 2;
        //        break;
        //    case 3:
        //        randomValue += 1;
        //        break;
        //}

        for (int i = 0; i < correct*4 + randomValue*4; i++)
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

    int Factorial(int x)
    {
        if (x < 0)
        {
            return -1;
        }
        else if (x == 1 || x == 0)
        {
            return 1;
        }
        else
        {
            return x * Factorial(x - 1);
        }
    }

    //Calcul de la probabilité
    private float Poisson(int k, float lambda)
    {
        return (Mathf.Pow(lambda, k) / Factorial(k)) * Mathf.Exp(-lambda);
    }

    //Loi de poisson 
    //Proba sont enregistrées en cumulées pour par la suite obtenir des intervales 
    private List<float> PoissonProbas(int numberOfPossibilities, float lambda)
    {
        List<float> probas = new List<float>(numberOfPossibilities);

        for(int i = 0; i < numberOfPossibilities; i++)
        {
            probas.Add(Poisson(i, lambda)+(i>0?probas[i-1]:0));
        }

        return probas;
    }

    private int PickFromPoisson()
    {
        float randomValue = Random.Range(0f,1f);

        float previous = 0f;
        float next = 0.1f;

        for (int i = 0; i < computedProbas.Count; i++)
        {
            if(i == computedProbas.Count-1)
            {
                next = 1;
            }
            else
            {
                next = computedProbas[i];
            }
            if (randomValue > previous && randomValue <= next)
            {
                //Debug.Log(i);
                return i;
            }

            previous = next;
        }
        return 0;
    }

    private void verifyPoisson()
    {

        int iter = 10000;

        List<float> arrayCompteur = new List<float> { 0f, 0f, 0f, 0f, 0f ,0f , 0f, 0f};

        for (int i = 0; i < iter; i++)
        {
            int index = PickFromPoisson();
            arrayCompteur[index] = arrayCompteur[index]+=1;
        }

        for (int y = 0; y < arrayCompteur.Count; y++)
        {
            Debug.Log(arrayCompteur[y] / iter);
        }
    }
}
