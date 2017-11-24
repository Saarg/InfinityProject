using UnityEngine;

public class UIDirectionControl : MonoBehaviour
{
	private Vector3 parentStartRotation;

    private void Start()
    {
		parentStartRotation = transform.parent.transform.rotation.eulerAngles;
    }

    private void Update()
    {
		Vector3 rotParent = transform.parent.transform.rotation.eulerAngles;
		Vector3 diff = rotParent - parentStartRotation;
		Vector3 newRot = new Vector3 (90 - diff.x, 0, 0);

		transform.localRotation = Quaternion.Euler (newRot);
    }
}
