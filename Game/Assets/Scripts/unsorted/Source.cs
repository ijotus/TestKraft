using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public class Source : MonoBehaviour, ISource
    {
        public event EventHandler Capture;
        public event EventHandler Dragg;
        public event EventHandler Drop;

        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

        public Color Color
        {
            get { return _renderer.color; }
            set { _renderer.color = value; }
        }


        void Awake()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
        }
        void OnMouseDrag()
        {
            Dragg.SafeRaise(this, EventArgs.Empty);
        }

        void OnMouseDown()
        {
            Capture.SafeRaise(this, EventArgs.Empty);
        }

        void OnMouseUp()
        {
            Drop.SafeRaise(this, EventArgs.Empty);
        }

        Collider2D _collider;
        SpriteRenderer _renderer;
    }
}