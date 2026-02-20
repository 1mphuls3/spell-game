using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

 /*
  * All code is original work, with Unity Documentation referenced for identifying Unity
  * specific methods and their correct usage and outputs.
  */
public class ChestOpen : MonoBehaviour
{
    [SerializeField] private Animator spriteAnimator;
    [SerializeField] private List<ModifierDefinition> modifiersList;
    [SerializeField] private ModifierItem item;

    private ModifierDefinition modifier;
    private bool isOpen = false;

    private void OnEnable()
    {
        modifier = modifiersList[Random.Range(0, modifiersList.Count)];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isOpen)
        {
            isOpen = true;
            spriteAnimator.SetTrigger("chestOpen");
            StartCoroutine(SpawnItem());
        }
    }

    private IEnumerator SpawnItem()
    {
        yield return new WaitForSeconds(0.5f);
        item.gameObject.SetActive(true);
        item.modifier = modifier;
    }
}
