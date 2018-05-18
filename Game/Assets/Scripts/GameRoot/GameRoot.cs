using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

namespace game
{
    public class GameRoot  : IGameRoot
    {
        const string KEY = "BEST_SCORES";
        const float PIXELS_PER_UNIT = 100;
        public event EventHandler<EventArgsGeneric<int>> ScoreChanged;
        public int BestScores
        {
            get { return PlayerPrefs.GetInt(KEY,1); }
            set
            {
                PlayerPrefs.SetInt(KEY, value);
                PlayerPrefs.Save();
            }
        }

        public int Scores
        {
            get { return _scores; }
            set
            {
                if (_scores != value)
                {
                    _scores = value;
                    ScoreChanged.SafeRaise(this, new EventArgsGeneric<int>(_scores));
                }
            }
        }

        public GameRoot
            (
            IResourcesManager resourcesManager,
            ITickable tickable,
            IGameConfig config,
            ICoroutine coroutine,
            ICameraManager cameraManager
            )
        {
            _resourcesManager = resourcesManager;
            _tickable = tickable;
            _config = config;
            _coroutine = coroutine;
            _cameraManager = cameraManager;
        }
        public void Start()
        {
            _config.Initialize();
            CreateUI();
            CreateField();
            StartGame();
        }

        void CreateUI()
        {
            var templ = _resourcesManager.Load<GameObject>("prefabs/UIRoot");
            var rootGO = GameObject.Instantiate(templ);
            _uiRoot =    rootGO.GetComponent<IUIRoot>();

            _dialog = rootGO.GetComponentInChildren<IUIDialog>(true);
            _dialog.ClickBtnCancel += (a, b) => { UnityEngine.Application.Quit(); };
            _dialog.ClickBtnOk += (a, b) => { _dialog.HideDialog(); StartGame(); };

                //SCORES
            var scoreTempl = _resourcesManager.Load<GameObject>("prefabs/UIScores");
            var goScore = GameObject.Instantiate(scoreTempl);
            _uiScore = goScore.GetComponent<IUIScore>();
            ScoreChanged+=(a,b)=> { _uiScore.SetScores(b.Object1.ToString()); };
            _uiScore.SetScores(Scores.ToString());
            _uiRoot.AddChild(goScore.transform);

            //TIME
            var timeTempl = _resourcesManager.Load<GameObject>("prefabs/UITime");
            var goTime = GameObject.Instantiate(timeTempl);
            _uiTime = goTime.GetComponent<IUITime>();
            _uiRoot.AddChild(goTime.transform);

        }

        void CreateField()
        {
            int count = 3;
            var templProgress = _resourcesManager.Load<GameObject>("prefabs/UIProgress");
            var templSource   = _resourcesManager.Load<GameObject>("prefabs/Source");
            var templReceiver = _resourcesManager.Load<GameObject>("prefabs/Receiver");

            var offsetX = 1;
            var receiverOffsetX = 2;
            var offsetY = 1;
            var halfScreen      = new Vector2(Screen.width, Screen.height)*(0.5f/ PIXELS_PER_UNIT); 
            Vector3 sourcePos   = new Vector3(-halfScreen.x + offsetX, 0, 0);
            Vector3 receiverPos = new Vector3( halfScreen.x - receiverOffsetX, 0, 0);
            float step = Screen.height / (3f * PIXELS_PER_UNIT);
            
            int i = 0;
            while (count-- > 0)
            {
                var source = GameObject.Instantiate(templSource).GetComponent<ISource>();
             
                _sources.Add(source.GetHashCode(), source);

                var posY = halfScreen.y - (i++ * step) - offsetY;
                sourcePos.y = posY;
                source.Position = sourcePos;
                _sourceInitialPosition.Add(source.GetHashCode(), sourcePos);


                var goProgress = GameObject.Instantiate(templProgress);
                var uiProgress = goProgress.GetComponent<IUIProgress>();
                _uiRoot.AddChild(goProgress.transform);


               


                var receiver = GameObject.Instantiate(templReceiver).GetComponent<IReceiver>();
                receiver.CollisionEnter += OnCollisionEnter;
                receiver.CollisionExit  += OnCollisionExit;
                _receivers.Add(receiver.GetHashCode(), receiver);

                receiverPos.y     = posY;
                receiver.Position = receiverPos;

                var progress = new ProgressHandler(_coroutine);
                progress.ProgressChanged += (a, b) => { uiProgress.SetProgress(b.Object1); };
                progress.ProgressDone += (a, b) =>
                {
                    _circleColors.Remove(HexUtil.ColorToUint(receiver.Color));
                    receiver.Color = GetColor(true, receiver.Color);
                };
                _receiverProgresses.Add(receiver.GetHashCode(), progress);
                var screenPos = _cameraManager.WorldToScreenPoint(receiver.Position + new Vector3(1,0,0));
                goProgress.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z / 10f);

            }
        }

