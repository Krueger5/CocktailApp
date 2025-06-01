using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(ScrollRect))]
public class SnapToItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public RectTransform content;
    public RectTransform[] items;
    public float snapSpeed = 10f;

    private ScrollRect scrollRect;
    private HorizontalLayoutGroup layoutGroup;
    private RectTransform viewport;
    private bool isSnapping = false;

    void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        viewport = scrollRect.viewport;
        layoutGroup = content.GetComponent<HorizontalLayoutGroup>();

        if (items == null || items.Length == 0)
        {
            int childCount = content.childCount;
            items = new RectTransform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                items[i] = content.GetChild(i).GetComponent<RectTransform>();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isSnapping = false;
        StopAllCoroutines();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(SnapToClosestItem());
    }

    IEnumerator SnapToClosestItem()
    {
        isSnapping = true;

        float contentPosX = content.anchoredPosition.x;
        float itemWidth = items[0].rect.width;
        float spacing = layoutGroup.spacing;
        float paddingLeft = layoutGroup.padding.left;
        float viewportWidth = viewport.rect.width;

        // Find the closest item
        float closestDistance = float.MaxValue;
        int closestIndex = 0;

        for (int i = 0; i < items.Length; i++)
        {
            float itemCenterPos = paddingLeft + i * (itemWidth + spacing) + itemWidth / 2f;
            float distance = Mathf.Abs(contentPosX + itemCenterPos - viewportWidth / 2f);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        // Snap target: item centered in viewport
        float targetItemCenter = paddingLeft + closestIndex * (itemWidth + spacing) + itemWidth / 2f;
        float snapToX = targetItemCenter - viewportWidth / 2f;
        Vector2 targetPosition = new Vector2(-snapToX, content.anchoredPosition.y);

        // Smoothly move content
        while (Vector2.Distance(content.anchoredPosition, targetPosition) > 0.1f)
        {
            content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPosition, Time.deltaTime * snapSpeed);
            yield return null;
        }

        content.anchoredPosition = targetPosition;
        isSnapping = false;
    }
}