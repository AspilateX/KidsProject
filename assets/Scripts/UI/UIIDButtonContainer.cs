using UnityEngine;
using TMPro;
public class UIIDButtonContainer : UIContainer<string>
{
    [SerializeField]
    private TMP_Text _name;
    private int _id;
    public override void Initialize(string content)
    {
        base.Initialize(content);
        _name.text = content;
    }
}
