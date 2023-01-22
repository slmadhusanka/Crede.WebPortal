<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="hld_solution_testing_log.aspx.cs" Inherits="WebPortal.hld_solution_testing_log" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pagetitle">
    <h1>Revital-Ox Resert HLD Solution Testing Log</h1>
    <nav>
    <ol class="breadcrumb">
        <li class="breadcrumb-item">Home</li>
        <li class="breadcrumb-item active">Solution Testing Log</li>
    </ol>
    </nav>
</div><!-- End Page Title -->
<section class="section">
      <div class="row">
        <div class="col-lg-12">
            <div class="card hdl-card">
            <div class="card-header">
                <h5 class="card-title">Revital-Ox Resert HLD Solution Testing Log</h5>
                <button type="button" runat="server" ID="btnAddNew" class="btn btn-primary btn-sm" onclick="ShowPanel('create')">
                <i class="bi bi-plus"></i> Add New
              </button>
            </div>
            <div class="card-body">

            <div class="table-responsive">
               <!-- Default Table -->
              <table id="myTable" class="table table-bordered table-striped hld-table" width="99.9%">
                  <thead>
                   <th class ="hide hide1">Id</th>
                   <th>Date</th>
                    <th>Time</th>
                    <th>Lot Number</th>
                    <th>Bottle Number</th>
                    <th>Temp. ( &gt; 20°C )</th>
                    <th>Daily (Pass/Fail)</th>
                    <th>Before Changing (Pass/Fail)</th>
                    <th>Date Changed</th>
                    <th>Next Change Date</th>
                    <th style="padding-right: 10px; width: 50px;">Settings</th>
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
                     <h5 class="modal-title" id="popup_header">Add New Solution Testing Log</h5>
                      <button type="button"  class="close" data-bs-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                      </button>
                    </div>
                    <div class="modal-body">
                        <form>
                            <div class="row mb-3">
                              <label for="inputDate" class="col-sm-5 col-form-label">Date <span class="red-star">*</span> :</label>
                              <div class="col-sm-7">  <input type="date"  Id="txtDate" class="form-control form-control-sm requied-field"> 
                                  <div class="invalid-feedback">Please enter a valid Date.</div>
                                  </div>
                            </div>

                            <div class="row mb-3">
                                  <label for="inputTime" class="col-sm-5 col-form-label">Time <span class="red-star">*</span> :</label>
                                    <div class="col-sm-7">     
                                        <input type="time"  Id="txtTime"  class="form-control form-control-sm requied-field">
                                        <div class="invalid-feedback">Please enter a valid Time.</div>
                                    </div>
                                </div>

                            <div class="row mb-3">
                              <label for="inputNumber" class="col-sm-5 col-form-label">Lot Number # <span class="red-star">*</span> :</label>
                              <div class="col-sm-7">
                                <input placeholder="Please enter Lot Number" Id="txtLotNum"  type="text" maxlength="20" class="form-control form-control-sm requied-field">
                                  <div class="invalid-feedback">Please enter a valid Lot Number.</div>
                              </div>
                            </div>
                            
                            <div class="row mb-3">
                              <label for="inputNumber" class="col-sm-5 col-form-label">Bottle Number # <span class="red-star">*</span> :</label>
                              <div class="col-sm-7">
                                <input placeholder="Please enter the Bottle Number" Id="txtBottletNum"  type="text" maxlength="20" class="form-control form-control-sm requied-field">
                              <div class="invalid-feedback">Please enter a valid Bottle Number.</div>
                                  </div>
                            </div>

                            <div class="row mb-3">
                              <label class="col-sm-5 col-form-label">Temp. (> 20°C) <span class="red-star">*</span> :</label>
                                <div class="col-sm-7">
                                <input type="number"  Id="txtTemp"  maxlength="3" class="form-control form-control-sm requied-field">
                                    <div class="invalid-feedback">Please enter a valid Temp.</div>
                                    </div>
                            </div>
                                
                            <div class="row mb-3">
                                <fieldset class="col-sm-5 col-form-label">Daily <span class="red-star">*</span> :</fieldset>
                                 <div class="col-sm-7" style="padding-top: 5px;">
                                <div class="form-check form-check-inline">
                                  <input class="form-check-input requied-field" type="radio"  name="gridRadios" id="rdoDailyPass" value="option1">
                                  <label class="form-check-label" for="rdoDailyPass">
                                    Pass
                                  </label>
                                </div>
                                <div class="form-check form-check-inline">
                                  <input class="form-check-input requied-field" type="radio" name="gridRadios" id="rdoDailyFail" value="option2">
                                  <label class="form-check-label" for="rdoDailyFail">
                                   Fail
                                  </label>
                                </div>
                                     </div>
                            </div>

                            <fieldset class="row mb-3">
                              <fieldset  class="col-sm-5 col-form-label">Before Changing <span class="red-star">*</span> :</fieldset>
                                 <div class="col-sm-7" style="padding-top: 5px;">
                                <div class="form-check form-check-inline">
                                  <input class="form-check-input requied-field" type="radio" name="gridRadios1" id="rdoBeforeChngPass" value="option1">
                                  <label class="form-check-label" for="rdoBeforeChngPass">
                                    Pass
                                  </label>
                                </div>
                                <div class="form-check form-check-inline">
                                  <input class="form-check-input requied-field" type="radio" name="gridRadios1" id="rdoBeforeChngFail" value="option2">
                                  <label class="form-check-label" for="rdoBeforeChngFail">
                                   Fail
                                  </label>
                                </div>
                                 </div>
                            </fieldset>

                            <div class="row mb-3">
                              <label for="inputDate" class="col-sm-5 col-form-label">Date Changed <span class="red-star">*</span> :</label>
                              <div class="col-sm-7">
                                <input type="date"  Id ="txtDateChange"  class="form-control form-control-sm requied-field">
                                  <div class="invalid-feedback">Please enter a valid Date Changed.</div>
                             </div>
                            </div>

                            <div class="row mb-3">
                              <label for="inputDate" class="col-sm-5 col-form-label">Next Change Date <span class="red-star">*</span> :</label>
                                <div class="col-sm-7">
                                <input type="date"  Id ="txtNextChangeDate"  class="form-control form-control-sm requied-field">
                                    <div class="invalid-feedback">Please enter a valid Next Change Date.</div>
                                </div>
                            </div>
                        </form>
                        </div>
                    <div class="modal-footer">
                      <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal">Close</button>
                      <button type="button" id ="btnSave" class="btn btn-primary btn-sm" onclick="SaveData()">Submit</button>
                      <button type="button" id ="btnEdit" class="btn btn-primary btn-sm" onclick="EditSaveData()">Submit</button>

                    </div>
                  </div>
                </div>
              </div><!-- End Large Modal-->
              <asp:HiddenField  ID="hdnIsEditAllowed" runat="server"/>
                <asp:HiddenField ID="hdnIsDeleteAllowed" runat="server"/>
            </div>
      </div>   
