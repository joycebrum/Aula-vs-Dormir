using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public PlayerAttributes playerAttributes;
    public int rendimento;
    [SerializeField] private int rendMinimo = 75;

    [Header("Elementos da UI")]
    [SerializeField] private Image rendimentoImg;
    [SerializeField] private Text rendimentoText;
    [SerializeField] private Image bathroomSkillImg;
    [SerializeField] private Text bathroomSkillText;
    [SerializeField] private Image waterSkillImg;
    [SerializeField] private Text waterSkillText;
    [SerializeField] private List<Sprite> mySpriteList;
    [SerializeField] private SpriteRenderer PlayerSprite;

	private void Start(){
        rendimento = playerAttributes.rendimentoInicial;
        PlayerSprite = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        StartCoroutine( RendimentoLoop() );
        UpdateRendimentoUI();
        UpdateSkillUI(bathroomSkillText,playerAttributes.passesBanheiro);
        UpdateSkillUI(waterSkillText,playerAttributes.passesAgua);
    }
    public void PerderRendimento(int perda = 1){
        print("Perdeu");
        rendimento -= perda;
        if(rendimento<0)
        {
            rendimento = 0;
        }
        UpdateRendimentoUI();
    }
    public void GanharRendimento(int ganho = 1){
        rendimento += ganho;
        if(rendimento > playerAttributes.rendimentoMaximo)
        {
            rendimento = playerAttributes.rendimentoMaximo;
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
        float numberRend = (float)rendimento / playerAttributes.rendimentoMaximo;
        rendimentoImg.fillAmount = numberRend; //valor entre 0 e 1
        rendimentoText.text = rendimento.ToString();
        if(numberRend>0.8)
        {
            PlayerSprite.sprite = mySpriteList[0];
        }
        else if(numberRend>0.6)
        {
            PlayerSprite.sprite = mySpriteList[1];
        }
        else if(numberRend>0.4)
        {
            PlayerSprite.sprite = mySpriteList[2];
        }
        else if(numberRend>0.2)
        {
            PlayerSprite.sprite = mySpriteList[3];
        }
        else
        {
            PlayerSprite.sprite = mySpriteList[4];
        }
    }
    private IEnumerator RendimentoLoop(){
        var lManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        while(lManager.tempoRestante >= 0){
            PerderRendimento();
            yield return new WaitForSeconds(0.1f);
        }
        if(rendimento>rendMinimo)
        {
            UpdateSceaneScript.updates++;
        }
    }
    /*private IEnumerator AtualizaSpritePlayer()
    {
        var lManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        while (lManager.tempoRestante > 0)
        {
            if()
            yield return new WaitForEndOfFrame();
        }
        //Acorda o Player
    }*/
}
