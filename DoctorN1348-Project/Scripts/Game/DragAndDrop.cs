using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private Vector3 originalPosition;

    public ItemData itemData;

    private bool hasDragged = false;
    private Vector3 startMousePos;
    bool isTouchingBowl;

    private void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position;
    }

    private void OnMouseDown()
    {
        if (InputLock.shopOpen) return; // BLOCK INPUT WHEN SHOP OPEN

        startMousePos = Input.mousePosition;
        offset = transform.position - GetMouseWorldPosition();
        hasDragged = false;

        Debug.Log("Mouse down on: " + gameObject.name);
    }

    private void OnMouseDrag()
    {
        if (InputLock.shopOpen) return;

        if (Vector3.Distance(Input.mousePosition, startMousePos) > 10f)
            hasDragged = true;

        Vector3 newPosition = GetMouseWorldPosition() + offset;
        transform.position = newPosition;
    }

    private void OnMouseUp()
    {
        if (InputLock.shopOpen) return;

        if (!hasDragged)
        {
            Debug.Log("Click detected, not a drag → returning item");
            ReturnToOriginalPosition();
            return;
        }

        if (isTouchingBowl)
        {
            TryAddToBowl();
        }
        else
        {
            ReturnToOriginalPosition();
        }
    }

    private void TryAddToBowl()
    {
        Debug.Log("Dragged itemData instance ID: " + itemData.GetInstanceID());
        Debug.Log("Inventory Quantity: " + Inventory.Instance.GetQuantity(itemData));

        bool used = Inventory.Instance.UseItem(itemData);

        if (!used)
        {
            Debug.Log("No more items left: " + itemData.itemName);
            ReturnToOriginalPosition();
            return;
        }

        Bowl bowl = FindObjectOfType<Bowl>();
        if (bowl != null)
            bowl.AddItem(gameObject, itemData);

        Debug.Log("Item added to bowl: " + gameObject.name);

        int remaining = Inventory.Instance.GetQuantity(itemData);

        if (remaining <= 0)
            gameObject.SetActive(false);
        else
            ReturnToOriginalPosition();
    }

    private void ReturnToOriginalPosition()
    {
        transform.position = originalPosition;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Mathf.Abs(mainCamera.transform.position.z);
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bowl"))
        {
            isTouchingBowl = true;
            Debug.Log("Touched the bowl!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bowl"))
        {
            isTouchingBowl = false;
            Debug.Log("Stopped touching bowl");
        }
    }
} 