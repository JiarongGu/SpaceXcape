using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour, IPointerClickHandler
{
    public string levelScene;

    public SelectionImage selectionImage;

    private List<SelectionImage> allSelectionImages;

    private void Start()
    {
        allSelectionImages = FindObjectsOfType<SelectionImage>().ToList();
        selectionImage.GetComponent<Image>().enabled = false;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        allSelectionImages.ForEach(x => x.GetComponent<Image>().enabled = false);
        selectionImage.GetComponent<Image>().enabled = true;
        GameSelection.SelectedLevel = levelScene;
    }
}
