using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;

namespace game
{
    public class Receiver : MonoBehaviour, IReceiver
    {
        public event EventHandler<EventArgsGeneric<ISource>> CollisionEnter;
        public event EventHandler<EventArgsGeneric<ISource>> CollisionExit;

        public Vector3 Position { get { return transform.position; } set { transform.position = value; } }

        public Color Color
        {
            get { return _renderer.color; }
            set { _renderer.color = value; }
        }

        private void Awake()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var source = other.gameObject.GetComponent<ISource>();
            if (source != null)
                CollisionEnter.SafeRaise(this, new EventArgsGeneric<ISource>(source));
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var source = other.gameObject.GetComponent<ISource>();
            if (source != null)
                CollisionExit.SafeRaise(this, new EventArgsGeneric<ISource>(source));
        }

        SpriteRenderer _renderer;
    }
}