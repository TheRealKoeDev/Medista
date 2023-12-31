﻿using Medista.Ultils;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Medista.Utils
{
    public static class WindowStateHandler
    {
        private static Window? _window;

        public static void Initialize(Window window)
        {

            _window = window;
            _window.Closing += StoreWindowState;

            GlobalKeyPressInterceptor.KeyPressed += KeyPressed;
            Microsoft.Win32.SystemEvents.DisplaySettingsChanged += DisplaySizeChanged;

            LoadWindowDimensions();
        }

        public static void ToggleMaximization()
        {
            if (_window == null)
            {
                return;
            }

            bool isMaximized = _window.WindowState == WindowState.Maximized;
            _window.WindowState = isMaximized ? WindowState.Normal : WindowState.Maximized;
        }

        public static void StoreWindowState(object? sender = null, EventArgs? args = null)
        {
            if (_window == null)
            {
                return;
            }

            bool isMinimized = _window?.WindowState == WindowState.Minimized;
            Properties.Settings.Default.WindowState = isMinimized ? WindowState.Normal.ToString("G") : _window?.WindowState.ToString("G");

            if (_window?.WindowState == WindowState.Normal)
            {
                Properties.Settings.Default.WindowLocation = _window.ToDrawPoint();
                Properties.Settings.Default.WindowSize = _window.ToDrawSize();
            }
            else
            {
                Properties.Settings.Default.WindowLocation = _window!.RestoreBounds.Location.ToDrawPoint();
                Properties.Settings.Default.WindowSize = _window!.RestoreBounds.Size.ToDrawSize();
            }

            Properties.Settings.Default.Save();
        }

        private static void LoadWindowDimensions()
        {
            _window!.WindowState = Enum.TryParse(Properties.Settings.Default.WindowState, false, out WindowState windowState) ? windowState : WindowState.Normal;

            if (Properties.Settings.Default.WindowSize.IsEmpty)
            {
                ResetWindowPositionAndSize(SystemParameters.WorkArea.Size);
            }
            else
            {
                _window.Width = Properties.Settings.Default.WindowSize.Width;
                _window.Height = Properties.Settings.Default.WindowSize.Height;
                _window.Left = Properties.Settings.Default.WindowLocation.X;
                _window.Top = Properties.Settings.Default.WindowLocation.Y;
            }
        }

        private static void DisplaySizeChanged(object? sender, EventArgs? args)
        {
            Size workingAreaSize = GetCurrentWorkingAreaSize();
            ResetWindowPositionAndSize(workingAreaSize);
        }

        private static void ResetWindowPositionAndSize(Size workingAreaSize)
        {
            if (_window == null)
            {
                return;
            }

            Size windowSize = CalulcateRecommendedWindowSize(workingAreaSize);

            _window.Width = windowSize.Width;
            _window.Height = windowSize.Height;

            _window.Left = (workingAreaSize.Width / 2) - (_window.Width / 2);
            _window.Top = (workingAreaSize.Height / 2) - (_window.Height / 2);
        }

        private static Size CalulcateRecommendedWindowSize(Size workingAreaSize)
        {
            double widthFactor = workingAreaSize.Width / 16;
            double heightFactor = workingAreaSize.Height / 9;

            double availableWidth = widthFactor < heightFactor ? workingAreaSize.Width : workingAreaSize.Height * 16 / 9;
            double availableHeight = widthFactor < heightFactor ? workingAreaSize.Width * 9 / 16 : workingAreaSize.Height;

            int width = (int)(availableWidth * 0.7);
            int height = (int)(availableHeight * 0.7);

            return new Size(width, height);
        }

        private static Size GetCurrentWorkingAreaSize()
        {
            WindowInteropHelper windowInteropHelper = new(_window);
            System.Drawing.Size workingAreaSize = Screen.FromHandle(windowInteropHelper.Handle).WorkingArea.Size;

            return new Size(workingAreaSize.Width, workingAreaSize.Height);
        }

        private static void KeyPressed(Keys key)
        {
            if (key == Keys.F11)
            {
                ToggleMaximization();
            }
        }
    }
}