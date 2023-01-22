<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListOfUser.aspx.cs" Inherits="WebPortal.Account.ListOfUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pagetitle">
        <nav>
            <ol class="breadcrumb">
              <li class="breadcrumb-item">Home</li>
              <li class="breadcrumb-item">Table Maintenance</li>
              <li class="breadcrumb-item active">User</li>
            </ol>
        </nav>
    </div><!-- End Page Title -->
    <section class="section">
                  <div class="row">
                    <div class="col-lg-12">
                        <div class="card hdl-card">
                            <div class="card-header">
                                <h5 class="card-title">User list</h5>
                                    <asp:LinkButton ID="btnAddNew" runat="server" PostBackUrl="~/Account/Register.aspx" CssClass="btn btn-primary btn-sm">
                                       <i class="bi bi-person-plus"></i> &nbsp;Add New User
                                    </asp:LinkButton>
                            </div>
                            <div class="card-body">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="loading" id="Waiting_UI">Loading&#8230;</div>
                                        <div class="alert alert-danger alert-dismissable fade in" id="error_alert" runat="server" visible="false">
                                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                            <strong>Error!</strong> <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                                        </div>
                                        <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CssClass="hide hide1"></asp:LinkButton>
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvUserSummary" runat="server" GridLines="Vertical" 
                                                OnRowEditing="gvUserSummary_RowEditing" OnRowDataBound="gvUserSummary_RowDataBound"
                                                ShowHeaderWhenEmpty="true" CssClass="table table-bordered table-striped table-hover"
                                                AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Username" DataField="UserName">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                        <HeaderStyle Wrap="false" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="User id" DataField="User_ID">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="false" CssClass="hide1" />
                                                        <HeaderStyle Wrap="false" CssClass="hide1" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="First name" DataField="FirstName">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" />
                                                        <HeaderStyle Wrap="false" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Last name" DataField="LastName">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" />
                                                        <HeaderStyle Wrap="false" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Display name" DataField="DisplayName">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" />
                                                        <HeaderStyle Wrap="false" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Role code" DataField="RoleCode">
                                                        <ItemStyle HorizontalAlign="Center" CssClass="hide1" />
                                                        <HeaderStyle Wrap="false" CssClass="hide1" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Role" DataField="RoleDescription">
                                                        <ItemStyle HorizontalAlign="left" Wrap="true" />
                                                        <HeaderStyle Wrap="false" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Region code" DataField="RegionCode">
                                                        <ItemStyle HorizontalAlign="Left" CssClass="hide1" />
                                                        <HeaderStyle Wrap="false" CssClass="hide1" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Region" DataField="Region">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="hide1" />
                                                        <HeaderStyle Wrap="False" CssClass="hide1" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Clinic code" DataField="FacilityCode">
                                                        <ItemStyle HorizontalAlign="Left" CssClass="hide1" />
                                                        <HeaderStyle Wrap="true" CssClass="hide1" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Clinic" DataField="FacilityDescription">
                                                        <ItemStyle HorizontalAlign="Left"  Wrap="true"  />
                                                        <HeaderStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Unit code" DataField="UnitCode">
                                                        <ItemStyle HorizontalAlign="Left" CssClass="hide1" />
                                                        <HeaderStyle Wrap="true" CssClass="hide1" />
                                                    </asp:BoundField>
                                                   <asp:BoundField HeaderText="Unit and description" DataField="Unit" >
                                                        <ItemStyle HorizontalAlign="Left"  Wrap="true" CssClass="hide1"  />
                                                        <HeaderStyle Wrap="true" CssClass="hide1" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Email" DataField="Email">
                                                        <ItemStyle  HorizontalAlign="Left"  Wrap="false"  />
                                                        <HeaderStyle Wrap="true" />
                                                    </asp:BoundField>
                                                     <asp:BoundField HeaderText="Phone" DataField="PhoneNumber">
                                                        <ItemStyle  HorizontalAlign="Left"  Wrap="false"  />
                                                        <HeaderStyle Wrap="true" />
                                                    </asp:BoundField>
                                                      <asp:BoundField HeaderText="Occupation" DataField="Occupation">
                                                        <ItemStyle  HorizontalAlign="Left"  Wrap="true"  />
                                                        <HeaderStyle Wrap="true" />
                                                    </asp:BoundField>

                                                    <asp:BoundField HeaderText="Disabled" DataField="IsLockedOut">
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="true" />
                                                        <HeaderStyle Wrap="true" CssClass="text-center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Reviewer" DataField="IsAuditor">
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="true"  />
                                                        <HeaderStyle Wrap="true" CssClass="text-center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Active" DataField="IsActive">
                                                        <ItemStyle  HorizontalAlign="Center"  Wrap="true"  />
                                                        <HeaderStyle  Wrap="False" CssClass="text-center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center no-sort hide" AccessibleHeaderText="Edit">
                                                        <HeaderStyle CssClass="hide" Wrap="False" />
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
                                                                      <asp:LinkButton style="color: #212529;" ID="btnEdit" CssClass="dropdown-item" runat="server" CommandName="Edit" AlternateText="Edit"
                                                                                      ToolTip="Click to Edit"><i class="fa fa-pencil-square-o" aria-hidden="true"></i> Edit</asp:LinkButton>
                                                                  </li>
                                                                      
                                                                  <li>
                                                                      <button style="color: #dc3545;" type="button" class="dropdown-item" onclick='customDeleteDialouge("<%# Eval("User_ID") %>", "<%# HttpUtility.JavaScriptStringEncode(Convert.ToString(Eval("FirstName")) + " " + Convert.ToString(Eval("LastName"))) %>")'>
                                                                          <i class="fa fa-trash-o" aria-hidden="true"></i> Delete
                                                                      </button>
                                                                  </li>
                                                              </ul>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                          
                                    <asp:HiddenField ID="hdnIsEditAllowed" runat="server" Visible="false" />
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
            var lbl_serch = $("<label />").text('Search by :').attr('for', 'ddlSearch');
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
            $('#MainContent_gvUserSummary> thead > tr:eq(0)').find('.no-sort').attr('data-orderable', 'false');

            var table = $("#MainContent_gvUserSummary").DataTable({
                pageLength: 100,
                "order": [[0, "desc"]],
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
            var text = "Are you sure you want to delete '" + message + "' user?";
            var title = "Delete '" + message + "' user";
            var jc = $.confirm({
                title: title,
                conten: text,
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
                                url: "ListOfUser.aspx/DeleteUser",
                                contentType: 'application/json; charset=utf-8',
                                data: '{"id":"' + id + '"}',
                                dataType: 'json',
                                success: function (data) {
                                    console.log(data.d);
                                    if (data.d) {
                                        RefreshWindow();
                                    }
                                    else {

                                        $.alert({
                                            title: 'Alert!',
                                            content: 'This user cannot be deleted because of a reference with audit data.',
                                        });

                                    }
                                },
                                error: function (data) {
                                    console.log('Error = ' + JSON.stringify(data));
                                }
                            })
                        }
                    }
                }
            })
        }

        function RefreshWindow() {
            __doPostBack('ctl00$MainContent$btnRefresh', '');
        }
    </script>
</asp:Content>