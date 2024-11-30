using System.Collections;
using System.Collections.Generic;
using Fusion;
using FusionExamples.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
	public class LevelManager : NetworkSceneManagerDefault
	{
		public const int LAUNCH_SCENE = 0;
		public const int LOBBY_SCENE = 1;
		
		//[SerializeField] private UIScreen _dummyScreen;
		//[SerializeField] private UIScreen _lobbyScreen;
		//[SerializeField] private CanvasFader fader;
		
		[SerializeField] private GameObject LoadingScreen;
		
		/// <summary>
		/// StageSelectScreen : Host
		/// TeamDialogScreen : Guest
		/// </summary>
		[SerializeField] private UIScreen StageSelectScreen;
		[SerializeField] private UIScreen TeamDialogScreen;

		public static LevelManager Instance => Singleton<LevelManager>.Instance;
		
		public static void LoadMenu()
		{
			Instance.Runner.LoadScene(SceneRef.FromIndex(LOBBY_SCENE));
		}
		

		// ステージ選択すると呼ばれる
		public static void LoadTrack(int sceneIndex)
		{
			Instance.Runner.LoadScene(SceneRef.FromIndex(sceneIndex));
		}

		protected override IEnumerator LoadSceneCoroutine(SceneRef sceneRef, NetworkLoadSceneParameters sceneParams)
		{
			Debug.Log($"Loading scene {sceneRef}");

			PreLoadScene(sceneRef.AsIndex);
			
			yield return base.LoadSceneCoroutine(sceneRef, sceneParams);
			
			// Delay one frame, so we're sure level objects has spawned locally
			yield return null;
			
			// Now we can safely spawn karts
			if (GameManager.CurrentTrack != null && sceneRef.AsIndex > LOBBY_SCENE)
			{
				if (Runner.GameMode == GameMode.Host)
				{
					foreach (var player in RoomPlayer.Players)
					{
						player.GameState = RoomPlayer.EGameState.GameCutscene;
						GameManager.CurrentTrack.SpawnPlayer(Runner, player);
					}
				}
			}

			if (sceneRef.AsIndex > LOBBY_SCENE)
			{
				PostLoadScene();
			}
			
		}

		private void PreLoadScene(int scene)
		{
			if (scene > LOBBY_SCENE)
			{
				// Show an empty dummy UI screen - this will stay on during the game so that the game has a place in the navigation stack. Without this, Back() will break
				Debug.Log("Showing Dummy");
				//UIScreen.Focus(_dummyScreen);
				LoadingScreen.SetActive(true);
			}
			else if(scene==LOBBY_SCENE)
			{
				this.gameObject.GetComponent<Canvas>().enabled = true;
				foreach (RoomPlayer player in RoomPlayer.Players)
				{
					player.IsReady = false;
				}
				//UIScreen.activeScreen.BackTo(_lobbyScreen);
				if (RoomPlayer.Local.IsLeader)
				{
					UIScreen.activeScreen.BackTo(StageSelectScreen);
				}
				else
				{
					UIScreen.activeScreen.BackTo(TeamDialogScreen);
				}
				
			}
			else
			{
				UIScreen.BackToInitial();
			}
			//fader.gameObject.SetActive(true);
			//fader.FadeIn();
		}
	
		private void PostLoadScene()
		{
			//fader.FadeOut();
			LoadingScreen.SetActive(false);
			this.gameObject.GetComponent<Canvas>().enabled = false;		// SetActive(false)でもいいけど、一応残しとく。
		}
	}
}