using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
    private List<float> consumption = new List<float>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public   float sumConsuption ;
    void Start()
    {
        float sum = 0;
        int i= 0;
        InputField[] inputFields = gameObject.GetComponentsInChildren<InputField>();
        foreach (float elem in consumption)
        {
            inputFields[i].text = consumption[i].ToString();

            
            sum += elem;
        }
        sumConsuption = sum;
        obj.SetActive(false);
    }

    public void OnChangeValueConsuption()
    {
        int i = 0;
        InputField[] inputFields = gameObject.GetComponentsInChildren<InputField>();
        foreach (InputField elem in inputFields)
        {
            if (elem.tag == ("Not_Home"))
            {
                consumption[i] = System.Convert.ToSingle(elem.text);

                i++;
            }
            
        }
        float sum = 0;
        foreach (float elem in consumption)
        {
            
            sum += elem;
        }
        sumConsuption = sum;
       
        
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
                if (elem.tag == ("Not_Home"))
                {
                    elem.text = (consumption[i]).ToString();
                    i++;                       
                }
            }            
        }
    }
}

