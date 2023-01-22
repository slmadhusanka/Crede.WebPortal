﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListOfClinic.aspx.cs" Inherits="WebPortal.SystemPages.ListOfFacilityM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="pagetitle">
     
      <nav>
        <ol class="breadcrumb">
          <li class="breadcrumb-item">Home</li>
          <li class="breadcrumb-item">Clinic & Equipment</li>
          <li class="breadcrumb-item active">Clinic</li>
        </ol>
      </nav>
    </div><!-- End Page Title -->
    
    <section class="section">
          <div class="row">
            <div class="col-lg-12">
                <div class="card hdl-card">
                    <div class="card-header">
                        <h5 class="card-title">Clinic list</h5>
                         <asp:LinkButton role="button" ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" CssClass="btn btn-primary btn-sm">
                            <i class="bi bi-plus"></i> Add New
                        </asp:LinkButton>
                    </div>
                    <div class="card-body">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                        <div class="loading" id="Waiting_UI">Loading&#8230;</div>
                            <!--Error alerrt-->
                            <div class="alert alert-danger alert-dismissable fade in" id="error_alert" runat="server" visible="false">
                                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                <strong>Error!</strong> <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                            </div>
                            <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CssClass="hide hide1"></asp:LinkButton>
                        <div class="table-responsive">
                            <asp:GridView ID="gvDimFacility"
                             runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"
                                CssClass = "table table-bordered table-striped table-hover"
                                onrowdatabound="gvDimFacility_RowDataBound"
                                onrowediting="gvDimFacility_RowEditing">  
                                <Columns>
                                    <asp:BoundField HeaderText="Clinic code" DataField="FacilityCode" ItemStyle-CssClass="hide1" HeaderStyle-CssClass="hide1">
                                        <ItemStyle Wrap="False" HorizontalAlign="Left" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Clinic" DataField="Description">
                                        <ItemStyle Wrap="False" CssClass="" HorizontalAlign="Left" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="FacilityTypeCode" DataField="FacilityTypeCode">
                                        <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="hide1" />
                                        <HeaderStyle Wrap="False" CssClass="hide1" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Clinic type" DataField="FacilityTypeDesc">
                                        <ItemStyle Wrap="False" HorizontalAlign="Left" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="RegionCode" DataField="RegionCode">
                                        <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="hide1" />
                                        <HeaderStyle Wrap="False" CssClass="hide1" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Region" DataField="RegionCodeDesc">
                                        <ItemStyle Wrap="False" HorizontalAlign="Left" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Last changed date" DataField="LastChangedDate" ItemStyle-CssClass="hide1" HeaderStyle-CssClass="hide1">
                                        <ItemStyle Wrap="False" HorizontalAlign="Left" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Description long" DataField="DescriptionLong">
                                        <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="hide1" />
                                        <HeaderStyle Wrap="False" CssClass="hide1" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Description short" DataField="DescriptionShort">
                                        <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="hide1" />
                                        <HeaderStyle Wrap="False" CssClass="hide1" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Active" DataField="IsActive">
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                        <HeaderStyle Wrap="False" CssClass="text-center"/>
                                    </asp:BoundField>
                                    <asp:TemplateField  HeaderText="Edit" HeaderStyle-CssClass="text-center no-sort hide" AccessibleHeaderText="Edit">
                                        <HeaderStyle CssClass="hide" Width="60px" Wrap="False" />
                                        <ItemStyle CssClass="hide" Wrap="False" HorizontalAlign="Center"/>
                                        <ItemTemplate>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Settings" AccessibleHeaderText="Select" HeaderStyle-CssClass="text-center no-sort">
                                        <HeaderStyle Width="60px" Wrap="False" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <div class="btn-group">
                                              <button class="btn btn-outline-primary btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                <i class="bi bi-gear"></i>
                                              </button>
                                              <ul class="dropdown-menu">
                                                <li>
                                                    <asp:LinkButton  style="color: #212529;" ID="btnEdit" CssClass="dropdown-item" runat="server" CommandName="Edit" AlternateText="Edit" meta:resourcekeyToolTip="Click to Edit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>
                                                        <i class="bi bi-pencil"></i> Edit
                                                    </asp:LinkButton> 
                                                </li>
                                                <li>
                                                    <button style="color: #dc3545;" type="button" class="dropdown-item" onclick='customDeleteDialouge("<%# Eval("FacilityCode") %>", "<%# HttpUtility.JavaScriptStringEncode(Convert.ToString(Eval("Description"))) %>")'>
                                                        <i class="bi bi-trash"></i> Delete
                                                    </button>
                                                </li>
                                              </ul>
                                            </div>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="hdnIsEditAllowed" runat="server" Visible="false" />
                            <asp:HiddenField ID="hdnIsDeleteAllowed" runat="server" Visible="false" />
                        </div>
                            
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
            </div>
          </div>
    </section>
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="PageScriptContent" runat="server">
    <script type="text/javascript">
        function GenerateTableSerch(select) {
            var div_md_6 = $("<div />", { class: "col-md-9 data-ser" });
            var form_inline = $("<div />", { class: "form-inline search-pan pull-right" });
            var search_box = $("<div />", { class: "search-box form-row" });
            var form_group_1 = $("<div />", { class: "form-group", style: "padding-right: 15px;" });
            var lbl_serch = $("<label />").text('Search by : ').attr('for', 'ddlSearch');
            var form_group = $("<div />", { class: "form-group" });
            var search_input = $('<input type="text" class="form-control form-control-sm search-ddl txtSearch data-search-text" id="txtSearch" placeholder="Search"/>');
            var search_button = $('<button type="button" class="btn btn-outline-secondary btn-sm data-search-btn" id="btnClear">').append($("<i class='fa fa-times' aria-hidden='true'></i>"));
            
            
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
            $('#MainContent_gvDimFacility > thead > tr:eq(0)').find('.no-sort').attr('data-orderable', 'false');

            var table = $("#MainContent_gvDimFacility").DataTable({
                pageLength: 100,
                "order": [[1, "asc"]],
                dom: "lrt<'myinfo'i>p",
                initComplete: function () {
                    var select = $('<select class="form-control form-control-sm search-ddl" id="ddlSearch"/>');
                    $('<option />', { value: '', text: 'Any' }).appendTo(select);
                    $(this).find('thead > tr:eq(0) > th:visible').not('.no-sort').each(function () {
                        $('<option />', { value: $(this).index(), text: $(this).text() }).appendTo(select);
                    });

                    var div_md_6 = GenerateTableSerch(select);
                    $(".dataTables_length > label").wrap("<div class='col-md-3 data-sot'></div>");
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
            var text = "Are you sure you want to delete '" + message + "' Clinic?";
            var title = "Delete '" + message + "' Clinic";

            $('#dvAlert').html(document.createTextNode(text));

            var jc = $.confirm({
                title: title,
                content: text,
                buttons: {
                    cancel: {
                        text: '<i class="fa fa-times" aria-hidden="true"></i> Cancel',
                        btnClass: 'btn btn-warning',
                        action: function () {}
                    },
                    delete: {
                        text: '<i class="fa fa-check" aria-hidden="true"></i> Delete',
                        btnClass: 'btn btn-danger',
                        action: function () {
                            $.ajax({
                                type: "POST",
                                url: "ListOfClinic.aspx/DeleteFacility",
                                contentType: 'application/json; charset=utf-8',
                                data: '{"id":"' + id + '"}',
                                dataType: 'json',
                                success: function (data) {



                                    
                                    switch (data.d) {
                                        case 0:
                                            $.alert({
                                                title: 'Warning ',
                                                content: 'Unable to delete.',
                                            });
                                            break;
                                        case 1:
                                            $.alert({
                                                title: 'Success ',
                                                content: 'Data deleted successfully.',
                                            });
                                            break;
                                        case 2:
                                            $.alert({
                                                title: 'Warning ',
                                                content: 'Unable to delete the selected data as it is referencing users list and reprocessing log and equipment list.',
                                            });
                                            break;
                                        case 3:
                                            $.alert({
                                                title: 'Warning ',
                                                content: 'Unable to delete the selected data as it is referencing users list and equipment list.',
                                            });
                                            break;
                                        case 4:
                                            $.alert({
                                                title: 'Warning ',
                                                content: 'Unable to delete the selected data as it is referencing reprocessing log and equipment list.',
                                            });
                                            break;
                                        case 5:
                                            $.alert({
                                                title: 'Warning ',
                                                content: 'Unable to delete the selected data as it is referencing reprocessing log and user list.',
                                            });
                                            break;
                                        case 6:
                                            $.alert({
                                                title: 'Warning ',
                                                content: 'Unable to delete the selected data as it is referencing equipment list.',
                                            });
                                            break;
                                        case 7:
                                            $.alert({
                                                title: 'Warning ',
                                                content: 'Unable to delete the selected data as it is referencing user list.',
                                            });
                                            break;
                                        case 8:
                                            $.alert({
                                                title: 'Warning ',
                                                content: 'Unable to delete the selected data as it is referencing reprocessing log.',
                                            });
                                            break;
                                    }

                                    RefreshWindow();

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