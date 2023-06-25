using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NarratorController : MonoBehaviour
{
    public Animator animator;
    public bool edit = true;
    public GameObject StateController;

    public List<GameObject> tempGos;
    public string audioName="A";

    public void Start()
    {
        StateController = GameObject.Find("StateController");
        //if(StateController.GetComponent<StateController>().currStage!=1 && StateController.GetComponent<StateController>().currStage!=0 && StateController.GetComponent<StateController>().fitacolaCurrStage!=30)
            //TapNarrator();
    }
    // Start is called before the first frame update
    public void TapNarrator()
    {
        print("narrator");
        // Check if the animator's open bool is false before running the code
        if (!animator.GetBool("open"))
        {
            float msgTime = 20f;

            int currStage = StateController.GetComponent<StateController>().currStage;
            int milestoneCurrentTopic = StateController.GetComponent<StateController>().milestoneCurrentTopic;

            SetTextAndAudio(currStage, milestoneCurrentTopic);
            /*if (currStage != 0)
                StateController.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>(audioName));*/
            animator.SetBool("open", true);
            Invoke("CloseAnimator", msgTime);
        }
        else
        {
            StateController.GetComponent<AudioSource>().Stop();
            animator.SetBool("open", false);
        }
    }

private void SetTextAndAudio(int currStage, int milestoneCurrentTopic)
{
    string[] messagesPsyco = {
        "O módulo de Psicoeducação reúne um conjunto de mensagens psicoeducativas sobre diferentes aspetos relacionados com a perda gestacional precoce e involuntária. Terá de percorrer o ambiente virtual e, recolher os objetos ao longo do percurso. Cada objeto terá uma mensagem psicoeducativa, escute, leia e absorva a informação. Desfrute da sua caminhada.",
        "O módulo de Psicoeducação reúne um conjunto de mensagens psicoeducativas sobre diferentes aspetos relacionados com a perda gestacional precoce e involuntária. Terá de percorrer o ambiente virtual e, recolher os objetos ao longo do percurso. Cada objeto terá uma mensagem psicoeducativa, escute, leia e absorva a informação. Desfrute da sua caminhada.",
        "O tópico presente no percurso contém mensagens psicoeducativas relacionadas com os efeitos da perda na vida conjugal. O mapa estará disponível para se situar no ambiente virtual",
        "Irá encontrar objetos com mensagens psicoeducativas focadas nos sentimentos. Lembre-se que pode consultar o mapa para se orientar.",
        "Ao longo do caminho terá objetos com mensagens psicoeducativas relacionadas com os efeitos da perda gestacional nas suas interações familiares e sociais.",
        "No módulo Validação da Perda terá a oportunidade de realizar atividades relacionadas com rituais de homenagem. Estas atividades facilitam o processo de validação da perda e, simbolicamente, permitem a criação de algo que remeta para a memória do bebé.",
        "No módulo de Partilha Social terá a oportunidade de partilhar a sua experiência e ouvir relatos de mulheres que tenham passado por uma perda. Este contexto pretende facilitar a partilha sobre a sua experiência e ter contacto com as experiências de outras mulheres.",
        "O módulo de Adaptação à perda disponibiliza recomendações relacionadas com estilo de vida saudável. Poderá encontrar conteúdos sobre: Saúde mental e Autocuidado; Atividade e Saúde Física; Vida Social e Regresso à Rotina."
    };

    string[] audioNames = { "", "A", "D", "B", "C", "E", "H", "J" };

    if (currStage == 0)
    {
        GameObject.Find("Banner").transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "bem vindo ao avir";
    }
    else if (currStage == 1 && milestoneCurrentTopic > 0 && milestoneCurrentTopic < messagesPsyco.Length)
    {
        GameObject.Find("Banner").transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = messagesPsyco[milestoneCurrentTopic];
        audioName = audioNames[milestoneCurrentTopic];
    }
    /*else if (StateController.GetComponent<StateController>().fitacolaCurrStage == 20)
    {
        GameObject.Find("Banner").transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "No jardim virtual terá a oportunidade de criar um espaço com flores, plantas, árvores e outros objetos de decoração. Sempre que quiser poderá modificar os elementos e tirar uma fotografia ao seu espaço final.";
        audioName = "F";
    }
    else if (StateController.GetComponent<StateController>().fitacolaCurrStage == 22)
    {
        GameObject.Find("Banner").transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "No presente cenário terá a oportunidade de escrever ou gravar uma mensagem sobre a sua experiência. Terá a oportunidade de rever ou eliminar os conteúdos criados.";
        audioName = "G";
    }
    else if (StateController.GetComponent<StateController>().fitacolaCurrStage == 31)
    {
        GameObject.Find("Banner").transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "No presente cenário poderá ouvir os testemunhos de mulheres que passaram por uma perda gestacional precoce.";
        audioName = "I";
    }
    else if (StateController.GetComponent<StateController>().fitacolaCurrStage == 30)
    {
        
    }
    else if (StateController.GetComponent<StateController>().fitacolaCurrStage == 40)
    {
        GameObject.Find("Banner").transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "No presente ambiente terá a oportunidade de explorar os diferentes tópicos relacionados com um estilo de vida saudável. A cada medalha corresponde um tópico diferente. Poderá rever a informação sempre que assim o desejar.";
        audioName = "K";
    }*/


}

    private void CloseAnimator()
    {
        animator.SetBool("open", false);
    }
}
