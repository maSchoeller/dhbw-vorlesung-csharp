﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaSchoeller.Dublin.Core {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class LogMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LogMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MaSchoeller.Dublin.Core.LogMessages", typeof(LogMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sie konnten sich nicht Autentifizieren..
        /// </summary>
        internal static string WcfAutherizeResultMessage {
            get {
                return ResourceManager.GetString("WcfAutherizeResultMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Du hast keine Rechte den Admin status zu ändern..
        /// </summary>
        internal static string WcfNoAdminRightsMessage {
            get {
                return ResourceManager.GetString("WcfNoAdminRightsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Something went wrong while start the wcf Service listen on &apos; {0} &apos;, close the application they blocking the startup or change the port in &apos;appsettings.json&apos;.
        /// </summary>
        internal static string WcfPortBindingError {
            get {
                return ResourceManager.GetString("WcfPortBindingError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The wcf Service started listening on &apos;{0}&apos;..
        /// </summary>
        internal static string WcfSuccessfullyStarted {
            get {
                return ResourceManager.GetString("WcfSuccessfullyStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User with the name &apos;{0}&apos; logged in to the server..
        /// </summary>
        internal static string WcfUserLogin {
            get {
                return ResourceManager.GetString("WcfUserLogin", resourceCulture);
            }
        }
    }
}
