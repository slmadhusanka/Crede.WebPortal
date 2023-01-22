<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateSystemConfigurationM.aspx.cs" Inherits="WebPortal.SystemPages.UpdateSystemConfigurationM" %>
<%@ Register TagPrefix="ajaxtool" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=20.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Edit Configuration</h3>
                </div>
                <div class="panel-body">
                    <!--Success alert-->
                    <div class="alert alert-success alert-dismissable" id="success_alert" runat="server" visible="false">
                       
                        <strong>Success!</strong> <asp:Literal ID="SuccessMessage" runat="server"></asp:Literal>
                    </div>
                    <!--Error alerrt-->
                    <div class="alert alert-danger alert-dismissable" id="error_alert" runat="server" visible="false">
                       
                        <strong>Error!</strong> <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </div>

                    <div class="form-horizontal col-xs-12 col-sm-12 col-lg-6">
                        <div class="form-group hide hide1">
                            <asp:Label ID="FacilityCodeLabel" runat="server" 
                    AssociatedControlID="lblsystemconfigid" Visible="false" CssClass="control-label col-xs-12 col-sm-4 col-lg-4">Configuration ID:</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-5">
                                <asp:Label ID="lblsystemconfigid" runat="server" AssociatedControlID="txtauditduration" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <%--<div class="form-group">
                            <asp:Label ID="lblResult1" runat="server" CssClass="control-label col-xs-12 col-sm-4 col-lg-3" AssociatedControlID="txtReviewLimit">Review limit :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-5">
                                <div class="input-group ck-input">
                                    <span class="input-group-addon no-bg">
                                        <asp:CheckBox ID="chkReviewLimit" runat="server" OnCheckedChanged="chkReviewLimit_CheckedChanged" AutoPostBack="true" Checked="true" />
                                    </span>
                                    <asp:TextBox ID="txtReviewLimit" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">
                                        Reviews
                                    </span>
                                </div>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtReviewLimit" ErrorMessage="Review limit must be &gt; 999"
                                    Operator="GreaterThan" Type="Integer" ValueToCompare="999" ValidationGroup="RegisterUserValidationGroup" CssClass="failureNotification" />
                                <ajaxtool:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender7" TargetControlID="txtReviewLimit"
                                    FilterMode="ValidChars" FilterType="Numbers">
                                </ajaxtool:FilteredTextBoxExtender>
                            </div>
                        </div>--%>
                        <div class="form-group">
                            <asp:Label ID="HCWDetailsLabel" runat="server" AssociatedControlID="txtauditduration" CssClass="control-label col-xs-12 col-sm-4 col-lg-4">Initial Audit Duration <span class="red-star">*</span> :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-5">
                                <div class="input-group">
                                    <asp:TextBox ID="txtauditduration" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon" data-toggle="popover" data-placement="top" data-content="Enter the time in minutes allowed to complete the audit" data-trigger="hover">
                                        <i class="fa fa-question-circle"></i>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtauditduration"
                                    CssClass="failureNotification" ErrorMessage="Initial Audit Duration required." ToolTip="Initial Audit Duration required"
                                    ValidationGroup="RegisterUserValidationGroup" Display="Dynamic"></asp:RequiredFieldValidator>

                                <ajaxtool:FilteredTextBoxExtender runat="server" ID="filtertextboc1" TargetControlID="txtauditduration"
                                    FilterMode="ValidChars" FilterType="Numbers"   >
                                </ajaxtool:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" AssociatedControlID="txtadditionaltime" CssClass="control-label col-xs-12 col-sm-4 col-lg-4">Additional Time :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-5">
                                <div class="input-group">
                                    <asp:TextBox ID="txtadditionaltime" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon" data-toggle="popover" data-placement="top" data-content="Enter the additional time in minutes allowed to complete the audit" data-trigger="hover">
                                        <i class="fa fa-question-circle"></i>
                                    </span>
                                </div>
                                <ajaxtool:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" 
                                    TargetControlID="txtadditionaltime" FilterMode="ValidChars" FilterType="Numbers"   >
                                </ajaxtool:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label7" runat="server" AssociatedControlID="txtMinHCWObservation" CssClass="control-label col-xs-12 col-sm-4 col-lg-4">Minimum HCPs Observed <span class="red-star">*</span> :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-5">
                                <div class="input-group">
                                    <asp:TextBox ID="txtMinHCWObservation" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon" data-toggle="popover" data-placement="top" data-content="Enter the minimum number of HCPs to be audited" data-trigger="hover">
                                        <i class="fa fa-question-circle"></i>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMinHCWObservation"
                                    CssClass="failureNotification" ErrorMessage="Minimum HCPs Observed required." ToolTip="Minimum HCPs Observed required"
                                    ValidationGroup="RegisterUserValidationGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                  
                                <ajaxtool:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender6" TargetControlID="txtMinHCWObservation"
                                    FilterMode="ValidChars" FilterType="Numbers"   >
                                </ajaxtool:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label4" runat="server" AssociatedControlID="txtminobservation" CssClass="control-label col-xs-12 col-sm-4 col-lg-4">Minimum Observations <span class="red-star">*</span> :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-5">
                                <div class="input-group">
                                    <asp:TextBox ID="txtminobservation" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon" data-toggle="popover" data-placement="top" data-content="Enter the minimum number of total observations for the audit" data-trigger="hover">
                                        <i class="fa fa-question-circle"></i>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtminobservation"
                                    CssClass="failureNotification" ErrorMessage="Minimum Observations required." ToolTip="Minimum Observations required"
                                    ValidationGroup="RegisterUserValidationGroup" Display="Dynamic"></asp:RequiredFieldValidator>

                                <ajaxtool:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender2" TargetControlID="txtminobservation"
                                    FilterMode="ValidChars" FilterType="Numbers"   >
                                </ajaxtool:FilteredTextBoxExtender>
                            </div>
                        </div>
                          <div class="form-group">
                            <asp:Label ID="Label5" runat="server" AssociatedControlID="txtobservationperhcw" 
                                CssClass="control-label col-xs-12 col-sm-4 col-lg-4">Minimum Observations/HCP <span class="red-star">*</span> :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-5">
                                <div class="input-group">
                                    <asp:TextBox ID="txtobservationperhcw" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon" data-toggle="popover" data-placement="top" data-content="Enter the Minimum Observations/HCP for the audit" data-trigger="hover">
                                        <i class="fa fa-question-circle"></i>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtobservationperhcw"
                                    CssClass="failureNotification" ErrorMessage="Minimum Observations/HCP required." ToolTip="Minimum Observations/HCP required"
                                    ValidationGroup="RegisterUserValidationGroup" Display="Dynamic"></asp:RequiredFieldValidator>

                                <ajaxtool:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender3" TargetControlID="txtobservationperhcw"
                                    FilterMode="ValidChars" FilterType="Numbers"   >
                                </ajaxtool:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                    <div class="form-horizontal col-xs-12 col-sm-12 col-lg-6">
                      
                        <div class="form-group">
                            <asp:Label ID="Label3" runat="server" AssociatedControlID="txtobservationperhcw" 
                                CssClass="control-label col-xs-12 col-sm-4 col-lg-4">Maximum Observations/HCP <span class="red-star">*</span> :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-5">
                                <div class="input-group">
                                    <asp:TextBox ID="txtMaxobservationperhcw" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon" data-toggle="popover" data-placement="top" data-content="Enter the maximum observations/HCP for the audit" data-trigger="hover">
                                        <i class="fa fa-question-circle"></i>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMaxobservationperhcw"
                                    CssClass="failureNotification" ErrorMessage="Maximum Observations/HCP required." ToolTip="Maximum Observations/HCP required"
                                    ValidationGroup="RegisterUserValidationGroup" Display="Dynamic"></asp:RequiredFieldValidator>

                                <ajaxtool:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender5" TargetControlID="txtMaxobservationperhcw"
                                    FilterMode="ValidChars" FilterType="Numbers"   >
                                </ajaxtool:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-offset-4 col-lg-offset-4 col-sm-7 col-lg-5">
                                <div class="checkbox">
                                    <asp:Label ID="Label8" runat="server" AssociatedControlID="chkenableresulttimer">
                                        <asp:CheckBox ID="chkenableresulttimer" onclick="check_result_timer();" runat="server" />Enable Result Timer
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" AssociatedControlID="txtresultimeduration" 
                                CssClass="control-label col-xs-12 col-sm-4 col-lg-4">Result Timer Duration :</asp:Label>
                            <div class="col-xs-12 col-sm-7 col-lg-5">
                                <div class="input-group">
                                    <asp:TextBox Enabled="false" ID="txtresultimeduration" runat="server" CssClass=" form-control"></asp:TextBox>
                                    <span class="input-group-addon" data-toggle="popover" data-placement="top" data-content="Enter the minimum time in seconds required for a successful Wash or Rub" data-trigger="hover">
                                        <i class="fa fa-question-circle"></i>
                                    </span>
                                </div>
                                <ajaxtool:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender4" TargetControlID="txtresultimeduration"
                                    FilterMode="ValidChars" FilterType="Numbers"   >
                                </ajaxtool:FilteredTextBoxExtender>
                            </div>
                        </div>
                          <div class="form-group">
                            <div class="col-xs-12 col-sm-offset-4 col-lg-offset-4 col-sm-7 col-lg-5">
                                <div class="checkbox">
                                    <asp:Label ID="Label6" runat="server" AssociatedControlID="chkEnaglePPE">
                                        <asp:CheckBox ID="chkEnaglePPE" runat="server" />Enable PPE
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                          <div class="form-group">
                            <div class="col-xs-12 col-sm-offset-4 col-lg-offset-4 col-sm-7 col-lg-5">
                                <div class="checkbox">
                                    <asp:Label ID="Label9" runat="server" AssociatedControlID="chkEnaglePrecaution">
                                        <asp:CheckBox ID="chkEnaglePrecaution" runat="server" />Enable Precautions
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <div class="pull-right">
                        <asp:LinkButton ID="UpdateUserButton" runat="server" ValidationGroup="RegisterUserValidationGroup" OnClick="UpdateUserButton_Click" CssClass="btn btn-success"><i class="fa fa-check" aria-hidden="true"></i> Save</asp:LinkButton>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
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
        function pageLoad() {
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
            $(".input-group-addon").popover({
                trigger: 'hover'
            });
        }

        function check_result_timer() {
            var chkenable = document.getElementById("<%=chkenableresulttimer.ClientID%>");
            if (chkenable.checked == true) {
                // txtresultimeduration Label1
                document.getElementById("<%=txtresultimeduration.ClientID%>").value = "";
                document.getElementById("<%=txtresultimeduration.ClientID%>").disabled = false;
                //   alert('true');
            }
            else {

                document.getElementById("<%=txtresultimeduration.ClientID%>").value = "";
                document.getElementById("<%=txtresultimeduration.ClientID%>").disabled = true;

                //alert('false');
            }
        }
    </script>
</asp:Content>