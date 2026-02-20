using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
public class ModifierItem : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public ModifierDefinition modifier;

    void Start()
    {
        spriteRenderer.sprite = modifier.icon;
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerSpellManager manager = player.GetComponent<PlayerSpellManager>();
        manager.Equip(modifier);
        gameObject.SetActive(false);
    }
}
