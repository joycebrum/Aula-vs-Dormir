using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public PlayerAttributes playerAttributes;
    [SerializeField] private int rendimento;
    [SerializeField] private int rendMinimo = 75;

    [Header("Elementos da UI")]
    [SerializeField] private Image rendimentoImg;
    [SerializeField] private Text rendimentoText;
    [SerializeField] private Image bathroomSkillImg;
    [SerializeField] private Text bathroomSkillText;
    [SerializeField] private Image waterSkillImg;
    [SerializeField] private Text waterSkillText;

	private void Start(){
        rendimento = playerAttributes.rendimentoInicial;
        StartCoroutine( RendimentoLoop() );
        UpdateRendimentoUI();
        UpdateSkillUI(bathroomSkillText,playerAttributes.passesBanheiro);
        UpdateSkillUI(waterSkillText,playerAttributes.passesAgua);
    }
    public void PerderRendimento(int perda = 1){
        rendimento -= perda;
        if(rendimento<0)
        {
            rendimento = 0;
        }
        UpdateRendimentoUI();
    }
    public void GanharRendimento(int ganho = 1){
        rendimento += ganho;
        if(rendimento > 100)
        {
            rendimento = 100;
        }
        UpdateRendimentoUI();
    }
    public void UsarBanheiro(){
        if(playerAttributes.passesBanheiro <= 0) return;
        playerAttributes.passesBanheiro--;
        UpdateSkillUI(bathroomSkillText,playerAttributes.passesBanheiro);
        StartCoroutine(SkillCooldown(bathroomSkillImg,playerAttributes.cooldownBanheiro));
        StartCoroutine(GameObject.Find("LevelManager").GetComponent<LevelManager>().StopCreatorForSeconds(playerAttributes.duracaoBanheiro));
    }
    public void UsarAgua(){
        if(playerAttributes.passesAgua <= 0) return;
        playerAttributes.passesAgua--;
        GanharRendimento(playerAttributes.rendimentoPorAgua);
        UpdateSkillUI(waterSkillText,playerAttributes.passesAgua);
        StartCoroutine(SkillCooldown(waterSkillImg,playerAttributes.cooldownAgua));
    }
    private IEnumerator SkillCooldown(Image skillImg, float cooldown){
        Color usedColor = Color.gray;
        skillImg.GetComponent<Button>().interactable = false;
        skillImg.color = usedColor;
        yield return new WaitForSeconds(cooldown);
        skillImg.color = Color.white;
        skillImg.GetComponent<Button>().interactable = true;
    }
    private void UpdateSkillUI(Text skillText,int usosRestantes){
        skillText.text = usosRestantes.ToString();
    }
    private void UpdateRendimentoUI(){
        rendimentoImg.fillAmount = (float)rendimento/playerAttributes.rendimentoInicial;
        rendimentoText.text = rendimento.ToString();
    }
    private IEnumerator RendimentoLoop(){
        var lManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        while(lManager.tempoRestante > 0){
            PerderRendimento();
            yield return new WaitForSeconds(0.1f);
        }
        if(rendimento>rendMinimo)
        {
            UpdateSceaneScript.updates++;
        }
    }
}
