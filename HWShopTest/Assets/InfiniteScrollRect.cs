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
	private void Awake()
	{
		Init();
	}
	public void Init()
	{
		scrollRect = GetComponent<ScrollRect>();
		scrollRect.onValueChanged.AddListener(OnScroll);
		contentChildCount = scrollRect.content.childCount;
		contentChildren = new RectTransform[contentChildCount];
		for (int i = 0; i < contentChildCount; i++)
		{
			contentChildren[i] = (RectTransform)scrollRect.content.GetChild(i);
		}
		distanceBetweenCells = contentChildren[0].GetComponent<RectTransform>().anchoredPosition.y -
			 contentChildren[1].GetComponent<RectTransform>().anchoredPosition.y;
		cutOffLoopbackPosition = distanceBetweenCells * contentChildCount / 2;
	}

	private void OnScroll(Vector2 newPosition)
	{
		for (int i = 0; i < contentChildren.Length; i++)
		{
			if (scrollRect.transform.InverseTransformPoint(contentChildren[i].position).y > cutOffLoopbackPosition)
			{
				loopBackPosition = contentChildren[i].anchoredPosition;
				loopBackPosition.y -= distanceBetweenCells * contentChildCount;
				contentChildren[i].anchoredPosition = loopBackPosition;
				scrollRect.content.GetChild(0).SetAsLastSibling();
			}
			else if (scrollRect.transform.InverseTransformPoint(contentChildren[i].position).y < -cutOffLoopbackPosition)
			{
				loopBackPosition = contentChildren[i].anchoredPosition;
				loopBackPosition.y += distanceBetweenCells * contentChildCount;
				contentChildren[i].anchoredPosition = loopBackPosition;
				scrollRect.content.GetChild(contentChildCount - 1).SetAsFirstSibling();
			}
		}
	}
}
