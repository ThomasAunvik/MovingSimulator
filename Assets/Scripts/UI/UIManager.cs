using MovingSim;
using UnityEngine;
using TMPro;

using MovingSim.Player;

namespace MovingSim.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private GameObject dialogue;
        [SerializeField] private GameObject throwOrKeep;

        [SerializeField] private TMP_Text dialogueText;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text descriptionText;

        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Transform meshTransfrom;
        [SerializeField] private Vector3 rotationSpeed;

        public bool uiOpen { get; private set; }

        private Item currentItem;

        private void LateUpdate()
        {
            if (uiOpen)
            {
                meshTransfrom.Rotate(rotationSpeed * Time.deltaTime);
            }
        }

        public void OpenDialogue(Item item)
        {
            dialogue.SetActive(true);
            dialogueText.text = item.dialogue;
        }

        public void CloseDialogue()
        {
            dialogue.SetActive(false);
        }

        public void OpenThrowOrKeep(Item item)
        {
            dialogue.SetActive(false);
            throwOrKeep.SetActive(true);

            if(nameText != null) nameText.text = item.itemName;
            descriptionText.text = item.description;
            uiOpen = true;
            currentItem = item;

            meshFilter.mesh = item.GetMesh();
            meshRenderer.materials = item.GetMaterials();

            meshTransfrom.rotation = Quaternion.identity;
            meshTransfrom.localScale = item.transform.localScale;
        }

        public void CloseThrowOrKeeep()
        {
            throwOrKeep.SetActive(false);
            uiOpen = false;
            currentItem = null;
            player.UnselectItem();
        }

        public void TrashCurrentItem()
        {
            player.UnselectItem();
            currentItem.Trash();
            CloseThrowOrKeeep();
        }

        public void KeepCurrentItem()
        {
            currentItem.Keep();
            CloseThrowOrKeeep();
        }
    }
}
