<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage_afloeser.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Afloeser.Default" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_HeaderMainTitle" runat="server">
    Afløsersystem - Ældre & Handicap
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_LeftMarg" runat="server">

    <br />
    <asp:Table runat="server" Visible="true" ID="tableCurrentUser" HorizontalAlign="Center">

        <asp:TableRow>
            <asp:TableCell CssClass="loggedOnBox" Style="vertical-align: middle">
                <br />
                <asp:Label runat="server" Text="" ID="lblLoggedOn"></asp:Label>
                <br />
                <br />
                <asp:Label runat="server" Text="" ID="lblGroupMembership"></asp:Label>
                <br />
            </asp:TableCell>

        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                   <br />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="logOutBox" Style="vertical-align: middle">

                <br />
                <div style="cursor: pointer">
                    <telerik:RadButton runat="server" ID="btnLogOut" Skin="Windows7" ForeColor="#ffb519" Text="Log ud" Font-Bold="true" Font-Size="Medium"
                        Font-Underline="true" Width="190px" ButtonType="LinkButton" OnClick="btnLogOut_Click" Height="33px"
                        Icon-PrimaryIconUrl="~/images/logout_32px.png" Icon-PrimaryIconWidth="33px" Icon-PrimaryIconHeight="33px" Icon-PrimaryIconLeft="3px" Icon-PrimaryIconTop="3px" SingleClick="true" SingleClickText="Logger ud.."
                        ToolTip="Link til medarbejderportalen" CssClass="linkButton" HoveredCssClass="LinkButtonStyle" BackColor="Transparent" BorderStyle="None">
                    </telerik:RadButton>
                </div>
                <br />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_Content" runat="server">

    <asp:UpdatePanel runat="server" ID="UpdatePanel_Schedule" UpdateMode="Always">
        <ContentTemplate>

            <telerik:RadTabStrip ID="TabStrip_afloeser" runat="server" Skin="Bootstrap" SelectedIndex="0"
                MultiPageID="MultiPage_afloeser" OnTabClick="TabStrip_afloeser_TabClick">
                <Tabs>
                    <telerik:RadTab Text="Aktivér kode" Font-Bold="true" Font-Names="Arial" Font-Size="Small" Selected="True"></telerik:RadTab>
                    <telerik:RadTab Text="Deaktivér Kode" Font-Bold="true" Font-Names="Arial" Font-Size="Small"></telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="MultiPage_afloeser" runat="server" SelectedIndex="0">
                <telerik:RadPageView runat="server" ID="PageViewActivate">


                    <telerik:RadWindowManager runat="server" ID="RadWindowManager_afloeser" EnableShadow="true" CenterIfModal="true" Modal="true">
                    </telerik:RadWindowManager>

                    <asp:Table runat="server" ID="tableUserCreation" CssClass="scheduleBox" HorizontalAlign="Center" Width="40%">
                        <asp:TableRow>
                            <asp:TableCell Width="100%">

                                <asp:Table runat="server" CssClass="tableRowEven">
                                    <asp:TableRow Width="100%">
                                        <asp:TableCell Width="100%" CssClass="leftMargTableCell" ColumnSpan="2">
                            
                           <asp:Label runat="server" Text="Aktivering af afløserkode til CARE:" Font-Size="Large"
                               Font-Bold="true"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Width="100%">
                                        <asp:TableCell Width="50%" CssClass="leftMargTableCell">

                                            <telerik:RadMaskedTextBox runat="server" ID="txtBoxTemporary_socialSecurityNumber" AutoPostBack="True" Mask="######-####"
                                                Skin="Bootstrap" SelectionOnFocus="CaretToBeginning" Width="100%" ResetCaretOnFocus="true"
                                                OnTextChanged="txtBoxTemporary_socialSecurityNumber_TextChanged" HideOnBlur="true" EmptyMessage="Personnummer">
                                                <ClientEvents OnFocus="fnFocus" />
                                            </telerik:RadMaskedTextBox>

                                            <telerik:RadToolTip ID="toolTipSocialSecurity_codeBehind" runat="server" RenderMode="Lightweight" Animation="Resize" Skin="Bootstrap"
                                                Text="Indtast personnummer på den person som skal anvende koden. <br/> Tryk derefter TAB eller ENTER." TargetControlID="txtBoxTemporary_socialSecurityNumber"
                                                Position="MiddleRight" BackColor="#EEF6F8" Font-Names="Arial" Font-Size="10" RelativeTo="Element" ShowEvent="FromCode" AutoCloseDelay="5000">
                                            </telerik:RadToolTip>

                                            <telerik:RadToolTip ID="toolTipSocialSecurity_onClick" runat="server" RenderMode="Lightweight" Animation="Resize" Skin="Bootstrap"
                                                Text="Indtast personnummer på den person som skal anvende koden. <br/>Tryk derefter TAB eller ENTER." TargetControlID="txtBoxTemporary_socialSecurityNumber"
                                                Position="MiddleRight" BackColor="#EEF6F8" Font-Names="Arial" Font-Size="10" RelativeTo="Element" ShowEvent="OnClick" AutoCloseDelay="5000">
                                            </telerik:RadToolTip>
                                        </asp:TableCell>
                                        <asp:TableCell Width="50%" CssClass="leftMargTableCell" RowSpan="1">
                                            <asp:Label runat="server" ID="lblTemporary_name" Font-Italic="true" Font-Size="11"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow Width="100%" runat="server" ID="tableRowDateAndTime" Visible="false">
                                        <asp:TableCell CssClass="leftMargTableCell">
                                            <telerik:RadDatePicker ID="datePickerEndDate" runat="server" Skin="Bootstrap" Culture="da-DK" Width="100%" Font-Size="Small" Visible="true"
                                                OnSelectedDateChanged="datePickerEndDate_SelectedDateChanged" AutoPostBack="true" DateInput-EmptyMessage="Slut dato">
                                                <DateInput ID="DateInputEndDate" Width="" DisplayDateFormat="dd.MM.yyyy" runat="server" DateFormat="dd.MM.yyyy"></DateInput>
                                            </telerik:RadDatePicker>
                                        </asp:TableCell>

                                        <asp:TableCell CssClass="leftMargTableCell">
                                            <telerik:RadTimePicker runat="server" ID="timePickerEndTime" Width="100%" Skin="Bootstrap" DateInput-EmptyMessage="Slut tid"
                                                OnSelectedDateChanged="datePickerEndDate_SelectedDateChanged" AutoPostBack="true" TimeView-Interval="00:15:00" TimeView-Columns="4">
                                                <DateInput runat="server"></DateInput>
                                            </telerik:RadTimePicker>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow Width="100%" runat="server" ID="tableRowTemporaryCodes" Visible="false">

                                        <asp:TableCell CssClass="leftMargTableCell">
                                            <telerik:RadComboBox ID="comboTemporaryCodesAvailable" runat="server" Skin="Bootstrap" Font-Names="Arial" AutoPostBack="true"
                                                EmptyMessage="Vælg bruger" AllowCustomText="true" Width="100%"
                                                Font-Size="Small" Visible="true" OnSelectedIndexChanged="comboTemporaryCodesAvailable_SelectedIndexChanged" Sort="Descending">
                                            </telerik:RadComboBox>
                                        </asp:TableCell>

                                    </asp:TableRow>

                                    <asp:TableRow Width="100%" runat="server" ID="tableRowButton" Visible="false">

                                        <asp:TableCell CssClass="leftMargTableCell" ColumnSpan="2">
                                            <telerik:RadButton runat="server" ID="btnActivateTemporaryCode" Skin="Bootstrap" Text="Aktivér bruger" Width="100%"
                                                SingleClick="true" SingleClickText="Opretter..." OnClick="btnActivateTemporaryCode_Click">
                                            </telerik:RadButton>
                                        </asp:TableCell>

                                    </asp:TableRow>

                                </asp:Table>


                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </telerik:RadPageView>

                <telerik:RadPageView runat="server" ID="PageViewInActivate">

                    <asp:Table runat="server" ID="table1" CssClass="scheduleBox" HorizontalAlign="Center" Width="60%">
                        <asp:TableRow>
                            <asp:TableCell Width="100%">

                                <asp:Table runat="server" CssClass="tableRowEven">
                                    <asp:TableRow Width="100%">
                                        <asp:TableCell Width="30%" CssClass="leftMargTableCell" ColumnSpan="1">
                                            </asp:TableCell>
                                        <asp:TableCell Width="70%" CssClass="leftMargTableCell" ColumnSpan="1">
                            
                           <asp:Label runat="server" Text="Koder i brug:" Font-Size="Large"
                               Font-Bold="true"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow Width="100%">

                                        <asp:TableCell Width="30%" CssClass="leftMargTableCell" ColumnSpan="1">
                                            <telerik:RadButton runat="server" ID="BtnCancel" Skin="Bootstrap" Text="Annullér bestilling" Width="100%"
                                                SingleClick="true" SingleClickText="Vent..." OnClick="BtnCancel_Click" Visible="false" Font-Bold="true">
                                            </telerik:RadButton>
                                        </asp:TableCell>

                                        <asp:TableCell Width="70%" CssClass="leftMargTableCell" ColumnSpan="1">
                                            <telerik:RadGrid runat="server" Skin="Bootstrap" CellSpacing="0" GridLines="None"
                                                AllowSorting="false" AutoGenerateDeleteColumn="False"
                                                AutoGenerateColumns="False" ID="grid_temporaryCodesInUse" Width="100%"
                                                Font-Size="X-Small" MasterTableView-ExpandCollapseColumn-Display="false"
                                                HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center"
                                                OnSelectedIndexChanged="grid_temporaryCodesInUse_SelectedIndexChanged"
                                                OnItemDataBound="grid_temporaryCodesInUse_ItemDataBound">

                                                <ClientSettings AllowColumnsReorder="False" ReorderColumnsOnClient="False" EnablePostBackOnRowClick="true">
                                                    <Selecting AllowRowSelect="True"></Selecting>
                                                </ClientSettings>

                                                <MasterTableView Width="100%" CommandItemDisplay="None" NoDetailRecordsText="Ingen koder i brug" Font-Size="Small">
                                                    <DetailTables>
                                                        <telerik:GridTableView NoDetailRecordsText="Ingen">
                                                        </telerik:GridTableView>
                                                    </DetailTables>
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="ID" UniqueName="ID" HeaderText="ID" Display="false"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Kategori" UniqueName="Kategori" HeaderText="NAVN"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="TempCode_username" UniqueName="TempCode_username" HeaderText="TempCode_username" Display="false"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="StartDato" UniqueName="StartDato" HeaderText="STARTDATO" DataFormatString="{0:dd.MM.yy HH:mm}"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="SlutDato" UniqueName="SlutDato" HeaderText="SLUTDATO" DataFormatString="{0:dd.MM.yy HH:mm}"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Opretter" UniqueName="Opretter" HeaderText="OPRETTER"></telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>

                                            </telerik:RadGrid>

                                            <telerik:RadToolTip ID="ToolTipGrid" runat="server" RenderMode="Lightweight" Animation="Resize" Skin="Bootstrap"
                                                Text="Klik på den bestilling der skal annulleres." TargetControlID="grid_temporaryCodesInUse"
                                                Position="MiddleRight" BackColor="#EEF6F8" Font-Names="Arial" Font-Size="10" RelativeTo="Element" ShowEvent="FromCode" AutoCloseDelay="5000">
                                            </telerik:RadToolTip>

                                        </asp:TableCell>
                                    </asp:TableRow>

                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>

                </telerik:RadPageView>

            </telerik:RadMultiPage>


        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.25;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/loader_gif.gif" AlternateText="HENTER DATA..." ToolTip="HENTER DATA..." Style="padding: 10px; position: fixed; top: 25%; left: 40%; opacity: 0.7;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <script type="text/javascript">
        //Put your JavaScript code here.

        function alertAndRedirect(arg) {
            if (arg == true) {
                window.location.href = "Default.aspx";
            }
        }

        function callBackFn(arg) {

        }

        function fnFocus() {
            document.getElementById("txtBoxTemporary_socialSecurityNumber").focus();
        }

    </script>

</asp:Content>


