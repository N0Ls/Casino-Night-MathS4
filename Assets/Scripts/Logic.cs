using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    public GameObject greenSquare;
    public GameObject redSquare;
    public GameObject blackSquare;

    private GameObject[] instanciatedArray;

    List<GameObject> instanciatedList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateSquares();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void GenerateSquares()
    {
  
        for (int i = 0; i < 6; i++)
        {
            instanciatedList.Add(Instantiate(greenSquare, new Vector3(-8.4f + i * 2.5f, 0.0f,0f), new Quaternion()));
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

    public void move()
    {
        Debug.Log("clic");
        for (int i = 0; i < instanciatedList.Count; i++)
        {
            instanciatedList[i].gameObject.transform.position = new Vector2(instanciatedList[i].gameObject.transform.position.x + 2, instanciatedList[i].gameObject.transform.position.y);
        }
    }

    private void OnDestroy()
    {
        RemoveAllEnemies();
        //destroyInstanciated();
    }
}
