var glbParamObject = {};
var BaseController = {
    funInit: function (paramObject) {
        glbParamObject = paramObject;
        //-----Tooltip-----------
        $('[data-toggle="popover"]').popover();

        $('body').on('click', function (e) {
            $('[data-toggle="popover"]').each(function () {
                //the 'is' for buttons that trigger popups
                //the 'has' for icons within a button that triggers a popup
                if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                    $(this).popover('hide');
                }
            });
        });
        //-----Tooltip-----------
        $(document).ajaxError(function (xhr, props, exception) {
            var _msg = "";
            if (props.status === 0) {
                _msg = 'Not connect.\n Verify Network.';
            } else if (props.status == 404) {
                _msg = 'Requested page not found. [404]';
            } else if (props.status == 500) {
                _msg = 'Internal Server Error [500].';
            } else if (props.status == 401) {
                _msg = 'session is expried.';
            }
            else if (exception === 'parsererror') {
                _msg = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                _msg = 'Time out error.';
            } else if (exception === 'abort') {
                _msg = 'Ajax request aborted.';
            }
            else {
                _msg = 'Uncaught Error.\n' + props.responseText;
            }
            toastr.error(_msg);
            if (props.status == 401) {
                window.setTimeout(function () { window.location.href = glbParamObject.Virutalpath + '/Home/Logout' }, 1000);
            }
        });
        $(document).ajaxStart(function () {
            BaseController.funshowLoading(true);
        });
        $(document).ajaxStop(function () {
            BaseController.funshowLoading(false);
        });

        $(document).on("click", "tbody tr", function (e) {
            $(this).addClass("selected").siblings().removeClass("selected");
        });

        $("body").off('change', 'input[type="file"]').on("change", 'input[type="file"]', function () {
            var _files = this.files;
            var _intlen = this.files.length;
            if (_intlen > 0) {
                var _currentbox = $(this);
                var _fileData = new FormData();
                for (var i = 0; i < _intlen; i++) {
                    _fileData.append(_files[i].name, _files[i]);
                }
                _fileData.append('__RequestVerificationToken', gettoken());
                $.ajax({
                    url: glbParamObject.Virutalpath + '/Base/CheckValidFile',
                    type: "POST",
                    dataType: "json",
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data
                    data: _fileData,
                    success: function (responce) {
                        if (responce.Result == glbParamObject.ResponceResultWARNING) {
                            _currentbox.val('');
                            toastr.warning(responce.Msg);
                        }
                    }
                });
            }

        });
    },
    funInitDataTableCus: function () {
        $('#exampledatatable thead tr').clone(true).appendTo('#exampledatatable thead');
        $('#exampledatatable thead tr:eq(1) th').each(function (i) {

            var title = $(this).text();
            var _idName = $(this).attr('data-idn');
            var _notextbox = $(this).attr('data-nosearchtext');
            var _htmlText = "";
            if (_notextbox == undefined || _notextbox == "" || _notextbox == null) {
                if (_idName != undefined && _idName != "" && _idName != null) {
                    _htmlText = '<input id=' + _idName + ' type="text" class="form-control input-sm" placeholder="Search ' + title.trim() + '" />';
                }
                else {
                    _htmlText = '<input type="text" class="form-control input-sm" placeholder="Search ' + title.trim() + '" />';
                }
            }

            $(this).html(_htmlText);

            $('input', this).on('keyup change', function () {
                if (table.column(i).search() !== this.value) {
                    table
                        .column(i)
                        .search(this.value)
                        .draw();
                }
            });
        });

        var buttonCommon = {
            exportOptions: {
                format: {
                    body: function (data, row, column, node) {
                        // Strip = from data
                        return data.replace(new RegExp('<[^>]*>', 'g'), '').replace(/[=]/g, '');
                    }
                }
            }
        };
        var table = $('.exampledatatable').DataTable({
            orderCellsTop: true,
            fixedHeader: true,
            bSortable: true,
            responsive: true,
            "stateSave": true,
            "bStateSave": true,
            "order": [],
            "columnDefs": [{
                "targets": 'no-sort',
                "orderable": false
            }],
            buttons: [
                $.extend(true, {}, buttonCommon, {
                    extend: 'copy', exportOptions: { columns: ':visible :not(.notexport)' }
                }),
                $.extend(true, {}, buttonCommon, {
                    extend: 'csv', title: 'Role Summary List', exportOptions: { columns: ':visible :not(.notexport)' }
                }),
                $.extend(true, {}, buttonCommon, {
                    extend: 'excel', title: 'Role Summary List', exportOptions: { columns: ':visible :not(.notexport)' }
                }),
            ],
            dom: "<'row'<'col-sm-4'l><'col-sm-4'<'float-right'B>><'col-sm-4'<'float-right'f>>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-md-5'i><'col-md-7'<'float-right'p>>>",
            stateLoadParams: function (settings, data) {
                for (i = 0; i < data.columns["length"]; i++) {
                    var col_search_val = data.columns[i].search.search;
                    if (col_search_val != "") {
                        $("input", $("#exampledatatable thead tr:eq(1) th")[i]).val(col_search_val);
                    }
                }
            }
        });
    },
    funDeleteRecord: function (_paramPkId, _paramTableName) {
        if (_paramPkId > 0 && _paramTableName != null && _paramTableName != "") {
            $.confirm({
                text: "Are you sure you want to delete this record ?",
                title: "Confirmation",
                confirmButton: "Yes",
                cancelButton: "No",
                post: false,
                confirmButtonClass: "btn btn-success",
                cancelButtonClass: "btn btn-danger",
                dialogClass: "modal-dialog ",
                confirm: function (button) {
                    $("body").removeClass('modal-open');

                    $.ajax({
                        url: glbParamObject.Virutalpath + '/Common/deletRecord',
                        data: {
                            __RequestVerificationToken: gettoken(),
                            paramPkId: _paramPkId,
                            paramTableName: _paramTableName
                        },
                        dataType: "json",
                        type: "POST",
                        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                        async: true,
                        success: function (responce) {
                            if (responce.Result == glbParamObject.ResponceResultOK) {
                                toastr.success(responce.Message);
                                if (_paramTableName == 'tblParamValue') {
                                    ParamSetupController.funRefreshGrid();
                                }
                                else {
                                    window.setTimeout(function () { location.reload(); }, 1000);
                                }
                            }
                            else if (responce.Result == glbParamObject.ResponceResultWARNING) {
                                toastr.warning(responce.Message);
                            }
                            else if (responce.Result == glbParamObject.ResponceResultERROR) {
                                toastr.error(responce.Message);
                            }
                        }
                    });
                },
                cancel: function (button) {
                },
            });
        }
        else {
            toastr.warning("No record found.");
            return;
        }
    },
    isValidNumberWithouDecimal: function (el, evnt) {
        $(el).val($(el).val().replace(/[^\d].+/, ""));
        var _textlength = $(el).attr("data-textlength");
        if (textlength != undefined && textlength != null && $(el).val() != "" && $(el).val() != null) {
            if ($(el).val().length >= _textlength) {
                return false;
            }
        }
        if ((event.which < 48 || event.which > 57)) {
            return false;
        }
        return true;
    },
    IsValidAlphabetsNumberiSpecAnd: function (e, t, _txtarea) {
        var regexp = null;
        if (_txtarea == "txtarea") {
            regexp = new RegExp(/^[a-zA-Z0-9 .&_\,\"\-()\/]*$/gm);//.&_,"-()/
        }
        else if (_txtarea == "txtmaster") {
            regexp = new RegExp(/^[a-zA-Z0-9 &_\,\"\-()\/]*$/);//&_,"-()/
        }
        else {
            regexp = new RegExp(/^[a-zA-Z0-9& -]*$/);//-&
        }
        if (window.event) {
            keynum = e.keyCode;
        } else if (e.which) {
            keynum = e.which;
        }
        var test = regexp.test(String.fromCharCode(keynum));
        return test;
    },
    funshowLoading: function (flag) {
        if (flag) {
            $(".quick-nav-overlay").fadeIn();
            $("#divLoader").slideDown();
        }
        else {
            $(".quick-nav-overlay").fadeOut();
            $("#divLoader").slideUp();
        }
    },
    funValidationCheck: function () {
        var blIsValid = true;
        $('.require').each(function (index, val) {
            if ($(this).val() == "" && $(this).val().length == 0) {
                if (index == 0) {
                    $(this).focus();
                }
                $(this).addClass('is-invalid');
                blIsValid = false;
            }
            else {
                $(this).removeClass('is-invalid');
            }

        });
        return blIsValid;
    },
    funPopupFileUpload: function (_paramPkId, _paramDocType, _paramIschange, _flg) {
        if (_paramPkId > 0) {

            $.ajax({
                url: glbParamObject.Virutalpath + '/Base/FCSPopupFileUpload',
                data: {
                    __RequestVerificationToken: gettoken(),
                    paramPkId: _paramPkId,
                    paramDocType: _paramDocType,
                    paramIschange: _paramIschange
                },
                dataType: "html",
                type: "Post",
                async: true,
                success: function (response) {
                    $("#divcommonModel").find('#divcommonModelbtnYes').hide();
                    $("#divcommonModel").find('#divcommonModelbtnNo').html('Close');
                    $("#divcommonModel").find('.modal-dialog').css('max-width', '50%');
                    $("#divcommonModel").find('.modal-title').html('Attachment');
                    $("#divcommonModel").find('.modal-body').html(response);
                    $("#divcommonModel").modal('show');
                }
            });
        }
        else {
            toastr.warning('No data found');
        }
    },
    funFileUpload: function (_paramPkId, _paramDocType) {
        var _fileUpload = $("#txtFCSFileUpload").get(0);
        var _files = _fileUpload.files;

        if (_fileUpload.value) {
            // Create FormData object
            var _fileData = new FormData();

            // Looping over all files and add it to FormData object
            for (var i = 0; i < _files.length; i++) {
                _fileData.append("txtFCSFileUpload", _files[i]);
            }
            _fileData.append('__RequestVerificationToken', gettoken());
            // Adding one more key to FormData object
            _fileData.append('paramPkId', _paramPkId);
            _fileData.append('paramDocType', _paramDocType);
            $.ajax({
                url: glbParamObject.Virutalpath + '/Base/FCSFileUpload',
                data: _fileData,
                type: "POST",
                dataType: "json",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                success: function (responce) {
                    if (responce.Result == glbParamObject.ResponceResultOK) {
                        $('.clsAttchCount_' + _paramPkId + "-" + _paramDocType).html(responce.OtherParameter.fileCount);
                        $("#txtFCSFileUpload").val('');
                        toastr.success(responce.Message);
                        BaseController.funGetAttachmentFile(_paramPkId, _paramDocType, 'Yes');
                    }
                    else if (responce.Result == glbParamObject.ResponceResultWARNING) {
                        toastr.warning(responce.Message);
                        $("#txtFCSFileUpload").val('');
                    }
                    else if (responce.Result == glbParamObject.ResponceResultERROR) {
                        toastr.error(responce.Message);
                        $("#txtFCSFileUpload").val('');
                    }
                }
            });
        }
        else {
            toastr.warning('Please Select the file');
            return false;
        }
    },
    funGetAttachmentFile: function (_paramPkId, _paramDocType, _paramIschange) {
        if (_paramPkId > 0) {
            $.ajax({
                url: glbParamObject.Virutalpath + '/Base/FCSGetFileList',
                data: {
                    __RequestVerificationToken: gettoken(),
                    paramPkId: _paramPkId,
                    paramDocType: _paramDocType,
                    paramIschange: _paramIschange
                },
                dataType: "html",
                type: "Post",
                async: true,
                success: function (responce) {
                    $("#dv_FCSAttachmentList").html('');
                    $("#dv_FCSAttachmentList").html(responce);
                }
            });
        }
    },
    fundeleteFilebyServer: function (_elem) {

        $.confirm({
            text: "Are you sure you want to delete this record ?",
            title: "Confirmation",
            confirmButton: "Yes",
            cancelButton: "No",
            post: false,
            confirmButtonClass: "btn btn-success",
            cancelButtonClass: "btn btn-danger",
            dialogClass: "modal-dialog",
            confirm: function (button) {
                $("body").removeClass('modal-open');
                var _valueofArr = $(_elem).attr('data-value').split('^');
                var _paramDocName = _valueofArr[0];
                var _paramPkId = _valueofArr[1];
                var _paramDocType = _valueofArr[2];
                $.ajax({
                    url: glbParamObject.Virutalpath + '/Base/FCSDeleteFile',
                    data: {
                        __RequestVerificationToken: gettoken(),
                        paramPkId: _paramPkId,
                        paramDocType: _paramDocType,
                        paramDocName: _paramDocName
                    },
                    dataType: "json",
                    type: "POST",
                    async: true,
                    success: function (responce) {
                        if (responce.Result == glbParamObject.ResponceResultOK) {
                            $('.clsAttchCount_' + _paramPkId + "-" + _paramDocType).html(responce.OtherParameter.fileCount);
                            $(_elem).closest("tr").remove();
                            toastr.success(responce.Message);
                            if (responce.OtherParameter.fileCount == null || responce.OtherParameter.fileCount == '0') {
                                BaseController.funCommonpopClose();
                            }
                        }
                        else if (responce.Result == glbParamObject.ResponceResultWARNING) {
                            toastr.warning(responce.Message);
                        }
                        else if (responce.Result == glbParamObject.ResponceResultERROR) {
                            toastr.error(responce.Message);
                        }
                    }
                });
            },
            cancel: function (button) {
            },
        });
    },
    funCommonpopClose: function () {
        $("#divcommonModel").find('#divcommonModelbtnYes').show();
        $("#divcommonModel").find('#divcommonModelbtnYes').html('<span class="fa fa-lg fa-check"></span>&nbsp;Yes');
        $("#divcommonModel").find('#divcommonModelbtnYes').removeAttr('onclick');
        $("#divcommonModel").find('#divcommonModelbtnNo').html('No');
        $('#divcommonModel').find('.modal-footer .btnCommonModelClose').attr('onclick', 'BaseController.funCommonpopClose()');
        $("#divcommonModel").find('.modal-body').html('');
        $("#divcommonModel").find('.modal-body').empty();
        $("#divcommonModel").modal('hide');
    },

}