﻿using SpaceProject.Services;
using UnityEngine;

namespace SpaceProject.Tools.Components
{
    public class AutoLocatorObject : DontDestroyObject
    {
        [Tooltip("Если NULL - автоопределение")]
        [SerializeField] protected Component component;

        protected override void Awake()
        {
            base.Awake();

            if (component == null)
                component = this;
            ComponentLocator.Register(component);
        }

        protected virtual void OnDestroy()
        {
            ComponentLocator.Unregister(component.GetType());
        }
    }
}