        void StartGame()
        {
            foreach (var source in _sources)
            {
                source.Value.Dragg   += OnDragg;
                source.Value.Drop    += OnDrop;
                source.Value.Capture += OnCapture;
                source.Value.Color = GetColor(false, source.Value.Color);
            }

            foreach (var receiver in _receivers)
            {
                receiver.Value.Color = GetColor(true, receiver.Value.Color);
            }

            Scores = 0;
            _uiScore.SetBestScores(BestScores.ToString());
            _time = _config.Timer;
            _tickable.SecondTick += OnSecondTick;

            foreach(var v in _receiverProgresses)
            {
                v.Value.Start();
            }
        }

        void FinishGame()
        {
            _circleColors.Clear();
            _squareColors.Clear();

            if (BestScores < Scores)
                BestScores = Scores;

            _dialog.SetResult(string.Format("Твой результат {0}", Scores ));
            _dialog.ShowDialog();
            _tickable.SecondTick -= OnSecondTick;
            foreach (var source in _sources)
            {
                source.Value.Dragg   -= OnDragg;
                source.Value.Drop    -= OnDrop;
                source.Value.Capture -= OnCapture;
                source.Value.Position = _sourceInitialPosition[source.Key];
            }

            foreach (var v in _receiverProgresses)
            {
                v.Value.Stop();
            }

        }
        void OnDragg(object sender, EventArgs e)
        {
            var source = (ISource)sender;
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            source.Position = objPosition;
        }

        void OnCapture(object sender, EventArgs e)
        {
            _currentSource = (ISource)sender;
        }

        void OnDrop(object sender, EventArgs e)
        {
            if(_currentSource != null && _currentReceiver != null)
            {
                if(_currentSource.Color == _currentReceiver.Color)
                {
                    Scores += 1;
                    _squareColors.Remove(HexUtil.ColorToUint(_currentSource.Color));
                    _currentSource.Color = GetColor(false, _currentSource.Color);
                }
            }

            var pos = _sourceInitialPosition[_currentSource.GetHashCode()];
            _currentSource.Position = pos;
            _currentSource = null;

        }

        void OnCollisionEnter(object sender, EventArgsGeneric<ISource> e)
        {
            _currentReceiver = (IReceiver)sender;
        }

        void OnCollisionExit(object sender, EventArgsGeneric<ISource> e)
        {
            _currentReceiver = null;
        }

        void OnSecondTick(object sender, EventArgs e)
        {
            _time = Mathf.Max(0,_time - 1);
         
            _uiTime.SetTime(TimeSpan.FromSeconds(_time).ToString());

            if (_time == 0)
                FinishGame();
        }

        Color GetColor(bool forCircle, Color lastColor)
        {
            if(forCircle)
            {
                return GetColor(_circleColors, lastColor);
            }
            else
            {
                return GetColor(_squareColors, lastColor);
            }
        }

        Color GetColor(Dictionary<uint, int> dic, Color lastColor)
        {
            var avaible = _config.Colors.Where((color) =>
            {
                var test = HexUtil.ColorToUint(color);
                return !dic.ContainsKey(test) && lastColor != color;
            });

            var index = UnityEngine.Random.Range(0, avaible.Count());
            var result = avaible.ElementAt(index);
            var cl = HexUtil.ColorToUint(result);
            dic.Add(cl, 0);
            return result;
        }

        Dictionary<uint, int> _circleColors = new Dictionary<uint, int>();
        Dictionary<uint, int> _squareColors = new Dictionary<uint, int>();

        ISource   _currentSource;
        IReceiver _currentReceiver;

        Dictionary<int, IProgressHandler> _receiverProgresses = new Dictionary<int, IProgressHandler>();
        Dictionary<int, Vector3>   _sourceInitialPosition = new Dictionary<int, Vector3>();
        Dictionary<int, ISource>   _sources   = new Dictionary<int, ISource>();
        Dictionary<int, IReceiver> _receivers = new Dictionary<int, IReceiver>();

        private float _time = 0;
        private IUIDialog _dialog;
        private int _scores;
        private IUITime  _uiTime;
        private IUIScore _uiScore;
        private IUIRoot                            _uiRoot;
        private readonly IResourcesManager         _resourcesManager;
        private readonly ITickable                 _tickable;
        private readonly IGameConfig               _config;
        private readonly ICoroutine                _coroutine;
        private readonly ICameraManager            _cameraManager;
    }
}