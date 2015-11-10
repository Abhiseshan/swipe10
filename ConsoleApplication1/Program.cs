using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYNCTRLLib;
using SYNCOMLib;
using WindowsInput.Native;
using WindowsDesktop;
using System.Runtime.InteropServices;
using System.Windows;
using System.Diagnostics;

namespace ConsoleApplication1
{
    public class Syn
    {
        //VirtualDesktopManager vdm = new VirtualDesktopManager();
        //VirtualDesktop desktop;

        //Constants
        const int LeftMouseButtonClick = 65537;
        const int VirtualLeftMouseButtonClick = 1114112;

        SynAPICtrl SynTP_API = new SynAPICtrl();
        SynDeviceCtrl SynTP_Dev = new SynDeviceCtrl();
        SynPacketCtrl SynTP_Pack = new SynPacketCtrl();
        SynAPICtrl SynCtrl = new SynAPICtrl();
        int DeviceHandle;

        int maxX = 100000;
        int maxY = 100000;
        int minX = 0;
        int minY = 0;

        //Variables Handling Up and Down Gestures
        int ydelta;
        int y;
        int staticy;

        int ActivationUp = 10000;
        int ActivationDown = 10000;
        int minLengthY = 10000;

        bool isUpActive = false;

        //Variables handling Left and Right Gestures 
        int xdelta;
        int x;
        int staticx;

        int ActivationLeft = 10000;
        int ActivationRight = 10000;
        int minLengthX = 10000;

        //Notification Gesture
        int ySwipeMin = 100000;
        int ySwipeMax = 100000;

