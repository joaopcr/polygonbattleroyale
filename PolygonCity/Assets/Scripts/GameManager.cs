using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public MatchSettings matchSettings;

	[SerializeField]
	private GameObject sceneCamera;

	void Awake ()
	{
		if (instance != null) {
			Debug.LogError ("More than one GameManager in scene.");
		} else {
			instance = this;
		}
	}

	void Update ()
	{
		if (players.Count >= matchSettings.maxPlayers && SceneManager.GetActiveScene().name == "MP") {
			//ComecarJogo ();
		}
	}

	public void SetSceneCameraActive(bool isActive){
		if (sceneCamera == null)
			return;

		sceneCamera.SetActive (isActive);
	}

	public GameObject networkManager;

	void ComecarJogo ()
	{
		networkManager.GetComponent<MyNetwork> ().MudarScene ("Jogo");
	}

	private const string PLAYER_ID_PREFIX = "Player ";

	private static Dictionary<string, Player> players = new Dictionary<string, Player> ();

	public static void RegisterPlayer (string _netID, Player _player)
	{		
		string _playerID = PLAYER_ID_PREFIX + _netID;

		_player.transform.name = _playerID;
		players.Add (_playerID, _player);
		Debug.Log ("_playerID: " + _playerID + "\t_netID: " + _netID);
	}

	public static void UnRegisterPlayer (string _playerID)
	{
		players.Remove (_playerID);
	}

	public static Player GetPlayer (string _playerID)
	{
		return players [_playerID];
	}

	public static void PrintDictionary ()
	{
		foreach (var item in players) {
			//Debug.Log (item.Key);
		}
	}

	/*void OnGUI ()
	{
		GUILayout.BeginArea (new Rect (200, 200, 200, 500));
		GUILayout.BeginVertical ();

		foreach (string _playerID in players.Keys) {
			GUILayout.Label (_playerID + "  -  " + players [_playerID].transform.name);
		}

		GUILayout.EndVertical ();
		GUILayout.EndArea ();
	}*/

}
