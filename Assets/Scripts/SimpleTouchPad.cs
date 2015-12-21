using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	private Vector2 origin;
	private Vector2 direction;
	public float smoothing;
	private Vector2 smoothDirection;

	void Awake() {
		direction = Vector2.zero;
	}

	public void OnPointerDown (PointerEventData data) {
		origin = data.position;
	}

	public void OnPointerUp (PointerEventData data) {
		Vector2 currentPosition = data.position;	
		Vector2 directionRaw = currentPosition - origin;
		direction = directionRaw.normalized;
	}

	public void OnDrag (PointerEventData data) {		
		direction = Vector2.zero;
	}

	public Vector2 GetDirection() {
		smoothDirection = Vector2.MoveTowards (smoothDirection,direction,smoothing);
		return smoothDirection;	
	}

}
