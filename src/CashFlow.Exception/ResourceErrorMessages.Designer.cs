﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CashFlow.Exception {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ResourceErrorMessages {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceErrorMessages() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("CashFlow.Exception.ResourceErrorMessages", typeof(ResourceErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        public static string UNKNOW_ERROR {
            get {
                return ResourceManager.GetString("UNKNOW_ERROR", resourceCulture);
            }
        }
        
        public static string TITLE_REQUIRED {
            get {
                return ResourceManager.GetString("TITLE_REQUIRED", resourceCulture);
            }
        }
        
        public static string AMOUNT_MUST_BE_GREATER_THAN_ZERO {
            get {
                return ResourceManager.GetString("AMOUNT_MUST_BE_GREATER_THAN_ZERO", resourceCulture);
            }
        }
        
        public static string DATE_LESS_OR_EQUAL_THAN_TODAY {
            get {
                return ResourceManager.GetString("DATE_LESS_OR_EQUAL_THAN_TODAY", resourceCulture);
            }
        }
        
        public static string INVALID_PAYMENT {
            get {
                return ResourceManager.GetString("INVALID_PAYMENT", resourceCulture);
            }
        }
        
        public static string NOT_FOUND {
            get {
                return ResourceManager.GetString("NOT_FOUND", resourceCulture);
            }
        }
        
        public static string NAME_REQUIRED {
            get {
                return ResourceManager.GetString("NAME_REQUIRED", resourceCulture);
            }
        }
        
        public static string EMAIL_REQUIRED {
            get {
                return ResourceManager.GetString("EMAIL_REQUIRED", resourceCulture);
            }
        }
        
        public static string INVALID_EMAIL {
            get {
                return ResourceManager.GetString("INVALID_EMAIL", resourceCulture);
            }
        }
        
        public static string PASSWORD_REQUIRED {
            get {
                return ResourceManager.GetString("PASSWORD_REQUIRED", resourceCulture);
            }
        }
        
        public static string PASSWORD_INVALID {
            get {
                return ResourceManager.GetString("PASSWORD_INVALID", resourceCulture);
            }
        }
        
        public static string EMAIL_ALREADY_EXISTS {
            get {
                return ResourceManager.GetString("EMAIL_ALREADY_EXISTS", resourceCulture);
            }
        }
        
        public static string PASSWORD_OR_EMAIL_INVALID {
            get {
                return ResourceManager.GetString("PASSWORD_OR_EMAIL_INVALID", resourceCulture);
            }
        }
    }
}
