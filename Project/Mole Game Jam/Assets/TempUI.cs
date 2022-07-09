using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;


public class TempUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _moleHealthText;
    [SerializeField] TextMeshProUGUI _babiesHealthText;
    [SerializeField] TextMeshProUGUI _statusText;

    

    public void SetMoleHealthText(float value)
    {
            _moleHealthText.text = "Mole Health: " + Mathf.Ceil(value).ToString();   
    }

    public void SetBabiesHealthText(float value)
    {
        
            _babiesHealthText.text = "Babies Health: " + Mathf.Ceil(value).ToString();


    }

    public void SetStatusText(string value)
    {

            _statusText.text = value;

    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
