using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        
        GenerateSquares();
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Debug.Log(stageDimensions);
    }

    // Update is called once per frame
    void Update()
    {
        
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
    private IEnumerator Rotate()
    {

        timeInterval = 0.025f;

        for (int i = 0; i < instanciatedList.Count; i++)
        {

            if (instanciatedList[i].gameObject.transform.position.x >= 17.5f)
            {
                int randomValue = Random.Range(0, Squares.Length);

                GameObject.Destroy(instanciatedList[i].gameObject);
                instanciatedList.RemoveAt(instanciatedList.Count-1);

                instanciatedList.Insert(0,Instantiate(Squares[randomValue], new Vector3(-17.5f - (3 * distanceGap) / 4, 0.0f, 0f), new Quaternion()));


                Debug.Log(instanciatedList.Count);
            }
            else
            {
                Debug.Log(i + " Je décale");
                instanciatedList[i].gameObject.transform.position = new Vector2(instanciatedList[i].gameObject.transform.position.x + distanceGap/4, instanciatedList[i].gameObject.transform.position.y);
            }

        }

        yield return new WaitForSeconds(timeInterval);
    }


    public void move()
    {
        StartCoroutine("Rotate");

    }

    private void OnDestroy()
    {
        RemoveAllEnemies();
        //destroyInstanciated();
    }
}
