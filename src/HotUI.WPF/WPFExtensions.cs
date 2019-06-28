﻿using System;
using System.Windows;

namespace HotUI.WPF
{
    public static class WPFExtensions
    {
        static WPFExtensions()
        {
            UI.Init();
        }

        public static IUIElement ToIUIElement(this View view)
        {
            if (view == null)
                return null;
            var handler = view.ViewHandler;
            if (handler == null)
            {
                handler = Registrar.Handlers.GetRenderer(view.GetType());
                view.ViewHandler = handler;
            }

            var iUIElement = handler as IUIElement;
            return iUIElement;
        }

        public static UIElement ToView(this View view)
        {
            var handler = view.ToIUIElement();
            return handler?.View;
        }

        public static UIElement ToEmbeddableView(this View view)
        {
            var handler = view.ToIUIElement();
            if (handler == null)
                throw new Exception("Unable to build handler for view");

            return new HotUIContainerView(view);
        }
    }
}