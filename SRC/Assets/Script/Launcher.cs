using UnityEngine;


namespace TestingNetwork
{
	public class Launcher : Photon.PunBehaviour
	{
		[Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
		public byte MaxPlayersPerRoom = 8;
		public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;

		private string _gameVersion = "1";

		private void Awake()
		{
			PhotonNetwork.logLevel = Loglevel;
			PhotonNetwork.autoJoinLobby = false;

			PhotonNetwork.automaticallySyncScene = true;
		}
		private void Start()
		{
			Connect();
		}

		public void Connect()
		{
			if (!PhotonNetwork.connected)
			{
				PhotonNetwork.ConnectUsingSettings(_gameVersion);
				return;
			}


			PhotonNetwork.JoinRandomRoom();
		}

		public override void OnConnectedToMaster()
		{
			Debug.LogWarning("I am connected !");
			PhotonNetwork.JoinRandomRoom();
		}

		public override void OnDisconnectedFromPhoton()
		{
			Debug.LogWarning("I am disconnected !");
		}

		public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
		{
			PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
		}

		public override void OnJoinedRoom()
		{
			Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
		}
	}
}