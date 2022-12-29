/// <summary>
/// Script de trigger qui premet de prendre la quete
/// </summary>
using UnityEngine;

public class LearnTaker : MonoBehaviour
{
    //variable globale de la quete en question (abilities)
    MentorGiver _mentorGiver;


    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        // si je rentre en contact avec le mentor
        if (other.gameObject.tag == "Mentor")
        {
            //Que le quete n'a pas encore ete accepter
            if (_mentorGiver == null)
            {
                // Je recuperation du script MentorGiver
                _mentorGiver = other.gameObject.GetComponent<MentorGiver>();


                if (!_mentorGiver.LearnAbilities.IsActive)// quete non active
                {
                    //j'affiche le panel de la quete
                    _mentorGiver.CanvaLearning.SetActive(true);

                    _mentorGiver.LearnInfo[0].text = _mentorGiver.LearnAbilities.Title;
                    _mentorGiver.LearnInfo[1].text = _mentorGiver.LearnAbilities.Description;

                }
                else // quete active
                {
                    //TODO:...afficher canvas d'attente
                    if (_mentorGiver.LearnAbilities.IsCompleted)// quete terminer
                    {
                        print("abiities ok");
                        GetComponent<PlayerCollision>().ShowDialCanvaTxt(_mentorGiver.QuestCompletedMessage);
                        //_mentorGiver.HideObjectAfterQuest();
                        _mentorGiver.ShowObjectAfterTaken();
                    }
                }



            }

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Mentor")
        {
            //masque le panel
            _mentorGiver.CanvaLearning.SetActive(false);
            _mentorGiver = null;
        }
    }

    //gestins du panel button
    public void TakeAbilities()
    {
        _mentorGiver.LearnAbilities.IsActive = true;
        _mentorGiver.LearnAbilities.Pickup.SetActive(true);
        _mentorGiver.CanvaLearning.SetActive(false);


    }
}
