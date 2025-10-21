using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class InteractManager : BaseManager<InteractManager>
{
    private InteractItem _currentInteractItem;
    
    private void Awake()
    {
        SetDoNotDestroyOnLoad();
    }
    
    public void InteractCurrentTarget()
    {
        if (!_currentInteractItem) return;
        
        Debug.Log($"[Interact] {_currentInteractItem.name}");
        _currentInteractItem.OnClick();
        _currentInteractItem = null;
    }
    
    public void SetInteractTarget(InteractItem item)
    {
        _currentInteractItem = item;
    }
    
    public void SetInteractTargetOld(IInteractable item)
    {
        
    }
}
