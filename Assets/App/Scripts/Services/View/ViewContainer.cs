using System;
using System.Collections.Generic;
using App.Scripts.Data;
using App.Scripts.UI;
using UnityEngine;

namespace App.Scripts.Services.View
{
    public class ViewContainer : IViewContainer
    {
        private readonly UIInitialData _initial;
        private readonly Dictionary<Type, BaseView> _views = new();
        private float _match = 0.5f;

        public ViewContainer(UIInitialData parameters)
        {
            _initial = parameters;
            CalculateCanvasMatch();
        }
        
        public T ShowView<T>() where T : BaseView
        {
            var view = GetView<T>(false);
            view.Show();
            return view;
        }

        public T GetView<T>(bool isHide = true) where T : BaseView
        {
            var type = typeof(T);
            if (!_views.ContainsKey(type)) 
                InstantiateView<T>(isHide);
            var view = (T)_views[type];
            return view;
        }
        
        private void InstantiateView<T>(bool isHide) where T : BaseView
        {
            var loadedObject = Resources.Load<T>(DirectoryConstants.ViewsFolderPath + typeof(T).Name);
            var view = UnityEngine.Object.Instantiate(loadedObject, _initial.UIParent);
            view.CorrectCanvasScaler(_match);
            view.Initialize();
            if(isHide)
                view.Hide();
            _views.Add(typeof(T), view);
        }
        
        private void CalculateCanvasMatch()
        {
            var defaultRatio = 1920f / 1080f;
            var currentRatio = (float) Screen.width / Screen.height;
            _match = currentRatio > defaultRatio ? 0f : 1f;
        }
    }
}