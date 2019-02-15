using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerController : MonoBehaviour {

    public static PlayerAttributes playerAttributes;

    public GameObject Parent;
    public ObjectCreator cafeCreat;
    private ObjectCreator objCreat;
    public int rendimento;
    [SerializeField] public int rendMinimo = 75;
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
    private int valorCoffee;
    private float velocidadeItens;//velocidade que os itens recebem ao receber o efeito negativo
    private float velocidadeExtraParaItens;
    [SerializeField] private Text textCafe;
    [SerializeField] private Text textEfeitoCafe;
    [SerializeField] private Text textEfeitoCafeQuant;

    public static string playerSaveDataFile = "/playerAttributes.dat";

    private int CountCafe;
    public bool coffeeEffect;
    private bool GoodEffect;

    private void Start() {
        velocidadeItens = 0f;
        velocidadeExtraParaItens = 0f;
        coffeeEffect = false;
        CountCafe = 0;
        GoodEffect = false;
        objCreat = GameObject.Find("ObjCreator_Bom").GetComponent<ObjectCreator>();
        PlayerSprite = GameObject.Find("Player").GetComponent<SpriteRenderer>();
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
        playerAttributes.passesBanheiro -= 1;
        UpdateSkillUI(bathroomSkillText, playerAttributes.passesBanheiro);
        StartCoroutine(SkillCooldown(bathroomSkillImg, playerAttributes.cooldownBanheiro));
        StartCoroutine(GameObject.Find("LevelManager").GetComponent<LevelManager>().StopCreatorForSeconds(playerAttributes.duracaoBanheiro));
    }
    public void UsarAgua() {
        if (playerAttributes.passesAgua <= 0) return;
        playerAttributes.passesAgua -= 1;
        GanharRendimento(playerAttributes.rendimentoPorAgua);
        UpdateSkillUI(waterSkillText, playerAttributes.passesAgua);
        StartCoroutine(SkillCooldown(waterSkillImg, playerAttributes.cooldownAgua));
    }
    public void UsarCafe(int damage, int duracao, int efeitonegativo, float velocidadeInicial)
    {
        ObjectCreator.coffee = false;
        CountCafe++;
        textEfeitoCafe.text = "Toque + ";
        textEfeitoCafeQuant.text = "" + damage;
        textCafe.text = "" + CountCafe;
        valorCoffee = damage;
        velocidadeItens = velocidadeInicial + velocidadeExtraParaItens;
        GoodEffect = true;
        coffeeEffect = true;
        StartCoroutine(CoffeeCooldown(duracao, efeitonegativo));
    }
    public void EfeitoNegativoCafe(int duracao)
    {
        textEfeitoCafe.text = "Velocidade + ";
        textEfeitoCafeQuant.text = "" + velocidadeItens;
        objCreat.valocidadeAMais = velocidadeItens;
        ChangeVelocity(velocidadeItens);
        StartCoroutine(CoffeeBadCooldown(duracao));
    }
    private IEnumerator CoffeeBadCooldown(int duracao)
    {
        yield return new WaitForSeconds((int)duracao);//espera metade do tempo
        objCreat.valocidadeAMais = 0;
        ChangeVelocity(-velocidadeItens);
        coffeeEffect = false;//acaba o efeito por completo e outro cafe ja pode ser tomado
        velocidadeExtraParaItens += 1f;
        ObjectCreator.coffee = true;
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
    
    private IEnumerator CoffeeCooldown(int duracao, int efeitonegativo)
    {
        yield return new WaitForSeconds(duracao);
        GoodEffect = false;//acabou apenas o efeito bom
        EfeitoNegativoCafe(efeitonegativo);
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
    public static PlayerAttributes GetPlayerAttributes()
    {
        if(PlayerController.playerAttributes == null)
        {
            PlayerController.LoadPlayerAttributes();
            return playerAttributes;
        }
        else
        {
            return PlayerController.playerAttributes;
        }
    }
    public static PlayerAttributes getPlayerAttributesInicial()
    {
        PlayerAttributes playerAttributes = new PlayerAttributes();
        playerAttributes.nome = "";
        playerAttributes.rendimentoInicial = 0;
        playerAttributes.rendimentoMaximo = 100;
        playerAttributes.ganhoPorToque = 2;
        playerAttributes.cooldownAgua = 2;
        playerAttributes.cooldownBanheiro = 5;
        playerAttributes.duracaoBanheiro = 5;
        playerAttributes.rendimentoPorAgua = 20;
        playerAttributes.passesAgua = 5;
        playerAttributes.passesBanheiro = 5;
        return playerAttributes;
    }

    public static void SavePlayerAttributes()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + playerSaveDataFile);
        bf.Serialize(file, playerAttributes);
        file.Close();
    }

    public static void LoadPlayerAttributes()
    {
        if(File.Exists(Application.persistentDataPath + playerSaveDataFile))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + playerSaveDataFile, FileMode.Open);
            playerAttributes = new PlayerAttributes();
            playerAttributes = (PlayerAttributes)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            playerAttributes = getPlayerAttributesInicial();
            SavePlayerAttributes();
        }
    }
    public static void ResetPlayerAttributes()
    {
        playerAttributes = getPlayerAttributesInicial();
        SavePlayerAttributes();
    }
}
