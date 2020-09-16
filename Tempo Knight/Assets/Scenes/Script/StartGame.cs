using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Button button;
    public EventTrigger eventTrigger;

    private void Start() {
        button.onClick.AddListener(OnClick);
        //滑鼠觸碰事件
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
    }

    private void OnClick() {
        SceneManager.LoadScene(1);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        BPeerM.score = 0;
        this.gameObject.GetComponent<GrowOnBeat>().enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        this.gameObject.GetComponent<GrowOnBeat>().enabled = false;
        this.gameObject.transform.localScale = new Vector2(0.5f, 0.5f);

    }
}
