﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Afloeser.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("skb.local")]
        public string fqdn {
            get {
                return ((string)(this["fqdn"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("skbwebuser")]
        public string userID_LC {
            get {
                return ((string)(this["userID_LC"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("weblogin21")]
        public string pass_LC {
            get {
                return ((string)(this["pass_LC"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Kode")]
        public string temporary_pass {
            get {
                return ((string)(this["temporary_pass"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("LDAP-RW-SVC")]
        public string username_admin {
            get {
                return ((string)(this["username_admin"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("vL3dQqyt3NaZ7quuwASk")]
        public string password_admin {
            get {
                return ((string)(this["password_admin"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://svlpm2:8080/soap/services/PDataOnline/PData_GetNameFromCpr")]
        public string Afloeser_PDataGetName_liveCycleService_PDataOnline_PData_GetNameFromCprService {
            get {
                return ((string)(this["Afloeser_PDataGetName_liveCycleService_PDataOnline_PData_GetNameFromCprService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://svlpm2:8080/soap/services/PDataOnline/ValidateCpr")]
        public string Afloeser_PDataValidateSocialSecurity_liveCycleService_PDataOnline_ValidateCprService {
            get {
                return ((string)(this["Afloeser_PDataValidateSocialSecurity_liveCycleService_PDataOnline_ValidateCprServ" +
                    "ice"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("LDAP://OU=Vikarer,OU=Aeldre Handicap fagsekretariat,OU=Aeldre,OU=Skanderborg Komm" +
            "une,DC=skb,DC=local")]
        public string ldapPath {
            get {
                return ((string)(this["ldapPath"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=svsql01;Initial Catalog=LogPersondataTraek;User ID=LogPersonDataTraek" +
            "_afloeserAehW;Password=ITzAPu55L3W1th500Br1ckZ")]
        public string connectionString {
            get {
                return ((string)(this["connectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>IT-udvikler;VALUE=UDVIK</string>
  <string>Afløserkoder Administrator;VALUE=admin</string>
  <string>Afløserkoder Tjørnehaven;VALUE=TJAE</string>
  <string>Afløserkoder Akut team Fælles nat (A&amp;T);VALUE=AKUT</string>
  <string>Afløserkoder Baunegården;VALUE=BAUG</string>
  <string>Afløserkoder Bøgehaven;VALUE=BOEG</string>
  <string>Afløserkoder Dagmargården;VALUE=DAAG</string>
  <string>Afløserkoder Dalbogård;VALUE=DALB</string>
  <string>Afløserkoder Kildegården;VALUE=KILD</string>
  <string>Afløserkoder Møllehjørnet;VALUE=MOEL</string>
  <string>Afløserkoder PBL;VALUE=PBL</string>
  <string>Afløserkoder Præstehaven;VALUE=PRAE</string>
  <string>Afløserkoder Ryvang;VALUE=RYVG</string>
  <string>Afløserkoder Søkilde;VALUE=SOEK</string>
  <string>Afløserkoder Kildegården Korttid;VALUE=KORT</string>
  <string>Afløserkoder Sygeplejen Skanderborg;VALUE=SKB</string>
  <string>Afløserkoder Søndervang;VALUE=SOEN</string>
  <string>Afløserkoder Sølund Sundhed;VALUE=SOEL</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection validGroupList {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["validGroupList"]));
            }
        }
    }
}
