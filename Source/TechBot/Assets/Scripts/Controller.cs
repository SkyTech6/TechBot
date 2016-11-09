using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controller : MonoBehaviour {

	TwitchIRC irc;

	public Button bHome;
	public Button bLaunch;
	public Button bSettings;
	public Button bLink;
	public Button bGetAuth;

	public GameObject pHome;
	public GameObject pLaunch;
	public GameObject pSettings;
	public GameObject pLink;

	public GameObject currentPanel;

	void Start()
	{
		irc = this.GetComponent<TwitchIRC> ();
		currentPanel = pHome;

		bHome.onClick.AddListener (() => ActivateHome ());
		bLaunch.onClick.AddListener (() => LaunchBot ());
		bSettings.onClick.AddListener (() => ActivateSettings ());
		bLink.onClick.AddListener (() => ActivateLink ());

		bGetAuth.onClick.AddListener (() => GetAuth ());

	}

	void ActivateHome()
	{
		currentPanel.SetActive (false);
		pHome.SetActive (true);
		currentPanel = pHome;
	}

	void LaunchBot()
	{
		currentPanel.SetActive (false);
		pLaunch.SetActive (true);
		currentPanel = pLaunch;
		irc.OnButtonLaunch ();
		bLaunch.onClick.RemoveAllListeners ();
		bLaunch.GetComponentInChildren<Text> ().text = "Disable Bot";
		bLaunch.onClick.AddListener (() => DisableBot ());
	}

	void DisableBot()
	{
		bLaunch.onClick.RemoveAllListeners ();
		ActivateHome ();
		XMLManager.xmlMan.SaveUsers ();
		irc.OnButtonDisable ();
		bLaunch.GetComponentInChildren<Text> ().text = "Launch Bot";
		bLaunch.onClick.AddListener (() => LaunchBot ());
	}

	void ActivateSettings()
	{
		currentPanel.SetActive (false);
		pSettings.SetActive (true);
		currentPanel = pSettings;
	}

	void ActivateLink()
	{
		currentPanel.SetActive (false);
		pLink.SetActive (true);
		currentPanel = pLink;
	}

	void GetAuth()
	{
		Application.OpenURL ("https://twitchapps.com/tmi/");
	}
		
}
