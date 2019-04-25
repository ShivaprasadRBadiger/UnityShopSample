using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class InfiniteScrollRect : MonoBehaviour
{
	ScrollRect scrollRect;
	Vector2 boundry;
	RectTransform[] contentChildren;
	int contentChildCount = 0;
	float distanceBetweenCells = 0;
	float cutOffLoopbackPosition = 0;
	Vector2 loopBackPosition = Vector2.zero;
	int virtualTopIndex = 0;
	int virtualBotIndex = 0;
	public int virtualElements = 0;
	Vector2 clampVector = Vector2.zero;
	float clampYMin, clampYMax;

	public void Init()
	{
		scrollRect = GetComponent<ScrollRect>();
		scrollRect.onValueChanged.AddListener(OnScroll);
		contentChildCount = scrollRect.content.childCount;
		contentChildren = new RectTransform[contentChildCount];
		for (int i = 0; i < contentChildCount; i++)
		{
			contentChildren[i] = (RectTransform)scrollRect.content.GetChild(i);
			if (i < contentChildCount - 1)
				contentChildren[i].GetComponent<ScrollCell>().UpdateCell(i);
		}
		distanceBetweenCells = contentChildren[0].GetComponent<RectTransform>().anchoredPosition.y -
			 contentChildren[1].GetComponent<RectTransform>().anchoredPosition.y;
		cutOffLoopbackPosition = distanceBetweenCells * contentChildCount / 2;

		virtualTopIndex = 0;
		virtualBotIndex = contentChildCount - 1;

		clampYMin = contentChildren[0].GetComponent<RectTransform>().anchoredPosition.y;
		clampYMax = distanceBetweenCells * (virtualElements - contentChildCount) + (distanceBetweenCells / 2);
		if (virtualElements < contentChildCount)
		{
			scrollRect.vertical = false;
		}
	}

	private void OnScroll(Vector2 newPosition)
	{
		for (int i = 0; i < contentChildren.Length; i++)
		{
			if (scrollRect.transform.InverseTransformPoint(contentChildren[i].position).y > cutOffLoopbackPosition)
			{
				if (virtualBotIndex < virtualElements - 1)
				{
					virtualBotIndex++;
					virtualTopIndex++;
					scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
					loopBackPosition = contentChildren[i].anchoredPosition;
					loopBackPosition.y -= distanceBetweenCells * contentChildCount;
					contentChildren[i].anchoredPosition = loopBackPosition;
					scrollRect.content.GetChild(0).SetAsLastSibling();
					contentChildren[i].GetComponent<ScrollCell>().UpdateCell(virtualBotIndex);
				}
			}
			else if (scrollRect.transform.InverseTransformPoint(contentChildren[i].position).y < -cutOffLoopbackPosition)
			{
				if (virtualTopIndex > 0)
				{
					virtualTopIndex--;
					virtualBotIndex--;
					loopBackPosition = contentChildren[i].anchoredPosition;
					loopBackPosition.y += distanceBetweenCells * contentChildCount;
					contentChildren[i].anchoredPosition = loopBackPosition;
					scrollRect.content.GetChild(contentChildCount - 1).SetAsFirstSibling();
					contentChildren[i].GetComponent<ScrollCell>().UpdateCell(virtualTopIndex);
				}
			}
			clampVector = scrollRect.content.anchoredPosition;
			clampVector.y = Mathf.Clamp(clampVector.y, clampYMin, clampYMax);
			scrollRect.content.anchoredPosition = clampVector;
		}
	}
}
