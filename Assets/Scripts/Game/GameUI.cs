using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ShootEmUp
{
    public sealed class GameUI :
        MonoBehaviour,
        IGameStartListener,
        IGamePauseListener,
        IGameResumeListener,
        IGameFinishListener
    {
        private GameManager _gameManager;

        private Button _startButton;
        private Button _pauseButton;
        private Text _countdownText;
        private Text _gameOverText;

        public void Construct(GameManager gameManager)
        {
            this._gameManager = gameManager;
            this.BuildUI();
        }

        public void OnStartGame()
        {
            this._startButton.gameObject.SetActive(false);
            this._pauseButton.gameObject.SetActive(true);
        }

        public void OnPauseGame()
        {
        }

        public void OnResumeGame()
        {
        }

        public void OnFinishGame()
        {
            this._pauseButton.gameObject.SetActive(false);
            this._countdownText.gameObject.SetActive(false);
            this._gameOverText.gameObject.SetActive(true);
        }

        public void SetCountdown(string value)
        {
            this._countdownText.text = value;
            this._countdownText.gameObject.SetActive(true);
        }

        public void HideCountdown()
        {
            this._countdownText.gameObject.SetActive(false);
        }

        private void OnStartClicked()
        {
            this._gameManager.StartGame();
        }

        private void OnPauseClicked()
        {
            if (this._gameManager.State == GameState.Playing)
            {
                this._gameManager.PauseGame();
            }
            else if (this._gameManager.State == GameState.Paused)
            {
                this._gameManager.ResumeGame();
            }
        }

        private void BuildUI()
        {
            var canvas = new GameObject("GameCanvas");
            canvas.transform.SetParent(this.transform);
            var canvasComponent = canvas.AddComponent<Canvas>();
            canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();

            var eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();

            this._startButton = this.CreateButton(canvas.transform, "StartButton", "START");
            this._startButton.onClick.AddListener(this.OnStartClicked);

            this._pauseButton = this.CreateButton(canvas.transform, "PauseButton", "PAUSE");
            this._pauseButton.onClick.AddListener(this.OnPauseClicked);
            this._pauseButton.gameObject.SetActive(false);

            this._countdownText = this.CreateText(canvas.transform, "CountdownText", string.Empty);
            this._countdownText.fontSize = 80;
            this._countdownText.gameObject.SetActive(false);

            this._gameOverText = this.CreateText(canvas.transform, "GameOverText", "GAME OVER");
            this._gameOverText.fontSize = 60;
            this._gameOverText.gameObject.SetActive(false);
        }

        private Button CreateButton(Transform parent, string name, string label)
        {
            var buttonGo = new GameObject(name, typeof(RectTransform));
            buttonGo.transform.SetParent(parent, false);

            var image = buttonGo.AddComponent<Image>();
            image.color = new Color(0.15f, 0.15f, 0.15f, 0.85f);

            var button = buttonGo.AddComponent<Button>();
            var colors = button.colors;
            colors.normalColor = new Color(0.3f, 0.6f, 0.9f);
            colors.highlightedColor = new Color(0.4f, 0.7f, 1.0f);
            colors.pressedColor = new Color(0.2f, 0.5f, 0.8f);
            button.colors = colors;

            var rect = buttonGo.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(220f, 70f);

            var labelGo = new GameObject("Label", typeof(RectTransform));
            labelGo.transform.SetParent(buttonGo.transform, false);
            var labelText = labelGo.AddComponent<Text>();
            labelText.text = label;
            labelText.alignment = TextAnchor.MiddleCenter;
            labelText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            labelText.color = Color.white;
            labelText.fontSize = 28;
            var labelRect = labelGo.GetComponent<RectTransform>();
            labelRect.anchorMin = Vector2.zero;
            labelRect.anchorMax = Vector2.one;
            labelRect.sizeDelta = Vector2.zero;

            return button;
        }

        private Text CreateText(Transform parent, string name, string content)
        {
            var textGo = new GameObject(name, typeof(RectTransform));
            textGo.transform.SetParent(parent, false);

            var text = textGo.AddComponent<Text>();
            text.text = content;
            text.alignment = TextAnchor.MiddleCenter;
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.color = Color.white;
            text.fontSize = 40;

            var rect = textGo.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(400f, 120f);

            return text;
        }
    }
}
