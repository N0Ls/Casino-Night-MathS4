using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probabilities
{
    static int Factorial(int x)
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
    static float Poisson(int k, float lambda)
    {
        return (Mathf.Pow(lambda, k) / Factorial(k)) * Mathf.Exp(-lambda);
    }

    //Loi de poisson 
    //Proba sont enregistrées en cumulées pour par la suite obtenir des intervales 
    static List<float> PoissonProbas(int numberOfPossibilities, float lambda)
    {
        List<float> probas = new List<float>(numberOfPossibilities);

        for (int i = 0; i < numberOfPossibilities; i++)
        {
            probas.Add(Poisson(i, lambda) + (i > 0 ? probas[i - 1] : 0));
        }

        return probas;
    }

    public static int PickFromPoisson(int nbPossibilities, float lambda)
    {
        float randomValue = Random.Range(0f, 1f);

        float previous = 0f;
        float next = 0.1f;

        List<float> computedProbas = Probabilities.PoissonProbas(nbPossibilities, lambda); ;

        for (int i = 0; i < computedProbas.Count; i++)
        {
            if (i == computedProbas.Count - 1)
            {
                next = 1;
            }
            else
            {
                next = computedProbas[i];
            }
            if (randomValue > previous && randomValue <= next)
            {
                Debug.Log(i);
                return i;
            }

            previous = next;
        }
        return 0;
    }

    static List<float> verifyPoisson()
    {
        int iter = 10000;

        List<float> arrayCompteur = new List<float> { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };

        for (int i = 0; i < iter; i++)
        {
            int index = PickFromPoisson(arrayCompteur.Count,2.7f);
            arrayCompteur[index] = arrayCompteur[index] += 1;
        }

        return arrayCompteur;
    }


}
