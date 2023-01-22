<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="hld_reprocessing_log.aspx.cs" Inherits="WebPortal.hld_reprocessing_log" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageStyleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pagetitle">
        <h1>Transvaginal Transducer HLD Reprocessing log</h1>
        <nav>
            <ol class="breadcrumb">
                <li class="breadcrumb-item">Home</li>
                <li class="breadcrumb-item active">Reprocessing Log</li>
            </ol>
        </nav>
    </div>
    <!-- End Page Title -->
    <section class="section">
        <div class="row">
            <div class="col-lg-12">
                <div class="card hdl-card">
                    <div class="card-header">
                        <h5 class="card-title">Transvaginal Transducer HLD Reprocessing log</h5>
                        <%--<button type="button"  ID="btnAddNew" class="btn btn-primary btn-sm" data-bs-toggle="modal" data-bs-target="#largeModal">--%>
                                                                                                                                                                             
                            <button type="button" runat="server" ID="btnAddNew" data-bs-toggle="modal" data-bs-target="#largeModal" class="btn btn-primary btn-sm" onclick="ShowPanel('create')">   
                           <i class="bi bi-plus"></i> Add New
                       </button>
                    </div>
                    <div class="card-body">


                        <div class="table-responsive">
                            <table id="table2" class="table table-bordered table-striped hld-table" width="99.9%">
                                <thead>
                                    <tr>
                                        <th class="hide">ID</th>
                                        <th>Date</th>
                                        <th>Clinic</th>
                                        <th>Tech</th>
                                        <th>Transducer (Serial Number)</th>
                                        <th>Visit Number</th>
                                        <th>Time HLD Initiated</th>
                                        <th>Time HLD Completed</th>
                                        <th style="width: 60px;">Settings</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                        <!-- End Default Table Example -->

                    </div>
                </div>



                <div class="modal fade" id="largeModal" tabindex="-1">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-header">
                     <h5 id="popup_header">Add New Reprocessing Log</h5>
                      <button type="button"  class="close" data-bs-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                                </button>


                            </div>
                            <div class="modal-body">
                                    <form class="row" autocomplete="off">
                                    <div class="row mb-3">
                                        <label class="col-sm-5 col-form-label">Clinic <span class="red-star">*</span> :</label>
                                        <div class="col-sm-7">
                                            <select class="form-control form-control-sm requied-field" id="Lab" aria-label="Lab">
                                        
                                            </select>
                                            <div class="invalid-feedback">Please select a valid Clinic.</div>
                                        </div>
                                    </div>
                                       <div class="row mb-3">
                                        <label class="col-sm-5 col-form-label">Tech <span class="red-star">*</span> :</label>
                                        <div class="col-sm-7">
                                            <select class="form-control form-control-sm requied-field" id="Tec" aria-label="Tech">
                                        
                                            </select>
                                            <div class="invalid-feedback">Please select a valid Tech.</div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label for="inputDate" class="col-sm-5 col-form-label">Date <span class="red-star">*</span> :</label>
                                        <div class="col-sm-7">
                                            <input type="date" id="inputDate" class="form-control form-control-sm requied-field">
                                            <div class="invalid-feedback">Please enter a Date.</div>
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <label class="col-sm-5 col-form-label">Transducer (SN) <span class="red-star">*</span> :</label>
                                        <div class="col-sm-7">
                                            <select class="form-control form-control-sm requied-field" id="Transducer" aria-label="Transducer (SN)">
                                        
                                            </select>
                                            <div class="invalid-feedback">Please select a valid Transducer.</div>
                                        </div>
                                    </div>


                                    <div class="row mb-3">
                                        <label for="inputNumber" class="col-sm-5 col-form-label">Visit Number <span class="red-star">*</span> :</label>
                                        <div class="col-sm-7">
                                            <input id="visitNumber" placeholder="Please Enter Visit Number" type="text" maxlength="20" class="form-control form-control-sm requied-field">
                                            <div class="invalid-feedback">Please enter a valid Visit Number.</div>
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <label for="inputTime" class="col-sm-5 col-form-label">Time Initiated <span class="red-star">*</span> :</label>
                                        <div class="col-sm-7">
                                            <input value="now" type="time" id="TimeHLDInitiated" class="form-control form-control-sm requied-field">
                                            <div class="invalid-feedback">Please enter a valid Time Initiated.</div>
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <label for="inputTime" class="col-sm-5 col-form-label">Time Completed <span class="red-star">*</span> :</label>
                                        
                                         <div class="col-sm-7">
                                            <input value="now" type="time" id="TimeHLDCompleted" class="form-control form-control-sm requied-field">
                                             <div class="invalid-feedback">Please enter a valid Time Completed.</div>
                                        </div>
                                    </div>
                                </form>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal">Close</button>
                                <button type="button" id ="btnSave" onclick="addNewRerocessingLog(event)" class="btn btn-primary btn-sm">Submit</button>
                                <button type="button" id ="btnEdit" class="btn btn-primary btn-sm" onclick="EditSaveData()">Submit</button>
                            </div>
                        </div>
                    </div>
                </div>



                <!-- End Large Modal-->
              <asp:HiddenField  ID="hdnIsEditAllowed" runat="server"/>
                <asp:HiddenField ID="hdnIsDeleteAllowed" runat="server"/>  
               

            </div>
        </div>

      


    </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageScriptContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            LoadTable();
            document.getElementById("inputDate").valueAsDate = new Date();

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

        var table;
        function GenerateTableSerch(select) {
            var div_md_6 = $("<div />", { class: "col-md-9 data-ser" });
            var form_inline = $("<div />", { class: "form-inline search-pan pull-right" });
            var search_box = $("<div />", { class: "search-box form-row" });
            var form_group_1 = $("<div />", { class: "form-group", style: "padding-right: 15px;" });
            var lbl_serch = $("<label />").text('Search by :').attr('for', 'ddlSearch');
            var form_group = $("<div />", { class: "form-group", style: "display: inline-flex;" });
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
        function addNewRerocessingLog(event) {

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


            var Date = $('#inputDate').val();
            if (!Date) {
                $.alert('Please provide a valid date');
                return false;
            }
            var Transducer = $('#Transducer').val();
            if (!Transducer) {
                $.alert('Please provide a valid Transducer');
                return false;
            }
            var Lab = $('#Lab').val();
            if (!Lab) {
                $.alert('Please provide a valid Lab');
                return false;
            }
            var Tec = $('#Tec').val();
            if (!Tec) {
                $.alert('Please provide a valid Tech');
                return false;
            }
            var VisitNumber = $('#visitNumber').val();
            if (!visitNumber) {
                $.alert('Please provide a valid visit number');
                return false;
            }
            var TimeHLDInitiated = $('#TimeHLDInitiated').val();
            if (!TimeHLDInitiated) {
                $.alert('Please select Time Initiated');
                return false;
            }
            var TimeHLDCompleted = $('#TimeHLDCompleted').val();
            if (!TimeHLDCompleted) {
                $.alert('The time completed must be later than the "Time Initiated".');
                return false;
            }

            if (TimeHLDInitiated > TimeHLDCompleted) {

                $.alert('The time completed must be later than the "Time Initiated".');
                return false;
            }
            var data = {
                "Date": Date,
                "Transducer": Transducer,
                "Lab": Lab,
                "Tec": Tec,
                "VisitNumber": VisitNumber,
                "TimeHLDInitiated": TimeHLDInitiated,
                "TimeHLDCompleted": TimeHLDCompleted
            };

            $.ajax({
                url: "hld_reprocessing_log.aspx/CreateReprocessingLog",
                type: "POST",
                contentType: 'application/json',
                data: JSON.stringify(data),
                beforeSend: function () {
                    $('#largeModal').modal('hide');

                },
                success: function (result) {
                    console.log(result);
                    table.ajax.reload(function () {
                        $("#loader").hide();
                    });
                },
                error: function (err) {

                    console.log("Failed , " + err);
                }
            });
        }
        function LoadTable() {
            var IsEditAllowed = "true";
            var IsDeleteAllowed = "true";

            //$.fn.dataTable.moment('MMM D, YYYY');
            //debugger;
            table = $('#table2').DataTable({
                "ajax": {
                    "type": "GET",
                    "url": "hld_reprocessing_log.aspx/LoadTable",
                    "contentType": "application/json; charset=utf-8",
                    "dataType": "json",
                    "beforeSend": function (request) {
                        $("#loader").show();
                    },
                    "dataSrc": function (json) {
                        return JSON.parse(json.d);
                    },
                    "fnDrawCallback": function (oSettings) {
                        $("#loader").hide();
                    }
                },
                "deferRender": true,
                pageLength: 100,
                dom: "lrt<'myinfo'i>p",
                initComplete: function () {
                    $("#loader").hide();
                    var select = $('<select class="form-control form-control-sm search-ddl" id="ddlSearch"/>');
                    $('<option />', { value: '', text: 'Any' }).appendTo(select);
                    $(this).find('thead > tr:eq(0) > th').not('.no-sort,.hide,.hide1').each(function () {
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

                    SetPopocer();
                },
                "order": [[0, "desc"]],
                columns: [
                    { 'data': 'ReprocessingLogID', className: 'hide hide1' },
                    { 'data': 'Date' },
                    { 'data': 'LabName' },
                    { 'data': 'TecName' },
                    {
                        'data': null, 'render': function (data, type, row) {
                            return '<span class="badge badge-primary"> ' + data.DeviceNo + ' </span> Sn: ' + data.Transducer_serial_no;
                        }
                    },
                    { 'data': 'VisitNumber' },
                    { 'data': 'TimeHLDInitiated' },
                    { 'data': 'TimeHLDCompleted' },
                    {
                        'data': null, className: 'text-center no-sort', 'render': function (data, type, row) {
                            // debugger;
                            return '<div class="btn-group"><button class="btn btn-outline-primary btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false"><i class="bi bi-gear"></i></button><ul class="dropdown-menu"><li><a style="color: #212529;" class="dropdown-item" onclick="return EditData(event,\'' + data.ReprocessingLogID + '\');"><i class="bi bi-pencil"></i> Edit</a></li><li><a style="color: #dc3545;" class="dropdown-item" onclick="DeleteData(event,\'' + data.ReprocessingLogID + '\');"><i class="bi bi-trash"></i> Delete</a></li></ul></div>'


                        }
                    }

                ]
            });
        }
        function SetPopocer() {
            $('.dataTable > tbody > tr').find('td.data-ellipsis').each(function () {
                $(this).popover({
                    container: 'body',
                    trigger: 'hover',
                    placement: 'left',
                    html: true,
                    content: $(this).text()
                });
            });
        }
        function getBool(val) {
            if (val == null || val == "" || typeof val === "undefined" || val.toString() == "undefined") {
                return false;
            }
            else {
                return !!JSON.parse(String(val).toLowerCase());
            }
        }
        function GetSerialNumber(event) {
            event.preventDefault();
            $.ajax({
                type: "GET",
                url: "hld_reprocessing_log.aspx/GetFormData",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                beforeSend: function () {
                    $("#loader").show();
                },
                success: function (data) {
                    debugger;
                    var defaultDeviceIdResult = data.d[1];
                    var defaultDeviceId = JSON.parse(defaultDeviceIdResult)[0];
                    var deviceId;
                    if (defaultDeviceId != undefined) {
                        deviceId = defaultDeviceId.DeviceId;
                    } else {
                        deviceId = '';
                    }

                    var defaultLabIdResult = data.d[3];
                    var defaultLabId = JSON.parse(defaultLabIdResult);
                    if (defaultLabId.length == 1) {
                        defaultLabId = JSON.parse(defaultLabIdResult)[0];
                    } else {

                        defaultLabId = '';
                    }
                    var LabId;
                    if (defaultLabId != undefined) {
                        LabId = defaultLabId.Facility_Code;
                    } else {
                        LabId = '';
                    }

                    var userID = JSON.parse(data.d[5]);
                    var TecId;
                    if (userID != undefined) {
                        TecId = userID;
                    } else {
                        TecId = '';
                    }

                    //Lab
                    $("#Lab").empty();
                    var txt = '<option value="" selected>--Select Clinic--</option>';
                    $.each(JSON.parse(data.d[2]), function () {

                        if (LabId == this.FacilityCode) {
                            txt += '<option value="' + this.FacilityCode + '"  selected="selected">' + this.Description + '</option>';
                        } else {
                            txt += '<option value="' + this.FacilityCode + '">' + this.Description + '</option>';
                        }

                    });
                    $('#Lab').append($(txt));

                    //Tec
                    $("#Tec").empty();
                    var txt = '<option value="" selected>--Select Tech--</option>';
                    $.each(JSON.parse(data.d[4]), function () {

                        if (TecId == this.UserID) {
                            txt += '<option value="' + this.UserID + '"  selected="selected">' + this.UserName + '</option>';
                        } else {
                            txt += '<option value="' + this.UserID + '">' + this.UserName + '</option>';
                        }

                    });
                    $('#Tec').append($(txt));

                    //Transducer
                    $("#Transducer").empty();
                    var txt = '<option value="" selected>--Select serial number--</option>';
                    $.each(JSON.parse(data.d[0]), function () {

                        if (deviceId == this.ID) {
                            txt += '<option value="' + this.ID + '"  selected="selected">' + this.Transduser + '</option>';
                        } else {
                            txt += '<option value="' + this.ID + '">' + this.Transduser + '</option>';
                        }

                    });
                    $('#Transducer').append($(txt));

                    $("#loader").hide();
                },
                error: function (data) {
                    $("#loader").hide();
                    console.log('Error = ' + JSON.stringify(data));
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

                            debugger;

                            var data = {
                                "Id": Number(Id)
                            }
                            $.ajax({
                                type: "POST",
                                url: "hld_reprocessing_log.aspx/DeleteData",
                                contentType: 'application/json',
                                data: JSON.stringify(data),
                                dataType: 'json',
                                beforeSend: function () {
                                    $("#loader").show();
                                },
                                success: function (data) {
                                    if (data.d) {
                                        jc.close();
                                        table.ajax.reload(function () {
                                            $("#loader").hide();

                                        });
                                    }
                                    else {
                                        $("#loader").hide();
                                        $.alert({
                                            title: 'Warning',
                                            content: 'This Solution Testing Log cannot be deleted. It is referenced',
                                        });
                                    }
                                },
                                error: function (data) {
                                    $("#loader").hide();
                                    console.log('Error = ' + JSON.stringify(data));
                                }
                            });
                            return false;
                        }
                    }
                }
            });




        }
        function clearcomponent() {

            $('#Date').val("");
            $('#Transducer').val("");
            $('#Lab').val("");
            $('#Tec').val("");
            $('#visitNumber').val("");
            $('#TimeHLDInitiated').val("");
            $("#TimeHLDCompleted").val("");
            //GetSerialNumber();

        }
        function EditData(event, Id) {


            // GetSerialNumber(event);

            event.preventDefault();
            var Qdata = {
                "Id": Number(Id)
            }

            $.ajax({
                type: "POST",
                url: "hld_reprocessing_log.aspx/GetReprocessingLogDetails",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(Qdata),
                dataType: 'json',
                beforeSend: function () {
                    // $("#loader").show();
                },
                success: function (data) {

                    clearcomponent();

                    debugger;
                    var defaultDeviceIdResult = data.d[1];
                    var defaultDeviceId = JSON.parse(defaultDeviceIdResult)[0];
                    var deviceId;
                    if (defaultDeviceId != undefined) {
                        deviceId = defaultDeviceId.DeviceId;
                    } else {
                        deviceId = '';
                    }

                    var defaultLabIdResult = data.d[3];
                    var defaultLabId = JSON.parse(defaultLabIdResult);
                    if (defaultLabId.length == 1) {
                        defaultLabId = JSON.parse(defaultLabIdResult)[0];
                    } else {

                        defaultLabId = '';
                    }
                    var LabId;
                    if (defaultLabId != undefined) {
                        LabId = defaultLabId.Facility_Code;
                    } else {
                        LabId = '';
                    }

                    var userID = JSON.parse(data.d[5]);
                    var TecId;
                    if (userID != undefined) {
                        TecId = userID;
                    } else {
                        TecId = '';
                    }

                    //Lab
                    $("#Lab").empty();
                    var txt = '<option value="" selected>--Select Clinic--</option>';
                    $.each(JSON.parse(data.d[2]), function () {

                        if (LabId == this.FacilityCode) {
                            txt += '<option value="' + this.FacilityCode + '"  selected="selected">' + this.Description + '</option>';
                        } else {
                            txt += '<option value="' + this.FacilityCode + '">' + this.Description + '</option>';
                        }

                    });
                    $('#Lab').append($(txt));

                    //Tec
                    $("#Tec").empty();
                    var txt = '<option value="" selected>--Select Tech--</option>';
                    $.each(JSON.parse(data.d[4]), function () {

                        if (TecId == this.UserID) {
                            txt += '<option value="' + this.UserID + '"  selected="selected">' + this.UserName + '</option>';
                        } else {
                            txt += '<option value="' + this.UserID + '">' + this.UserName + '</option>';
                        }

                    });
                    $('#Tec').append($(txt));

                    //Transducer
                    $("#Transducer").empty();
                    var txt = '<option value="" selected>--Select serial number--</option>';
                    $.each(JSON.parse(data.d[0]), function () {

                        if (deviceId == this.ID) {
                            txt += '<option value="' + this.ID + '"  selected="selected">' + this.Transduser + '</option>';
                        } else {
                            txt += '<option value="' + this.ID + '">' + this.Transduser + '</option>';
                        }

                    });
                    $('#Transducer').append($(txt));

                    $("#loader").hide();


                    EditId = Id;
                    var result = data.d[6];
                    var Editdetails = JSON.parse(result)[0];


                    var Dates = new Date(Editdetails.Date);
                    var day = ("0" + Dates.getDate()).slice(-2);
                    var month = ("0" + (Dates.getMonth() + 1)).slice(-2);
                    Dates = Dates.getFullYear() + "-" + (month) + "-" + (day);


                    $("#inputDate").val(Dates);
                    $("#Transducer").val(Editdetails.Transducer);
                    $("#Lab").val(Editdetails.Lab);
                    $("#Tec").val(Editdetails.Tec);
                    $("#visitNumber").val(Editdetails.VisitNumber);
                    $("#TimeHLDInitiated").val(Editdetails.TimeHLDInitiated);
                    $("#TimeHLDCompleted").val(Editdetails.TimeHLDCompleted);


                    ShowPanel('Edit');


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
                GetSerialNumber(event);
                $('#btnSave').show();
                $('#btnEdit').hide();
                $('#popup_header').text('Add Reprocessing Log');
                document.getElementById("TimeHLDCompleted").value = moment().format('kk:mm');
                document.getElementById("TimeHLDInitiated").value = moment().format('kk:mm');
            } else {

                $('#btnSave').hide();
                $('#btnEdit').show();

                $('#popup_header').text('Edit Reprocessing Log Details');
            }

            $('#largeModal').modal('show');
        }
        function EditSaveData() {

            debugger;

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


            var Date = $('#inputDate').val();
            if (!Date) {
                $.alert('Please provide a valid date');
                return false;
            }
            var Transducer = $('#Transducer').val();
            if (!Transducer) {
                $.alert('Please provide a valid Transducer');
                return false;
            }
            var Lab = $('#Lab').val();
            if (!Lab) {
                $.alert('Please provide a valid Lab');
                return false;
            }
            var Tec = $('#Tec').val();
            if (!Tec) {
                $.alert('Please provide a valid Tech');
                return false;
            }

            var VisitNumber = $('#visitNumber').val();
            if (!visitNumber) {
                $.alert('Please provide a valid visit number');
                return false;
            }
            var TimeHLDInitiated = $('#TimeHLDInitiated').val();
            if (!TimeHLDInitiated) {
                $.alert('Please select TimeHLDInitiated');
                return false;
            }
            var TimeHLDCompleted = $('#TimeHLDCompleted').val();
            if (!TimeHLDCompleted) {
                $.alert('Please provide TimeHLDCompleted');
                return false;
            }
            var data = {
                "inputDate": Date,
                "Transducer": Transducer,
                "Lab": Lab,
                "Tec": Tec,
                "visitNumber": VisitNumber,
                "TimeHLDInitiated": TimeHLDInitiated,
                "TimeHLDCompleted": TimeHLDCompleted,
                "EditId": EditId
            };

            $.ajax({
                url: "hld_reprocessing_log.aspx/EditReprocessingLog",
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
    </script>
</asp:Content>
