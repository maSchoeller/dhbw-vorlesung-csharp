﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaSchoeller.Dublin.Client {
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
    internal class DisplayMesages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DisplayMesages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MaSchoeller.Dublin.Client.DisplayMesages", typeof(DisplayMesages).Assembly);
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
        ///   Looks up a localized string similar to Der Server besitzt bereits eine neue Version des Models..
        /// </summary>
        internal static string ConcurrentServerException {
            get {
                return ResourceManager.GetString("ConcurrentServerException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Die Verbindung zum Server ist aus unbekannten Gründen verloren gegangen. Sie werden abgemeldet..
        /// </summary>
        internal static string DisconnectMessage {
            get {
                return ResourceManager.GetString("DisconnectMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Server Verbindung verloren.
        /// </summary>
        internal static string DisconnectMessageCaption {
            get {
                return ResourceManager.GetString("DisconnectMessageCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Beim Speichern ist etwas schief gegangen..
        /// </summary>
        internal static string ErrorOnSave {
            get {
                return ResourceManager.GetString("ErrorOnSave", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Die neuen Passwörter stimmen nicht über ein..
        /// </summary>
        internal static string NewPasswordAreNotSame {
            get {
                return ResourceManager.GetString("NewPasswordAreNotSame", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Das alte und neue Passwort stimmen über ein..
        /// </summary>
        internal static string OldAndNewAreSame {
            get {
                return ResourceManager.GetString("OldAndNewAreSame", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Benutzer konnte nicht gelöscht werden..
        /// </summary>
        internal static string UserCantDelete {
            get {
                return ResourceManager.GetString("UserCantDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Der Benutzer existiert bereits.
        /// </summary>
        internal static string UserIdAlreadyExists {
            get {
                return ResourceManager.GetString("UserIdAlreadyExists", resourceCulture);
            }
        }
    }
}