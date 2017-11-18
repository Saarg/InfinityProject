using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlBinding : MonoBehaviour {
    public InputField UpField;
    public InputField DownField;
    public InputField LeftField;
    public InputField RightField;
    public InputField JumpField;
    public InputField FireField;

    private MultiOSControls _controls;

    // Checks if there is anything entered into the input field.
    public void BindUp(InputField input)
    {
        if (input.text.Length > 0 && input.text.Length < 2)
        {
            Debug.Log("Up is" + input.text);
            // Ici, on applique la valeur de l'input field up dans la clé qui correspond a up  dans notre input control
            //_controls = GameObject.Find("Controls").GetComponent<MultiOSControls>();
            
        }
        else
        {
            //Debug.Log("Main Input Empty");
        }
    }

    public void BindDown(InputField input)
    {
        if (input.text.Length > 0 && input.text.Length < 2)
        {
            Debug.Log("down is" + input.text);
        }
        else
        {
        }
    }

    public void BindLeft(InputField input)
    {
        if (input.text.Length > 0 && input.text.Length < 2)
        {
            Debug.Log("left is" + input.text);
        }
        else
        {
            //Debug.Log("Main Input Empty");
        }
    }

    public void BindRight(InputField input)
    {
        if (input.text.Length > 0 && input.text.Length < 2)
        {
            Debug.Log("right is" + input.text);
        }
        else
        {
            //Debug.Log("Main Input Empty");
        }
    }

    public void BindJump(InputField input)
    {
        if (input.text.Length > 0 && input.text.Length < 2)
        {
            Debug.Log("jump is" + input.text);
        }
        else
        {
            //Debug.Log("Main Input Empty");
        }
    }

    public void BindFire(InputField input)
    {
        if (input.text.Length > 0 && input.text.Length < 2)
        {
            Debug.Log("fire is" + input.text);
        }
        else
        {
            //Debug.Log("Main Input Empty");
        }
    }

    public void Start()
    {
        //Adds a listener that invokes the "LockInput" method when the player finishes editing the main input field.
        //Passes the main input field into the method when "LockInput" is invoked
        UpField.onEndEdit.AddListener(delegate { BindUp(UpField); });
        DownField.onEndEdit.AddListener(delegate { BindDown(DownField); });
        LeftField.onEndEdit.AddListener(delegate { BindLeft(LeftField); });
        RightField.onEndEdit.AddListener(delegate { BindRight(RightField); });
        JumpField.onEndEdit.AddListener(delegate { BindJump(JumpField); });
        FireField.onEndEdit.AddListener(delegate { BindFire(FireField); });
    }
}
