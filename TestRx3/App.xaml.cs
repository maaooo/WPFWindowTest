using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;

namespace TestRx3 {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        [DllImport("kernel32", SetLastError = true)]
        public static extern bool AllocConsole();
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool AttachConsole(int dwProcessId);

        protected override void OnStartup(StartupEventArgs e) {
//             AttachConsole(Process.GetCurrentProcess().Id);
//             AllocConsole();
        }
    }
}
