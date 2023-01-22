<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AuthenticatedReport.aspx.cs" Inherits="WebPortal.ReportViewer.AuthenticatedReport" %>
<asp:Content ID="StylesContent" ContentPlaceHolderID="PageStyleContent" runat="server">
    <style>
        .td_report
        {
            width:226px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">List of reports</h3>
        </div>
        <div class="panel-body">
            <div class="report-top-text">
                Click on the <strong>Description</strong> to view report
            </div>
            <div class="table-responsive">
                <table id="reportTable" class="table table-bordered table-striped table-hover">
                    <asp:ListView ID="lvReportCategory" runat="server" 
                        OnItemDataBound="lvReportCategory_ItemDataBound" 
                        onitemcommand="lvReportCategory_ItemCommand">
                        <ItemTemplate>
                            <thead>
                                <tr>
                                    <th colspan="2">
                                        <strong>Report category : <%# Eval("ReportCategory")%></strong>
                                    </th>
                                </tr>
                                <tr>
                                    <th style="width: 100px;">No:</th>
                                    <th>Description</th>
                                </tr>
                            </thead>
                            <asp:ListView ID="lvReports" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                        <%# Eval("SortOrder")%>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkReport" runat="server" Text='<%# Eval("ReportDescription")%>'
                                            CommandName = "ShowReport" CommandArgument='<%# Eval("ReportID")  + ";" + Eval("ReportCode")+ ";" + Eval("ReportKey") + ";" + Eval("ReportDescription")%>'>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </ItemTemplate>
                    </asp:ListView>
                </table>
            </div>
        </div>
    </div>
    <div>
        <!-- <div ID="divRoleID" runat="server"></div> -->
        <fieldset>
            
            
        
        <br />
           <%-- <asp:Table runat="server" ID="reportTable" CssClass="report_table" CellPadding="0"
                CellSpacing="0" Border="0">
            </asp:Table>--%>
            <div class="successNotification">
                <asp:Label ID="lblnote" runat="server" Text=""></asp:Label>
            </div>
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="PageScriptContent" runat="server">
    <script type="text/javascript" language="javascript">
        //Purpose:Method For Launching Report in new Window
        function LaunchReport(url) {
            var popupWin;
            popupWin = window.open(url, '_blank', 'menubar=no,toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,width=1000,height=620');
        }

        $(document).ready(function () {
            $('#reportTable > thead > tr:eq(0)').find('.sort-disable').attr('data-orderable', 'false');
            $('#reportTable').DataTable({
                dom: "rrtrr",
                pageLength: 100,
                "ordering": false
            });

        });
    </script>
</asp:Content>
