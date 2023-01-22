<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemConfigurations.aspx.cs" Inherits="WebPortal.SystemPages.SystemConfigurations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="pagetitle">
              <h1>Edit System Configuration</h1>
              <nav>
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">Home</li>
                  <li class="breadcrumb-item">System Configuration</li>
                  <li class="breadcrumb-item active">Configuration</li>
                </ol>
              </nav>
            </div><!-- End Page Title -->
            
            <section class="section">
                  <div class="row">
                    <div class="col-lg-12">
                        <div class="card hdl-card">
                        <div class="card-header">
                            <h5 class="card-title">Edit System Configuration</h5>
                        </div>
                        <div class="card-body">
                             <span id = "spnStatus"> </span>  
                            <div class="row">
                                <div class="col-md-4 ">
                                    <div class="card Config-card">
                                        <div class="card-body">  
                                          <h5 class="card-title">User account settings</h5>
                                         
                                            
                                             <div class="form-group con-bg-pan">
                                                 <div class="row">
                                                     <div class="col-md-6">
                                                         <div class="ios-switch" style="float: left; padding-right: 10px;">
                                                             <input id="chkPasswordExpire" type="checkbox" />
                                                             <label for="chkPasswordExpire" class="label-success"></label>
                                                         </div>
                                                         <label style="margin-bottom: 0;" class="control-label" for="txtDescription">Password Expiration</label>
                                                     </div>
                                                     <div class="col-md-6">
                                                         <div id="dvNumberofdays" class="hide-days input-group">
                                                             <input class="form-control" type="number" id="txtPasswordExpDate" value="0" class="sname" />
                                                             <div class="input-group-append">
                                                                 <span class="input-group-text">Days</span>
                                                             </div>
                                                          </div>
                                                     </div>
                                                 </div>
                                                
                
                                            </div>
                                            
                                            <div class="form-group con-bg-pan">
                                                <div class="row">
                                                    <label class="control-label col-md-6" style="margin: 0;" for="txtDeactivateNoLogin">Number of days without a login to deactivate account  <span class="red-star">*</span></label>
                                                        <div class="ios-switch  col-md-6" style="float: right; margin: 0;">
                                                        <div  class="input-group">
                                                            <input type="number" class="form-control sname" id="txtDeactivateNoLogin" value="0" />
                                                            <div class="input-group-append">
                                                                <span class="input-group-text">Days</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                            </div>
                                            
                                            <div class="form-group con-bg-pan">
                                                <div class="row">
                                                    <label class="control-label  col-md-6" style="margin: 0;"  for="txtPasswordResetExpire">Password reset link expires in <span class="red-star">*</span></label>
                                                    
                                                        <div class="ios-switch col-md-6" style="float: right;">
                                                            <div id="Div1" class="hide-days input-group">
                                                                <input type="number" class="text-box form-control" id="txtPasswordResetExpire" value="0" />
                                                                <div class="input-group-append">
                                                                    <span class="input-group-text span-2">Minute(s)</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                </div>
                                            </div>
                                             <div class="form-group d-none">
                                                <div class="ios-switch">
                                                    <input id="chkMFAL" type="checkbox" />
                                                    <label for="chkMFAL" class="label-success"></label>
                                                </div>
                                                <label class="control-label" for="txtDescription">Multifactor authentication for all users</label>
                
                                            </div>
                                            
                                            <div class="form-group d-none">
                                                <div class="ios-switch">
                                                    <input id="chkMAAOnly" type="checkbox" />
                                                    <label for="chkMAAOnly" class="label-success"></label>
                                                </div>
                                                <label class="control-label" for="txtDescription">Multifactor authentication for Administrators only  </label>
                
                                            </div>
                                            
                                            </div>
                                        
                                        
                                      </div>
                                </div>
                                
                                <div class="col-md-4 hide">
                                    <div class="card Config-card">
                                            <div class="card-body">  
                                              <h5 class="card-title">Multifactor authentication (MFA) settings</h5>
                                                
                                                
                                                
                                                
                                                 <div class="form-group con-bg-pan">
                                                     <div class="row">
                                                         <div class="ios-switch col-md-12" style="float: left; padding-right: 10px;">
                                                             <input id="cb_mfa" type="checkbox" />
                                                             <label style="padding-left: 10px;" for="cb_mfa" class="label-success"> Enable Multifactor authentication for User Roles</label>
                                                         </div>
                                                     </div>
                                                </div>
                                                
                                                <div class="form-group con-bg-pan">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:DropDownList  ID="ddl_roles" placeholder="Select User Roles" runat="server" ClientIDMode="Static" CssClass="form-control" multiple="multiple">
                                                                
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                            </div>    
                                    </div>
                                </div>
                                
                                <div class="col-md-4">
                                    <div class="card Config-card">
                                            <div class="card-body">  
                                              <h5 class="card-title">Global site settings</h5>
                                                <div class="form-group con-bg-pan">
                                                    <div class="row">
                                                        <label class="control-label col-md-6" for="txtDescription">Session Timeout 
                                                            <span class="red-star">*</span></label>
                                                         <div class="ios-switch col-md-6" style="float: right;">
                                                             <div class="input-group">
                                                                  <input type="number" id="txtSessionTimeOut" value="0" class="form-control" />
                                                                  <div class="input-group-append">
                                                                     <span class="input-group-text span-2">Minute(s)</span>
                                                                 </div>
                                                             </div>
                                                            
                                                         </div>                                                   
                                                    </div>
                                                </div>
                                                
                                                <div class="form-group con-bg-pan">
                                                    <div class="row">
                                                        <label class="control-label col-md-6" for="txtDescription">Company name <span class="red-star">*</span></label>
                                                      
                                                        <div class="ios-switch col-md-6" style="margin: 0;">
                                                            <input type="text" id="txtCompanyName" class="sname form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                            </div>    
                                    </div>
                                </div>
                            
                        </div>
                        <div class="card-footer">
                            <div class="pull-right">
                                
                                <button type="button" id="Button1" class="btn btn-primary btn-sm" onclick="AddSysConfiguration(event);">
                                                                                           <i class="bi bi-save"></i>&nbsp;Save
                                                                                       </button>
                            </div>
                           
                        </div>
                        </div>
                    </div>
                  </div>
                  </div>
            </section>
            
        </ContentTemplate>
     </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="loading">
                Loading&#8230;
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
 
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="PageScriptContent" runat="server">

    
       <script type="text/javascript">
            var multipleCancelButton;
       
           function Setdata() {

               $.ajax({

                   type: "GET",
                   url: "SystemConfigurations.aspx/GetData",
                   contentType: 'application/json; charset=utf-8',
                   dataType: 'json',
                   async: false,
                   beforeSend: function () {
                       $("#loader").show();
                   },
                   success: function (data) {
                       var result = data.d;
                       var setting = JSON.parse(result[0]);
                       var count = JSON.parse(result[1]);
                      
                       var user_rokes = JSON.parse(result[2]);
                       
                       $.each(setting, function () {
                           if (this.ConfigurationName == "SMS Survey") {
                               $('#smsSurvey').attr('checked', getBool(this.Value));

                           }

                           else if (this.ConfigurationName == "Session TimeOut") {
                               $('#txtSessionTimeOut').val(this.Value);
                           }
                           else if (this.ConfigurationName == "MinimumResponsesReport") {
                               $('#txtMinResponse').val(this.Value);
                           }
                           else if (this.ConfigurationName == "Password Expiration") {
                               if (Number(this.Value) > 0) {
                                   $("#chkPasswordExpire").attr("checked", true);
                                   $("#dvNumberofdays").show()
                                   $("p").show();
                                   $('#txtPasswordExpDate').val(this.Value);
                               }
                               else {
                                   $("p").hide();
                               }

                           }

                           else if (this.ConfigurationName == "Account Deactivate Days") {
                               $('#txtDeactivateNoLogin').val(this.Value);
                           }

                           else if (this.ConfigurationName == "Password Expiration") {
                               $('#txtPasswordResetExpire').val(this.Value);
                           }

                           else if (this.ConfigurationName == "Send Emails") {
                               $('#txtEmailReceiver').val(this.Value);
                           }
                           else if (this.ConfigurationName == "Company Name") {
                               $('#txtCompanyName').val(this.Value);
                           }

                           else if (this.ConfigurationName == "All Users") {
                               $('#chkMFAL').attr('checked', getBool(this.Value));
                           }

                           else if (this.ConfigurationName == "Administrators Only") {
                               $('#chkMAAOnly').attr('checked', getBool(this.Value));
                           }
                           else if (this.ConfigurationName == "MFA") {
                               $('#cb_mfa').attr('checked', getBool(this.Value));
                           }
                           else if (this.ConfigurationName == "PasswordResetLinkExpire") {
                               $('#txtPasswordResetExpire').val(this.Value);
                           }
                       });
                       if(user_rokes.length > 0){
                           $.each(user_rokes, function(i,e){
                               $("#ddl_roles option[value='" + e + "']").prop("selected", true);
                           });
                       }
                       $("#chkPasswordExpire").change(function () {

                           var Chkpassexpire = $("#chkPasswordExpire").prop("checked");

                           if (Chkpassexpire == true) {
                               var setting = JSON.parse(result[0]);
                               var val = setting[2]["Value"]
                               $("p").show();
                               $("#dvNumberofdays").show()
                               $("#txtPasswordExpDate").val(val);
                           }
                           else {

                               $("#dvNumberofdays").hide()
                               $("p").hide();
                               $("#txtPasswordExpDate").val(0);
                           }
                       });

                       $("#chkMFAL").click(function () {
                           $("#chkMAAOnly").prop("checked", true);
                       });

                       $.each(count, function () {

                           $('#spansmscout').text(this.smscount);
                       });

                       if ($('#cb_mfa').is(":checked")) {
                           multipleCancelButton.enable();
                           $('#ddl_roles').removeAttr('disable');
                       }
                       else {
                           multipleCancelButton.removeActiveItems();
                           multipleCancelButton.disable();
                           $('#ddl_roles').attr('disable','disable');
                           $('#ddl_roles').val('');
                       }
                       $("#loader").hide();

                   },
                   error: function (data) {
                       //$("#MainContent_UpdateProgress1").hide();
                       console.log('Error = ' + JSON.stringify(data));
                   }

               });

           }

           function getBool(val) {
               return !!JSON.parse(String(val).toLowerCase());
           }




           function AddSysConfiguration(event) {
               event.preventDefault();
               if ($('#txtSessionTimeOut').val() < 10) {

                   $.alert('Your Session Timeout must be greater than 10 minutes');
                   return false;
               }
               var validate = ValidateBeforeSave();
               if (validate) {
                   var settings = []

                   for (i = 1; i <= 11; i++) {
                       var settingsObj = {};
                       //if (i == '1') {
                       //    settingsObj.Id = '1';
                       //    settingsObj.configurationName = 'SMS Survey'
                       //    settingsObj.value = $('#smsSurvey').is(':checked').toString();
                       //}
                       //else
                       if (i == '2') {
                           settingsObj.Id = '2';
                           settingsObj.configurationName = 'Session TimeOut'
                           settingsObj.value = $('#txtSessionTimeOut').val();
                       }
                       else if (i == '3') {
                           settingsObj.Id = '3';
                           settingsObj.configurationName = 'Password Expiration'
                           settingsObj.value = $('#txtPasswordExpDate').val();
                       }
                       else if (i == '4') {
                           settingsObj.Id = '4';
                           settingsObj.configurationName = 'Account Deactivate Days'
                           settingsObj.value = $('#txtDeactivateNoLogin').val();
                       }
                       //else if (i == '5') {
                       //    settingsObj.Id = '5';
                       //    settingsObj.configurationName = 'Send Emails'
                       //    settingsObj.value = $('#txtEmailReceiver').val();
                       //}
                       else if (i == '6') {
                           settingsObj.Id = '6';
                           settingsObj.configurationName = 'Company Name'
                           settingsObj.value = $('#txtCompanyName').val();
                       }
                       else if (i == '7') {
                           settingsObj.Id = '7';
                           settingsObj.configurationName = 'All Users'
                           settingsObj.value = $('#chkMFAL').is(':checked').toString();
                       }
                       else if (i == '8') {
                           settingsObj.Id = '8';
                           settingsObj.configurationName = 'Administrators Only'
                           settingsObj.value = $('#chkMAAOnly').is(':checked').toString();
                       }
                       else if (i == '9') {
                           settingsObj.Id = '9';
                           settingsObj.configurationName = 'MinimumResponsesReport'
                           settingsObj.value = $('#txtMinResponse').val();
                       }
                       else if (i == '10') {
                           settingsObj.Id = '10';
                           settingsObj.configurationName = 'MFA'
                           settingsObj.value = $('#cb_mfa').is(':checked').toString();
                       }
                       else if (i == '11') {
                           settingsObj.Id = '11';
                           settingsObj.configurationName = 'PasswordResetLinkExpire'
                           settingsObj.value = $('#txtPasswordResetExpire').val();
                       }
                       settings.push(settingsObj);
                   }
                  
                   var data = {
                       "settings": settings
                   }

                   $.ajax({
                       type: "POST",
                       url: "SystemConfigurations.aspx/SaveData",
                       data: JSON.stringify(data),
                       contentType: 'application/json; charset=utf-8',
                       dataType: 'json',
                       beforeSend: function () {
                           $("#loader").show();
                       },
                       success: function (data) {
                           $("#loader").hide();
                           $("#spnStatus").show();
                           $("#spnStatus").text("System configuration saved successfully");
                           $("#spnStatus").css('color', 'green');
                       },
                       error: function (err) {
                           $('#loader').hide();

                           setTimeout(function () {
                               $("#spnStatus").hide('blind', {}, 500)
                           }, 3000);

                           $("#spnStatus").text("Something went wrong, please contact system administrator");
                           $("#spnStatus").css('color', 'red');
                       }
                   });

                   update_mfa();
               }
           }

           function ValidateBeforeSave() {
               var counter = 0;
               if ($('#chkPasswordExpire').is(':checked') && ($('#txtPasswordExpDate').val().length == 0 || Number($('#txtPasswordExpDate').val()) <= 7)) {
                   counter++;
                   $('#chkPasswordExpire').closest('.form-group').addClass('has-error');
                   $.alert('Password Expiration days count cannot less than 7 days.');
               }
               else {
                   $('#chkPasswordExpire').closest('.form-group').removeClass('has-error');
               }

               if ($('#txtDeactivateNoLogin').val().length == 0 || Number($('#txtDeactivateNoLogin').val()) <= 0) {
                   counter++;
                   $('#txtDeactivateNoLogin').closest('.form-group').addClass('has-error');
                   $.alert('Number of days to deactivate account when no logging cannot be empty');
               }
               else {
                   $('#txtDeactivateNoLogin').closest('.form-group').removeClass('has-error');
               }

               if ($('#txtPasswordResetExpire').val().length == 0 || Number($('#txtPasswordResetExpire').val()) <= 0) {
                   counter++;
                   $('#txtPasswordResetExpire').closest('.form-group').addClass('has-error');
                   $.alert('Passowrd reset link expire in days cannot be empty');
               }
               else {
                   $('#txtPasswordResetExpire').closest('.form-group').removeClass('has-error');
               }

               if ($('#cb_mfa').is(':checked') && $('#ddl_roles').val() == null) {
                   counter++;
                   $('#cb_mfa').closest('.form-group').addClass('has-error');
                   $.alert('Passowrd reset link expire in days cannot be empty');
               }
               else {
                   $('#cb_mfa').closest('.form-group').removeClass('has-error');
               }

               if ($('#txtSessionTimeOut').val().length == 0 || Number($('#txtSessionTimeOut').val()) <= 0) {
                   counter++;
                   $('#txtSessionTimeOut').closest('.form-group').addClass('has-error');
                   $.alert('Session Timeout value cannot be empty');
               }
               else {
                   $('#txtSessionTimeOut').closest('.form-group').removeClass('has-error');
               }

               if ($('#txtCompanyName').val().length == 0) {
                   counter++;
                   $('#txtCompanyName').closest('.form-group').addClass('has-error');
                   $.alert('Company name cannot be empty');
               }
               else {
                   $('#txtCompanyName').closest('.form-group').removeClass('has-error');
               }

               //if ($('#txtEmailReceiver').val().length == 0 && !validate_email($('#txtEmailReceiver').val())) {
               //    counter++;
               //    $('#txtEmailReceiver').closest('.form-group').addClass('has-error');
               //    $.alert('New account email receiver cannot be empty');
               //}
               //else {
               //    $('#txtEmailReceiver').closest('.form-group').removeClass('has-error');
               //}

               //if ($('#txtMinResponse').val().length == 0 || Number($('#txtMinResponse').val()) <= 0) {
               //    counter++;
               //    $('#txtMinResponse').closest('.form-group').addClass('has-error');
               //    $.alert('Minimum number of Responses for a Survey to report results cannot be empty');
               //}
               //else {
               //    $('#txtMinResponse').closest('.form-group').removeClass('has-error');
               //}
               var result = true;
               if (counter != 0) {
                   result = false;
               }
               return result;
           }



           function update_mfa() {
               var data
               if ($('#cb_mfa').is(':checked')) {
                   var user_roles = $('#ddl_roles').val();
                   data = {
                       "user_roles": user_roles
                   }
               }
               else {
                   data = {
                       "user_roles": []
                   }
               }

               $.ajax({
                   type: "POST",
                   url: "SystemConfigurations.aspx/UpdateMFA",
                   data: JSON.stringify(data),
                   contentType: 'application/json; charset=utf-8',
                   dataType: 'json',
                   beforeSend: function () {
                       $("#loader").show();
                   },
                   success: function (data) {
                       $("#loader").hide();
                       $("#spnStatus").show();
                       $("#spnStatus").text("System configuration saved successfully");
                       $("#spnStatus").css('color', 'green');
                   },
                   error: function (err) {
                       $('#loader').hide();

                       setTimeout(function () {
                           $("#spnStatus").hide('blind', {}, 500)
                       }, 3000);

                       $("#spnStatus").text("Something went wrong, please contact system administrator");
                       $("#spnStatus").css('color', 'red');
                   }
               });
           }

           function pageLoad() {
               multipleCancelButton = new Choices('#ddl_roles', {
                   removeItemButton: true,
                   maxItemCount:5,
                   searchResultLimit:5,
                   renderChoiceLimit:5
                 }); 
               $(".fancybox").fancybox();
               $(".various").fancybox({
                   maxWidth: 500,
                   maxHeight: 180,
                   fitToView: false,
                   width: '90%',
                   height: '90%',
                   autoSize: false,
                   closeClick: false,
                   openEffect: 'elastic',
                   closeEffect: 'elastic'
               });

               $("#spnStatus").empty();



               Setdata();

               $('#cb_mfa').on('change', function () {
                   if ($(this).is(':checked')) {
                       multipleCancelButton.enable();
                   }
                   else {
                       multipleCancelButton.removeActiveItems();
                       multipleCancelButton.disable();
                       $('#ddl_roles').attr('disable', 'disable');
                       $('#ddl_roles').val('');
                   }
               });
           }
       </script>
</asp:Content>