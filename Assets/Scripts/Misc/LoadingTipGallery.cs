using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LoadingTipGallery : MonoBehaviour
{
    public GameObject StateController;
    // Start is called before the first frame update
    public string audioName="A";
    void Start()
    {
        StateController = GameObject.Find("StateController");
        
        string currScene = StateController.GetComponent<StateController>().currScene;
        int milestoneCurrentTopic = StateController.GetComponent<StateController>().milestoneCurrentTopic;
        SetTextAndAudio(currScene, milestoneCurrentTopic);
        StateController.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>(audioName));
        
    }

    private void SetTextAndAudio(string currStage, int milestoneCurrentTopic)
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

    string[] audioNames = { "A", "A", "D", "B", "C", "E", "H", "J" };

    if (currStage == "Gallery")
    {
        transform.GetComponent<TextMeshPro>().text = "bem vindo ao avir";
    }
    else if (currStage == "Exploration" && milestoneCurrentTopic >= 0 && milestoneCurrentTopic < messagesPsyco.Length)
    {
        transform.GetComponent<TextMeshPro>().text = messagesPsyco[milestoneCurrentTopic];
        audioName = audioNames[milestoneCurrentTopic];
    }
    else if (currStage == "Garden")
    {
       transform.GetComponent<TextMeshPro>().text = "No jardim virtual terá a oportunidade de criar um espaço com flores, plantas, árvores e outros objetos de decoração. Sempre que quiser poderá modificar os elementos e tirar uma fotografia ao seu espaço final.";
        audioName = "F";
    }
    else if (currStage == "Chest")
    {
        transform.GetComponent<TextMeshPro>().text = "No presente cenário terá a oportunidade de escrever ou gravar uma mensagem sobre a sua experiência. Terá a oportunidade de rever ou eliminar os conteúdos criados.";
        audioName = "G";
    }
    else if (currStage == "Testemunhos")
    {
        transform.GetComponent<TextMeshPro>().text = "No presente cenário poderá ouvir os testemunhos de mulheres que passaram por uma perda gestacional precoce.";
        audioName = "I";
    }
    else if (currStage == "Share")
    {
        transform.GetComponent<TextMeshPro>().text = "Terá a oportunidade de partilhar a sua experiência com a Iva, uma agente virtual que se encontra num ambiente seguro e onde pode expressar-se à vontade";
        audioName = "I2";
    }
    else if (currStage == "Adaptation")
    {
        transform.GetComponent<TextMeshPro>().text = "No presente ambiente terá a oportunidade de explorar os diferentes tópicos relacionados com um estilo de vida saudável. A cada medalha corresponde um tópico diferente. Poderá rever a informação sempre que assim o desejar.";
        audioName = "K";
    }


}
}
