using UnityEngine;

public class MoveManagementView : MonoBehaviour
{
    [SerializeField]
    private MoveManagementPanel _availablePanel;

    [SerializeField]
    private MoveManagementPanel _equippedPanel;

    [SerializeField]
    private ClickableUI _saveButton;

    [SerializeField]
    private ClickableUI _unequipAllButton;

    public MoveManagementPanel AvailablePanel =>
        _availablePanel;

    public MoveManagementPanel EquippedPanel =>
        _equippedPanel;

    public ClickableUI SaveButton =>
        _saveButton;

    public ClickableUI UnequipAllButton =>
        _unequipAllButton;
}