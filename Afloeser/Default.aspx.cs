using Afloeser.Controllers;
using Afloeser.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Afloeser
{
    public partial class Default : System.Web.UI.Page
    {

        private Controllers.Controller_afloeser controller = new Controllers.Controller_afloeser();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                LoadPage();

                string loggedOn = ConcatLoggedOnString();
                if (loggedOn != string.Empty)
                {
                    tableCurrentUser.Visible = true;
                    lblLoggedOn.Text = loggedOn;
                }
            }

        }

        private void LoadPage()
        {
            toolTipSocialSecurity_codeBehind.Show();
            //datePickerEndDate.SelectedDate = DateTime.Today;

            controller.FillComboTemporaryCodesAvailable(comboTemporaryCodesAvailable);
            //controller.Groups();
            //controller.GetGroups("xolofo");
            //controller.GetMembersInGroup("Afløserkoder Baunegården");
            //controller.ValidGroupMembership_loggedInUser();
            GroupMembership();
        }

        private void GroupMembership()
        {
            List<ValidGroup> group_list = controller.ValidGroupMembership_loggedInUser();
            string text = "";

            if (group_list.Count > 0)
            {
                text = "Adgang til:" + "<br/>";
                foreach (ValidGroup group in group_list)
                {
                    text = text + group.GroupValue;
                    text = text + "<br/>";
                }
            }
            lblGroupMembership.Text = text;
        }

        protected string ConcatLoggedOnString()
        {
            string loggedOn = "";
            string samAccountName = controller.samaccountnameCurrentUser();
            string displayName = "";

            if (samAccountName != string.Empty)
            {
                displayName = controller.displayNameCurrentUser(samAccountName);

                loggedOn = "Logget ind: " + samAccountName;

                if (displayName != string.Empty)
                {
                    loggedOn = "Logget ind: " + displayName + " (" + samAccountName + ")";
                }
            }
            return loggedOn;
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Request.Cookies.Clear();
            FormsAuthentication.SignOut();
            Response.Redirect("http://medarbejderportalen/Sider/default.aspx", true);
        }

        protected void txtBoxTemporary_socialSecurityNumber_TextChanged(object sender, EventArgs e)
        {
            ValidateCpr(txtBoxTemporary_socialSecurityNumber.Text);
        }

        private void ValidateCpr(string cpr)
        {
            bool validPersonNr = controller.CheckCpr(cpr);

            if (validPersonNr == true)
            {
                string name = controller.GetNameFromPData(cpr);

                if (name != string.Empty)
                {

                    string username_loggedInUser = controller.samaccountnameCurrentUser();
                    controller.AddLoggingInfoToDB(username_loggedInUser, cpr);

                    lblTemporary_name.Text = name;

                    tableRowDateAndTime.Visible = true;
                }
            }
            else
            {
                lblTemporary_name.Text = "";
                txtBoxTemporary_socialSecurityNumber.Text = "";

                tableRowDateAndTime.Visible = false;
                tableRowTemporaryCodes.Visible = false;
                tableRowButton.Visible = false;

                string alert = "Personnummeret er ikke gyldigt.";


                RadWindowManager_afloeser.RadAlert(alert, 300, 180, "Ugyldigt CPR", "callBackFn");

                ResetInputFields();
            }
        }

        protected void datePickerEndDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            CheckTimeAndDateInput();
        }


        private void CheckTimeAndDateInput()
        {
            if (datePickerEndDate.SelectedDate.ToString() != string.Empty && timePickerEndTime.SelectedTime != null)
            {
                if (controller.AddHoursAndMinsToDate((DateTime)datePickerEndDate.SelectedDate, (TimeSpan)timePickerEndTime.SelectedTime) > DateTime.Now)
                {
                    controller.FillComboTemporaryCodesAvailable(comboTemporaryCodesAvailable);
                    tableRowTemporaryCodes.Visible = true;
                }
                else
                {
                    tableRowTemporaryCodes.Visible = false;

                    string alert;
                    alert = "Sluttidspunkt skal være senere end nuværende dato og tid";
                    RadWindowManager_afloeser.RadAlert(alert, 300, 180, "Ugyldigt tidspunkt", "callBackFn");
                }
            }
            else
            {
                tableRowTemporaryCodes.Visible = false;
            }
        }

        protected void comboTemporaryCodesAvailable_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (comboTemporaryCodesAvailable.SelectedItem != null)
            {
                tableRowButton.Visible = true;
            }
            else
            {
                tableRowButton.Visible = false;
            }
        }

        protected void btnActivateTemporaryCode_Click(object sender, EventArgs e)
        {
            if (CheckForEmptyInputFields() == false)
            {
                string samAccountName = comboTemporaryCodesAvailable.SelectedItem.Value;
                DateTime accountExpirationDate_temporary = controller.AddHoursAndMinsToDate((DateTime)datePickerEndDate.SelectedDate, (TimeSpan)timePickerEndTime.SelectedTime);
                string fqdn = Properties.Settings.Default.fqdn;
                string username_admin = Properties.Settings.Default.username_admin;
                string password_admin = Properties.Settings.Default.password_admin;
                string socialSecurityNumber_temporary = txtBoxTemporary_socialSecurityNumber.Text;
                string temporary_name = lblTemporary_name.Text;
                string temporary_codeName = comboTemporaryCodesAvailable.SelectedItem.Text;
                string temporary_codeName_prefix = controller.GetCodeNamePrefix(temporary_codeName);
                string temporaryCode_username = comboTemporaryCodesAvailable.SelectedItem.Value;

                List<TemporaryUser> list = controller.TemporaryUserIsCreated(socialSecurityNumber_temporary, DateTime.Now, temporary_codeName_prefix);

                if (list.Count == 0)
                {
                    //AddToDataBase();
                    bool success_AD = controller.UpdateUserAccountInActiveDirectory(samAccountName, accountExpirationDate_temporary, socialSecurityNumber_temporary);

                    Tuple<DateTime, bool> userData = Controller_afloeser.GetTemporaryInfo(samAccountName, fqdn, username_admin, password_admin);
                    DateTime expDate = userData.Item1;
                    bool accountEnabled = userData.Item2;

                    if (accountEnabled == true && success_AD) //((accountEnabled == true && expDate == accountExpirationDate_vikar)) 
                    {
                        string loggedOnUser_userName = controller.samaccountnameCurrentUser();

                        bool success_DB = controller.AddToDataBase(socialSecurityNumber_temporary, temporary_name, accountExpirationDate_temporary, 
                            temporary_codeName, temporaryCode_username, loggedOnUser_userName);

                        if (success_DB)
                        {
                            string alert;
                            alert = "Brugeren er aktiveret.";

                            RadWindowManager_afloeser.RadAlert(alert, 300, 180, "Bruger aktiveret", "alertAndRedirect");
                        }
                        else
                        {

                        }

                        ResetInputFields();
                    }
                    else
                    {
                        string alert;
                        alert = "Brugeren blev IKKE aktiveret.";
                        RadWindowManager_afloeser.RadAlert(alert, 300, 180, "Ikke aktiveret", "alertAndRedirect");
                    }
                }
                else
                {
                    string message = "Denne bruger har en kode i brug" + "<br/><br/>";

                    foreach (TemporaryUser item in list)
                    {
                        if (item.Kategori != string.Empty)
                        {
                            message = message + "Kode navn: " + item.Kategori;
                            message = message + "<br/>";
                        }
                        if (item.StartDato != null)
                        {
                            message = message + "Start dato: " + item.StartDato;
                            message = message + "<br/>";
                        }
                        if (item.SlutDato != null)
                        {
                            message = message + "Slut dato: " + item.SlutDato;
                            message = message + "<br/>";
                        }

                        message = message + "<br/> Déaktiver evt denne kode hvis du vil oprette en ny.";

                    }
                    RadWindowManager_afloeser.RadAlert(message, 300, 180, "Bruger allerede oprettet", "alertAndRedirect");
                }
            }
            else
            {
                CheckInputVaribles();
            }
        }

        protected void CheckInputVaribles()
        {

            string alertMessage = "Mangler at udfylde: \\n" + "\\n";

            if (txtBoxTemporary_socialSecurityNumber.Text == string.Empty)
            {
                alertMessage += "Personnummer \\n";
            }

            if (datePickerEndDate.SelectedDate == null)
            {
                alertMessage += "Slutdato \\n";
            }
            if (timePickerEndTime.SelectedTime == null)
            {
                alertMessage += "Sluttidspunkt \\n";
            }

            RadWindowManager_afloeser.RadAlert(alertMessage, 300, 180, "Ugyldigt CPR", "callBackFn");
        }

        private bool CheckForEmptyInputFields()
        {
            bool emptyFieldFound = false;


            if (txtBoxTemporary_socialSecurityNumber.Text == string.Empty || datePickerEndDate.SelectedDate == null || comboTemporaryCodesAvailable.SelectedItem == null) //|| datePickerStartDato.SelectedDate == null || timePickerStartTid.SelectedTime == null || timePickerStartTid.SelectedTime == null
            {
                emptyFieldFound = true;
            }

            return emptyFieldFound;
        }

        private void ResetInputFields()
        {
            txtBoxTemporary_socialSecurityNumber.Text = "";
            lblTemporary_name.Text = "";
            datePickerEndDate.Clear();
            datePickerEndDate.SelectedDate = DateTime.Today;
            timePickerEndTime.Clear();
            comboTemporaryCodesAvailable.ClearSelection();
            tableRowButton.Visible = false;
            tableRowDateAndTime.Visible = false;
            tableRowTemporaryCodes.Visible = false;
            controller.FillComboTemporaryCodesAvailable(comboTemporaryCodesAvailable);
            txtBoxTemporary_socialSecurityNumber.Focus();
        }

        protected void TabStrip_afloeser_TabClick(object sender, Telerik.Web.UI.RadTabStripEventArgs e)
        {
            if (TabStrip_afloeser.SelectedIndex == 0)
            {

            }

            if (TabStrip_afloeser.SelectedIndex == 1)
            {
                controller.PopulateGrid_temporaryCodesInUse(grid_temporaryCodesInUse);

                if (grid_temporaryCodesInUse.Items.Count > 0)
                {
                    ToolTipGrid.Show();
                }

            }
        }



        protected void grid_temporaryCodesInUse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grid_temporaryCodesInUse.SelectedItems.Count == 1)
            {
                TemporaryUser tempUser = controller.GetTemporaryUserFromGrid(grid_temporaryCodesInUse);

                BtnCancel.Text = "Annullér bestilling - " + tempUser.Kategori;
                BtnCancel.Visible = true;
            }
            else
            {
                BtnCancel.Visible = false;
            }
        }

        protected void grid_temporaryCodesInUse_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void grid_temporaryCodesInUse_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            if (grid_temporaryCodesInUse.SelectedItems.Count == 1)
            {
                TemporaryUser tempUser = controller.GetTemporaryUserFromGrid(grid_temporaryCodesInUse);

                int id = tempUser.ID;
                string username = tempUser.TempCode_username;//tempUser.Kategori;
                DateTime disablingDateTime = DateTime.Now;
                string loggedInUser_username = controller.samaccountnameCurrentUser();

                bool success = controller.DisableAccountInActiveDirectory(username, disablingDateTime);

                if (success)
                {
                    bool success_DB = controller.DeleteInDataBase(id, loggedInUser_username, disablingDateTime);

                    if (success_DB)
                    {
                        RadWindowManager_afloeser.RadAlert("Bestillingen blev annuleret", 300, 180, "Deaktiveret", "alertAndRedirect");
                    }
                    else
                    {
                        RadWindowManager_afloeser.RadAlert("Der gik noget galt!", 300, 180, "Ikke slettet - DATABASE", "alertAndRedirect");
                    }


                }

                else
                {
                    RadWindowManager_afloeser.RadAlert("Der gik noget galt!", 300, 180, "Ikke slettet - AD", "alertAndRedirect");
                }
            }
        }
    }
}