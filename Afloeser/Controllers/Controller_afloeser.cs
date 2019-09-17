using Afloeser.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using Telerik.Web.UI;
using DAL.LogPersonalData;

namespace Afloeser.Controllers
{
    public class Controller_afloeser
    {
        private DAL.LogPersonalData.InterfaceRepo<LogAfloeserAeH> logPersonalDataRepo = new LogPersonalDataRepo(Properties.Settings.Default.connectionString);


        public bool AddLoggingInfoToDB(string samaccountName_loggedInUser, string socialSecurityNumber)
        {
            try
            {
                LogAfloeserAeH logData = new LogAfloeserAeH();
                logData.samaccountName = samaccountName_loggedInUser;
                logData.dataAccessed = "For- og efternavn for borger med CPR: " + socialSecurityNumber;
                logData.reasonForAccessingData = "Opslag af navn i forbindelse med oprettelse af bruger til afløsersystem for Ældre og Handicap.";//"Sagsbehandler slår borgernavn op i forbindelse med underretning om et barn / en ungs trivsel.";
                logData.dataAccessedDateTime = DateTime.Now;

                logPersonalDataRepo.Add(logData);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string samaccountnameCurrentUser()
        {
            string username = "";
            string fqdn = Afloeser.Properties.Settings.Default.fqdn;

            if (HttpContext.Current.User.Identity != null && HttpContext.Current.User.Identity.Name.ToString().Length > 4)
            {
                username = HttpContext.Current.User.Identity.Name.ToString().Remove(0, 4);
            }

            else if (Environment.UserName != string.Empty)
            {
                username = Environment.UserName;
            }
            else if (System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString() != string.Empty)
            {
                username = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString().Remove(0, 4);
            }

            return username;
        }

        public string displayNameCurrentUser(string samAccountName)
        {
            string fqdn = Afloeser.Properties.Settings.Default.fqdn;

            string displayName = Skb.ActiveDirectory.DAL.Query.DisplaySamAccountName(samAccountName, fqdn);

            return displayName;
        }

        public bool CheckCpr(string cpr)
        {
            PDataValidateSocialSecurity_liveCycleService.PDataOnline_ValidateCprService service = new PDataValidateSocialSecurity_liveCycleService.PDataOnline_ValidateCprService();
            service.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.userID_LC, Properties.Settings.Default.pass_LC);

            bool validPersonNr = service.invoke(cpr);

            return validPersonNr;
        }

        public string GetNameFromPData(string cpr)
        {
            PDataGetName_liveCycleService.PDataOnline_PData_GetNameFromCprService service = new PDataGetName_liveCycleService.PDataOnline_PData_GetNameFromCprService();
            service.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.userID_LC, Properties.Settings.Default.pass_LC);

            string navn = service.invoke(cpr);

            return navn;
        }

        public void FillComboTemporaryCodesAvailable(RadComboBox combo)
        {
            combo.Items.Clear();
            DateTime today = DateTime.Now;
            List<ADUser> list = GetTemporaryCodesAvailableFromAD();
            int index = 0;

            var result = list.OrderBy(c => c.DisplayName, new AlphanumComparatorFast()).ToList();

            List<ValidGroup> list_groupMemberships = ValidGroupMembership_loggedInUser();

            foreach (ValidGroup group in list_groupMemberships)
            {

                foreach (ADUser user in result)
                {
                    if (user.SAMAccountName.StartsWith(group.GroupValue, StringComparison.InvariantCultureIgnoreCase) || group.GroupValue.Equals("ADMIN", StringComparison.InvariantCultureIgnoreCase))
                    {
                        DateTime acountExpires = DateTime.ParseExact(user.AccountExpires, "dd-MM-yyyy HH:mm:ss", null);
                        if (acountExpires <= today && temporaryCodeIsOutdated(user.SAMAccountName) == false)
                        {
                            combo.Items.Insert(index, new RadComboBoxItem { Text = user.DisplayName, Value = user.SAMAccountName });
                            index++;
                        }
                    }
                }

            }
        }

