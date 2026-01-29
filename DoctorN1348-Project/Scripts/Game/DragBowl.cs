using UnityEngine;

public class DragBowl : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private Vector3 originalPos;

    private void Start()
    {
        mainCamera = Camera.main;
        originalPos = transform.position;
    }

    private void OnMouseDown()
    {
        if (InputLock.shopOpen) return; // BLOCK INPUT WHEN SHOP OPEN

        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        if (InputLock.shopOpen) return;

        transform.position = GetMouseWorldPosition() + offset;
    }

    private void OnMouseUp()
    {
        if (InputLock.shopOpen) return;

        PatientController patient = GetPatientUnderMouse();

        if (patient != null)
        {
            Bowl bowl = FindAnyObjectByType<Bowl>();

            if (bowl != null)
                bowl.UseItemsOnPatient(patient);
        }

        transform.position = originalPos;
    }

    private PatientController GetPatientUnderMouse()
    {
        Vector2 pos = GetMouseWorldPosition();
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

        if (hit.collider == null)
            return null;

        return hit.collider.GetComponent<PatientController>();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = Mathf.Abs(mainCamera.transform.position.z);
        return mainCamera.ScreenToWorldPoint(mouse);
    }
}
