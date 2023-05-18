using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
[SerializeField]
public RectTransform UIButtons;

public float TweenLength;
public float TweenWidth;

[Header("Exit")]
public float ExitLength;
public float ExitWidth;
 
    void Start()
    {
        UIButtons = GetComponent<RectTransform>();
        TweenLength = UIButtons.sizeDelta.x * 1.1f;
        TweenWidth = UIButtons.sizeDelta.y * 1.1f;

        ExitLength = UIButtons.sizeDelta.x;
        ExitWidth = UIButtons.sizeDelta.y;
    }

    public void OnCursorEnter() => Tween();
  
     // Debug.Log(UIButtons.sizeDelta + "Size TWeen");

    public void OnCursorExit() => OnExitTween();

    public void OnMouseClick() => Clicked();


   private void Tween()
    {
     LeanTween.size(UIButtons, UIButtons.sizeDelta = new Vector2(TweenLength,TweenWidth),0f).setEasePunch();
    }

    private void OnExitTween()
    {
    LeanTween.size(UIButtons,  UIButtons.sizeDelta = new Vector2(ExitLength,ExitWidth) , 0f).setEasePunch();
    }

    private void Clicked()
    {
     LeanTween.size(UIButtons, UIButtons.sizeDelta * 1.1f, 0.9f).setEaseInOutCirc();
    }




}//class


