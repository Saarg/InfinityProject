using UnityEngine;

public class UIDirectionControl : MonoBehaviour
{
    private Quaternion startRotation;

    private void Start()
    {
		startRotation = Quaternion.Euler(new Vector3 (90,0,0));
    }


    private void LateUpdate()
    {
		Vector3 rotParent = transform.parent.transform.rotation.eulerAngles

		Debug.Log ("local");
		Debug.Log (transform.parent.transform.localRotation.eulerAngles);
    }
}
