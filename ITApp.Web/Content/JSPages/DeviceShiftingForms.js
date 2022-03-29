var DeviceShiftingController = {
    funValidateDeviceShiftingForm: function () {

        $("#frmtblDeviceShifting").validate({
            // Rules for form validation
            onkeyup: false,
            rules: {
                Location: {
                    required: true,
                    //|Data Already exist Kalpesh End|
                },
                DeviceType: {
                    required: true,
                },
                DeviceName: {
                    required: true,
                },
                ShiftingType: {
                    required: true,
                },
            },
            // Messages for form validation
            messages: {
                Location: {
                    required: 'Please select location',
                },
                DeviceType: {
                    required: 'Please select Device Type',
                },
                DeviceName: {
                    required: 'Please enter Device Name',
                },
                ShiftingType: {
                    required: 'Please select Shifting Type',
                },
            },
            // Do not change code below
            errorPlacement: function (error, element) {
                error.insertAfter(element.parent());
            }
        });
    },
    GetfunDocumentInit: function () {
        var DeviceShiftingFormId = $("#DeviceShiftingFormId").val();
        if ($("#requestFomsVM_IsPending").val() == 'True') {
            if (DeviceShiftingFormId == 0) {
                $(".clsProcessSteps").hide();
                $(".clsOwner").hide();
                $(".clsDepartmentHead").hide();
                $(".clsITHelpDeskAdmin").hide();
                $(".clsITHelpDesk").hide();
                $(".ApproveClass").hide();
            }
            else {

                var PendingWith = $("#requestFomsVM_PendingWith").val();
                var Status = $("#requestFomsVM_Status").val();
                var ReturnRejectWith = $("#requestFomsVM_ReturnRejectWith").val();
                var LastPendingWith = $("#requestFomsVM_LastPendingWith").val();
                if (PendingWith == "" && Status == "Close") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#DeviceType').attr('disabled', true);
                    $('#ShiftingType').attr('disabled', true);
                    $('#DeviceName').attr('readonly', true);
                    $('#DeviceNo').attr('readonly', true);
                    $('#FromLocation').attr('readonly', true);
                    $('#ToLocation').attr('readonly', true);
                    $('#ShiftingReason').attr('readonly', true);
                    $('#OwnerRemarks').attr('readonly', true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                    $('#ITHelpdeskRemarks').attr('readonly', true);
                    $('#UpdatedinSampatti').attr('disabled', true);
                    $(".ApproveClass").hide();
                    $(".HideClass").hide();
                }

                if (PendingWith == "Owner") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#DeviceType').attr('disabled', true);
                    $('#ShiftingType').attr('disabled', true);
                    $('#DeviceName').attr('readonly', true);
                    $('#DeviceNo').attr('readonly', true);
                    $('#FromLocation').attr('readonly', true);
                    $('#ToLocation').attr('readonly', true);
                    $('#ShiftingReason').attr('readonly', true);
                    $(".clsDepartmentHead").hide();
                    $(".clsITHelpDeskAdmin").hide();
                    $(".clsITHelpDesk").hide();
                    $(".HideClass").hide();
                    $(".clsclosebtn").hide();

                }
                else if (PendingWith == "Department Head") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#DeviceType').attr('disabled', true);
                    $('#ShiftingType').attr('disabled', true);
                    $('#DeviceName').attr('readonly', true);
                    $('#DeviceNo').attr('readonly', true);
                    $('#FromLocation').attr('readonly', true);
                    $('#ToLocation').attr('readonly', true);
                    $('#ShiftingReason').attr('readonly', true);
                    $('#OwnerRemarks').attr('readonly', true);
                    $(".clsITHelpDeskAdmin").hide();
                    $(".clsITHelpDesk").hide();
                    $(".HideClass").hide();
                    $(".clsclosebtn").hide();

                }
                else if (PendingWith == "IT Helpdesk Admin") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#DeviceType').attr('disabled', true);
                    $('#ShiftingType').attr('disabled', true);
                    $('#DeviceName').attr('readonly', true);
                    $('#DeviceNo').attr('readonly', true);
                    $('#FromLocation').attr('readonly', true);
                    $('#ToLocation').attr('readonly', true);
                    $('#ShiftingReason').attr('readonly', true);
                    $('#OwnerRemarks').attr('readonly', true);
                    $('#DHRemarks').attr('readonly', true);
                    $(".clsITHelpDesk").hide();
                    $(".HideClass").hide();
                    $(".clsclosebtn").hide();
                }
                else if (PendingWith == "IT Helpdesk") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#DeviceType').attr('disabled', true);
                    $('#ShiftingType').attr('disabled', true);
                    $('#DeviceName').attr('readonly', true);
                    $('#DeviceNo').attr('readonly', true);
                    $('#FromLocation').attr('readonly', true);
                    $('#ToLocation').attr('readonly', true);
                    $('#ShiftingReason').attr('readonly', true);
                    $('#OwnerRemarks').attr('readonly', true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                    $(".HideClass").hide();
                    $(".clsapprovebtn").hide();
                    $(".clsReturnbtn").hide();
                    $(".clsRejectbtn").hide();

                }
                else if (PendingWith == "Initiator" && Status == "Returned") {
                    //$(".clsProcessSteps").hide();
                    //$(".clsOwner").hide();
                    //$(".clsDepartmentHead").hide();
                    //$(".clsITHelpDeskAdmin").hide();
                    //$(".clsITHelpDesk").hide();
                    //$(".ApproveClass").hide();

                    if (ReturnRejectWith == "Owner") {
                        $('#OwnerRemarks').attr('readonly', true);
                        $(".clsDepartmentHead").hide();
                        $(".clsITHelpDeskAdmin").hide();
                        $(".clsITHelpDesk").hide();
                        $(".HideClass").show();
                        $(".clsclosebtn").hide();
                        $(".ApproveClass").hide();
                    }

                    else if (ReturnRejectWith == "Department Head") {
                        $('#OwnerRemarks').attr('readonly', true);
                        $('#DHRemarks').attr('readonly', true);
                        $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                        $(".clsITHelpDeskAdmin").hide();
                        $(".clsITHelpDesk").hide();
                        $(".HideClass").show();
                        $(".clsclosebtn").hide();
                        $(".ApproveClass").hide();

                    }
                    else if (ReturnRejectWith == "IT Helpdesk Admin") {
                        $('#OwnerRemarks').attr('readonly', true);
                        $('#DHRemarks').attr('readonly', true);
                        $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                        $(".clsITHelpDesk").hide();
                        $(".HideClass").show();
                        $(".clsclosebtn").hide();
                        $(".ApproveClass").hide();
                    }
                    else if (ReturnRejectWith == "IT Helpdesk") {

                        $('#OwnerRemarks').attr('readonly', true);
                        $('#DHRemarks').attr('readonly', true);
                        $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                        $('#UpdatedinSampatti').attr('readonly', true);
                        $(".HideClass").show();
                        $(".clsapprovebtn").hide();
                        $(".clsReturnbtn").hide();
                        $(".clsRejectbtn").hide();
                        $(".ApproveClass").hide();

                    }
                }
            }

            $('.i-checks').on('ifChecked', function (event) {
                if ('IsAccept' == this.id) {
                    $(".HideClass").show();
                }
            });
            $('.i-checks').on('ifUnchecked', function (event) {
                if ('IsAccept' == this.id) {
                    $(".HideClass").hide();
                }
            });
        }
        else {
            var PendingWith = $("#requestFomsVM_PendingWith").val();
            var Status = $("#requestFomsVM_Status").val();
            var ReturnRejectWith = $("#requestFomsVM_ReturnRejectWith").val();
            if (DeviceShiftingFormId == 0) {
                $(".clsProcessSteps").hide();
                $(".clsOwner").hide();
                $(".clsDepartmentHead").hide();
                $(".clsITHelpDeskAdmin").hide();
                $(".clsITHelpDesk").hide();
                $(".ApproveClass").hide();
            }
            else if (PendingWith == "Initiator" && Status == "Returned") {

                if (ReturnRejectWith == "Owner") {
                    $('#OwnerRemarks').attr('readonly', true);
                    $(".clsDepartmentHead").hide();
                    $(".clsITHelpDeskAdmin").hide();
                    $(".clsITHelpDesk").hide();
                    $(".HideClass").show();
                    $(".clsclosebtn").hide();
                    $(".ApproveClass").hide();
                }
                else if (ReturnRejectWith == "Department Head") {
                    $('#OwnerRemarks').attr('readonly', true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                    $(".clsITHelpDeskAdmin").hide();
                    $(".clsITHelpDesk").hide();
                    $(".HideClass").show();
                    $(".clsclosebtn").hide();
                    $(".ApproveClass").hide();

                }
                else if (ReturnRejectWith == "IT Helpdesk Admin") {
                    $('#OwnerRemarks').attr('readonly', true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                    $(".clsITHelpDesk").hide();
                    $(".HideClass").show();
                    $(".clsclosebtn").hide();
                    $(".ApproveClass").hide();
                }
                else if (ReturnRejectWith == "IT Helpdesk") {

                    $('#OwnerRemarks').attr('readonly', true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                    $('#UpdatedinSampatti').attr('readonly', true);
                    $(".HideClass").show();
                    $(".clsapprovebtn").hide();
                    $(".clsReturnbtn").hide();
                    $(".clsRejectbtn").hide();
                    $(".ApproveClass").hide();

                }
            }
            else if (Status == "Reject") {
                if (ReturnRejectWith == "Owner") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#DeviceType').attr('disabled', true);
                    $('#ShiftingType').attr('disabled', true);
                    $('#DeviceName').attr('readonly', true);
                    $('#DeviceNo').attr('readonly', true);
                    $('#FromLocation').attr('readonly', true);
                    $('#ToLocation').attr('readonly', true);
                    $('#ShiftingReason').attr('readonly', true);
                    $(".clsDepartmentHead").hide();
                    $(".clsITHelpDeskAdmin").hide();
                    $(".clsITHelpDesk").hide();
                    $(".HideClass").hide();
                    $(".clsclosebtn").hide();
                    $(".ApproveClass").hide();
                }
                else if (ReturnRejectWith == "Department Head") {
                    $('#OwnerRemarks').attr('readonly', true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                    $(".clsITHelpDeskAdmin").hide();
                    $(".clsITHelpDesk").hide();
                    $(".HideClass").hide();
                    $(".clsclosebtn").hide();
                    $(".ApproveClass").hide();

                }
                else if (ReturnRejectWith == "IT Helpdesk Admin") {
                    $('#OwnerRemarks').attr('readonly', true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                    $(".clsITHelpDesk").hide();
                    $(".HideClass").hide();
                    $(".clsclosebtn").hide();
                    $(".ApproveClass").hide();
                }
                else if (ReturnRejectWith == "IT Helpdesk") {

                    $('#OwnerRemarks').attr('readonly', true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                    $('#UpdatedinSampatti').attr('readonly', true);
                    $(".HideClass").hide();
                    $(".clsapprovebtn").hide();
                    $(".clsReturnbtn").hide();
                    $(".clsRejectbtn").hide();
                    $(".ApproveClass").hide();

                }
            }
            else {
                $('#OnBehalfof').attr('readonly', true);
                $('#Location').attr('disabled', true);
                $('#DeviceType').attr('disabled', true);
                $('#ShiftingType').attr('disabled', true);
                $('#DeviceName').attr('readonly', true);
                $('#DeviceNo').attr('readonly', true);
                $('#FromLocation').attr('readonly', true);
                $('#ToLocation').attr('readonly', true);
                $('#ShiftingReason').attr('readonly', true);
                $('#OwnerRemarks').attr('readonly', true);
                $('#DHRemarks').attr('readonly', true);
                $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                $('#ITHelpdeskRemarks').attr('readonly', true);
                $('#UpdatedinSampatti').attr('disabled', true);
                $(".HideClass").hide();
                $(".ApproveClass").hide();
            }

        }
    },
    funSubmitDeviceShiftingFormValue: function () {
        DeviceShiftingController.funValidateDeviceShiftingForm();

        if ($("#frmtblDeviceShifting").valid()) {
            $('#btnSubmit').attr('disabled', true);
            $.ajax({

                url: glbParamObject.Virutalpath + '/DeviceShifting/SubmitDeviceShiftingFormValue',
                data: $("#frmtblDeviceShifting").serialize(),
                type: 'POST',
                dataType: 'json',
                success: function (responce) {
                    $("#divAlert").html("");
                    if (responce.Result == "OK") {
                        //toastr.success(responce.Message);
                        //window.setTimeout(function () {
                        //    window.location.href = glbParamObject.Virutalpath + '/Home/Index'
                        //}, 2000);
                        swal({
                            title: responce.Message.split('|')[0],
                            text: responce.Message.split('|')[1],
                        });
                        window.setTimeout(function () {
                            window.location.href = glbParamObject.Virutalpath + '/Home/Index'
                        }, 3000);
                    }
                    else if (responce.Result == "DUPLICATE") {
                        $("#" + responce.OtherParameter.AlertFieldName).focus();
                        $("#divAlert").html(responce.OtherParameter.AlertMessage);
                        $("#divAlert").show();
                    }
                    else if (responce.Result == "WARNING") {
                        $("#divAlert").html(responce.OtherParameter.AlertMessage);
                        $("#divAlert").show();
                        window.setTimeout(function () {

                            window.location.href = glbParamObject.Virutalpath + '/DeviceShifting/DeviceShiftingForms'
                        }, 5000);
                    }
                    else if (responce.Result == "ERROR") {
                        //$("#divAlert").html(responce.OtherParameter.AlertMessage);
                        //$("#divAlert").show();
                        toastr.error(responce.Message);
                    }
                },
                error: function (xxx) {

                }
            });
        }
        else {
            /* $("#btnSubmit").ladda().ladda('stop');*/
        }

    },

    funApproveDeviceShiftingFormValue: function () {
        DeviceShiftingController.funValidateDeviceShiftingForm();

        if ($("#frmtblDeviceShifting").valid()) {
            $.ajax({

                url: glbParamObject.Virutalpath + '/DeviceShifting/ApproveDeviceShiftingFormValue',
                data: $("#frmtblDeviceShifting").serialize(),
                type: 'POST',
                dataType: 'json',
                success: function (responce) {
                    $("#divAlert").html("");
                    if (responce.Result == "OK") {
                        toastr.success(responce.Message);
                        window.setTimeout(function () {
                            window.location.href = glbParamObject.Virutalpath + '/Home/Index'
                        }, 2000);
                    }
                    else if (responce.Result == "DUPLICATE") {
                        $("#" + responce.OtherParameter.AlertFieldName).focus();
                        $("#divAlert").html(responce.OtherParameter.AlertMessage);
                        $("#divAlert").show();
                    }
                    else if (responce.Result == "WARNING") {
                        $("#divAlert").html(responce.OtherParameter.AlertMessage);
                        $("#divAlert").show();
                        window.setTimeout(function () {

                            window.location.href = glbParamObject.Virutalpath + '/DeviceShifting/DeviceShiftingForms'
                        }, 5000);
                    }
                    else if (responce.Result == "ERROR") {
                        //$("#divAlert").html(responce.OtherParameter.AlertMessage);
                        //$("#divAlert").show();
                        toastr.error(responce.Message);
                    }
                },
                error: function (xxx) {

                }
            });
        }
        else {
            /* $("#btnSubmit").ladda().ladda('stop');*/
        }
    },

    funCloseDeviceShiftingFormValue: function () {
        DeviceShiftingController.funValidateDeviceShiftingForm();

        if ($("#frmtblDeviceShifting").valid()) {
            $.ajax({

                url: glbParamObject.Virutalpath + '/DeviceShifting/CloseDeviceShiftingFormValue',
                data: $("#frmtblDeviceShifting").serialize(),
                type: 'POST',
                dataType: 'json',
                success: function (responce) {
                    $("#divAlert").html("");
                    if (responce.Result == "OK") {
                        toastr.success(responce.Message);
                        window.setTimeout(function () {
                            window.location.href = glbParamObject.Virutalpath + '/Home/Index'
                        }, 2000);
                    }
                    else if (responce.Result == "DUPLICATE") {
                        $("#" + responce.OtherParameter.AlertFieldName).focus();
                        $("#divAlert").html(responce.OtherParameter.AlertMessage);
                        $("#divAlert").show();
                    }
                    else if (responce.Result == "WARNING") {
                        $("#divAlert").html(responce.OtherParameter.AlertMessage);
                        $("#divAlert").show();
                        window.setTimeout(function () {

                            window.location.href = glbParamObject.Virutalpath + '/DeviceShifting/DeviceShiftingForms'
                        }, 5000);
                    }
                    else if (responce.Result == "ERROR") {
                        //$("#divAlert").html(responce.OtherParameter.AlertMessage);
                        //$("#divAlert").show();
                        toastr.error(responce.Message);
                    }
                },
                error: function (xxx) {

                }
            });
        }
        else {
            /* $("#btnSubmit").ladda().ladda('stop');*/
        }
    },

    funReturnDeviceShiftingFormValue: function () {
        $.ajax({

            url: glbParamObject.Virutalpath + '/DeviceShifting/ReturnDeviceShiftingFormValue',
            data: $("#frmtblDeviceShifting").serialize(),
            type: 'POST',
            dataType: 'json',
            success: function (responce) {
                $("#divAlert").html("");
                if (responce.Result == "OK") {
                    toastr.success(responce.Message);
                    window.setTimeout(function () {
                        window.location.href = glbParamObject.Virutalpath + '/Home/Index'
                    }, 2000);
                }
                else if (responce.Result == "DUPLICATE") {
                    $("#" + responce.OtherParameter.AlertFieldName).focus();
                    $("#divAlert").html(responce.OtherParameter.AlertMessage);
                    $("#divAlert").show();
                }
                else if (responce.Result == "WARNING") {
                    $("#divAlert").html(responce.OtherParameter.AlertMessage);
                    $("#divAlert").show();
                    window.setTimeout(function () {

                        window.location.href = glbParamObject.Virutalpath + '/DeviceShifting/DeviceShiftingForms'
                    }, 5000);
                }
                else if (responce.Result == "ERROR") {
                    //$("#divAlert").html(responce.OtherParameter.AlertMessage);
                    //$("#divAlert").show();
                    toastr.error(responce.Message);
                }
            },
            error: function (xxx) {

            }
        });
    },

    funRejectDeviceShiftingFormValue: function () {
        $.ajax({

            url: glbParamObject.Virutalpath + '/DeviceShifting/RejectDeviceShiftingFormValue',
            data: $("#frmtblDeviceShifting").serialize(),
            type: 'POST',
            dataType: 'json',
            success: function (responce) {
                $("#divAlert").html("");
                if (responce.Result == "OK") {
                    toastr.success(responce.Message);
                    window.setTimeout(function () {
                        window.location.href = glbParamObject.Virutalpath + '/Home/Index'
                    }, 2000);
                }
                else if (responce.Result == "DUPLICATE") {
                    $("#" + responce.OtherParameter.AlertFieldName).focus();
                    $("#divAlert").html(responce.OtherParameter.AlertMessage);
                    $("#divAlert").show();
                }
                else if (responce.Result == "WARNING") {
                    $("#divAlert").html(responce.OtherParameter.AlertMessage);
                    $("#divAlert").show();
                    window.setTimeout(function () {

                        window.location.href = glbParamObject.Virutalpath + '/DeviceShifting/DeviceShiftingForms'
                    }, 5000);
                }
                else if (responce.Result == "ERROR") {
                    //$("#divAlert").html(responce.OtherParameter.AlertMessage);
                    //$("#divAlert").show();
                    toastr.error(responce.Message);
                }
            },
            error: function (xxx) {

            }
        });
    },
}