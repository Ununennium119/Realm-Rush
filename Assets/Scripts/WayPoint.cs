using UnityEngine;
using UnityEngine.EventSystems;

public class WayPoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private bool isPlaceable;
    public bool IsPlaceable => isPlaceable;

    [SerializeField] private GameObject ballistaPrefab;

    public bool IsHovered { get; private set; }


    public void OnPointerEnter(PointerEventData eventData)
    {
        IsHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsHovered = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isPlaceable) return;

        Instantiate(ballistaPrefab, transform.position, Quaternion.identity);
        isPlaceable = false;
    }
}