        public Syn()
        {
            SynCtrl.Initialize();
            SynCtrl.Activate();
            SynTP_API.Initialize();
            SynTP_API.Activate();
            DeviceHandle = SynCtrl.FindDevice(SynConnectionType.SE_ConnectionAny, SynDeviceType.SE_DeviceTouchPad, -1);
            if (DeviceHandle == -1)
            {
                Console.WriteLine("Touchpad not found");
                Console.Read();
            }
            else {
                Console.WriteLine("Touchpad found");
                SynTP_Dev.Select(DeviceHandle);
                SynTP_Dev.Activate();
                SynTP_Dev.OnPacket += SynTP_Dev_OnPacket;

                //Setting Activation Zone Values
                ActivationUp = SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_YHiBorder);
                ActivationDown = SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_YLoBorder);
                ActivationRight = SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_XHiBorder) - 500;
                ActivationLeft = SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_XLoBorder) + 500;

                maxY = SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_YHiSensor);
                minY = SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_YLoSensor);
                maxX = SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_XHiSensor);
                minX = SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_XLoSensor);

                minLengthY = (maxY - minY) - (maxY - ActivationUp) - (ActivationDown - minY) - 1000;
                minLengthX = (maxX - minX) - (maxX - ActivationRight) - (ActivationLeft - minX) - 2000;

                int mid = (maxY - minY)/2;
                ySwipeMax = (maxY - mid);
                ySwipeMin = (mid - minY);

                //Console.WriteLine(minLengthX);
                //IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
                //desktop = VirtualDesktop.FromId(vdm.GetWindowDesktopId(handle));

                Console.Read();
            }
        }

        private void SynTP_Dev_OnPacket()
        {

            SynTP_Dev.LoadPacket(SynTP_Pack);
            //Console.WriteLine(SynTP_Pack.GetLongProperty(SynPacketProperty.SP_ExtraFingerState));
            try {
                //if (SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_AcquireUnacquireGestures) != -1) Console.WriteLine("SP_AcquireUnacquireGestures " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_AcquireUnacquireGestures));
                //if (SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_Gestures) != 3) Console.WriteLine("SynDeviceProperty.SP_Gestures " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_Gestures));
                //if (SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_IsHScroll) != 0) Console.WriteLine("SynDeviceProperty.SP_IsHScroll " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_IsHScroll));
                //if (SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_MultiFingerGestures) != 7730) Console.WriteLine("SynDeviceProperty.SP_MultiFingerGestures " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_MultiFingerGestures));
                //if (SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_SecondaryGestures) != 0) Console.WriteLine("SynDeviceProperty.SP_SecondaryGestures" + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_SecondaryGestures));
                //if (SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_VerticalScrollingFlags) != 4) Console.WriteLine("SynDeviceProperty.SP_VerticalScrollingFlags " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_VerticalScrollingFlags));
                //if (SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_2FVerticalScrollingFlags) != 69) Console.WriteLine("SynDeviceProperty.SP_2FVerticalScrollingFlags " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_2FVerticalScrollingFlags));
                //Console.WriteLine("ydEKTA= " + SynTP_Pack.GetLongProperty(SynPacketProperty.SP_YDelta));
                //Console.WriteLine("Y= " +SynTP_Pack.GetLongProperty(SynPacketProperty.SP_Y));
                //Console.WriteLine("ymICKEYS= " + SynTP_Pack.GetLongProperty(SynPacketProperty.SP_YMickeys));
                //Console.WriteLine("SP_YDPI= " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_YDPI));
                //Console.WriteLine("SP_YHiBorder= " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_YHiBorder));
                //Console.WriteLine("SP_YHiBorderVScroll= " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_YHiBorderVScroll));
                //Console.WriteLine("SP_YHiRim= " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_YHiRim));
                //Console.WriteLine("SP_YHiSensor= " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_YHiSensor));
                //Console.WriteLine("SP_YHiWideBorder= " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_YHiWideBorder));
                //Console.WriteLine("SP_YLoBorder= " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_YLoBorder));
                //Console.WriteLine("SP_YLoBorderVScroll= " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_YLoBorderVScroll));
                //Console.WriteLine("SP_YLoRim= " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_YLoRim));
                //Console.WriteLine("SP_YLoSensor= " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_YLoSensor));
                //Console.WriteLine("SP_YLoWideBorder= " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_YLoWideBorder));
                //Console.WriteLine("Click = " + SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_1FingerPressButtonAction));
                //Console.WriteLine("staticY = " + staticy);

                //Console.WriteLine("X= " +SynTP_Pack.GetLongProperty(SynPacketProperty.SP_X));

                //Up and Down Swipe Gestures

                if (SynTP_Pack.GetLongProperty(SynPacketProperty.SP_ButtonState) == LeftMouseButtonClick || SynTP_Pack.GetLongProperty(SynPacketProperty.SP_ButtonState) == VirtualLeftMouseButtonClick)
                    if (isUpActive)
                        isUpActive = false;

                ydelta = SynTP_Pack.GetLongProperty(SynPacketProperty.SP_YDelta);
                y = SynTP_Pack.GetLongProperty(SynPacketProperty.SP_Y);

                if (isValidGesture(SynTP_Pack.GetLongProperty(SynPacketProperty.SP_ExtraFingerState)) && ydelta > 0)
                {
                    if (y >= ActivationUp)
                        executeUp(1);
                    else
                        staticy += ydelta;
                }

                else if (isValidGesture(SynTP_Pack.GetLongProperty(SynPacketProperty.SP_ExtraFingerState)) && ydelta < 0)
                {
                    if (y <= ActivationDown && -staticy >= minLengthY) {
                        executeDown();
                    }
                    else staticy += ydelta;
                }

                else
                {
                    if (-staticy >= minLengthY)
                    {
                        executeDown();
                    }
                    else if (staticy >= minLengthY)
                        executeUp(2);
                    staticy = 0;
                }

                //Left and Right Swipe Gestures
                xdelta = SynTP_Pack.GetLongProperty(SynPacketProperty.SP_XDelta);
                x = SynTP_Pack.GetLongProperty(SynPacketProperty.SP_X);

                if (isValidGesture(SynTP_Pack.GetLongProperty(SynPacketProperty.SP_ExtraFingerState)) && xdelta > 0)
                {
                    if (x >= ActivationUp && staticx >= minLengthX)
                        executeLeft();
                    else
                        staticx += xdelta;
                }

                else if (isValidGesture(SynTP_Pack.GetLongProperty(SynPacketProperty.SP_ExtraFingerState)) && xdelta < 0)
                {
                    if (x <= ActivationDown && -staticx >= minLengthX)
                    {
                        executeRight();
                    }
                    else staticx += xdelta;
                }

                else
                {
                    if (-staticx >= minLengthX)
                    {
                        executeRight();
                    }
                    else if (staticx >= minLengthX)
                        executeLeft();
                    staticx = 0;
                }
                 
                //Console.WriteLine("yswipe Max " + ySwipeMax);
                //Console.WriteLine("yswipemin " + ySwipeMin);
                //if ((x >= SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_XHiBorder) && x <= maxX) && -xdelta >= 10 && (y <= ySwipeMax && y >= ySwipeMin))  Console.WriteLine("true");

                if ((x>= (SynTP_Dev.GetLongProperty(SynDeviceProperty.SP_XHiBorder)) && x<= maxX) && -xdelta >= 10 && (y<=ySwipeMax && y >= ySwipeMin))
                {
                    WindowsInput.InputSimulator kb = new WindowsInput.InputSimulator();
                    kb.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_A);
                }

            }
            catch (ArgumentException)
            {
                Console.Write(":(");
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        void executeUp(int area)
        {
            Console.WriteLine("up: " + area);
            if (!isUpActive) {
                isUpActive = true;
                WindowsInput.InputSimulator kb = new WindowsInput.InputSimulator();
                kb.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.TAB);
            }

                
            //vdm.GetWindowDesktopId
            //var desktop = VirtualDesktop.FromHwnd();
            //IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
            //VirtualDesktop desktop = VirtualDesktop.FromId(vdm.GetWindowDesktopId(handle));
            //Console.WriteLine(" " + desktop.Id);
            //desktop.GetRight()?.Switch();
            //desktop = desktop.GetRight();
            //Console.WriteLine(" " + desktop.Id);
            //if (desktop != null)
            //desktop.Switch();
            staticy = 0;
        }

        void executeDown()
        {
            WindowsInput.InputSimulator kb = new WindowsInput.InputSimulator();
            if (isUpActive)
            {
                isUpActive = false;
                kb.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.TAB);
            }
            else
                kb.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_D);

            staticy = 0;
        }


        void executeLeft()
        {
            List<VirtualKeyCode> Modify = new List<VirtualKeyCode>();
            Modify.Add(VirtualKeyCode.LWIN);
            Modify.Add(VirtualKeyCode.CONTROL);
            WindowsInput.InputSimulator kb = new WindowsInput.InputSimulator();
            kb.Keyboard.ModifiedKeyStroke(Modify, VirtualKeyCode.LEFT);

            //Console.WriteLine("ELFT");
            //desktop = desktop.GetLeft();
            //if (desktop != null)
            //desktop.Switch();

            staticx = 0;
        }

        void executeRight()
        {
            List<VirtualKeyCode> Modify = new List<VirtualKeyCode>();
            Modify.Add(VirtualKeyCode.LWIN);
            Modify.Add(VirtualKeyCode.CONTROL);
            WindowsInput.InputSimulator kb = new WindowsInput.InputSimulator();
            kb.Keyboard.ModifiedKeyStroke(Modify, VirtualKeyCode.RIGHT);
            //Console.WriteLine("RIGHT");

            staticx = 0;
        }

        bool isValidGesture(int finger)
        {
            if (finger == 3 || finger == 771 || finger == 515) 
                return true;
            else return false;
        }

        bool isValidGestureRL(int finger)
        {
            if (finger == 3 || finger == 771 || finger == 514)
                return true;
            else return false;
        }
    }

    class main
    {
        static void Main(string[] args)
        {
            Syn s1 = new Syn();
        }
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("a5cd92ff-29be-454c-8d04-d82879fb3f1b")]
    [System.Security.SuppressUnmanagedCodeSecurity]
    public interface IVirtualDesktopManager
    {
        [PreserveSig]
        int IsWindowOnCurrentVirtualDesktop(
            [In] IntPtr TopLevelWindow,
            [Out] out int OnCurrentDesktop
            );
        [PreserveSig]
        int GetWindowDesktopId(
            [In] IntPtr TopLevelWindow,
            [Out] out Guid CurrentDesktop
            );

        [PreserveSig]
        int MoveWindowToDesktop(
            [In] IntPtr TopLevelWindow,
            [MarshalAs(UnmanagedType.LPStruct)]
            [In]Guid CurrentDesktop
            );
    }

    [ComImport, Guid("aa509086-5ca9-4c25-8f95-589d3c07b48a")]
    public class CVirtualDesktopManager
    {

    }

    public class VirtualDesktopManager
    {
        public VirtualDesktopManager()
        {
            cmanager = new CVirtualDesktopManager();
            manager = (IVirtualDesktopManager)cmanager;
        }
        ~VirtualDesktopManager()
        {
            manager = null;
            cmanager = null;
        }
        private CVirtualDesktopManager cmanager = null;
        private IVirtualDesktopManager manager;

        public bool IsWindowOnCurrentVirtualDesktop(IntPtr TopLevelWindow)
        {
            int result;
            int hr;
            if ((hr = manager.IsWindowOnCurrentVirtualDesktop(TopLevelWindow, out result)) != 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }
            return result != 0;
        }

        public Guid GetWindowDesktopId(IntPtr TopLevelWindow)
        {
            Guid result;
            int hr;
            if ((hr = manager.GetWindowDesktopId(TopLevelWindow, out result)) != 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }
            return result;
        }

        public void MoveWindowToDesktop(IntPtr TopLevelWindow, Guid CurrentDesktop)
        {
            int hr;
            if ((hr = manager.MoveWindowToDesktop(TopLevelWindow, CurrentDesktop)) != 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }
        }
    }
}
