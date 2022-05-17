using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Extract_GetInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
    private List<float> production = new List<float>() {1, 1, 1 };
    public float multiProduction;
    void Start()
    {
        float multi = 1;
        int i = 0;
        InputField[] inputFields = gameObject.GetComponentsInChildren<InputField>();
        foreach (float elem in production)
        {
            inputFields[i].text = production[i].ToString();


            multi *= elem;
        }
        multiProduction = multi;
        obj.SetActive(false);
    }

    public void OnChangeValueConsuption()
    {
        int i = 0;
        InputField[] inputFields = gameObject.GetComponentsInChildren<InputField>();
        foreach (InputField elem in inputFields)
        { if (elem.tag == ("Not_Home_Panel"))
            {
                production[i] = System.Convert.ToSingle(elem.text);

                i++;
            }
            
        }
        float multi = 1;
        foreach (float elem in production)
        {

            multi *= elem;
        }
        multiProduction = multi;


    }

    // Update is called once per frame
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))

        {
            obj.SetActive(true);
            int i = 0;
            InputField[] inputFields = gameObject.GetComponentsInChildren<InputField>();
            foreach (InputField elem in inputFields)
            {
                if (elem.tag == ("Not_Home_Panel"))
                {
                    elem.text = (production[i]).ToString();
                    i++;                    
                }
            }                       
        }
    }
}
