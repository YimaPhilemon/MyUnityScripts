using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ScrollSelect : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
	public ScrollRect scrollRect;
	private TMP_InputField inputField;

	private bool dragging = false;
	private bool clickStarted = false;

	private void Start()
	{
		inputField = GetComponent<TMP_InputField>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		dragging = true;
		ExecuteEvents.Execute(scrollRect.gameObject, eventData, ExecuteEvents.beginDragHandler);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (dragging)
		{
			ExecuteEvents.Execute(scrollRect.gameObject, eventData, ExecuteEvents.dragHandler);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		dragging = false;
		ExecuteEvents.Execute(scrollRect.gameObject, eventData, ExecuteEvents.endDragHandler);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		clickStarted = true;
		//inputField.interactable = false;	   

		if (inputField == null)
			return;

		inputField.DeactivateInputField(true);
		ExecuteEvents.Execute(scrollRect.gameObject, eventData, ExecuteEvents.pointerDownHandler);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (clickStarted)
		{
			clickStarted = false;

			if (inputField == null)
				return;

			StartCoroutine(Delay());

			EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
		}
	}

	IEnumerator Delay()
	{
		yield return new WaitForSeconds(0.8f);

		inputField.interactable = true;
	}
}
