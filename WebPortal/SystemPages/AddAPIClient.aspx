<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddAPIClient.aspx.cs" Inherits="WebPortal.SystemPages.AddAPIClient" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <ContentTemplate>
        <div class="panel panel-default">
            <div class="panel-heading">
                <asp:HyperLink CssClass="btn-circle btn btn-back" ID="HyperLink1" runat="server" NavigateUrl="~/SystemPages/ListOfAPIClients.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i></asp:HyperLink>
                <h3 class="panel-title">Add API client</h3>
            </div>
            <div class="panel-body">
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

                <div class="form-horizontal col-xs-12 col-sm-12 col-lg-6">
                    <div class="form-group">
                        <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">Client name<span class="red-star">*</span> :</asp:Label>
                        <div class="col-xs-12 col-sm-7 col-lg-8">
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="NameRequired" runat="server" ControlToValidate="txtName" Display="Dynamic"
                                CssClass="failureNotification" ErrorMessage="*API client name is required." ToolTip="API client name is required."
                                ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group hide1">
                        <asp:Label ID="lblScope" runat="server" AssociatedControlID="txtScope" CssClass="control-label col-xs-12 col-sm-4 col-lg-3">Scope<span class="red-star">*</span> :</asp:Label>
                        <div class="col-xs-12 col-sm-7 col-lg-8">
                            <asp:TextBox ID="txtScope" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ScopeRequired" runat="server" ControlToValidate="txtScope" Display="Dynamic"
                                CssClass="failureNotification" ErrorMessage="*API client scope is required." ToolTip="API client scope is required."
                                ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="form-horizontal col-xs-12 col-sm-12 col-lg-6 hide1">
                    <div class="form-group">
                        <div class="col-xs-12 col-sm-offset-4 col-lg-offset-3 col-sm-7 col-lg-8">
                            <div class="checkbox">
                                <asp:Label ID="lblIaActive" runat="server" AssociatedControlID="cbIsActive">
                                    <asp:CheckBox ID="cbIsActive" runat="server" Checked="true" />Active
                                </asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-footer">
                <div class="pull-right">
                    <asp:LinkButton ID="btnRefresh" runat="server" CssClass="btn btn-warning" CausesValidation="false" PostBackUrl="~/SystemPages/ListOfAPIClients.aspx"><i class="fa fa-times" aria-hidden="true"></i> Cancel</asp:LinkButton>
                    <asp:LinkButton ID="UpdateUserButton" runat="server" ValidationGroup="RegisterUserValidationGroup" OnClick="UpdateUserButton_Click" CssClass="btn btn-success"><i class="fa fa-check" aria-hidden="true"></i> Save</asp:LinkButton>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </ContentTemplate>
    <asp:HiddenField ID="hdnIsSaved" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnClientId" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnClientSecret" ClientIDMode="Static" runat="server" />
    <div id="dvAlert" style="display: none">
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            if ($('#hdnIsSaved').val() === 'true') {

                var content = '' +
                    '<div class="form-horizontal col-md-12" id="append-form">' +
                    '<div class="form-group">' +                    
                    '<label class="col-md-4 control-label" for="txtClientId">Client ID</label>' +
                    '<div class="col-md-8">' +
                    '<input type="text" id="txtClientId" value="' +
                    $('#hdnClientId').val() +
                    '" class="form-control" readonly />' +
                    '</div>' +
                    '</div>' +
                    
                    '<div class="form-group">' +
                    '<label class="col-md-4 control-label" for="txtClientSecret">Client Secret</label>' +
                    '<div class="col-md-8">' +
                    '<input type="text" id="txtClientSecret" value="' +
                    $('#hdnClientSecret').val() +
                    '" class="form-control" readonly />' +
                    '</div>' +
                    '</div>' +
                    
                    '<div class="form-group">' +
                    '<label class="col-md-4 control-label" for="txtScopeSaved">Scope</label>' +
                    '<div class="col-md-8">' +
                    '<input type="text" id="txtScopeSaved" value="' +
                    'WebApi.Read' +
                    '" class="form-control" readonly />' +
                    '</div>' +
                    '</div>' +
                    
                    '<div class="form-group">' +
                    '<label class="col-md-4 control-label" for="txtGrantType">Grant Type</label>' +
                    '<div class="col-md-8">' +
                    '<input type="text" id="txtGrantType" value="' +
                    'client_credentials' +
                    '" class="form-control" readonly />' +
                    '</div>' +
                    '</div>' +                  
                    
                    '</div>';

                function download(filename, text) {
                    var element = document.createElement('a');
                    element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(text));
                    element.setAttribute('download', filename);

                    element.style.display = 'none';
                    document.body.appendChild(element);

                    element.click();

                    document.body.removeChild(element);
                }

                var title = "Client Credentials";
                $('#dvAlert').html(content);
                var dlg = $.confirm({
                    title: title,
                    content: content,
                    buttons: {
                        cancel: {
                            text: '<i class="fa fa-times" aria-hidden="true"></i> Cancel',
                            btnClass: 'btn btn-warning',
                            action: function () {
                               window.location.href = "/SystemPages/ListOfAPIClients.aspx";
                               $(this).dialog("close");
                            }
                        },
                        Download: {
                            text: '<i class="fa fa-download" aria-hidden="true"></i> Download',
                            btnClass: 'btn btn-success',
                            action: function () {
                              var text = {
                                  client_id: $('#hdnClientId').val(),
                                  client_secret: $('#hdnClientSecret').val(),
                                  scope: 'WebApi.Read',
                                  grant_type: 'client_credentials'
                              };
                              var filename = "Credentials.json";
                              var string = JSON.stringify(text, null, 4);
                              download(filename, string);
                            }
                        }
                    }
                })                
            }
        });
    </script>
</asp:Content>
