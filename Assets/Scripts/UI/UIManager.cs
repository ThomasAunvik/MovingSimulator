using MovingSim;
using UnityEngine;
using TMPro;

using MovingSim.Player;
using System.Collections;

using UnityEngine.UI;

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

        [SerializeField] private Button letGoButton;

        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Transform meshTransfrom;
        [SerializeField] private Vector3 rotationSpeed;

        public bool uiOpen { get; private set; }

        private IItem currentItem;

        private Coroutine dialogueEnumerator;
        private Coroutine descriptionEnumerator;

        private void LateUpdate()
        {
            if (uiOpen)
            {
                meshTransfrom.Rotate(rotationSpeed * Time.deltaTime, Space.World);
            }
        }

        public void OpenDialogue(IItem item)
        {
            if (dialogueEnumerator != null) StopCoroutine(dialogueEnumerator);
            dialogueEnumerator = StartCoroutine(DisplayText(item.GetDialogue(), dialogueText));
            dialogue.SetActive(true);
        }

        IEnumerator DisplayText(string inText, TMP_Text outText)
        {
            outText.text = "";
            yield return new WaitForSeconds(0.4f);
            for(int wordIndex = 0; wordIndex < inText.Length; wordIndex++)
            {
                char character = inText[wordIndex];
                outText.text += character;
                yield return new WaitForSeconds(0.05f);
            }
        }

        public void CloseDialogue()
        {
            dialogue.SetActive(false);
            if(dialogueEnumerator != null) StopCoroutine(dialogueEnumerator);
        }

        public void OpenCurrentThrowOrKeep()
        {
            if (player == null)
            {
                player = FindObjectOfType<PlayerController>();
            }

            player.OpenThrowOrKeepUIOnCurrentItem();
        }

        public void OpenThrowOrKeep(IItem item)
        {
            if (descriptionEnumerator != null) StopCoroutine(descriptionEnumerator);
            descriptionEnumerator = StartCoroutine(DisplayText(item.GetDescription(), descriptionText));

            dialogue.SetActive(false);
            throwOrKeep.SetActive(true);

            if(nameText != null) nameText.text = item.GetName();

            uiOpen = true;
            currentItem = item;

            Item.MeshViewOffset offset = item.GetViewOffset();

            meshFilter.mesh = item.GetMesh();
            meshRenderer.materials = item.GetMaterials();

            meshTransfrom.localEulerAngles = offset.rotation;
            meshTransfrom.localScale = offset.size * new Vector3(1,1,1);
        }

        public void CloseThrowOrKeeep()
        {
            if (player == null)
            {
                player = FindObjectOfType<PlayerController>();
            }

            throwOrKeep.SetActive(false);
            uiOpen = false;
            currentItem = null;
            player.UnselectItem();

            if (descriptionEnumerator != null) StopCoroutine(descriptionEnumerator);

            int notCompleted = 0;
            for(int i = 0; i < Item.itemList.Count; i++)
            {
                Item item = Item.itemList[i];
                if(item.destroying || item.isKeeping) {
                    continue;
                }
                notCompleted++;
            }

            if(notCompleted == 0)
            {
                UnlockLetGo();
            }
        }

        public void TrashCurrentItem()
        {
            if(player == null)
            {
                player = FindObjectOfType<PlayerController>();
            }
            player.UnselectItem();
            currentItem.Trash();
            CloseThrowOrKeeep();
        }

        public void KeepCurrentItem()
        {
            if (player == null)
            {
                player = FindObjectOfType<PlayerController>();
            }
            currentItem.Keep();
            CloseThrowOrKeeep();
        }

        private void UnlockLetGo()
        {
            letGoButton.gameObject.SetActive(true);
            letGoButton.onClick.AddListener(() => { letGoButton.gameObject.SetActive(false); LevelManager.instance.EndGame(); });
        }
    }
}
