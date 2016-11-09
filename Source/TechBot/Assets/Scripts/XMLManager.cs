using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLManager : MonoBehaviour {

	public static XMLManager xmlMan;

	public InputField banner;
	public GameObject banPanel;

	void Awake()
	{
		xmlMan = this;
	}

	void Start()
	{
		LoadUsers ();
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.F8)) {
			banPanel.SetActive (true);
		}	

		if (Input.GetKeyDown (KeyCode.Y) && banPanel.activeInHierarchy && !banner.isFocused) {
			BanUser (banner.text);
			banPanel.SetActive (false);
			banner.text.Remove (0);
		}

		if (Input.GetKeyDown (KeyCode.N) && banPanel.activeInHierarchy && !banner.isFocused) {
			banPanel.SetActive (false);
			banner.text.Remove (0);
		}
	}

	//List of users
	public UserDatabase userDB;

	public void SaveUsers()
	{
		XmlSerializer serializer = new XmlSerializer (typeof(UserDatabase));
		FileStream stream = new FileStream (Application.persistentDataPath + "/user_data.xml", FileMode.Create);
		serializer.Serialize (stream, userDB);
		stream.Close ();
	}

	public void LoadUsers()
	{
		XmlSerializer serializer = new XmlSerializer (typeof(UserDatabase));
		FileStream stream = new FileStream (Application.persistentDataPath + "/user_data.xml", FileMode.OpenOrCreate);
		userDB = serializer.Deserialize (stream) as UserDatabase;
		stream.Close ();
	}

	public void AddUser(string username)
	{
		User newUser = new User ();
		newUser.userName = username;
		newUser.currency = 0;
		newUser.banned = 0;
		xmlMan.userDB.list.Add (newUser);
	}

	public void BanUser(string username)
	{
		if (userDB.list.Exists (x => x.userName == username))
			userDB.list.Find (x => x.userName == username).banned = 1;
	}
}

[System.Serializable]
public class User
{
	public string userName;

	public int currency;

	// 0 = false, 1 = true
	public int banned;
}

[System.Serializable]
public class UserDatabase
{
	public List<User> list = new List<User>();	
}