using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public PlayerAttributes playerAttributes;
    public GameObject Parent;
    public ObjectCreator cafeCreat;
    private ObjectCreator objCreat;
    public int rendimento;
    [SerializeField] private int rendMinimo = 75;
    //[SerializeField] private List<GameObject> FlyingPrefabes;
    [Header("Elementos da UI")]
    [SerializeField] private Image rendimentoImg;
    Color InitialColor = Color.green;
    [SerializeField] private Text rendimentoText;
    [SerializeField] private Image bathroomSkillImg;
    [SerializeField] private Text bathroomSkillText;
    [SerializeField] private Image waterSkillImg;
    [SerializeField] private Text waterSkillText;
    [SerializeField] private List<Sprite> mySpriteList;
    [SerializeField] private SpriteRenderer PlayerSprite;

    //Campos relacionados ao efeito cafe
    [SerializeField] private int valorCoffee;
    [SerializeField] private float velocidadeItens;//velocidade que os itens recebem ao receber o efeito negativo
    [SerializeField] private Text textCafe;
    [SerializeField] private Text textEfeitoCafe;
    [SerializeField] private Text textEfeitoCafeQuant;

    private int CountCafe;
    public bool coffeeEffect;
    private bool GoodEffect;

    private void Start() {
        coffeeEffect = false;
        CountCafe = 0;
        GoodEffect = false;
        objCreat = GameObject.Find("ObjCreator_Bom").GetComponent<ObjectCreator>();
         PlayerSprite = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        StartCoroutine(RendimentoLoop());
        UpdateRendimentoUI();
        UpdateSkillUI(bathroomSkillText, playerAttributes.passesBanheiro);
        UpdateSkillUI(waterSkillText, playerAttributes.passesAgua);
    }
    public void PerderRendimento(int perda = 1) {
        rendimento -= perda;
        if (rendimento < 0)
        {
            rendimento = 0;
        }
        UpdateRendimentoUI();
    }
    public void GanharRendimento(int ganho = 1) {
        rendimento += ganho;
        if (GoodEffect)
        {
            rendimento += valorCoffee;
        }
        if (rendimento > playerAttributes.rendimentoMaximo)
        {
            rendimento = playerAttributes.rendimentoMaximo;
        }
        UpdateRendimentoUI();
    }
    public void UsarBanheiro() {
        if (playerAttributes.passesBanheiro <= 0) return;
        playerAttributes.passesBanheiro--;
        UpdateSkillUI(bathroomSkillText, playerAttributes.passesBanheiro);
        StartCoroutine(SkillCooldown(bathroomSkillImg, playerAttributes.cooldownBanheiro));
        StartCoroutine(GameObject.Find("LevelManager").GetComponent<LevelManager>().StopCreatorForSeconds(playerAttributes.duracaoBanheiro));
    }
    public void UsarAgua() {
        if (playerAttributes.passesAgua <= 0) return;
        playerAttributes.passesAgua--;
        GanharRendimento(playerAttributes.rendimentoPorAgua);
        UpdateSkillUI(waterSkillText, playerAttributes.passesAgua);
        StartCoroutine(SkillCooldown(waterSkillImg, playerAttributes.cooldownAgua));
    }
    public void UsarCafe(int damage, int duracao)
    {
        CountCafe++;
        textEfeitoCafe.text = "Toque + ";
        textEfeitoCafeQuant.text = "" + damage;
        textCafe.text = "" + CountCafe;
        valorCoffee = damage;
        GoodEffect = true;
        coffeeEffect = true;
        StartCoroutine(CoffeeCooldown(duracao));
    }
    public void EfeitoNegativoCafe(int duracao)
    {
        textEfeitoCafe.text = "Velocidade + ";
        textEfeitoCafeQuant.text = "" + velocidadeItens;
        objCreat.quant = velocidadeItens;
        ChangeVelocity(velocidadeItens);
        StartCoroutine(CoffeeBadCooldown(duracao));
    }
    private IEnumerator CoffeeBadCooldown(int duracao)
    {
        yield return new WaitForSeconds((int)duracao / 2);//espera metade do tempo
        objCreat.quant = 0;
        ChangeVelocity(-velocidadeItens);
        coffeeEffect = false;//acaba o efeito por completo e outro cafe ja pode ser tomado
        cafeCreat.Coffee = false;
        velocidadeItens += 1f;
        textEfeitoCafe.text = "Sem Efeito";
        textEfeitoCafeQuant.text = "";
    }
    private void ChangeVelocity(float quant)
    {
        //FlyingObjects[] Temp = Parent.GetComponentsInChildren<FlyingObjects>();
        GameObject[] Temp = GameObject.FindGameObjectsWithTag("Flying");
        for (int i=0; i<Temp.Length;i++)
        {
            Temp[i].GetComponent<FlyingObjects>().velocity += quant;
        }
    }
    
    private IEnumerator CoffeeCooldown(int duracao)
    {
        yield return new WaitForSeconds(duracao);
        GoodEffect = false;//acabou apenas o efeito bom
        EfeitoNegativoCafe(duracao);
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
        if(numberRend < 0.30f)
        {
            rendimentoImg.color = Color.red;
        }
        else
        {
            rendimentoImg.color = InitialColor;
        }
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
            if(lManager.level.levelType != LevelType.HISTORY)
            {
                PerderRendimento();
            }
            yield return new WaitForSeconds(0.1f);
        }
        if(rendimento>rendMinimo)
        {
            UpdateSceaneScript.updates++;
        }
    }
}
