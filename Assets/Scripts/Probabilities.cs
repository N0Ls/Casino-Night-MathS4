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
    public static float Poisson(int k, float lambda)
    {
        return (Mathf.Pow(lambda, k) / Factorial(k)) * Mathf.Exp(-lambda);
    }

    //Loi de poisson 
    //Fonction de répartition
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
                //Debug.Log(i);
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

    public static float Geometric(int k, float p)
    {
        return (Mathf.Pow(1-p, k-1) * p);
    }

    static List<float> GeometricProbas(int numberOfPossibilities, float p)
    {
        List<float> probas = new List<float>(numberOfPossibilities);

        for (int i = 0; i < numberOfPossibilities; i++)
        {
            probas.Add(Geometric(i+1, p) + (i > 0 ? probas[i - 1] : 0));
        }

        return probas;
    }

    public static int PickFromGeometric(int nbPossibilities, float p)
    {
        float randomValue = Random.Range(0f, 1f);

        float previous = 0f;
        float next = 0.1f;

        List<float> computedProbas = Probabilities.GeometricProbas(nbPossibilities, p); ;

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
                return i;
            }

            previous = next;
        }
        return 0;
    }

    public static int GeometricPick(float p)
    {
        float randomValue = Random.Range(0f, 1f);
        int n = 1;
        while (randomValue >= p)
        {
            randomValue *= Random.Range(0f, 1f);
            n++;
        }
        return n;
    }

    public static List<float> verifyGeometric()
    {
        int iter = 10000;

        List<float> arrayCompteur = new List<float> { 0f, 0f, 0f };

        for (int i = 0; i < iter; i++)
        {
            int index = GeometricPick(0.55f);
            if (index > 3) index = 3;
            arrayCompteur[index-1] = arrayCompteur[index-1] += 1;
        }

        for (int i = 0; i < arrayCompteur.Count; i++)
        {
            //Debug.Log(i + " " + arrayCompteur[i]/10000);
        }

        return arrayCompteur;
    }

    static int CoeffBinomial(int n, int k)
    {
        if (k > n / 2) k = n - k;

        int result = 1;
        for (int i = 1; i <= k; i++)
        {
            result *= (n - k + i);
            result /= i;
        }
        return result;
    }

    static float Bernoulli(int k, float p, int n)
    {
	    return Mathf.Pow(p, k) * Mathf.Pow(1 - p, n - k);
    }

    public static int pickFromBernoulli(float p)
    {
        float randomValue = Random.Range(0f, 1f);

        if (randomValue <= p) return 1;
        else return 0;
    }


    public static float Binomial(int n, int k, float p)
    {
        return CoeffBinomial(n,k) * Mathf.Pow(p,k) * Mathf.Pow(1-p, n-k);
    }

    public static int pickFromBinomial(int n, float p)
    {
        int wins = 0;
        for (int i = 0; i < n; i++)
        {
            if (pickFromBernoulli(p)==1)
                wins++;
        }
        return wins;
    }


}