        private bool temporaryCodeIsOutdated(string teporaryCode_username)
        {
            bool usernameIsOutdated = false;

            List<string> oldCodes = new List<string>();
            oldCodes.Add("SPL1");
            oldCodes.Add("SPL2");
            oldCodes.Add("SPL3");
            oldCodes.Add("SPL4");
            oldCodes.Add("SPL5");
            oldCodes.Add("SPL6");
            oldCodes.Add("SPL7");
            oldCodes.Add("SPL8");
            oldCodes.Add("SPL9");
            oldCodes.Add("SPL10");
            oldCodes.Add("SPL11");
            oldCodes.Add("SSA1");
            oldCodes.Add("SSA2");
            oldCodes.Add("SSA3");
            oldCodes.Add("SSA4");
            oldCodes.Add("SSA5");
            oldCodes.Add("SSH1");
            oldCodes.Add("SSH2");
            oldCodes.Add("SSH3");
            oldCodes.Add("SSH4");
            oldCodes.Add("SSH5");

            if (oldCodes.Contains(teporaryCode_username, StringComparer.OrdinalIgnoreCase))
            {
                usernameIsOutdated = true;
            }

            return usernameIsOutdated;
        }

        public static List<ADUser> GetTemporaryCodesAvailableFromAD()
        {
            List<ADUser> vikarList = new List<ADUser>();
            string ldapPath = Properties.Settings.Default.ldapPath;//"LDAP://OU=Vikarer,OU=Aeldre Handicap fagsekretariat,OU=Aeldre,OU=Skanderborg Kommune,DC=skb,DC=local";
            DirectoryEntry searchRoot = new DirectoryEntry(ldapPath);
            DirectorySearcher dirSearcher = new DirectorySearcher(searchRoot);
            dirSearcher.Filter = "(objectCategory=person)";
            dirSearcher.PageSize = 100;

            SearchResultCollection result = dirSearcher.FindAll();

            foreach (SearchResult record in result)
            {
                string displayName = "";
                string accountExpires = "";
                string sAMAccountName = "";



                if (record.Properties.Contains("displayName"))
                {
                    displayName = record.Properties["displayName"][0].ToString();
                }
                if (record.Properties.Contains("accountExpires"))
                {
                    string expiration_string = record.Properties["accountExpires"][0].ToString();
                    DateTime accountExpiresDateTime;

                    if (expiration_string != "9223372036854775807")
                    {
                        accountExpiresDateTime = DateTime.FromFileTime((long)record.Properties["accountExpires"][0]);
                    }
                    else
                    {
                        accountExpiresDateTime = DateTime.MinValue;
                    }
                    

                    accountExpires = accountExpiresDateTime.ToString();
                }
                if (record.Properties.Contains("sAMAccountName"))
                {
                    sAMAccountName = record.Properties["sAMAccountName"][0].ToString();
                }
                ADUser user = new ADUser(displayName, accountExpires, sAMAccountName);
                vikarList.Add(user);
            }

            return vikarList;
        }


        public DateTime AddHoursAndMinsToDate(DateTime date, TimeSpan time)
        {
            if (date != null && time != null)
            {
                date = date + time;
            }

            return date;
        }


        public bool UpdateUserAccountInActiveDirectory(string samAccountName_temporary, DateTime accountExpirationDate_temporary, string socialSecurityNumber_temporary)
        {
            string fqdn = Properties.Settings.Default.fqdn;
            string username_admin = Properties.Settings.Default.username_admin;
            string password_admin = Properties.Settings.Default.password_admin;
            string password_vikar = ConcatVikarPass(socialSecurityNumber_temporary);

           bool success = UpdateTemporaryAccountInAD(samAccountName_temporary, accountExpirationDate_temporary, password_vikar, fqdn, username_admin, password_admin);

            return success;
        }

