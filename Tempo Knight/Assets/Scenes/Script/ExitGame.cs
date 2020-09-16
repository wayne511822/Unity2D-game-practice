using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Button button;

    private void Start() {
        button.onClick.AddListener(OnClick);
    }

    void Update() {


    }

    private void OnClick() {
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData eventData) {

        this.gameObject.GetComponent<GrowOnBeat>().enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        this.gameObject.GetComponent<GrowOnBeat>().enabled = false;
        this.gameObject.transform.localScale = new Vector2(0.5f, 0.5f);

    }
}
