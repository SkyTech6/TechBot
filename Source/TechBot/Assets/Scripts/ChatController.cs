using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine.UI;

[RequireComponent(typeof(TwitchIRC))]
public class ChatController : MonoBehaviour {

	public int maxMessages = 100; //we start deleting UI elements when the count is larger than this var.
	private LinkedList<GameObject> messages =
		new LinkedList<GameObject>();
	private TwitchIRC IRC;

	private List<string> tips = new List<string> ();
	private List<string> tippers = new List<string>();

	public Text tipper;
	public Text tip;
	public float tipDur = 5f;
	public float tipGap = 3f;

	public Image potato;

	//when message is recieved from IRC-server or our own message.
	void OnChatMsgRecieved(string msg)
	{
		//parse from buffer.
		int msgIndex = msg.IndexOf("PRIVMSG #");
		string msgString = msg.Substring(msgIndex + IRC.channelName.Length + 11);
		string user = msg.Substring(1, msg.IndexOf('!') - 1);

		//remove old messages for performance reasons.
		if (messages.Count > maxMessages)
		{
			Destroy(messages.First.Value);
			messages.RemoveFirst();
		}

		//add new message.
		print (user + ": " + msgString);

		//Creates a user account if this is the first time the user has said something.
		if (XMLManager.xmlMan.userDB.list.Exists (x => x.userName == user)) {
		} else {
			XMLManager.xmlMan.AddUser (user);
		}

		if (msg.ToLower ().Contains ("#tip") && !CheckIfBanned(user))
			TipCommand (msgString, user);

		if (msg.ToLower ().Contains ("#potato") && !CheckIfBanned(user))
			StartCoroutine(PotatoCommand ());
		
	}

	//Checks if the user trying to use a command has previously been banned. 
	bool CheckIfBanned(string name)
	{
		if (XMLManager.xmlMan.userDB.list.Exists (x => x.banned == 1))
			return true;
		else
			return false;
	}

	void TipCommand(string msg, string user)
	{
		tips.Add (msg);
		tippers.Add (user);

		StartCoroutine (DisplayTips());
	}

	bool tipDisplayed = false;
	IEnumerator DisplayTips()
	{
		while (tipDisplayed)
			yield return null;

		tipDisplayed = true;
		tip.text = tips [0];
		tipper.text = tippers [0];

		yield return new WaitForSeconds (tipDur);

		tip.text = " ";
		tipper.text = " ";
		tips.RemoveAt (0);
		tippers.RemoveAt (0);
		yield return new WaitForSeconds (tipGap);
		tipDisplayed = false;
	}

	bool hotPotato = false;
	IEnumerator PotatoCommand()
	{
		while (hotPotato)
			yield return null;

		potato.enabled = true;
		hotPotato = true;
		yield return new WaitForSeconds (1.5f);
		potato.enabled = false;
		yield return new WaitForSeconds (5400f);
		hotPotato = false;
	}


	void Start()
	{
		IRC = this.GetComponent<TwitchIRC>();
		//IRC.SendCommand("CAP REQ :twitch.tv/tags"); //register for additional data such as emote-ids, name color etc.
		IRC.messageRecievedEvent.AddListener(OnChatMsgRecieved);

		tip.text = " ";
		tipper.text = " ";
	}
}