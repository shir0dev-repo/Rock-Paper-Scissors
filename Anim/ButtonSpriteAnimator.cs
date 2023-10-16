using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSpriteAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button _btn;
    [SerializeField] private Animator _animator;

    private const string playTriggerName = "OnPointerEnter";
    private const string stopTriggerName = "OnPointerExit";

    private void Awake()
    {
        if (_btn == null)
            _btn = GetComponent<Button>();
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetTrigger(playTriggerName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetTrigger(stopTriggerName);
    }
}
