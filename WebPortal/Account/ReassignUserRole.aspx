<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReassignUserRole.aspx.cs" Inherits="WebPortal.Account.ReassignUserRole" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <title>Crede Clean Hands</title> 
    <link rel="shortcut icon" href="~/images/favicon.ico" type="image/x-icon" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--Bootstrap css-->
    <link href="~/Styles/bootstrap.css" rel="stylesheet" type="text/css" />

    <!--Font awesome-->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous" />

    <!--Google fonts-->
    <link href="https://fonts.googleapis.com/css?family=Titillium+Web:200,200i,300,300i,400,400i,600,600i,700,700i,900" rel="stylesheet" />

    <!--Custom css files-->
    <link href="~/Styles/default.css" rel="stylesheet" type="text/css" /> 

    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.js" type="text/javascript"></script>
</head>
<body onkeydown = "return (event.keyCode!=13)">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" EnablePageMethods="True" AsyncPostBackTimeout="300">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title" id="lgText" runat="server">Reassign user role information and try again to deactivate/delete role.</h3>
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
                        <div class="alert alert-danger alert-dismissable fade in hide" id="div_alert">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                            <strong>Error!</strong> <p id="errorMessage"></p>
                        </div>
                        <div class="table-responsive">
                            <table id="tbl_useRoles" class="table table-bordered table-striped table-hover">
                                <thead>
                                    <tr>
                                        <th>Username</th>
                                        <th>Select role</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lvUserList" runat="server" OnItemDataBound="lvUserList_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                   <label> <%# Eval("FirstName")%> <%# Eval("LastName")%> </label>

                                                    <asp:HiddenField ID="hdnUserId" runat="server" Value='<%# Eval("UserID")%>' />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                   <asp:RequiredFieldValidator ID="FacilityTypeRequired" runat="server" ControlToValidate="ddlRole" Display="Dynamic"
                                                        CssClass="failureNotification" ErrorMessage="*User role is required." ToolTip="User role is required."
                                                        ValidationGroup="RoleValidationGroup"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>                            
                                        </ItemTemplate>
                                    </asp:ListView>        
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <div class="pull-right">
                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-warning" CausesValidation="false" OnClientClick="javascript:parent.jQuery.fancybox.close();"><i class="fa fa-times" aria-hidden="true"></i> Cancel</asp:LinkButton>
                            <asp:LinkButton ID="btnSave" runat="server" ValidationGroup="RoleValidationGroup" OnClick="btnSave_Click" CssClass="btn btn-success"><i class="fa fa-check" aria-hidden="true"></i> Save</asp:LinkButton>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