        private static bool UpdateTemporaryAccountInAD(string samAccountName_vikar, DateTime accountExpirationDate_vikar, string password_vikar, string fqdn, string username_admin, string password_admin)
        {
            using (PrincipalContext pcx = new PrincipalContext(ContextType.Domain, fqdn, username_admin, password_admin))
            {
                try
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(pcx, samAccountName_vikar);

                    if (user != null)
                    {
                        user.SetPassword(password_vikar);
                        user.AccountExpirationDate = accountExpirationDate_vikar;
                        user.Enabled = true;
                        user.Save();
                    }

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }


            }
        }

        public bool DisableAccountInActiveDirectory(string username_temporary, DateTime disablingDateTime)
        {
            string fqdn = Properties.Settings.Default.fqdn;
            string username_admin = Properties.Settings.Default.username_admin;
            string password_admin = Properties.Settings.Default.password_admin;

            bool success = DisableTemporaryAccountInAD(username_temporary, fqdn, username_admin, password_admin, disablingDateTime);

            return success;
        }

        private static bool DisableTemporaryAccountInAD(string temporary_username, string fqdn, string username_admin, 
            string password_admin, DateTime disablingDateTime)
        {
            using (PrincipalContext pcx = new PrincipalContext(ContextType.Domain, fqdn, username_admin, password_admin))
            {
                try
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(pcx, temporary_username);

                    if (user != null)
                    {
                        user.AccountExpirationDate = disablingDateTime;
                        user.Enabled = false;
                        user.Save();
                    }

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }


            }
        }

        private string ConcatVikarPass(string socialSecurityNumber)
        {
            if (socialSecurityNumber != string.Empty)
            {
                string pass_firstPart = Properties.Settings.Default.temporary_pass;
                string pass_secondPart = socialSecurityNumber.Substring(6);

                return pass_firstPart + pass_secondPart;
            }
            else
            {
                return "";
            }
        }

        public bool AddToDataBase(string temporary_socialSecurityNumber, string temporary_name, DateTime accountExpirationDate_temporary, 
            string temporary_codeName, string temporaryCode_username, string loggedOnUser_username)
        {
            using (DataClass_afloeserDataContext context = new DataClass_afloeserDataContext())
            {
                try
                {
                    AfloeserBrugere newUser = new AfloeserBrugere();

                    newUser.CPR = temporary_socialSecurityNumber;
                    newUser.Navn = temporary_name;
                    newUser.StartDato = DateTime.Now;
                    newUser.SlutDato = accountExpirationDate_temporary;
                    newUser.Kategori = temporary_codeName;
                    newUser.TempCode_username = temporaryCode_username;
                    newUser.Opretter = loggedOnUser_username;
                    newUser.Deleted = false;

                    context.AfloeserBrugeres.InsertOnSubmit(newUser);
                    context.SubmitChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }

        public bool DeleteInDataBase(int db_ID, string loggedInUser_username, DateTime disablingDateTime)
        {
            using (DataClass_afloeserDataContext context = new DataClass_afloeserDataContext())
            {
                try
                {
                    AfloeserBrugere userInDB = context.AfloeserBrugeres.Where(a => a.ID == db_ID).FirstOrDefault();

                    userInDB.Deleted = true;
                    userInDB.DeletedBy = loggedInUser_username;
                    userInDB.DeletedDateTime = disablingDateTime;
                    context.SubmitChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static Tuple<DateTime, bool> GetTemporaryInfo(string samAccountName_vikar, string fqdn, string username_admin, string password_admin)
        {
            using (PrincipalContext pcx = new PrincipalContext(ContextType.Domain, fqdn, username_admin, password_admin))
            {
                using (UserPrincipal user = UserPrincipal.FindByIdentity(pcx, samAccountName_vikar))
                {
                    Tuple<DateTime, bool> result;

                    if (user != null)
                    {

                        DateTime expirationDate = user.AccountExpirationDate.Value.ToLocalTime();
                        bool accountEnabled = (bool)user.Enabled;

                        result = new Tuple<DateTime, bool>(expirationDate, accountEnabled);

                        return result;
                    }
                    else return result = new Tuple<DateTime, bool>(DateTime.MinValue, false); ;
                }
            }
        }

        public void PopulateGrid_temporaryCodesInUse(RadGrid grid)
        {
            List<TemporaryUser> list = FilterGridList();//GetTemporayCodesInUse();

            grid.DataSource = list;
            grid.DataBind();
        }

        private List<TemporaryUser> FilterGridList()
        {
            List<ValidGroup> list_groupMembership = ValidGroupMembership_loggedInUser();

            List<TemporaryUser> list_allCodesInUse = GetTemporayCodesInUse();

            List<TemporaryUser> filtered_list = new List<TemporaryUser>();

            foreach (ValidGroup group in list_groupMembership)
            {
                foreach (TemporaryUser user in list_allCodesInUse)
                {
                    if (user.Kategori.StartsWith(group.GroupValue, StringComparison.InvariantCultureIgnoreCase) || group.GroupValue.Equals("ADMIN", StringComparison.InvariantCultureIgnoreCase)) //(user.SAMAccountName.StartsWith(group.GroupValue) || group.GroupValue == "ADMIN")
                    {
                        filtered_list.Add(user);
                    }
                }
            }
            return filtered_list;
        }

        public List<TemporaryUser> GetTemporayCodesInUse()
        {
            List<TemporaryUser> list = new List<TemporaryUser>();

            using (DataClass_afloeserDataContext context = new DataClass_afloeserDataContext())
            {

                DateTime today = DateTime.Now;

                var koderIBrug_list = (from table in context.AfloeserBrugeres
                                       where table.SlutDato >= today &&
                                       (table.Deleted != true || table.Deleted == null)
                                       select new
                                       {
                                           id = table.ID,
                                           kategori = table.Kategori,
                                           tempCode_username = table.TempCode_username,
                                           startDato = table.StartDato,
                                           slutDato = table.SlutDato,
                                           opretter = table.Opretter,
                                           deleted = table.Deleted
                                       }
                                ).ToList();

                foreach (var item in koderIBrug_list)
                {
                    list.Add(new TemporaryUser(item.id, item.kategori, item.tempCode_username, item.startDato, item.slutDato, item.opretter));
                }

                return list;
            }
        }

        public TemporaryUser GetTemporaryUserFromGrid(RadGrid grid)
        {
            TemporaryUser temporaryUser = new TemporaryUser();

            if (grid.SelectedItems.Count == 1)
            {
                foreach (GridDataItem item in grid.SelectedItems)
                {
                    temporaryUser.ID = Int32.Parse(item["ID"].Text);
                    temporaryUser.Kategori= item["Kategori"].Text;
                    temporaryUser.TempCode_username = item["TempCode_username"].Text;

                }
            }

            return temporaryUser;
        }

        public List<TemporaryUser> TemporaryUserIsCreated(string temporary_socialSecurityNumber, DateTime startDateTime, string temporary_codeName_prefix)
        {
            List<TemporaryUser> list = new List<TemporaryUser>();

            using (DataClass_afloeserDataContext context = new DataClass_afloeserDataContext())
            {

                var koderIBrug_list = (from table in context.AfloeserBrugeres
                                       where table.CPR == temporary_socialSecurityNumber &&
                                       table.StartDato < startDateTime &&
                                       table.SlutDato > startDateTime &&
                                       //GetCodeNamePrefix(table.Kategori) == temporary_codeName_prefix &&
                                       table.Deleted != true
                                       select new
                                       {
                                           id = table.ID,
                                           kategori = table.Kategori,
                                           tempCode_username = table.TempCode_username,
                                           startDato = table.StartDato,
                                           slutDato = table.SlutDato,
                                           opretter = table.Opretter,
                                           deleted = table.Deleted
                                       }
                ).ToList();

                foreach (var item in koderIBrug_list)
                {
                    if (GetCodeNamePrefix(item.kategori) == temporary_codeName_prefix)
                    {
                        list.Add(new TemporaryUser(item.id, item.kategori, item.tempCode_username, item.startDato, item.slutDato, item.opretter));
                    }                

                }
            }
            return list;
        }

        public string GetCodeNamePrefix(string temporary_codeName)
        {
            string prefix = "";

            if (temporary_codeName.Contains("-"))
            {
                int index = temporary_codeName.IndexOf("-");
                prefix = temporary_codeName.Substring(0, index);
            }

            return prefix;
        }

        public ArrayList Groups()
        {
            ArrayList groups = new ArrayList();
            foreach (System.Security.Principal.IdentityReference group in
                System.Web.HttpContext.Current.Request.LogonUserIdentity.Groups)
            {
                groups.Add(group.Translate(typeof
                    (System.Security.Principal.NTAccount)).ToString());
            }
            return groups;
        }

        public List<GroupPrincipal> GetGroups(string userName)
        {
            List<GroupPrincipal> result = new List<GroupPrincipal>();

            // establish domain context
            PrincipalContext yourDomain = new PrincipalContext(ContextType.Domain);

            // find your user
            UserPrincipal user = UserPrincipal.FindByIdentity(yourDomain, userName);

            // if found - grab its groups
            if (user != null)
            {
                PrincipalSearchResult<Principal> groups = user.GetGroups();//user.GetAuthorizationGroups();

                // iterate over all groups
                foreach (Principal p in groups)
                {
                    // make sure to add only group principals
                    if (p is GroupPrincipal)
                    {
                        result.Add((GroupPrincipal)p);
                    }
                }
            }

            return result;
        }

        public bool UserIsInGroup(string groupName, string username)
        {
            bool userIsInGroup = false;

            using (PrincipalContext yourDomain = new PrincipalContext(ContextType.Domain))
            {

                using (var group = GroupPrincipal.FindByIdentity(yourDomain, groupName))
                {

                    if (group == null)
                    {

                    }
                    else
                    {

                        var users = group.GetMembers(true);
                        foreach (UserPrincipal user in users)
                        {
                            if (user.SamAccountName == username)
                            {
                                userIsInGroup = true;
                            }
                        }
                    }
                }
            }
            return userIsInGroup;
        }

        public List<UserPrincipal> GetMembersInGroup(string groupName)
        {
            List<UserPrincipal> userList = new List<UserPrincipal>();

            using (PrincipalContext yourDomain = new PrincipalContext(ContextType.Domain))
            {
                using (var group = GroupPrincipal.FindByIdentity(yourDomain, groupName))
                {

                    if (group == null)
                    {
                        
                    }
                    else
                    {

                        var users = group.GetMembers(true);
                        foreach (UserPrincipal user in users)
                        {
                            userList.Add(user);

                        }
                    }
                }
            }
            return userList;

        }

        public List<ValidGroup> ValidGroupMembership_loggedInUser()
        {
            StringCollection stringColl = new StringCollection();
            List<ValidGroup> userIsMemberOfGroup_list = new List<ValidGroup>();

            if (Properties.Settings.Default.validGroupList != null)
            {
                stringColl = Properties.Settings.Default.validGroupList;
                List<string> validGroup_list = stringColl.Cast<string>().ToList();

                string loggedInUser_username = samaccountnameCurrentUser();

                foreach (string item in validGroup_list)
                {
                    string delimiter = ";VALUE=";
                    if (item.Contains(delimiter))
                    {
                        int index = item.IndexOf(delimiter);
                        string groupName = item.Substring(0, index);
                        string groupValue = item.Substring(item.LastIndexOf(delimiter) + delimiter.Length);

                        if (UserIsInGroup(groupName, loggedInUser_username))
                        {
                            userIsMemberOfGroup_list.Add(new ValidGroup(groupName, groupValue));
                        }

                    }

                }
            }
            return userIsMemberOfGroup_list;
        }
    }
}