using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChannelInformation : MonoBehaviour {

	public static ChannelInformation channelInfo = null;

	public InputField channelNameText;
	public InputField botNameText;
	public InputField oAuthKeyText;

	public string channelName;
	public string botName;
	public string oAuthKey;


	void Awake () 
	{
		if (channelInfo == null)
			channelInfo = this;
		else
			Destroy (this);

		LoadValues ();
	}

	void Start()
	{
		channelNameText.onEndEdit.AddListener (delegate {
			UpdateChannelName ();
		});

		botNameText.onEndEdit.AddListener (delegate {
			UpdateBotName ();
		});

		oAuthKeyText.onEndEdit.AddListener (delegate {
			UpdateOAuthKey ();
		});
	}

	void UpdateChannelName()
	{
		channelName = channelNameText.text.ToString (); 
		PlayerPrefs.SetString ("channel", channelName);
	}

	void UpdateBotName()
	{
		botName = botNameText.text.ToString ();
		PlayerPrefs.SetString ("bot", botName);
	}

	void UpdateOAuthKey()
	{
		oAuthKey = oAuthKeyText.text.ToLower ();
		PlayerPrefs.SetString ("oauth", oAuthKey);
	}


	void LoadValues()
	{
		channelNameText.text = PlayerPrefs.GetString ("channel");
		botNameText.text = PlayerPrefs.GetString ("bot");
		oAuthKeyText.text = PlayerPrefs.GetString ("oauth");

		channelName = channelNameText.text.ToString ();
		botName = botNameText.text.ToString ();
		oAuthKey = oAuthKeyText.text.ToString ();
	}
}
