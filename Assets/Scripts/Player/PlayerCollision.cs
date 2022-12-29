/// <summary>
/// Script de gestions des collisions du Player avec les objets de l'environnement
/// </summary>
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    PlayerInput _playerInput;
    GameObject _otherObject;
    SignBehaviourScript _sb;

    //Canvas lié aux Pnj
    [SerializeField] GameObject _dialCanvasPnj;
    [SerializeField] TMP_Text _txtCanvasPnj;


    //tableau pour le ramassage des pickup de capacitées
    [SerializeField] MentorGiver[] _mentorAbilitiesquestGivers;

    


    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        #region CONDITIONS INTERACT PANNEAUX
        //teste si le player appui sur le touche E
        if (_playerInput.Interact && _otherObject != null && !_sb.TextCanvas.activeInHierarchy)
        {
            //methode affichant la boite de dialogue
            ShowDial();

        }
        #endregion
    }

    //Si le player rentre en coll avec le panneaux
    //j'accede au script du panneau
    //et j'active le Button pour afficher la boite de dialogue
    void OnTriggerEnter2D(Collider2D other)
    {
        #region CONDITIONS TRIGGER ENTER PANNEAUX
        //Detection avec les panneaux
        if (other.gameObject.tag == "Sign")
        {
            _otherObject = other.gameObject;
            _sb = other.gameObject.GetComponent<SignBehaviourScript>();
            _sb.UI.SetActive(true);

        }
        #endregion

        #region CONDITIONS TRIGGER ENTER ABILITIES PICKUPS
        ////si je rentre en collision avec un pickup
        //if (other.gameObject.tag == "pickupAbilities")
        //{
        //     //si la quete est active
        //    if (_mentorAbilitiesquestGivers[0].LearnAbilities.IsActive == true)
        //    {
        //
        //        //j'incremente la viariable tableau de l'object collisionner
        //        _mentorAbilitiesquestGivers[0].LearnAbilities.IncrementCount();
        //       
        //    }
        //    
        //
        //}
        if (other.gameObject.tag == "pickupAbilitiesSprint")
        {
            //si la quete est active
            if (_mentorAbilitiesquestGivers[0].LearnAbilities.IsActive == true)
            {

                //j'incremente la viariable tableau de l'object collisionner
                _mentorAbilitiesquestGivers[0].LearnAbilities.IncrementCount();

            }


        }
        if (other.gameObject.tag == "pickupAbilitiesJump")
        {
            //si la quete est active
            if (_mentorAbilitiesquestGivers[1].LearnAbilities.IsActive == true)
            {

                //j'incremente la viariable tableau de l'object collisionner
                _mentorAbilitiesquestGivers[1].LearnAbilities.IncrementCount();

            }


        }
        if (other.gameObject.tag == "pickupAbilitiesDash")
        {
            //si la quete est active
            if (_mentorAbilitiesquestGivers[2].LearnAbilities.IsActive == true)
            {

                //j'incremente la viariable tableau de l'object collisionner
                _mentorAbilitiesquestGivers[2].LearnAbilities.IncrementCount();

            }


        }
        if (other.gameObject.tag == "pickupAbilitiesAttack")
        {
            //si la quete est active
            if (_mentorAbilitiesquestGivers[3].LearnAbilities.IsActive == true)
            {

                //j'incremente la viariable tableau de l'object collisionner
                _mentorAbilitiesquestGivers[3].LearnAbilities.IncrementCount();

            }


        }
        #endregion

    }

    //quant le Player quitte de coll du panneaux
    //je masque le button E et la boite de dialogue
    void OnTriggerExit2D(Collider2D other)
    {
        #region CONDITIONS TRIGGER EXIT PANNEAUX
        if (other.gameObject.tag == "Sign")
        {
            _otherObject = null;
            _sb.UI.SetActive(false);
            //methode qui ferme le panneau 1sec apres la sortie du trigger (APRES 1SEC)
            Invoke("HidePanel", 1);
        }
        #endregion

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        #region CONDITION COLLISION ENTER PNJ
        if (other.gameObject.tag == "Pnj")
        {
            ShowDialCanvaTxt(other.gameObject.GetComponent<PnjSimpleDial>().SimpleDial);
            //_dialCanvasPnj.SetActive(true);
            //_txtCanvasPnj.text = other.gameObject.GetComponent<PnjSimpleDial>().SimpleDial;
           
        }
        #endregion
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        #region CONDITION COLLISION EXIT PNJ
        if (other.gameObject.tag == "Pnj")
        {
            Invoke("HidePanelPnj", 0.5f);
        }
        #endregion
    }

    #region METODE HIDE PANNEL (MASQUE LE PANNEAUX)
    //desactive la boite de dialogue
    public void HidePanel()
    {
        _sb.TextCanvas.SetActive(false);
    }
    #endregion

    #region HIDE PANEL DIAL PNJ (MASQUE LA PANNEAUX DE DIAL D'UN PNJ)
    public void HidePanelPnj()
    {
        _dialCanvasPnj.SetActive(false);
    }
    #endregion

    #region SHOWDIAL (AFFICHE LE DIALOGUE DU PANNEAUX)
    //metthode affichant la boite de dialogue secondaire
    public void ShowDial()
    {
        _sb.TextCanvas.SetActive(true);
        _sb.TmpText.text = _sb.SignTexte;
        _sb.UI.SetActive(false);
    }
    #endregion

    #region SHOW DIAL CANVAS TXT (AFFICHE LE DIALOGUE DES PNJ)
    public void ShowDialCanvaTxt(string msg)
    {
        _dialCanvasPnj.SetActive(true);
        _txtCanvasPnj.text = msg;
        Invoke("HidePanelPnj", 1);
    }
    #endregion
}

