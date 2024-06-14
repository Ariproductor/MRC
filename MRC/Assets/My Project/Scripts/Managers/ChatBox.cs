using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Pun;

public class ChatBox : MonoBehaviourPun
{
    public TextMeshProUGUI chatLogText;
    public TMP_InputField chatInput;

    // instance
    public static ChatBox instance;

    void Awake()
    {
        instance = this;

        // Verificar se os componentes foram atribu�dos
        if (chatLogText == null)
        {
            Debug.LogError("chatLogText n�o foi atribu�do no Inspector");
        }

        if (chatInput == null)
        {
            Debug.LogError("chatInput n�o foi atribu�do no Inspector");
        }

        if (photonView == null)
        {
            Debug.LogError("PhotonView n�o est� anexado ao GameObject");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (EventSystem.current.currentSelectedGameObject == chatInput.gameObject)
                BtnEnviarMsg();
            else
                EventSystem.current.SetSelectedGameObject(chatInput.gameObject);
        }
    }

    // Quando o jogador aciona o bot�o enviar mensagem
    public void BtnEnviarMsg()
    {
        if (chatInput.text.Length > 0)
        {
            if (PhotonNetwork.LocalPlayer == null)
            {
                Debug.LogError("LocalPlayer n�o est� dispon�vel. Verifique se voc� est� conectado ao Photon.");
                return;
            }

            if (photonView == null)
            {
                Debug.LogError("PhotonView n�o est� dispon�vel.");
                return;
            }

            photonView.RPC("Log", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, chatInput.text);
            chatInput.text = "";
        }

        EventSystem.current.SetSelectedGameObject(null);
    }

    // chamado quando um jogador digita uma mensagem na caixa de bate-papo
    // envia para todos os jogadores na sala para atualizar sua interface do usu�rio
    [PunRPC]
    void Log(string playerName, string message)
    {
        // atualiza o chat log com as mensagens enviadas
        chatLogText.text += string.Format("<b>{0}:</b> {1}\n", playerName, message);

        // ajusta o tamanho do chat log conforme o tamanho do texto
        chatLogText.rectTransform.sizeDelta = new Vector2(chatLogText.rectTransform.sizeDelta.x, chatLogText.mesh.bounds.size.y + 20);
    }
}
