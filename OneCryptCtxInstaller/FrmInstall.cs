using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerRegistrationManager.OutputService;
using SharpShell;
using SharpShell.Diagnostics;
using SharpShell.ServerRegistration;
using System.Windows.Forms;

namespace OneCryptCtxInstaller
{
    

    public partial class FrmInstall : Form, IOutputService
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            if (args.Length > 0 && string.Equals(args[0], "verify", StringComparison.OrdinalIgnoreCase))
                return VerifyExtensions(Application.StartupPath);
            if (args.Length > 0 && string.Equals(args[0], "status", StringComparison.OrdinalIgnoreCase))
                return GetExtensionStatus(Application.StartupPath);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var verb = args.Length > 0 ? args[0] : VerbInstall;
            var language = args.Length > 1 ? args[1] : "en";
            Application.Run(new FrmInstall(verb, language));
            return 0;
        }

        private bool install = true;
        private string lang ="en";
        
        public FrmInstall() : this(VerbInstall, "en")
        {
        }

        internal FrmInstall(string verb, string language)
        {
            InitializeComponent();
            outputService = this;
            lang = string.Equals(language, "es-ES", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(language, "es", StringComparison.OrdinalIgnoreCase) ? "es" : "en";
            if (string.Equals(verb, VerbInstall, StringComparison.OrdinalIgnoreCase))
            {
                install = true;
                if (lang == "es")
                {
                    OK_Button.Text = "Instalar";
                    Cancel_Button.Text = "Cerrar";
                    lblMessage.Text = "Quieres instalar la extensión de OneCrypt en el menu de Windows Explorer. Ver imagen de la derecha." +Environment.NewLine + "Puedes acceder a esta opción en el menu <<Configurar>>" ;
                }
                else
                {
                     OK_Button.Text = "Install";
                    Cancel_Button.Text = "Close";
                    lblMessage.Text = "Do you want to install OneCrypt extensions Windows Explorer menu. See image."; 
                }
               
                
            }
            else
            {
                install = false;
                if (lang == "es")
                {
                    OK_Button.Text = "Desinstalar";
                    Cancel_Button.Text = "Cerrar";
                    lblMessage.Text = "Quieres Desinstalar la extensión de OneCrypt en el menu de Windows Explorer." + Environment.NewLine + "You can access to this option in configuration menu";
                }
                else
                {
                     OK_Button.Text = "UnInstall";
                    Cancel_Button.Text = "Close";
                    lblMessage.Text = "Do you want to Uninstall OneCrypt extensions."; 
                }
            }

        }

            public void WriteError(string error, bool log = false)
            {
                this.lblMessage.AppendText (error + Environment.NewLine )  ;
            }

            public void WriteMessage(string message, bool log = false)
            {
                this.lblMessage.AppendText( message + Environment.NewLine);
            }

            public void WriteSuccess(string message, bool log = false)
            {
            this.lblMessage.AppendText(message + Environment.NewLine);
        }
    

        private void OK_Button_Click(object sender, EventArgs e)
        {
            if (install == true)
            {
                String path = System.IO.Path.Combine(Application.StartupPath, "OneCryptCtxExplorer.dll");
                String[] vars= new String[] {"install",path,"-codebase" };
                Run(vars);
                String path2 = System.IO.Path.Combine(Application.StartupPath, "OneCryptIconHandler.dll");
                String[] vars2= new String[] {"install",path2,"-codebase" };
                Run(vars2);
            }
            else
            {
                String path = System.IO.Path.Combine(Application.StartupPath, "OneCryptCtxExplorer.dll");
                String[] vars= new String[] {"uninstall",path };
                Run(vars);
                String path2 = System.IO.Path.Combine(Application.StartupPath, "OneCryptIconHandler.dll");
                String[] vars2= new String[] {"uninstall",path2 };
                Run(vars2);
            }




        }

        /// <summary>
        /// The output service.
        /// </summary>
        private readonly IOutputService outputService;


        /// <summary>
        /// Runs the specified application using the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Run(string[] args)
        {
            Logging.Log("Started the Server Registration Manager.");
            //  Get the architecture.
            var registrationType = Environment.Is64BitOperatingSystem ? RegistrationType.OS64Bit : RegistrationType.OS32Bit;

            //  Get the verb, target and parameters.
            var verb = args[0];
            var target = args[1];
            var parameters = args.Skip(2);

            //  Based on the verb, perform the action.
            if (verb == VerbInstall)
                InstallServer(target, registrationType, parameters.Any(p => p == ParameterCodebase));
            else if (verb == VerbUninstall)
                UninstallServer(target, registrationType);
        }

        /// <summary>
        /// Installs a SharpShell server at the specified path.
        /// </summary>
        /// <param name="path">The path to the SharpShell server.</param>
        /// <param name="registrationType">Type of the registration.</param>
        /// <param name="codeBase">if set to <c>true</c> install from codebase rather than GAC.</param>
        private void InstallServer(string path, RegistrationType registrationType, bool codeBase)
        {
            //  Validate the path.
            if (File.Exists(path) == false)
            {
                outputService.WriteError("File '" + path + "' does not exist.", true);
                return;
            }

            //  Try and load the server types.
            IEnumerable<ISharpShellServer> serverTypes = null;
            try
            {
                serverTypes = LoadServerTypes(path);
            }
            catch (Exception e)
            {
                outputService.WriteError("An unhandled exception occured when loading the extension");
                System.Diagnostics.Trace.Write(e);
                Logging.Error("An unhandled exception occured when loading a extension server.", e);
                return;
            }

            //  Install each server type.
            foreach (var serverType in serverTypes)
            {
                //  Inform the user we're going to install the server.
                outputService.WriteMessage("Preparing to install (" + registrationType + "): " + serverType.DisplayName, true);

                //  Install the server.
                try
                {
                    SharpShell.ServerRegistration.ServerRegistrationManager.InstallServer(serverType, registrationType, codeBase);
                    SharpShell.ServerRegistration.ServerRegistrationManager.RegisterServer(serverType, registrationType);
                }
                catch (Exception e)
                {
                    outputService.WriteError("Failed to install and register the extension.");
                    Logging.Error("An unhandled exception occured installing and registering the server " + serverType.DisplayName, e);
                    continue;
                }

                outputService.WriteSuccess("    " + serverType.DisplayName + " installed and registered.", true);
            }
        }

        /// <summary>
        /// Uninstalls a SharpShell server located at 'path'.
        /// </summary>
        /// <param name="path">The path to the SharpShell server.</param>
        /// <param name="registrationType">Type of the registration.</param>
        private void UninstallServer(string path, RegistrationType registrationType)
        {
            if (File.Exists(path) == false)
            {
                outputService.WriteError("File '" + path + "' does not exist.", true);
                return;
            }

            //  Try and load the server types.
            IEnumerable<ISharpShellServer> serverTypes = null;
            try
            {
                serverTypes = LoadServerTypes(path);
            }
            catch (Exception e)
            {
                outputService.WriteError("An unhandled exception occured when loading the extension");
                System.Diagnostics.Trace.Write(e);
                Logging.Error("An unhandled exception occured when loading a SharpShell server.", e);
                return;
            }

            //  Install each server type.
            foreach (var serverType in serverTypes)
            {
                //  Inform the user we're going to install the server.
                Console.WriteLine("Preparing to uninstall (" + registrationType + "): " + serverType.DisplayName, true);

                //  Install the server.
                try
                {

                    SharpShell.ServerRegistration.ServerRegistrationManager.UnregisterServer(serverType, registrationType);
                    SharpShell.ServerRegistration.ServerRegistrationManager.UninstallServer(serverType, registrationType);
                }
                catch (Exception e)
                {
                    outputService.WriteError("Failed to unregister and uninstall the extension.");
                    Logging.Error("An unhandled exception occured un registering and uninstalling the server " + serverType.DisplayName, e);
                    continue;
                }

                outputService.WriteSuccess("    " + serverType.DisplayName + " unregistered and uninstalled.", true);
            }
        }
        /// <summary>
        /// Loads the server types from an assembly.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <returns>The SharpShell Server types from the assembly.</returns>
        private static IEnumerable<ISharpShellServer> LoadServerTypes(string assemblyPath)
        {
            //  Create an assembly catalog for the assembly and a container from it.
            var catalog = new AssemblyCatalog(Path.GetFullPath(assemblyPath));
            var container = new CompositionContainer(catalog);

            //  Get all exports of type ISharpShellServer.
            return container.GetExports<ISharpShellServer>().Select(st => st.Value);
        }

        private static int VerifyExtensions(string directory)
        {
            try
            {
                var contextMenu = Path.Combine(directory, "OneCryptCtxExplorer.dll");
                var iconHandler = Path.Combine(directory, "OneCryptIconHandler.dll");
                if (!File.Exists(contextMenu) || !File.Exists(iconHandler))
                    return 2;

                var contextServers = LoadServerTypes(contextMenu).ToArray();
                var iconServers = LoadServerTypes(iconHandler).ToArray();
                return contextServers.Length == 1 && iconServers.Length == 1 ? 0 : 3;
            }
            catch (Exception exception)
            {
                Logging.Error("Unable to verify OneCrypt Explorer extensions.", exception);
                return 1;
            }
        }

        private static int GetExtensionStatus(string directory)
        {
            try
            {
                var registrationType = Environment.Is64BitOperatingSystem ?
                    RegistrationType.OS64Bit : RegistrationType.OS32Bit;
                var paths = new[] {
                    Path.Combine(directory, "OneCryptCtxExplorer.dll"),
                    Path.Combine(directory, "OneCryptIconHandler.dll")
                };

                foreach (var path in paths)
                {
                    if (!File.Exists(path))
                        return 2;
                    foreach (var server in LoadServerTypes(path))
                    {
                        if (SharpShell.ServerRegistration.ServerRegistrationManager
                            .GetServerRegistrationInfo(server, registrationType) == null)
                            return 4;
                    }
                }
                return 0;
            }
            catch (System.Security.SecurityException)
            {
                // SharpShell reads the machine-wide Approved extensions key.
                // A standard user cannot query it on every Windows policy.
                return 5;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Trace.Write(exception);
                Console.Error.WriteLine(exception);
                return 1;
            }
        }

        private const string VerbHelp = @"help";
        private const string VerbInstall = @"install";
        private const string VerbUninstall = @"uninstall";

        private const string ParameterCodebase = @"-codebase";

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

