<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListOfAPIClients.aspx.cs" Inherits="WebPortal.SystemPages.ListOfAPIClients" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <div class="pull-left">
               
                <h3 class="panel-title">
                    API Client list</h3>
            </div>
            <div class="pull-right">
                <asp:LinkButton ID="btnAddNew" runat="server" PostBackUrl="~/SystemPages/AddAPIClient.aspx"
                    CssClass="btn btn-success"><i class="fa fa-plus" aria-hidden="true"></i> Add new API client</asp:LinkButton>
            </div>
            <div class="clearfix">
            </div>
        </div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="loading hide1" id="Waiting_UI">Loading&#8230;</div>
                    <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CssClass="hide hide1"></asp:LinkButton>
                    <div class="table-responsive">
                        <asp:GridView ID="gvAPIClient" runat="server" AutoGenerateColumns="False" GridLines="Vertical"
                            OnRowDataBound="gvAPIClient_RowDataBound"
                            CssClass="table table-bordered table-striped table-hover" ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:BoundField HeaderText="Client ID" DataField="ClientId">
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Name" DataField="Name">
                                    <ItemStyle Wrap="True" HorizontalAlign="Left" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Scope" DataField="Scope">
                                    <ItemStyle Wrap="True" HorizontalAlign="Left" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <%-- <asp:BoundField HeaderText="Active" DataField="IsActive" ItemStyle-CssClass="hide1" HeaderStyle-CssClass="hide1"> --%>
                                <%--     <ItemStyle Wrap="False" HorizontalAlign="Center" /> --%>
                                <%--     <HeaderStyle Wrap="False" CssClass="text-center"/> --%>
                                <%-- </asp:BoundField> --%>
                                 <asp:BoundField HeaderText="Created Date" DataField="CreatedAt">
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Delete" AccessibleHeaderText="Select" HeaderStyle-CssClass="text-center no-sort">
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <button type="button" class="btn btn-danger" onclick='customDeleteDialouge("<%# Eval("ClientId") %>", "<%# HttpUtility.JavaScriptStringEncode(Convert.ToString(Eval("Name"))) %>")'>
                                            <i class="fa fa-trash-o" aria-hidden="true"></i> Delete
                                        </button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:HiddenField ID="hdnIsDeleteAllowed" runat="server" Visible="false" />
                    <div id="dvAlert" style="display: none">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="loading">Loading&#8230;</div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="PageScriptContent" runat="server">
    <script type="text/javascript">
        function GenerateTableSerch(select) {
            var div_md_6 = $("<div />", { class: "col-md-6" });
            var form_inline = $("<div />", { class: "form-inline search-pan pull-right" });
            var search_box = $("<div />", { class: "search-box" });
            var form_group_1 = $("<div />", { class: "form-group", style: "padding-right: 15px;" });
            var lbl_serch = $("<label />").text('Search by :').attr('for', 'ddlSearch');
            var form_group = $("<div />", { class: "form-group" });
            var search_input = $('<input type="text" class="form-control input-sm txtSearch" id="txtSearch" placeholder="Search"/>');
            var search_button = $('<button type="button" class="btn btn-sm" id="btnClear">').append($("<i class='fa fa-times' aria-hidden='true'></i>"));

            form_group_1.append(lbl_serch);
            form_group_1.append(select);

            form_group.append(search_input);
            form_group.append(search_button);

            search_box.append(form_group_1);
            search_box.append(form_group);

            form_inline.append(search_box);
            div_md_6.append(form_inline);
            return div_md_6;
        }

        function pageLoad() {
            $('#MainContent_gvAPIClient > thead > tr:eq(0)').find('.no-sort').attr('data-orderable', 'false');

            var table = $("#MainContent_gvAPIClient").DataTable({
                pageLength: 100,
                "order": [[1, "asc"]],
                dom: "lrt<'myinfo'i>p",
                initComplete: function () {
                    var select = $('<select class="form-control input-sm" id="ddlSearch"/>');
                    $('<option />', { value: '', text: 'Any' }).appendTo(select);
                    $(this).find('thead > tr:eq(0) > th:visible').not('.no-sort').each(function () {
                        $('<option />', { value: $(this).index(), text: $(this).text() }).appendTo(select);
                    });

                    var div_md_6 = GenerateTableSerch(select);
                    $(".dataTables_length > label").wrap("<div class='col-md-6'></div>");
                    $(".dataTables_length").append(div_md_6);

                    $('#Waiting_UI').remove();

                    var column_no = 0;
                    $('#ddlSearch').on('change', function () {
                        column_no = Number($(this).val());
                        table.search('').columns().search('').draw();
                        $('#txtSearch').val("");
                    });

                    $('#txtSearch').on('input', function () {
                        console.log('input');
                        if ($('#ddlSearch').val() == "") {
                            if (table.search() !== $('#txtSearch').val()) {
                                table.search($('#txtSearch').val()).draw();
                            }
                        }
                        else {
                            if (table.columns([column_no]).search() !== $('#txtSearch').val()) {
                                table.columns([column_no]).search($('#txtSearch').val()).draw();
                            }
                        }
                    });

                    $('#btnClear').on('click', function () {
                        table.search('').columns().search('').draw();
                        $('#txtSearch,#ddlSearch').val("");
                    });
                }
            });
        }

        function customDeleteDialouge(id, message) {
            var text = "Are you sure you want to delete '" + message + "' client?";
            var title = "Delete '" + message + "' client";
            var JC = $.confirm({
                title: title,
                content: text,
                buttons: {
                    cancel: {
                        text: '<i class="fa fa-times" aria-hidden="true"></i> Cancel',
                        btnClass: 'btn btn-warning',
                        action: function () {}
                    },
                    Delete: {
                        text: '<i class="fa fa-check" aria-hidden="true"></i> Delete',
                        btnClass: 'btn btn-danger',
                        action: function () {
                            $.ajax({
                                type: "POST",
                                url: "ListOfAPIClients.aspx/DeleteClient",
                                contentType: 'application/json; charset=utf-8',
                                data: '{"id":"' + id + '"}',
                                dataType: 'json',
                                success: function (data) {
                                    console.log(data.d);
                                    if (data.d) {
                                        RefreshWindow();
                                    }
                                    else {
                                        console.log('Error in delete');
                                    }
                                },
                                error: function (data) {
                                    console.log('Error = ' + JSON.stringify(data));
                                }
                            });
                        }
                    }
                }
            });
        }

        function RefreshWindow() {
            __doPostBack('ctl00$MainContent$btnRefresh', '');
        }
    </script>
</asp:Content>