</section>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageScriptContent" runat="server">
            <script type="text/javascript">
                var table;
                var EditId;

                $(document).ready(function () {
                   // $('#example').DataTable();

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

                function getBool(val) {
                    return !!JSON.parse(String(val).toLowerCase());
                }

                function SaveData() {
                    
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
        
                    
                   // debugger;
                    var Date = $('#txtDate').val();
                    if (!Date) {
                        $.alert('Please provide a Date');
                        return false;
                    }

                    var Time = $('#txtTime').val();
                    if (!Time) {
                        $.alert('Please provide a Time');
                        return false;
                    }

                    var LotNumber = $('#txtLotNum').val();
                    if (!LotNumber) {
                        $.alert('Please provide Lot Number #');
                        return false;
                    }

                    var Temp = $('#txtTemp').val();
                    if (!Temp) {
                        $.alert('Please provide Temp. (> 20°C)');
                        return false;
                    }
                    if (Temp<21) {
                            $.alert({
                                title: 'Warning',
                                content: 'Temperature must be greater than 20°C.',
                            });
                       
                        return false;
                    }

                    var IsDaily = false;
                    if ($('#rdoDailyPass').is(':checked')) {

                        IsDaily = true
                    }

                    var IsBeforeChanging = false;
                    if ($('#rdoBeforeChngPass').is(':checked')) {

                        IsBeforeChanging = true
                    }

                    var DateChange = $('#txtDateChange').val();
                    if (!DateChange) {
                        $.alert('Please provide Date Changed');
                        return false;
                    }

                    if ($('#rdoDailyPass').is(':unchecked') & $('#rdoDailyFail').is(':unchecked')) {

                        $.alert('Please provide "Daily (Pass/Fail)"');
                        return false;
                    }
                    if ($('#rdoBeforeChngPass').is(':unchecked') & $('#rdoBeforeChngFail').is(':unchecked')) {

                        $.alert('Please provide "Before Changing (Pass/Fail) "');
                        return false;
                    }

                    var NextDateChange = $('#txtNextChangeDate').val();
                    if (!NextDateChange) {
                        $.alert('Please provide Next Change Date');
                        return false;
                    }
                    var BottleNumber = $('#txtBottletNum').val();
                    if (!BottleNumber) {
                        $.alert('Please provide Bottle Number #');
                        return false;
                    }
                    //save action
                    var data = {
                        "Date": Date,
                        "Time": Time,
                        "LotNumber": LotNumber,
                        "Temp": Temp,
                        "IsDaily": IsDaily,
                        "IsBeforeChanging": IsBeforeChanging,
                        "DateChange": DateChange,
                        "NextDateChange": NextDateChange,
                        "BottleNumber": BottleNumber

                    };
                    $.ajax({
                        url: "hld_solution_testing_log.aspx/SaveData",
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

                function EditSaveData() {
 
                    var Date = $('#txtDate').val();
                    if (!Date) {
                        $.alert('Please provide a Date');
                        return false;
                    }

                    var Time = $('#txtTime').val();
                    if (!Time) {
                        $.alert('Please provide a Time');
                        return false;
                    }

                    var LotNumber = $('#txtLotNum').val();
                    if (!LotNumber) {
                        $.alert('Please provide Lot Number #');
                        return false;
                    }

                    var BottleNumber = $('#txtBottletNum').val();
                    if (!BottleNumber) {
                        $.alert('Please provide Bottle Number #');
                        return false;
                    }

                    var Temp = $('#txtTemp').val();
                    if (!Temp) {
                        $.alert('Please provide Temp. (> 20°C)');
                        return false;
                    }

                    if (Temp<21) {
                            $.alert({
                                title: 'Warning',
                                content: 'Temperature must be greater than 20°C.',
                            });
                       
                        return false;
                    }

                    var IsDaily = false;
                    if ($('#rdoDailyPass').is(':checked')) {

                        IsDaily = true
                    }
                    if ($('#rdoDailyPass').is(':unchecked') & $('#rdoDailyFail').is(':unchecked')) {

                        $.alert('Please provide "Daily (Pass/Fail)"');
                        return false;
                    }
                    if ($('#rdoBeforeChngPass').is(':unchecked') & $('#rdoBeforeChngFail').is(':unchecked')) {

                        $.alert('Please provide "Before Changing (Pass/Fail) "');
                        return false;
                    }

                    var IsBeforeChanging = false;
                    if ($('#rdoBeforeChngPass').is(':checked')) {

                        IsBeforeChanging = true
                    }

                    var DateChange = $('#txtDateChange').val();
                    if (!DateChange) {
                        $.alert('Please provide Date Changed');
                        return false;
                    }
                    var NextDateChange = $('#txtNextChangeDate').val();
                    if (!NextDateChange) {
                        $.alert('Please provide Next Change Date');
                        return false;
                    }

                    //save action
                    var data = {
                        "Date": Date,
                        "Time": Time,
                        "LotNumber": LotNumber,
                        "BottleNumber": BottleNumber,
                        "Temp": Temp,
                        "IsDaily": IsDaily,
                        "IsBeforeChanging": IsBeforeChanging,
                        "DateChange": DateChange,
                        "NextDateChange": NextDateChange,
                        "EditId": EditId

                    };
                    $.ajax({
                        url: "hld_solution_testing_log.aspx/EditData",
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
                        $('#popup_header').text('Add Solution Testing Log');
                        document.getElementById("txtTime").value = moment().format('kk:mm');
                        document.getElementById("txtDate").valueAsDate = new Date();
                    } else {

                        $('#btnSave').hide();
                        $('#btnEdit').show();

                        $('#popup_header').text('Edit Solution Testing Log');
                    }

                    $('#largeModal').modal('show');
                }

                function LoadTable() {

                    var IsEditAllowed = $('#hdnIsEditAllowed').val();
                    var IsDeleteAllowed = $('#hdnIsDeleteAllowed').val();

                    $.fn.dataTable.moment('MMM D, YYYY');
                    
                    table = $('#myTable').DataTable({
                        "ajax": {
                            "type": "GET",
                            "url": "hld_solution_testing_log.aspx/LoadTable",
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
                            { 'data': 'Id', className: 'hide hide1' },
                            { 'data': 'Date' },
                            { 'data': 'Time' },
                            { 'data': 'LotNumber', className: 'text-center' },
                            { 'data': 'BottleNumber', className: 'text-center' },
                            { 'data': 'Temp', className: 'text-center' },
                            { 'data': 'DailyPassFail', className: 'text-center' },
                            { 'data': 'BeforeChangingPassFail', className: 'text-center' },
                            { 'data': 'DateChanged', className: 'text-center' },
                            { 'data': 'NextChangeDate', className: 'text-center' },
                            {
                                'data': null, className: 'text-center no-sort', 'render': function (data, type, row) {
                                    //if (getBool(IsEditAllowed)) {
                                  //  return ' <a href="" onclick="return EditData(event,\'' + data.Id + '\');">Edit</a> <a href="" onclick="DeleteData(event,\'' + data.Id + '\');">Delete</a>'
                                        return '<div class="btn-group"><button class="btn btn-outline-primary btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false"><i class="bi bi-gear"></i></button><ul class="dropdown-menu"><li><a style="color: #212529;" class="dropdown-item" onclick="return EditData(event,\'' + data.Id + '\');"><i class="bi bi-pencil"></i> Edit</a></li><li><a style="color: #dc3545;" class="dropdown-item" onclick="DeleteData(event,\'' + data.Id + '\');"><i class="bi bi-trash"></i> Delete</a></li></ul></div>'
                                    //}
                                    //else {

                                    //}
                                }
                            }
                        ]
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
                                        url: "hld_solution_testing_log.aspx/DeleteData",
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

                    $('#txtDate').val("");
                    $('#txtTime').val("");
                    //$('#txtLotNum').val("");
                    $('#txtBottletNum').val("");
                    $('#txtTemp').val("");
                    $('input[id=rdoDailyPass]').prop('checked', getBool('false'));
                    $('input[id=rdoBeforeChngPass]').prop('checked', getBool('false'));

                    $('input[id=rdoDailyFail]').prop('checked', getBool('false'));
                    $('input[id=rdoBeforeChngFail]').prop('checked', getBool('false'));

                    $('#txtDateChange').val("");
                    //$('#txtNextChangeDate').val("");

                }

                function EditData(event, Id) {
                    event.preventDefault();
                    var Qdata = {
                        "Id": Number(Id)
                    }
                    $.ajax({
                        type: "POST",
                        url: "hld_solution_testing_log.aspx/GetSolutionTestingLogDetails",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(Qdata),
                        dataType: 'json',
                        beforeSend: function () {
                           // $("#loader").show();
                        },
                        success: function (data) {

                            debugger;
                            clearcomponent();
                            EditId = Id;
                            var result = data.d;
                            var Editdetails = JSON.parse(result)[0];

                            var Dates = new Date(Editdetails.Date);
                            var day = ("0" + Dates.getDate()).slice(-2);
                            var month = ("0" + (Dates.getMonth() + 1)).slice(-2);
                            Dates = Dates.getFullYear() + "-" + (month) + "-" + (day);


                            $("#txtDate").val(Dates);

                           // $("#txtDate").val(dateval);
                            $("#txtTime").val(Editdetails.Time);
                            $("#txtLotNum").val(Editdetails.LotNumber);
                            $("#txtBottletNum").val(Editdetails.BottleNumber);
                            $("#txtTemp").val(Editdetails.Temp);                       

                            if (Editdetails.DailyPassFail) {
                                $('input[id=rdoDailyPass]').prop('checked', getBool('true'));
                            } else {
                                $('input[id=rdoDailyFail]').prop('checked', getBool('true'));
                            }

                            if (Editdetails.BeforeChangingPassFail) {
                                $('input[id=rdoBeforeChngPass]').prop('checked', getBool('true'));
                            } else {
                                $('input[id=rdoBeforeChngFail]').prop('checked', getBool('true'));
                            }

                            var Dates = new Date(Editdetails.DateChanged);
                            var day = ("0" + Dates.getDate()).slice(-2);
                            var month = ("0" + (Dates.getMonth() + 1)).slice(-2);
                            Dates = Dates.getFullYear() + "-" + (month) + "-" + (day);

                            $("#txtDateChange").val(Dates);

                            var Dates = new Date(Editdetails.NextChangeDate);
                            var day = ("0" + Dates.getDate()).slice(-2);
                            var month = ("0" + (Dates.getMonth() + 1)).slice(-2);
                            Dates = Dates.getFullYear() + "-" + (month) + "-" + (day);

                            $("#txtNextChangeDate").val(Dates);

                            ShowPanel('Edit');
                       
 
                        },
                        error: function (data) {
                            $("#loader").hide();
                            console.log('Error = ' + JSON.stringify(data));
                        }
                    });
                }

            </script>
</asp:Content>

