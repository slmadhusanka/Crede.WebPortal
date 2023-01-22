<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="WebPortal.Account.EditUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pagetitle">
              <nav>
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">Home</li>
                  <li class="breadcrumb-item">Table Maintenance</li>
                    <li class="breadcrumb-item">User</li>
                  <li class="breadcrumb-item active">Update User Information</li>
                </ol>
              </nav>
            </div><!-- End Page Title -->
            <section class="section">
              <div class="row">
                <div class="col-lg-12">
            
            
                    <div class="card hdl-card">
                        <div class="card-header">
                            <asp:HyperLink CssClass="btn-circle btn btn-back hide" ID="HyperLink1" runat="server" NavigateUrl="~/Account/ListOfUser.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i></asp:HyperLink>
                            <h5 class="card-title">Update User Information</h5>
                        </div>
                        <div class="card-body">
                            <!--Success alert-->
                            <div class="alert alert-success alert-dismissable fade in" id="success_alert" runat="server" visible="false">
                                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                <strong>Success!</strong> <asp:Literal ID="SuccessMessage" runat="server"></asp:Literal>
                            </div>
                            <!--Error alerrt-->
                            <div class="alert alert-danger alert-dismissable fade in" id="error_alert" runat="server" visible="false">
                                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                <strong>Error!</strong> <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                            </div>
                            <asp:HiddenField ID="hdnUserID" ClientIDMode="Static" runat="server"/>
                            
                            
                            <div class="col-md-8">
                                <div class="row g-3">
                                <div class="col-md-6 form-group">
                                    <asp:Label ID="lblFirstName" runat="server" CssClass="form-label" AssociatedControlID="txtUserFirstName">First Name <span class="red-star">*</span> :</asp:Label>
                                    
                                        <asp:TextBox ID="txtUserFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFirstNameRequired" runat="server" ControlToValidate="txtUserFirstName" Display="Dynamic"
                                            CssClass="failureNotification" ErrorMessage="*First Name is required." ToolTip="First Name is required."
                                            ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                                    
                                </div>
                                <div class="col-md-6 form-group">
                                    <asp:Label ID="lblLastName" runat="server" CssClass="form-label" AssociatedControlID="txtUserLastName">Last Name <span class="red-star">*</span> :</asp:Label>
                                    
                                        <asp:TextBox ID="txtUserLastName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvLastNameRequired" runat="server" ControlToValidate="txtUserLastName" Display="Dynamic"
                                            CssClass="failureNotification" ErrorMessage="*Last Name is required." ToolTip="Last Name is required."
                                            ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                                    
                                </div>
                                <div class="col-md-6 form-group">
                                    <asp:Label ID="UserNameLabel" runat="server" CssClass="form-label" AssociatedControlID="txtUserName">Username <span class="red-star">*</span> :</asp:Label>
                                    
                                        <asp:TextBox ID="txtUserName" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:CustomValidator ID="cvUserName" runat="server" EnableClientScript="true"
                                            ErrorMessage="*Username is required" ToolTip="Username is required." ValidateEmptyText="true"
                                            ClientValidationFunction="check_username" ControlToValidate="txtUserName"
                                            Display="Dynamic" CssClass="failureNotification" ValidationGroup="RegisterUserValidationGroup">
                                        </asp:CustomValidator>
                                    
                                </div>
                                <div class="col-md-6 form-group">
                                    <asp:Label ID="Label2" runat="server" CssClass="form-label" AssociatedControlID="txtDisplayName">Display Name <span class="red-star">*</span> :</asp:Label>
                                    
                                      <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control"></asp:TextBox>
                                      
                                     <asp:RequiredFieldValidator ID="rfvDisplayNameRequired" runat="server" ControlToValidate="txtDisplayName" Display="Dynamic"
                                            CssClass="failureNotification" ErrorMessage="Display Name is required." ToolTip="Display Name is required."
                                            ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                                    
                                </div>
                                <div class="col-md-6 form-group">
                                    <asp:Label ID="EmailLabel" runat="server" CssClass="form-label" AssociatedControlID="Email">E-mail <span class="red-star">*</span> :</asp:Label>
                                    
                                        <asp:TextBox ID="Email" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                       <asp:CustomValidator ID="cvEmail" runat="server" EnableClientScript="true"
                                           ErrorMessage="*E-mail is required" ToolTip="E-mail is required." ValidateEmptyText="true"
                                           ClientValidationFunction="check_email" ControlToValidate="Email"
                                           Display="Dynamic" CssClass="failureNotification" ValidationGroup="RegisterUserValidationGroup">
                                       </asp:CustomValidator>
                                    
                                </div>
                                 <div class="col-md-6 form-group">
                                    <asp:Label ID="Label7" runat="server" AssociatedControlID="txtPhoneNo" CssClass="form-label ">Phone Number  <span class="red-star">*</span> :</asp:Label>
                                    
                                     
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                              <div class="input-group-text">+1</div>
                                            </div>
                                            <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control"></asp:TextBox>
                                          </div>
                                     
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" EnableClientScript="true"
                                            ErrorMessage="*Phone Number is required" ToolTip="Phone Number is required." ControlToValidate="txtPhoneNo"
                                            Display="Dynamic" CssClass="failureNotification" ValidationGroup="RegisterUserValidationGroup">
                                        </asp:RequiredFieldValidator>
                                    
                                </div>
                                    
                                 <div class="col-md-6 form-group">
                                     <asp:Label ID="NewPasswordLabel" runat="server" CssClass="form-label" AssociatedControlID="NewPassword">Password :</asp:Label>
                                     
                                         <asp:TextBox ID="NewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                         <asp:RegularExpressionValidator ID="NewPasswordRegularExpression" runat="server" Display="Dynamic"
                                             ErrorMessage="*Password must be at least 8 characters long and include at least one special character, one number, and one upper case and lowecase letter."
                                             ValidationGroup="RegisterUserValidationGroup" ValidationExpression="^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).*$"
                                             ControlToValidate="NewPassword" CssClass="failureNotification">
                                         </asp:RegularExpressionValidator>
                                     
                                 </div>
                                 <div class="col-md-6 form-group">
                                     <asp:Label ID="ConfirmNewPasswordLabel" runat="server" CssClass="form-label" AssociatedControlID="ConfirmNewPassword">Retype Password :</asp:Label>
                                     
                                         <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                                             CssClass="failureNotification" ErrorMessage="*Retype password is required." Display="Dynamic"
                                             Enabled="false" ToolTip="Retype password is required." ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                                         <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" Display="Dynamic"
                                             ControlToValidate="ConfirmNewPassword" CssClass="failureNotification" ErrorMessage="*Retype Password must match the Password entry."
                                             ValidationGroup="RegisterUserValidationGroup"></asp:CompareValidator>
                                    
                                 </div> 
                                    
                                 <div class="col-md-6 form-group">
                                    <asp:Label ID="OccupationLbl" runat="server" CssClass="form-label" AssociatedControlID="TxtOccupation">Occupation :</asp:Label>
                                        <asp:TextBox ID="TxtOccupation" runat="server" CssClass="form-control"> </asp:TextBox>
                                </div>
                                <div class="col-md-6 form-group">
                                    <asp:Label ID="RoleLabel" runat="server" CssClass="form-label" AssociatedControlID="ddlRoleName">Role <span class="red-star">*</span> :</asp:Label>
                                    
                                        <asp:DropDownList ID="ddlRoleName" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RoleRequired" runat="server" ControlToValidate="ddlRoleName" Display="Dynamic"
                                            CssClass="failureNotification" ErrorMessage="*Role is required." ToolTip="Role is required."
                                            ValidationGroup="RegisterUserValidationGroup"> </asp:RequiredFieldValidator>
                                    
                                </div>
                                
                                <div class="col-md-6 form-group">
                                    <asp:Label ID="Label3" runat="server" CssClass="form-label" AssociatedControlID="ddlRegion">Region :</asp:Label>
                                    
                                        <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control"
                                            AutoPostBack="True" onselectedindexchanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    
                                </div>
                                <div class="col-md-6 form-group mluty_slect_width">
                                    <asp:Label ID="FacilityLabel" runat="server" CssClass="form-label" AssociatedControlID="ddlLabs">Clinic:</asp:Label>
                                    
                                    <asp:ListBox  ID="ddlLabs"  runat="server"  CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                    
                                </div>
                                <div class="col-md-6 form-group hide">
                                    <asp:Label ID="Label1" runat="server" CssClass="form-label" AssociatedControlID="ddlUnit">Equipment :</asp:Label>
                                    
                                        <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    
                                </div>
                                <div class="col-md-12 form-group check_box">
                                        <div class="form-check">
                                            <asp:CheckBox CssClass="form-check-input" ID="CheckBox1" runat="server" />
                                            <asp:Label CssClass="form-check-label" ID="Label8" runat="server" AssociatedControlID="CheckBox1">
                                                Account disabled
                                            </asp:Label>
                                        </div>
                                        <div class="form-check">
                                            <asp:CheckBox CssClass="form-check-input" ID="chkAuditor" runat="server" />
                                            <asp:Label CssClass="form-check-label" ID="Label4" runat="server" AssociatedControlID="chkAuditor">
                                                Reviewer
                                            </asp:Label>
                                        </div>
                                        <div class="form-check">
                                            <asp:CheckBox CssClass="form-check-input" ID="chkIsActive" runat="server" />
                                            <asp:Label CssClass="form-check-label" ID="Label5" runat="server" AssociatedControlID="chkIsActive">
                                                Active
                                            </asp:Label>
                                        </div>
                                </div>
                                
                            </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <div class="pull-right">
                                <asp:LinkButton ID="DeleteUserButton" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" OnClick="DeleteUserButton_Click">
                                    <i class="bi bi-x"></i> Cancel
                                </asp:LinkButton>
                                <asp:LinkButton ID="UpdateUserButton" runat="server" ValidationGroup="RegisterUserValidationGroup" OnClick="UpdateUserButton_Click" OnClientClick="Validate();" CssClass="btn btn-primary btn-sm">
                                    <i class="bi bi-save"></i> Save
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    
                </div>
              </div>
            </section>
            
              <asp:HiddenField ID="HdnLabValues" runat="server" />
            
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="loading">
                Loading&#8230;</div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="PageScriptContent" runat="server">
   <script type="text/javascript">
       $(document).ready(function () {

           $("#MainContent_txtPhoneNo").inputmask({ "mask": "(###) ###-####" });        

           var assignedLabs = $("#MainContent_HdnLabValues").val().split(',');
           // $('#ddlFacility').val(assignedLabs.split(','));
           if (assignedLabs.length > 0) {
               $.each(assignedLabs, function (i, e) {
                   debugger;
                   //console.log(e);
                   $("#MainContent_ddlLabs option[value='" + e.trim() + "']").prop("selected", true);
                   $("#MainContent_ddlLabs").multiselect('refresh');

               });
           }

            }
       )

       function pageLoad() {
           $('#MainContent_ddlLabs').multiselect({
               includeSelectAllOption: true,
               templates: {
                   button: '<button type="button" class="multiselect dropdown-toggle btn btn-primary" data-bs-toggle="dropdown" aria-expanded="false"><span class="multiselect-selected-text"></span></button>',
               }
           });
       }

     

       function Validate() {

           SetLabValues();

           if (document.getElementById('<%=NewPassword.ClientID %>').value != "")
                ValidatorEnable(document.getElementById('<%=ConfirmNewPasswordRequired.ClientID %>'), true);
            else
                ValidatorEnable(document.getElementById('<%=ConfirmNewPasswordRequired.ClientID %>'), false);
        }

        function CheckDisplayName(sender, args) {
            var chk = document.getElementById('<%=chkAuditor.ClientID%>').checked;
            var v = document.getElementById('<%=txtDisplayName.ClientID%>').value;
           if (chk == true && v == '') {

               args.IsValid = false;  // field is empty
           }
           else {
               args.IsValid = true;
           }
       }

       document.addEventListener('DOMContentLoaded', function () {
           [].forEach.call(document.querySelectorAll('input'), function (el, i) {
               el.addEventListener('blur', ValidatorOnChange);
           });
       });

       function check_username(sender, args) {
           debugger;
           var format = /^(?!.*([@.])\1)[a-zA-Z0-9-_.]{1,}@?(?!.*([@])\1)[a-z0-9A-Z-_.]+$/;
           var username = document.getElementById('txtUserName').value;
           var user_id = Number($('#hdnUserID').val()) || 0;
           if (username == "") {
               args.IsValid = false;
           } else if (username.length < 2) {
               sender.innerHTML = "*Username should have at least 2 characters";
               args.IsValid = false;
           } else if (username.length > 75) {
               sender.innerHTML = "*Username should have maximum 75 characters";
               args.IsValid = false;
           } else if (/\s/.test(username)) {
               sender.innerHTML = "*Spaces are not allowed for username";
               args.IsValid = false;

           } else if (!format.test(username)) {
               sender.innerHTML = "*Invalid username (username contains some disallowed characters.)";
               args.IsValid = false;
           } else {
               data = {
                   user_id: user_id,
                   username: username
               }
               $.ajax({
                   type: "POST",
                   url: "EditUser.aspx/CheckUsernameAvailability",
                   data: JSON.stringify(data),
                   async: false,
                   contentType: 'application/json; charset=utf-8',
                   dataType: 'json',
                   beforeSend: function () {
                       $("#loader").show();
                   },
                   error: function (e) {
                       $("#loader").hide();
                       console.error('Error = ' + JSON.stringify(e));
                   },
                   success: function (_result) {
                       if (_result.d) {
                           args.IsValid = true;
                       } else {
                           sender.innerHTML = "*Username '" + username + "' is already taken";
                           args.IsValid = false;
                       }
                   }
               });
           }
       }

       function check_email(sender, args) {
           var email = document.getElementById('Email').value;
           var user_id = Number($('#hdnUserID').val()) || 0;
           if (email == "") {
               args.IsValid = false;
           } else if (!validate_email(email)) {
               sender.innerHTML = "*Invalid E-mail address.";
               args.IsValid = false;
           } else {
               data = {
                   user_id: user_id,
                   email: email
               }
               $.ajax({
                   type: "POST",
                   url: "EditUser.aspx/CheckEmailAvailability",
                   data: JSON.stringify(data),
                   async: false,
                   contentType: 'application/json; charset=utf-8',
                   dataType: 'json',
                   beforeSend: function () {
                       $("#loader").show();
                   },
                   error: function (e) {
                       $("#loader").hide();
                       console.error('Error = ' + JSON.stringify(e));
                   },
                   success: function (_result) {
                       if (_result.d) {
                           args.IsValid = true;
                       } else {
                           sender.innerHTML = "*E-mail '" + email + "' is already in use.";
                           args.IsValid = false;
                       }
                   }
               });
           }
       }

       function validate_email(email) {
           var re = /^(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])+$/;
           return re.test(email);
       }

       function SetLabValues() {
           var LabValues = $('#MainContent_ddlLabs').val();
           $("#MainContent_HdnLabValues").val(LabValues);
           return true;
       }

   </script>
</asp:Content>
