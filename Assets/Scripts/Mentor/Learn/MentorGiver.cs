/// <summary>
/// Script qui contiens les detaile de la quete ce script est posé sur le LearnMentor
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MentorGiver : MonoBehaviour
{
    [Header("Classe de quete")]
    [SerializeField] LearnAbilities _learnAbilities;

    [Header("Canvas de dialogue du mentor")]
    [SerializeField] GameObject _canvaLearning;

    [Header("Information de la quete")]
    [SerializeField] TMP_Text[] _learnInfo;

    [Header("message de fin de quete" )]
    [SerializeField] string _QuestCompletedMessage;

    [Header("Tableau de gameObject qui disparaitrons a la fin de la quete")]
    [SerializeField] GameObject[] _toHideAfterQuestCompleted;

    [Header("Tableau de gameObject qui apparaitront au debut de la quete")]
    [SerializeField] GameObject[] _toShowAfterQuestTaken;

    public LearnAbilities LearnAbilities { get => _learnAbilities; set => _learnAbilities = value; }
    public GameObject CanvaLearning { get => _canvaLearning; set => _canvaLearning = value; }
    public TMP_Text[] LearnInfo { get => _learnInfo; set => _learnInfo = value; }
    public string QuestCompletedMessage { get => _QuestCompletedMessage; set => _QuestCompletedMessage = value; }
    public GameObject[] ToHideAfterQuestCompleted { get => _toHideAfterQuestCompleted; set => _toHideAfterQuestCompleted = value; }


    //METHODE QUI MASQUE DES GAMEOBJECT SPECIFIQUE QUANT LA QUETE EST TERMINER
    public void HideObjectAfterQuest()
    {
        foreach(GameObject go in _toHideAfterQuestCompleted)
        {
            go.SetActive(false);    
        }
    }

    //METHODE QUI ACTIVE LES OBJETS DE LA QUETE PRISE
   public void ShowObjectAfterTaken()
   {
       foreach (GameObject go in _toShowAfterQuestTaken)
       {
           go.SetActive(true);
       }
   }

}
