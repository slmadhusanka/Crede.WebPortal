<%@ Page Title="List Of Transducer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListOfTransduser.aspx.cs" Inherits="WebPortal.SystemPages.ListOfTransduser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pagetitle">
        <h1>Transducer (Serial Number)</h1>
        <nav>
        <ol class="breadcrumb">
            <li class="breadcrumb-item">Home</li>
            <li class="breadcrumb-item">Table Maintenance</li>
            <li class="breadcrumb-item active">Transducers</li>
        </ol>
        </nav>
    </div><!-- End Page Title -->
    <section class="section">
        <div class="row">
            <div class="col-lg-12">
                <div class="card hdl-card">
                <div class="card-header">
                    <h5 class="card-title">Transducers (Serial Number) </h5>
                    <button type="button" class="btn btn-primary btn-sm" data-bs-toggle="modal" data-bs-target="#largeModal" onclick="ShowPanel('create')">
                    <i class="bi bi-plus"></i> Add New
                  </button>
                </div>
                <div class="card-body">
        <!-- Start Default Table -->
                    <div class="table-responsive">
                            <table id="table2" class="table table-bordered table-striped hld-table" width="99.9%">
                                <thead>
                                    <tr>
                                        <th class="hide">ID</th>
                                        <th>Device No</th>
                                        <th>Transducer (Serial Number)</th>
                                        <th>Equipment</th>
                                        <th>Description</th>
                                        <th class="no-sort" style="width: 60px;">Settings</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
        <!-- End Default Table -->
    
                </div>
                </div>
                <!-- Start Large Modal--><!-- Start Large Modal-->
                <div class="modal fade" id="largeModal" tabindex="-1">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
        
                            <div class="modal-header">
                     <h5 id="popup_header">Add New Transducer (Serial Number)</h5>
                      <button type="button"  class="close" data-bs-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                                </button>


                            </div>
                            <div class="modal-body">
                                <form class="row">
                                    <%--<div class="row mb-3">
                                        <label class="col-sm-5 col-form-label">Transducer (Serial Number)</label>
                                        <div class="col-sm-7">
                                            <select class="form-select" id="Transducer" aria-label="Default select example">
                                            </select>
                                        </div>
                                    </div>--%>
                                    <div class="row mb-3">
                                        <label for="inputNumber" class="col-sm-5 col-form-label">Device No# <span class="red-star">*</span> :</label>
                                        <div class="col-sm-7">
                                            <input placeholder="--Device No Here--" id="DeviceNo" type="text" maxlength="400" class="form-control requied-field">
                                            <div class="invalid-feedback">Please enter a valid Device Number.</div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label for="inputNumber" class="col-sm-5 col-form-label">Transducer (SN) <span class="red-star">*</span> :</label>
                                        <div class="col-sm-7">
                                            <input placeholder="--Transducer (SN) Enter Here--" id="Transducer" type="text" maxlength="400" class="form-control requied-field">
                                            <div class="invalid-feedback">Please enter a valid Transducer.</div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label class="col-sm-5 col-form-label">Equipment <span class="red-star">*</span> :</label>
                                        <div class="col-sm-7">
                                            <select class="form-control requied-field" id="Unit" aria-label="Default select example">
                                            </select>
                                            <div class="invalid-feedback">Please select a valid Equipment.</div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label for="inputNumber" class="col-sm-5 col-form-label">Description :</label>
                                        <div class="col-sm-7">
                                            <input placeholder="--Description Here--" id="Discreption" type="text" maxlength="400" class="form-control">
                                        </div>
                                    </div>
                                </form>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal">Close</button>
                                <button type="button" id ="btnSave" onclick="addNewTransduser(event)" class="btn btn-primary btn-sm">Submit</button>
                                <button type="button" id ="btnEdit" class="btn btn-primary btn-sm" onclick="EditSaveData()">Submit</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Large Modal--><!-- End Large Modal-->
                <asp:HiddenField  ID="hdnIsEditAllowed" runat="server"/>
                <asp:HiddenField ID="hdnIsDeleteAllowed" runat="server"/> 
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#example2').DataTable();
            GetUnit();
            LoadTable();
            
             // detect all the required select field data changes
                  $(".requied-field").on('change', function () {    // 2nd (A)
                      errorCount = 0;
      
                      if ($(this).val().length != 0) {
      
                          $(this).siblings('.invalid-feedback').hide();
      
                          $('.requied-field').each(function () {
                              if ($(this).val().length == 0) {
                                  errorCount++;
                              }
                          });
      
                          if (errorCount == 0) {
                              $('.alert-data-validation').hide();
                          }
                      }
                      else {
                          $(this).siblings('.invalid-feedback').show();
                      }
                  });
      
                  // detect all the required text field data changes
                  $('.requied-field').on('input', function (e) {
                      errorCount = 0;
      
                      if ($(this).val().length != 0) {
      
                          $(this).siblings('.invalid-feedback').hide();
      
                          $('.requied-field').each(function () {
                              if ($(this).val().length == 0) {
                                  errorCount++;
                              }
                          });
      
                          if (errorCount == 0) {
                              $('.alert-data-validation').hide();
                          }
                      }
                      else {
                          $(this).siblings('.invalid-feedback').show();
                      }
                  });
            
        });

        function addNewTransduser(event) {
            
            // check validation
              errorCount = 0;
              $('.invalid-feedback').hide();
              $('.alert-data-validation').hide();
              $('.alert-error-message').hide();
  
              $('.requied-field').each(function () {
                  if ($(this).val().length == 0) {
                      errorCount++;
                      $(this).siblings('.invalid-feedback').show();
                      $('.alert-data-validation').show();
                  }
              });
  
              // if any validation error occurs we dismiss the operation
              if (errorCount > 0)
                  return;

            var Transducer = $('#Transducer').val();
            if (!Transducer) {
                $.alert('Please provide a valid Transducer');
                return false;
            }
            var DeviceNo = $('#DeviceNo').val();
            if (!DeviceNo) {
                $.alert('Please provide a valid device number');
                return false;
            }
            var Unit = $('#Unit').val();
            if (!Unit) {
                $.alert('Please provide Equipment');
                return false;
            }
            var Discreption = $('#Discreption').val();
            if (!Discreption) {
                //$.alert('Please provide a valid Discreption');
                //return false;
            }

            var data = {
                "Transducer": Transducer,
                "DeviceNo": DeviceNo,
                "Unit": Unit,
                "Discreption": Discreption
            };

            $.ajax({
                url: "ListOfTransduser.aspx/CreateTransduser",
                type: "POST",
                contentType: 'application/json',
                data: JSON.stringify(data),
                beforeSend: function () {
                    $('#largeModal').modal('hide');

                },
                success: function (result) {
                    table.ajax.reload(function () {
                        $("#loader").hide();
                    });
                },
                error: function (err) {

                    console.log("Failed , " + err);
                }
            });
        }

        function DeleteData(event, Id) {

            event.preventDefault();
            var jc = $.confirm({
                title: 'Confirm to Delete',
                content: 'Are you sure you want to delete?',
                buttons: {
                    No: {
                        text: '<i class="fa fa-times" aria-hidden="true"></i> Cancel',
                        btnClass: 'btn btn-warning',
                        action: function () {

                        }
                    },
                    Yes: {
                        text: '<i class="fa fa-check" aria-hidden="true"></i> Delete',
                        btnClass: 'btn btn-danger',
                        action: function () {


                            var data = {
                                "Id": Number(Id)
                            }
                            $.ajax({
                                type: "POST",
                                url: "ListOfTransduser.aspx/DeleteData",
                                contentType: 'application/json',
                                data: JSON.stringify(data),
                                dataType: 'json',
                                beforeSend: function () {
                                    $("#loader").show();
                                },
                                success: function (data) {

                                    switch (data.d) {
                                        case 0:
                                            $.alert({
                                                title: 'Warning ',
                                                content: 'Unable to delete.',

                                            });
                                            break;
                                        case 1:
                                            
                                            break;
                                        case 2:
                                            $.alert({
                                                title: 'Warning ',
                                                content: 'Unable to delete the selected data as it is referencing reprocessing log.',

                                            });
                                            break;
                                        case 3:
                                            Console.WriteLine("Thursday");
                                            break;
                                        case 5:
                                            Console.WriteLine("Friday");
                                            break;
                                        case 6:
                                            Console.WriteLine("Saturday");
                                            break;
                                        case 7:
                                            Console.WriteLine("Sunday");
                                            break;
                                    }
                                    jc.close();
                                    table.ajax.reload(function () {
                                        $("#loader").hide();

                                    });
                                   
                                      
                                },
                                error: function (data) {
                                    $("#loader").hide();
                                    console.log('Error = ' + JSON.stringify(data));
                                   //RefreshWindow();
                                }
                            });
                            return false;
                        }
                    }
                }
            });




        }

        function GetUnit() {
            $.ajax({
                
                type: "GET",
                url: "ListOfTransduser.aspx/GetFormDataUnit",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                beforeSend: function () {
                    $("#loader").show();
                },
                success: function (data) {
                    $("#Unit").empty();
                    var txt = '<option value="" selected>--Select Equipment--</option>';
                    $.each(JSON.parse(data.d), function () {
                        txt += '<option value="' + this.UnitCode + '">' + this.Unit + '</option>';
                    });
                    //$('#Transducer').empty();
                    $('#Unit').append($(txt));

                    $("#loader").hide();
                },
                error: function (data) {
                    $("#loader").hide();
                    console.log('Error = ' + JSON.stringify(data));
                }
            });
        }

        function ShowPanel(action) {
            $('#popup_header').text('');
            $('.invalid-feedback').hide();
            $('.alert-data-validation').hide();
            $('.alert-error-message').hide();
            $('.alert-error-message-2').hide();
            $('.alert-danger-status').hide();
            
            if (action == 'create') {
                clearcomponent();
                $('#btnSave').show();
                $('#btnEdit').hide();
                $('#popup_header').text('Add Transducers (Serial Number)');
            } else {

                $('#btnSave').hide();
                $('#btnEdit').show();

                $('#popup_header').text('Edit Transducers (Serial Number)');
            }

            $('#largeModal').modal('show');
        }

        function EditData(event, Id) {
            event.preventDefault();
            var Qdata = {
                "Id": Number(Id)
            }
            $.ajax({
                type: "POST",
                url: "ListOfTransduser.aspx/GetDetails",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(Qdata),
                dataType: 'json',
                beforeSend: function () {
                    // $("#loader").show();
                },
                success: function (data) {

                    clearcomponent();
                    EditId = Id;
                    var result = data.d;
                    var Editdetails = JSON.parse(result)[0];
                    
                    $("#Transducer").val(Editdetails.Transducer_serial_no);
                    $("#Unit").val(Editdetails.UnitCode);
                    $("#Discreption").val(Editdetails.Description);

                    $("#DeviceNo").val(Editdetails.DeviceNo);
                    
                    ShowPanel('Edit');


                },
                error: function (data) {
                    $("#loader").hide();
                    console.log('Error = ' + JSON.stringify(data));
                }
            });
        }
       
        function EditSaveData() {

            var Transducer = $('#Transducer').val();
            if (!Transducer) {
                $.alert('Please provide a valid Transducer');
                return false;
            }
            var Unit = $('#Unit').val();
            if (!Unit) {
                $.alert('Please provide Equipment');
                return false;
            }
            var DeviceNo = $('#DeviceNo').val();
            if (!DeviceNo) {
                $.alert('Please provide a valid Device No');
                return false;
            }
            var Discreption = $('#Discreption').val();
            if (!Discreption) {
                //$.alert('Please provide a valid Discreption');
                //return false;
            }
            //save action

            var data = {
                "Transducer": Transducer,
                "Unit": Unit,
                "Discreption": Discreption,
                "DeviceNo": DeviceNo,
                "EditId": EditId
            };
            
            $.ajax({
                url: "ListOfTransduser.aspx/EditData",
                type: "POST",
                contentType: 'application/json',
                data: JSON.stringify(data),
                beforeSend: function () {
                    $('#largeModal').modal('hide');
                    //$("#loader").show();
                },
                success: function (result) {
                    //reload table
                    table.ajax.reload(function () {
                        $("#loader").hide();
                    });

                },
                error: function (err) {
                    $('#loader').hide();
                    console.log("Failed , " + err);
                }
            });



        }
        function LoadTable() {

            //var IsEditAllowed = $('#hdnIsEditAllowed').val();
            //var IsDeleteAllowed = $('#hdnIsDeleteAllowed').val();

            $.fn.dataTable.moment('MMM D, YYYY');

            table = $('#table2').DataTable({
                "ajax": {
                    "type": "GET",
                    "url": "ListOfTransduser.aspx/LoadTable",
                    "contentType": "application/json; charset=utf-8",
                    "dataType": "json",
                    "beforeSend": function (request) {
                        //$("#MainContent_UpdateProg1").show();
                    },
                    "dataSrc": function (json) {
                        return JSON.parse(json.d);
                    },
                    "fnDrawCallback": function (oSettings) {
                        $("#MainContent_UpdateProg1").hide();
                    }
                },
                "deferRender": true,
                pageLength: 100,
                dom: "lrt<'myinfo'i>p",
                initComplete: function () {
                    //$("#MainContent_UpdateProg1").hide();
                    var select = $('<select class="form-control form-control-sm search-ddl" id="ddlSearch"/>');
                    $('<option />', { value: '', text: 'Any' }).appendTo(select);
                    $(this).find('thead > tr:eq(0) > th:visible').not('.no-sort').each(function () {
                        $('<option />', { value: $(this).index(), text: $(this).text() }).appendTo(select);
                    });
                    var div_md_6 = GenerateTableSerch(select);
                    $(".dataTables_length > label").wrap("<div class='col-md-3 data-sot'></div>");
                    $(".dataTables_length").append(div_md_6);

                    var column_no = 0;
                    $('#ddlSearch').on('change', function () {
                        column_no = Number($(this).val());
                        table.search('').columns().search('').draw();
                        $('#txtSearch').val("");
                    });

                    $('#txtSearch').on('input', function () {
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
                },
                "order": [[0, "Id"]],
                columns: [
                    { 'data': 'ID', className: 'hide hide1' },
                    { 'data': 'DeviceNo' },
                    { 'data': 'Transducer_serial_no' },
                    { 'data': 'Unit' },
                    { 'data': 'Description' },
                    {
                        'data': null, className: 'text-center no-sort', 'render': function (data, type, row) {

                            return '<div class="btn-group"><button class="btn btn-outline-primary btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false"><i class="bi bi-gear"></i></button><ul class="dropdown-menu"><li><a style="color: #212529;" class="dropdown-item" onclick="return EditData(event,\'' + data.ID + '\');"><i class="bi bi-pencil"></i> Edit</a></li><li><a style="color: #dc3545;" class="dropdown-item" onclick="DeleteData(event,\'' + data.ID + '\');"><i class="bi bi-trash"></i> Delete</a></li></ul></div>'

                        }
                    }
                ]
            });
        }

        function GenerateTableSerch(select) {
            var div_md_6 = $("<div />", { class: "col-md-9 data-ser" });
            var form_inline = $("<div />", { class: "form-inline search-pan pull-right" });
            var search_box = $("<div />", { class: "search-box form-row" });
            var form_group_1 = $("<div />", { class: "form-group", style: "padding-right: 15px;" });
            var lbl_serch = $("<label />").text('Search by :').attr('for', 'ddlSearch');
            var form_group = $("<div />", { class: "form-group", style: "display: inline-flex;" });
            var search_input = $('<input type="text" class="form-control form-control-sm search-ddl txtSearch data-search-text" id="txtSearch" placeholder="Search"/>');
            var search_button = $('<button type="button" class="btn btn-outline-secondary btn-sm data-search-btn">').append($("<i class='fa fa-times' aria-hidden='true'></i>"));

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

        function getBool(val) {
            return !!JSON.parse(String(val).toLowerCase());
        }
        function RefreshWindow() {
            __doPostBack('ctl00$MainContent$btnRefresh', '');
        }
        function clearcomponent() {
            $('#DeviceNo').val("");
            $('#Transducer').val("");
            $('#Discreption').val("");
            $('#Unit').val("");
            
           
        }
    </script>
</asp:Content>

