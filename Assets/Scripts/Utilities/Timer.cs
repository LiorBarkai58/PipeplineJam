using System;
using UnityEngine;

namespace Utilities {
    public abstract class Timer {
        protected float InitialTime;
        protected float Time { get; set; }
        public bool IsRunning { get; protected set; }
        
        public virtual float Progress => Time / InitialTime;
        public virtual float TimePassed => Time;

        
        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate { };

        protected Timer(float value) {
            InitialTime = value;
            IsRunning = false;
        }

        public void Start() {
            Time = InitialTime;
            if (!IsRunning) {
                IsRunning = true;
                OnTimerStart.Invoke();
            }
        }

        public void Stop() {
            if (IsRunning) {
                IsRunning = false;
                OnTimerStop.Invoke();
            }
        }
        
        public void Resume() => IsRunning = true;
        public void Pause() => IsRunning = false;
        
        public abstract void Tick(float deltaTime, bool debug = false);

        public void ChangeTime(float newTime) {
            InitialTime = newTime;
            Time = newTime;
        }

        

        public void Display()
        {
            Debug.Log($"Current time: {Time} \n IsRunning: {IsRunning} \n Progress: {Progress}" );
        }

        
    }
    
    //Countdown timer is given a time to countdown from and counts down to 0
    public class CountdownTimer : Timer {
        public CountdownTimer(float value) : base(value) { }
        public override float Progress => 1 - (Time / InitialTime);
        public override float TimePassed => InitialTime - Time;
        public virtual float TimeLeft => Time;
        

        public override void Tick(float deltaTime, bool debug = false)
        {
            if (IsRunning && Time > 0)
            {
                Time -= deltaTime;
            }

            if (IsRunning && Time <= 0)
            {
                Stop();
            }
        }
        
        public bool IsFinished => Time <= 0;
        
        public void Reset()
        {
            Time = InitialTime;
            IsRunning = false;
        }

        public void Reset(float newTime) {
            InitialTime = newTime;
            Reset();
        }
    }
    
    //Stopwatch timer counts up
    public class StopwatchTimer : Timer {
        public StopwatchTimer() : base(0) { }

        public override void Tick(float deltaTime, bool debug = false) {
            if (IsRunning) {
                Time += deltaTime;
            }
        }
        
        public void Reset() => Time = 0;
        
        public float GetTime() => Time;
    }

    public class CountdownWithInterval : CountdownTimer
    {
        public CountdownWithInterval(float value, float interval) : base(value)
        {
            this._interval = interval;
            _intervalTime = interval;
            Time -= interval;
        }

        public event Action OnInterval = delegate { };
        
        private float _intervalTime = 0;

        private readonly float _interval;
        
        public override void Tick(float deltaTime, bool debug = false) {
            if (IsRunning && Time > 0) {
                Time -= deltaTime;
                _intervalTime += deltaTime;
                if (_intervalTime > _interval)
                {
                    _intervalTime = 0;
                    OnInterval?.Invoke();
                }
            }
            
            if (IsRunning && Time <= 0) {
                Stop();
            }
        }
    }
}